using Microsoft.AspNetCore.Identity;
using Poemify.DAL.Enums;


namespace Poemify.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; } = UserType.Reader;
        public string Image { get; set; }
    }
}
