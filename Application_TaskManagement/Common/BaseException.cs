
using System.Net;


namespace Application_TaskManagement.Common
{
    public abstract class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected BaseException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
