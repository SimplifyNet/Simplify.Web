using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public interface IHttpResponse
{
	public HttpResponse Response { get; }
}