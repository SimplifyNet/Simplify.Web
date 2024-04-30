using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, HttpContext context);
}