using Microsoft.AspNetCore.Identity;
using Poemify.BLL.Interfaces;
using Poemify.DAL.Interfaces;
using Poemify.Helpers.Interfaces;
using Poemify.Models.DTOs.Request;
using Poemify.Models.DTOs.Response;
using Poemify.Models.Entities;
using Poemify.Models.Enums;
using System.Security.Claims;

namespace Poemify.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IRepository<AppUser> _userRepo;
        private IJWTAuthenticator _jWTAuthenticator;
        private IRepository<UserProfile> _profileManager;

        public AuthService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IJWTAuthenticator jWTAuthenticator)
        {

            _unitOfWork = unitOfWork;

            _userManager = userManager;
            _roleManager = roleManager;
            _userRepo = _unitOfWork.GetRepository<AppUser>();
            _profileManager = _unitOfWork.GetRepository<UserProfile>();
            _jWTAuthenticator = jWTAuthenticator;

        }

        public async Task<Response<LoginResponseDto>> SignIn(LoginRequestDto loginRequest)
        {
            var username = loginRequest.UserName.Trim().ToLower();
            AppUser existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser is null)
                throw new InvalidOperationException($"User with username {loginRequest.UserName} Doesn't Exist");

            var isCredentialCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
            if (!isCredentialCorrect)
                throw new InvalidOperationException("Wrong Password");
            var userProfile = await _profileManager.GetSingleByAsync(x => x.UserId == existingUser.Id);
            if (userProfile is null)
                throw new InvalidOperationException("User Profile not found");

            var additionalClaims = new List<Claim> { new Claim("userType", userProfile.UserType.GetStringValue()) };
            JwtToken token = await _jWTAuthenticator.GenerateJwtToken(existingUser,expires: null, additionalClaims);
            var result = new LoginResponseDto(token);
            return new Response<LoginResponseDto> { 
                Success = true,
                Message = "Login attempt succesful",
                Result = result
            };

            
            
        }

        public async Task<Response<UserRegistrationResponse>> SignUpAsync(UserRegistrationRequest request)
        {

            AppUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");



            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                FirstName = request.Firstname.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.MobileNumber,
               
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            

            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}";
                throw new InvalidOperationException(message);

            }

            string? role = "User";
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new AppRole(role));
                await _userManager.AddToRoleAsync(user, role);
            }
            else
                await _userManager.AddToRoleAsync(user, role);


            var userProfile = new UserProfile
            {
                Id = Guid.NewGuid().ToString(),
                Gender = request.Gender,
                UserType = UserType.Reader,
                UserId = user.Id,
                

            };
            var newProfile = await _profileManager.AddAsync(userProfile);
            if (newProfile == null)
                throw new InvalidOperationException("Problem creating user profile");
            var additionalClaims = new List<Claim> { new Claim("userType", newProfile.UserType.GetStringValue()) };
            JwtToken userToken = await _jWTAuthenticator.GenerateJwtToken(user,expires: null,additionalClaims);
            var sigUpResponse = new UserRegistrationResponse
            {
                Token = userToken,
                UserId = user.Id,
                UserName = user.UserName,
            };

            return new Response<UserRegistrationResponse>
            {
                Success = true,
                Message = "your account has been created",
                Result = sigUpResponse

            };

        }

      
    }
}
