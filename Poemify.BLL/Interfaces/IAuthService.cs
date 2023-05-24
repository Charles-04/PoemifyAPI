using Poemify.DAL.DTOs.Request;
using Poemify.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AccountResponse> CreateUser(UserRegistrationRequest request);
        Task UpdateUser();
        Task DeleteUser();
        Task RetrieveUser();
    }
}
