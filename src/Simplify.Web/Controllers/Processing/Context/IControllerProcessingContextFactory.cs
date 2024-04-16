using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}