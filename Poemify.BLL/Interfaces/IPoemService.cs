using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Interfaces
{
    public interface IPoemService
    {
        Task<Response<CreatePoemResponse>> CreatePoem(string userId, CreatePoemRequest poemRequest);
        Task<Response<DeletePoemResponse>> DeletePoem(DeletePoemRequest deletePoemRequest);
        Task<Response<UpdatePoemResponse>> UpdatePoem(UpdatePoemRequest updatePoemRequest);
        Task<IEnumerable<GetPoemCommentsResponse>> GetPoemComments(string poemId);
        Task GetPoemByTags();
        
    }
}
