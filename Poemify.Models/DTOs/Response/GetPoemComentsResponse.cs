using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
   public record GetPoemCommentsResponse
    {
        public string PoemTitle { get; set; }
        public string Commenter { get; set; }
        public string Comment { get; set; }
    }
}
