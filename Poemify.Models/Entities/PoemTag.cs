using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.Entities
{
    public class PoemTag
    {
        public string PoemId { get; set; }
        public string TagId { get; set; }

        public Poem Poem { get; set; }
        public Tag Tag { get; set; }
    }
}
