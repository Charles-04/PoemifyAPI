using Poemify.BLL.Interfaces;
using Poemify.DAL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using System;
using System.Collections;
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
        private IRepository<Tag> _tagManager;
        private IRepository<PoemTag> _poemTagManager;
        public PoemService(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
             _poemManager = _unitOfWork.GetRepository<Poem>();
             _userManager = _unitOfWork.GetRepository<AppUser>();
            _tagManager = _unitOfWork.GetRepository<Tag>();
            _poemTagManager = _unitOfWork.GetRepository<PoemTag>();
        }
        public async Task<Response<CreatePoemResponse>> CreatePoem(string userId, CreatePoemRequest poemRequest)
        {
            var user = await _userManager.GetSingleByAsync(x => x.Id == userId);
            if (user == null)
                throw new InvalidOperationException("User Not found");
            var tags = await GetTags(poemRequest.Tags);
            var poem = new Poem
            {
                Id = Guid.NewGuid().ToString(),
                Title = poemRequest.Title,
                Body = poemRequest.Body,
                AuthorId = user.Id
            };
            var newPoem = await _poemManager.AddAsync(poem);
            if (newPoem is null)
                throw new InvalidOperationException("Problem saving new poem");
            await AssociatePoemAndTags(tags, newPoem);
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
        private async Task AssociatePoemAndTags(IEnumerable<Tag> tags, Poem poem)
        {
            if (!tags.Any())
                return;
            foreach (var tag in tags)
            {
                await _poemTagManager.AddAsync(new PoemTag
                {
                    TagId = tag.Id,
                    PoemId = poem.Id
                });
            }
        }
        private async Task<IEnumerable<Tag>> GetTags(IEnumerable<string> tagList)
        {
            List<Tag> tags = new();
            foreach (var tag in tagList)
            {
                if (string.IsNullOrWhiteSpace(tag))
                {
                    
                    continue;
                }
                 var existingTag = await _tagManager.GetSingleByAsync(x => x.Name.ToLower() == tag.Trim().ToLower());
                if (existingTag == null)
                {
                    var newTag = new Tag()
                    {
                        Name = tag,
                        Id = Guid.NewGuid().ToString(),
                    };
                   var result = await _tagManager.AddAsync(newTag);
                    tags.Add(result);
                }
                tags.Add(existingTag);
            }
            return tags;
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
