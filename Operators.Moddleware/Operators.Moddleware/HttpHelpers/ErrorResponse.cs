namespace Operators.Moddleware.HttpHelpers {
    public class ErrorResponse<T> : SystemResponse<T> {

        public ErrorResponse(int statusCode, string message, string description) { 
            ResponseCode = statusCode;
            ResponseMessage = message; 
            ResponseDescription = description;
        }
        
    }
}
