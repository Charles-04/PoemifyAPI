using Microsoft.AspNetCore.Identity;
using Poemify.BLL.Interfaces;
using Poemify.DAL.DTOs.Request;
using Poemify.DAL.DTOs.Response;
using Poemify.DAL.Entities;
using Poemify.DAL.Interfaces;
using Poemify.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IRepository<AppUser> _userRepo;
        private IJWTAuthenticator _jWTAuthenticator;

        public AuthService( IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IJWTAuthenticator jWTAuthenticator)
        {
            
            _unitOfWork = unitOfWork;
            
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepo = _unitOfWork.GetRepository<AppUser>();
            
            _jWTAuthenticator = jWTAuthenticator;

        }

        public async Task<AccountResponse> CreateUser(UserRegistrationRequest request)
        {

            AppUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");



            AppUser user = new()
            {

                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                FirstName = request.Firstname.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.MobileNumber,
                Gender = request.Gender
            };
         
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}";
                throw new InvalidOperationException(message);

            }

            

            string? role = "User";
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _roleManager.CreateAsync(new AppRole(role));

            return new AccountResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                Message = "your account has been created"

            };

        }

        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public Task RetrieveUser()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
