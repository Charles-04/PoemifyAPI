using Microsoft.EntityFrameworkCore;
using Poemify.BLL.Interfaces;
using Poemify.BLL.Utility;
using Poemify.DAL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using Poemify.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Services
{
    public class CommentServices : ICommentService
    {
        private IServiceFactory _serviceFactory;
        private IUnitOfWork _unitOfWork;
        private IRepository<Poem> _poemManager;
        private IRepository<Comment> _commentsManager;
        private IPaginationService _paginationService;
        public CommentServices(IServiceFactory serviceFactory)
        {
                _serviceFactory = serviceFactory;
                _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
                _poemManager = _unitOfWork.GetRepository<Poem>();
            _commentsManager = _unitOfWork.GetRepository<Comment>();
            _paginationService = _serviceFactory.GetService<IPaginationService>();
        }
        public async Task<Response<PaginationResponse<CommentResponse>>> GetComments(GetPoemCommentsRequest request)
        {
            Poem poem = await _poemManager.GetSingleByAsync(x => x.Id == request.PoemId);
            if (poem != null)
            {
                IEnumerable<Comment> comments = _commentsManager.GetQueryable(x => x.PoemId == poem.Id).Include(x => x.Author).Include(x => x.Poem);
               PaginationResponse<CommentResponse> commentResponses = _paginationService.PaginateRecords<CommentResponse, Comment>(request.Page, request.PageSize, comments);
                return new Response<PaginationResponse<CommentResponse>>
                {
                    Success = true,
                    Message = "Comments retrieved",
                    Result = commentResponses
                };
            }
            else
            {
                throw new InvalidOperationException("Poem not found");
            }

        }
         public async Task Comment()
        {

        }
    }
}
