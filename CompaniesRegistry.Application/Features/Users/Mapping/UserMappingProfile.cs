using AutoMapper;
using CompaniesRegistry.Application.Features.Users.GetById;
using CompaniesRegistry.Domain.Users;

namespace CompaniesRegistry.Application.Features.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserResponse>().ReverseMap();
    }
}
