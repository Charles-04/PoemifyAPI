using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<Response<PaginationResponse<CommentResponse>>> GetComments(GetPoemCommentsRequest request);
    }
}
