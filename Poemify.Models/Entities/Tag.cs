using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.Entities
{
    public class Tag : BaseEntity
    {
        
        public string Name { get; set; }
        public virtual IEnumerable<Poem> Poems { get; set; }
    }
}
