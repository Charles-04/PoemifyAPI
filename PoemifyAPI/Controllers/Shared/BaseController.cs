using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Poemify.API.Controllers.Shared
{
  
    public class BaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetUserId()
        {
            ClaimsPrincipal user = HttpContext.User;
            string userId = user.FindFirstValue("Id")!;
            return userId;
        }
    }
}
