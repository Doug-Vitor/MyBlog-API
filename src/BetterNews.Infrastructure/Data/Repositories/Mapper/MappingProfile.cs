using AutoMapper;

namespace BetterNews.Infrastructure.Data.Repositories.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserInputModel, User>().ReverseMap();

            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
