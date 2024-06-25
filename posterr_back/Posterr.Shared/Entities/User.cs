namespace Posterr.Domain.Entities {
    public class User {

        public Guid Identifier { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }

    }
}
