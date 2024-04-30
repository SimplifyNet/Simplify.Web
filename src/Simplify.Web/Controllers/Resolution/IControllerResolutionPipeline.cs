using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionPipeline
{
	ControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context);
}