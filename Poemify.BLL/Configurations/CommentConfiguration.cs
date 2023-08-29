using AutoMapper;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Configurations
{
    public class CommentConfiguration : Profile
    {
        public CommentConfiguration() {
            CreateMap<Comment, CommentResponse>()
                   .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Content))
                   .ForMember(dest => dest.PoemTitle, opt => opt.MapFrom(src => src.Poem.Title))
                   .ForMember(dest => dest.Commenter, opt => opt.MapFrom(src => src.Author.UserName));
        }
    }
}
