using Poemify.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Helpers.Interfaces
{
  
        public interface IJWTAuthenticator
        {
            Task<JwtToken> GenerateJwtToken(AppUser user, string expires = null, List<Claim> additionalClaims = null);
        }
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Expires { get; set; }
    }
}

