using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserInputModel, User>().ReverseMap();
        CreateMap<CreateUserInputModel, UserDTO>().ReverseMap();
        CreateMap<SignInUserModel, UserDTO>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();

        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<CreatePostInputModel, Post>().ReverseMap();
    }
}
