
using Poemify.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Poemify.Models.DTOs.Request
{
    public record UserRegistrationRequest
    {
        [Required]
        public string Password { get; init; }
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Phone]
        public string MobileNumber { get; init; }
        [Required]
        public string UserName { get; init; }

        [Required]
        public string Firstname { get; init; }

        [Required]
        public string LastName { get; init; }
        [Required]
        public string Role { get; init; }
        [Required]
        public Gender Gender { get; init; }

        [Required]
        public UserType UserTypeId { get; init; }


    }
}
