using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
   public record GetPoemCommentsResponse
    {
      
        List<CommentResponse> Comments { get; init; }
    }
    public record CommentResponse
    {
        public string PoemTitle { get; init; }
        public string Commenter { get; init; }
        public string Comment { get; init; }
    }
}
