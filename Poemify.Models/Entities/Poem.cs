
namespace Poemify.Models.Entities
{
    public class Poem : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Likes { get; set; }
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }
        public bool Deleted { get; set; } = false;
        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual IEnumerable<PoemTag> PoemTags { get; set; }
        
        
    }
}
