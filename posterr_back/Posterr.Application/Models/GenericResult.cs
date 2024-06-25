namespace Posterr.Application.Models {
    public class GenericResult {
        public GenericResult(bool success) {
            this.Success = success;
        }

        public GenericResult(bool success, string message) {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = "";

    }
}
