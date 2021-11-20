public interface ITokenServices
{
    Task<string> GenerateTokenAsync(int? userId);
}