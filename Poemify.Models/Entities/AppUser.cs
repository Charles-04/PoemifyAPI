using Microsoft.AspNetCore.Identity;
using Poemify.Models.Enums;


namespace Poemify.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; } = UserType.Reader;
        public string? ImageUrl { get; set; }
        public virtual IEnumerable<Poem>? Poem { get; set; }
        public virtual ICollection<UserClaim>? Claims { get; set; }
        public virtual ICollection<UserLogin>? Logins { get; set; }
        public virtual ICollection<UserToken>? Tokens { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
