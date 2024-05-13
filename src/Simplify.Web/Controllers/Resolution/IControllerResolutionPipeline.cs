using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionPipeline
{
	IControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context);
}