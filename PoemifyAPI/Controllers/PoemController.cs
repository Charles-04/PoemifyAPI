using Microsoft.AspNetCore.Mvc;
using Poemify.API.Controllers.Shared;
using Poemify.BLL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Swashbuckle.AspNetCore.Annotations;

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
        [HttpPost("create-poem", Name = "poem-creation")]
        [SwaggerOperation(Summary ="Creates poem")]
        public async Task<ActionResult<Response<CreatePoemResponse>>> CreatePoem(CreatePoemRequest poemRequest)
        {
            var Id = GetUserId();
            var response = await _poemService.CreatePoem(Id, poemRequest);
            return Ok(response);
        }
        [HttpPut("update-poem", Name ="poem-update")]
        [SwaggerOperation(Summary ="Updates poem")]
        public async Task<ActionResult<Response<UpdatePoemResponse>>> UpdatePoem(UpdatePoemRequest updatePoemRequest)
        {
            var response = await _poemService.UpdatePoem(updatePoemRequest);
            return Ok(response); 
        }
        [HttpDelete("delete-poem", Name = "delete-poem")]
        [SwaggerOperation(summary:"Deletes poem")]
        public async Task<ActionResult<Response<DeletePoemResponse>>> DeletePoem(DeletePoemRequest deletePoemRequest)
        {
           
                var response = await _poemService.DeletePoem(deletePoemRequest);
                return Ok(response);
            
        }

    }
}
