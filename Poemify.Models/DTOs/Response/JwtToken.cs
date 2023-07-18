using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Expires { get; set; }
    }
}
