using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrlShortener.Data.Context;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Repository;
using UrlShortener.Service.Services.Authentication;
using UrlShortener.Service.Services.JwtGeneration;
using UrlShortener.Service.Services.Users;
using AutoMapper;
using UrlShortener.Service.Mapping;
using UrlShortener.Service.Services.Hashing;
using UrlShortener.Service.Services.Url;
using UrlShortener.Service.Services.UrlMapping;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using UrlShortener.Service.Services.UrlShortening;

namespace UrlShortener
{
  public class Program
  {
    public static void Main()
    {
      var builder = WebApplication.CreateBuilder();
      var services = builder.Services;

      var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("Credentials/Credentials.json")
      .Build();

      services.AddCors(options =>
      {
        options.AddPolicy("AllowOrigin", builder =>
        {
          builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();
        });
      });

      var signingKey = configuration["Jwt:SigningKey"]!;

      services.AddSwaggerGen(options =>
      {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = @"Past here your Jwt Token With Bearer prefix",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      });

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
        };
      });

      var databaseConnectionString = configuration["Database:UrlShortenerConnection"];


      services.AddDbContext<UrlShortenerContext>(options =>
      {
        options.UseSqlServer(databaseConnectionString);
      });

      var mapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<UserMapper>();
        cfg.AddProfile<UrlMapMapper>();
      });

      IMapper mapper = mapperConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<IAuthenticationService, AuthenticationService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IJwtGenerationService, JwtGenerationService>();
      services.AddScoped<IRepository<User>, Repository<User>>();
      services.AddScoped<IRepository<UrlMap>, Repository<UrlMap>>();
      services.AddScoped<IPasswordHashingService, PasswordHashingService>();
      services.AddScoped<IUrlMapService, UrlMapService>();
      services.AddScoped<IUrlShortenerService, UrlShortenerService>();

      services.AddControllersWithViews();
      services.AddControllers();

      var application = builder.Build();

      if (!application.Environment.IsDevelopment())
      {
        application.UseExceptionHandler("/Home/Error");
        application.UseHsts();
      
      }

      application.UseHttpsRedirection();
      application.UseStaticFiles();

      application.UseRouting();

      application.UseSwagger();
      application.UseSwaggerUI();
      application.UseCors("AllowOrigin");

      application.UseAuthorization();

      application.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      application.MapFallbackToController("RedirectToShortenedUrl", "UrlMap");

      application.Run();
    }
  }
}