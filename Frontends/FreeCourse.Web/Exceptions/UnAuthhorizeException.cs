using System.Runtime.Serialization;

namespace FreeCourse.Web.Exceptions
{
    public class UnAuthhorizeException : Exception
    {
        public UnAuthhorizeException()
        {
        }

        public UnAuthhorizeException(string? message) : base(message)
        {
        }

        public UnAuthhorizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnAuthhorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
