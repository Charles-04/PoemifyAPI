using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Request
{
    public record UpdatePoemRequest(string PoemId, string Body);
  
}
