using Poemify.Models.DTOs.Response;
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
  
}

