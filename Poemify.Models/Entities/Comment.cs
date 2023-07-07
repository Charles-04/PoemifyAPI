

namespace Poemify.Models.Entities
{
    public class Comment : BaseEntity
    {
        public AppUser Author { get; set; }
        public string Content { get; set; }
    }
}
