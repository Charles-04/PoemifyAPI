

namespace Poemify.Models.Entities
{
    public class Comment : BaseEntity
    {
        public AppUser Author { get; set; }
        public string Content { get; set; }
        public string PoemId { get; set; }
        public Poem Poem { get; set; }
    }
}
