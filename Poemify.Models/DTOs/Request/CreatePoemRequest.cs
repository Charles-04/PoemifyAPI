using Poemify.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Request
{
    public record CreatePoemRequest
    {
      
        public string Title { get; init; }
        public string Body { get; init; }
        public IEnumerable<Tag> Tags { get; init; }
    }
}
