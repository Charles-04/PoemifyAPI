using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
    public record CreatePoemResponse
    {
        public string Id { get; init; }
        public string AuthorId { get; init; }
    }
}
