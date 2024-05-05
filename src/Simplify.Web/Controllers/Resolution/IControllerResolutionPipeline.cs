using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionPipeline
{
	ControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context);
}