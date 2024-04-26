using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public interface IHttpRequest
{
	public HttpRequest Request { get; }
}