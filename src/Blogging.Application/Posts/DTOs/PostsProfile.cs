using AutoMapper;
using Blogging.Application.Posts.Commands.CreatePost;
using Blogging.Application.Posts.Commands.UpdatePost;
using Blogging.Domain.Entities;

namespace Blogging.Application.Posts.DTOs;
internal class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<CreatePostCommand, Post>();
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<UpdatePostCommand, Post>();
    }
}
