using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, HttpContext context);
}