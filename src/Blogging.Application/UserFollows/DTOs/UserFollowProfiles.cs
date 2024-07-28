using AutoMapper;
using Blogging.Application.UserFollows.Commands.FollowUser;
using Blogging.Domain.Entities;

namespace Blogging.Application.UserFollows.DTOs;
public class UserFollowProfiles : Profile
{
    public UserFollowProfiles()
    {
        CreateMap<CreateUserFollowCommand, UserFollow>();
        CreateMap<UserFollow, CurrentUserFollowersDto>()
                  .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.Followee.Id))
                  .ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.Followee.UserName));
        

    }
}
