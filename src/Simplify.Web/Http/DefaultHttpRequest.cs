using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public class DefaultHttpRequest(HttpRequest originalRequest) : IHttpRequest
{
	public HttpRequest Request => originalRequest;
}