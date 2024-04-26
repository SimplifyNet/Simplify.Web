using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public class DefaultHttpResponse(HttpResponse originalResponse) : IHttpResponse
{
	public HttpResponse Response => originalResponse;
}