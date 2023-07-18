using Poemify.BLL.Interfaces;
using Poemify.DAL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Services
{
    public class PoemService : IPoemService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<AppUser> _userManager;
        private IRepository<Poem> _poemManager;
        public PoemService(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
             _poemManager = _unitOfWork.GetRepository<Poem>();
             _userManager = _unitOfWork.GetRepository<AppUser>();
        }
        public async Task<Response<CreatePoemResponse>> CreatePoem(string userId, CreatePoemRequest poemRequest)
        {
            var user = await _userManager.GetSingleByAsync(x => x.Id == userId);
            if (user == null)
                throw new InvalidOperationException("User Not found");
            var poem = new Poem
            {
                Id = Guid.NewGuid().ToString(),
                Title = poemRequest.Title,
                Body = poemRequest.Body,
                Tags = poemRequest.Tags,
                AuthorId = user.Id
            };
            var newPoem = await _poemManager.AddAsync(poem);
            if (newPoem is null)
                throw new InvalidOperationException("Problem saving new poem");
            var result = new CreatePoemResponse
            {
                AuthorId = newPoem.AuthorId,
                Id = newPoem.Id
            };
            return new Response<CreatePoemResponse>
            {
                Success = true,
                Result = result,
                Message = $"Poem posted succesfully"
            };
        }

        public Task DeletePoem()
        {
            throw new NotImplementedException();
        }

        public Task GetComments()
        {
            throw new NotImplementedException();
        }

        public Task UpdatePoem()
        {
            throw new NotImplementedException();
        }
    }
}
