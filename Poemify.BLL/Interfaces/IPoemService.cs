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
        Task<Response<CreatePoemResponse>> CreatePoem(string Id, CreatePoemRequest poemRequest);
        Task DeletePoem();
        Task UpdatePoem();
        Task GetComments();
        
    }
}
