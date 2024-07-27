using AutoMapper;
using Blogging.Application.Comments.Commands.CreateComment;
using Blogging.Application.Comments.Commands.UpdateComment;
using Blogging.Application.Posts.Commands.UpdatePost;
using Blogging.Application.Posts.DTOs;
using Blogging.Domain.Entities;

namespace Blogging.Application.Comments.DTOs;
internal class CommentsProfile : Profile
{
    public CommentsProfile()
    {
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        CreateMap<UpdateCommentCommand, Comment>();
    }
}
