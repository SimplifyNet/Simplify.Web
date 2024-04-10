using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}