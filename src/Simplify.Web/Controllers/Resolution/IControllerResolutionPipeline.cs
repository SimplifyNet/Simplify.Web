using Simplify.Web.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Resolution;

public interface IControllerResolutionPipeline
{
	ControllerResolutionState Execute(IControllerMetadata initialController, IHttpContext context);
}