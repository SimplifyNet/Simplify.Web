using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Core2.Controllers.Security;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContext
{
	public IMatchedController Controller { get; }

	public SecurityStatus SecurityStatus { get; }

	public IHttpContext Context { get; }

	void SetResponseStatusCode(int code);
}