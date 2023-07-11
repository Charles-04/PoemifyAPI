using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;

namespace Poemify.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UserRegistrationResponse>> SignUpAsync(UserRegistrationRequest request);
        
    }
}
