using Posterr.Domain.Enums;

namespace Posterr.Domain.Entities {
    public class Content {

        public Guid Identifier { get; set; }

        public Guid UserID { get; set; }
        public virtual User? User { get; set; }

        public DateTime Date { get; set; }
        public EContentAction Action { get; set; }

        public Guid ContentPostID { get; set; }
        public virtual Post? ContentPost { get; set; }   

    }
}
