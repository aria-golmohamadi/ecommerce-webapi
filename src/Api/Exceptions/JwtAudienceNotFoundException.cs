namespace Api.Exceptions;

internal class JwtAudienceNotFoundException() : Exception("Token audience not found.")
{
}
