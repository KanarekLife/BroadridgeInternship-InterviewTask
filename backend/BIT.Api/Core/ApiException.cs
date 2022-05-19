using System.Net;

namespace BIT.Api.Core;

public abstract class ApiException : Exception
{
    public abstract int StatusCode { get; }
    public abstract string Title { get; }
    public abstract string Details { get; }
}