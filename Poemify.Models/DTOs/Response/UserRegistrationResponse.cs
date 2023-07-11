using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
    public record UserRegistrationResponse
    {
        public string UserId { get;init; }
        public string UserName { get;init; }
        public JwtToken Token { get; init; }
       
    }
}
