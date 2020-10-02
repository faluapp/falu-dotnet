using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Falu.Infrastructure
{
    /// 
    [Serializable]
    public class FaluException : Exception
    {
        /// 
        public FaluException() { }

        /// 
        public FaluException(string message) : base(message) { }

        /// 
        public FaluException(string message, Exception inner) : base(message, inner) { }

        /// 
        protected FaluException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// 
        public FaluException(HttpStatusCode statusCode, FaluError error, string message) : base(message)
        {
            StatusCode = statusCode;
            Error = error;
        }

        /// 
        public HttpStatusCode StatusCode { get; internal set; }

        /// 
        public FaluError Error { get; internal set; }

        /// 
        public HttpResponseMessage Response { get; internal set; }
    }
}
