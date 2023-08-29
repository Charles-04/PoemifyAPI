using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Response
{
    public record GetAllPoemsResponse
    {
        List<PoemResponse> Poems { get; init; }
    }
    public record PoemResponse
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Body { get; init; }
    }
}
