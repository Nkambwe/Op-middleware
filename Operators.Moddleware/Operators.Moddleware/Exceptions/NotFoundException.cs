namespace Operators.Moddleware.Exceptions {
    public class NotFoundException(string message) : Exception(message) {
        public int ErrorCode => 404;
    }
}
