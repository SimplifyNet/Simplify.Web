using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Processing.Context;

public interface IControllerProcessingContext
{
	public IMatchedController Controller { get; }

	public SecurityStatus SecurityStatus { get; }

	public IHttpContext Context { get; }

	void SetResponseStatusCode(int code);
}