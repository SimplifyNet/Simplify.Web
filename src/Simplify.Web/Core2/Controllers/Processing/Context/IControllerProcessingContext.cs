using Simplify.Web.Core2.Controllers.RouteMatching;
using Simplify.Web.Core2.Controllers.Security;

namespace Simplify.Web.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContext
{
	public IMatchedController Controller { get; }

	public SecurityStatus SecurityStatus { get; }

	public IHttpContext Context { get; }

	void SetResponseStatusCode(int code);
}