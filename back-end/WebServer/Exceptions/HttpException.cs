using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebServer.Exceptions
{
    public class HttpException : Exception
    {
        public int Status { get; set; }

        public HttpException(int status, string message) : base(message)
        {
            this.Status = status;
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
