namespace Operators.Moddleware.Exceptions {
    public class DuplicateException(string message): Exception(message) {
        public int StatusCode = 409;
    }
}
