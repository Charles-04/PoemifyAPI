using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        [StringLength(450)]
        public virtual int ApplicationUserId { get; set; }
        [StringLength(450)]
        public virtual int ApplicationRoleId { get; set; }
        public virtual AppUser ApplicationUser { get; set; }
        public virtual AppRole ApplicationRole { get; set; }
    }
}
