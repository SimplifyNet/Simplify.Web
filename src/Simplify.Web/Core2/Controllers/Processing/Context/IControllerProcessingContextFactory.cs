using Simplify.Web.Core2.Controllers.RouteMatching;

namespace Simplify.Web.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}