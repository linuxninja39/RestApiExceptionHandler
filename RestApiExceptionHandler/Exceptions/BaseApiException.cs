using System;

namespace RestApiExceptionHandler.Exceptions
{
    public abstract class BaseApiException: Exception
    {
        public int StatusCode => 500;

        protected BaseApiException() : base()
        {
        }

        protected BaseApiException(string msg) : base(msg)
        {
        }
    }
}