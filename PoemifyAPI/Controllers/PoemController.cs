using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poemify.API.Controllers.Shared;
using Poemify.BLL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;

namespace Poemify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoemController : BaseController
    {
        private IPoemService _poemService;
        public PoemController(IPoemService poemService)
        {
            _poemService = poemService;
        }
        [HttpPost("create-poem",Name ="create-poem")]
        public async Task<ActionResult<Response<CreatePoemResponse>>> CreatePoem(CreatePoemRequest poemRequest)
        {
             var Id = GetUserId();
             var response = await _poemService.CreatePoem(Id, poemRequest);
             return Ok(response);
        }
    }
}
