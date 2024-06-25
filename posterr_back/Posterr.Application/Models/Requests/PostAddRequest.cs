namespace Posterr.Application.Models.Requests {
    public class PostAddRequest {

        public Guid UserID { get; set; }
        public required string Content { get; set; }

    }
}
