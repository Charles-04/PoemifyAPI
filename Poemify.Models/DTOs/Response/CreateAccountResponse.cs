using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
    public class AccountResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
