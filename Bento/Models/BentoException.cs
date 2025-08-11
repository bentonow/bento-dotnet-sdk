using System;
using System.Net;
using System.Runtime.Serialization;

namespace Bento.Models;

[Serializable]
public class BentoException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public BentoException() : base("A Bento API error occurred")
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public BentoException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public BentoException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public BentoException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public BentoException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    protected BentoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        StatusCode = (HttpStatusCode)(info.GetValue(nameof(StatusCode), typeof(HttpStatusCode)) ?? HttpStatusCode.InternalServerError);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(StatusCode), StatusCode);
    }
}