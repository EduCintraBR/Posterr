namespace Posterr.Domain.Entities {
    public class Post {

        public Guid Identifier { get; set; }

        public Guid UserID { get; set; }
        public virtual User? User { get; set; }

        public required string Content { get; set; }
        
        public int RepostCount { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
