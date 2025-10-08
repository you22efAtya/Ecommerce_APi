namespace Domain.Exceptions
{
    public sealed class UnauthorizedException(string msg = "Invalid email or password") : Exception(msg)
    {
    }
}
