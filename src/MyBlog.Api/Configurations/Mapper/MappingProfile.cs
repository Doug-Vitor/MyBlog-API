using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserInputModel, User>().ReverseMap();
        CreateMap<CreateUserInputModel, UserViewModel>().ReverseMap();
        CreateMap<SignInUserModel, UserViewModel>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
    }
}
