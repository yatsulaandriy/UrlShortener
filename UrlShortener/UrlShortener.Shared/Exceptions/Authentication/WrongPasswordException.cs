namespace UrlShortener.Shared.Exceptions.Authentication
{
  public class WrongPasswordException : Exception
  {
    public WrongPasswordException() : base("You have inputed wrong password") { }
  }
}
