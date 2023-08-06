using Microsoft.EntityFrameworkCore;
using Poemify.BLL.Interfaces;
using Poemify.DAL.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using Poemify.Models.Enums;

namespace Poemify.BLL.Services
{
    public class PoemService : IPoemService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        private IRepository<AppUser> _userManager;
        private IRepository<Poem> _poemManager;
        private IRepository<Tag> _tagManager;
        private IRepository<PoemTag> _poemTagManager;
        public PoemService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
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
            var userProfile = await _unitOfWork.GetRepository<UserProfile>().GetSingleByAsync(u => u.UserId == user.Id);
            if (userProfile.UserType != UserType.Poet)
                throw new UnauthorizedAccessException("Poems can only be written by poets");
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
        private async Task AssociatePoemAndTags(List<Tag> tags, Poem poem)
        {

            if (!tags.Any())
                return;
            List<PoemTag> poemTags = new List<PoemTag>();

            foreach (var tag in tags)
            {
                poemTags.Add(new PoemTag
                {
                    TagId = tag.Id,
                    PoemId = poem.Id
                });
            }
            await _poemTagManager.AddRangeAsync(poemTags);
        }

        private async Task<List<Tag>> GetTags(IEnumerable<string> tagList)
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
                    if (result == null)
                        continue;
                    tags.Add(result);
                }
                tags.Add(existingTag);
            }
            tags.RemoveAll(x => x.Id == null);
            return tags;
        }
      
        public async Task<Response<UpdatePoemResponse>> UpdatePoem(UpdatePoemRequest updatePoemRequest)
        {
            var poem = await _poemManager.GetSingleByAsync(u => u.Id == updatePoemRequest.PoemId);
            if (poem == null)
                throw new InvalidOperationException("Poem not found");

            poem.Body = updatePoemRequest.Body;
            var response = await _poemManager.UpdateAsync(poem);
            if (response != null)
                throw new InvalidOperationException("Problem saving changes");

            var result = new UpdatePoemResponse(response.Id);
            return new Response<UpdatePoemResponse>() {
                Success = true,
                Message = "Poem updated successfully",
                Result = result
            };
        }

        public async Task<Response<DeletePoemResponse>> DeletePoem(DeletePoemRequest deletePoemRequest)
        {
            var user = await _userManager.GetSingleByAsync(u => u.Id == deletePoemRequest.UserId);
            var poem = await _poemManager.GetSingleByAsync(u => u.Id == deletePoemRequest.PoemId);
            if (poem == null || user == null)
                throw new InvalidOperationException("Poem or user doesn't exist");
            if (poem.AuthorId != user.Id)
                throw new InvalidOperationException("You can't delete a poem you didn't write");
            poem.Deleted = true;
            var deletedPoem = await _poemManager.UpdateAsync(poem);
            var result = new DeletePoemResponse(deletedPoem.Id);
            return new Response<DeletePoemResponse>()
            {
                Success = true,
                Message = "Poem deleted",
                Result = result
            };
        }

        public Task<IEnumerable<GetPoemCommentsResponse>> GetPoemComments(string poemId)
        {
            var poem = _poemManager.GetQueryable(x => x.Id == poemId).Include(c => c.Comments).SingleOrDefault();
            if (poem == null)
                throw new InvalidOperationException("Poem not found");
            
            throw new NotImplementedException();
        }

        public Task GetPoemByTags()
        {
            throw new NotImplementedException();
        }
    }
}
