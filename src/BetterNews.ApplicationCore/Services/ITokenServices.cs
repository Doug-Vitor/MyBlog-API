public interface ITokenServices
{
    Task<string> GenerateTokenAsync(User user);
}