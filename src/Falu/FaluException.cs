using System.Net;

namespace Falu;

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
    public FaluException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    /// 
    public HttpStatusCode StatusCode { get; }

    /// 
    public HttpResponseMessage? Response { get; internal set; }

    /// 
    public string? RequestId { get; internal set; }

    /// 
    public string? TraceId { get; internal set; }

    /// 
    public FaluError? Error { get; internal set; }
}
