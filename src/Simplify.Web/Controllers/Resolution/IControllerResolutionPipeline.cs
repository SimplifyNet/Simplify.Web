using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution;

/// <summary>
/// Represents a controller resolution pipeline.
/// </summary>
public interface IControllerResolutionPipeline
{
	/// <summary>
	/// Executes this pipeline.
	/// </summary>
	/// <param name="initialController">The initial controller.</param>
	/// <param name="context">The context.</param>
	IControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context);
}