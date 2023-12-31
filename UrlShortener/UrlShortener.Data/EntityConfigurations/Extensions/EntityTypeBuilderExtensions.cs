﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data.EntityConfigurations.Extensions
{
  public static class EntityTypeBuilderExtensions
  {
    public static void HasAutoGeneratedKeyOnAdd<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
      builder.HasKey(user => user.Id);

      builder.Property(user => user.Id)
      .IsRequired()
      .ValueGeneratedOnAdd();
    }
  }
}
