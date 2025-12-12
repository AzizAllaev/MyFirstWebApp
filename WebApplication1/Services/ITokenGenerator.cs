namespace JwtTokenSample.Services
{
	public interface ITokenGenerator
	{
		string GenerateToken(string username);
	}
}
