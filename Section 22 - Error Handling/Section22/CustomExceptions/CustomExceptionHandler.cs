namespace Section22.CustomExceptions
{
    public class CustomExceptionHandler : ArgumentNullException
    {
        public CustomExceptionHandler() 
        {
        }
        public CustomExceptionHandler(string? message) : base(message)
        { 
        }
        public CustomExceptionHandler(string? message, Exception? exception) : base(message, exception)
        {
            
        }
    }
}
