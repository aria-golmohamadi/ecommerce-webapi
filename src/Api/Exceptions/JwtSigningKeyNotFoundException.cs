namespace Api.Exceptions;

internal class JwtSigningKeyNotFoundException() : Exception("Token signing key not found.")
{
}
