using Poemify.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; } = UserType.Reader;
        public string? ImageUrl { get; set; }
        public virtual IEnumerable<Poem>? Poem { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
