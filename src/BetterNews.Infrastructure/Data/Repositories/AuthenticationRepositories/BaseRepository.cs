namespace BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories
{
    public abstract class BaseRepository
    {
        protected readonly AuthenticationContext Context;

        public BaseRepository(AuthenticationContext context) => Context = context;

    }
}
