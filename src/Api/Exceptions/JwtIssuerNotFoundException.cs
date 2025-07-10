namespace Api.Exceptions;

internal class JwtIssuerNotFoundException() : Exception("Token issuer not found.")
{ 
}
