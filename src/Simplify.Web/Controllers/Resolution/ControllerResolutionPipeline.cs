using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution;

/// <summary>
/// Provides the controller resolution pipeline.
/// </summary>
/// <seealso cref="IControllerResolutionPipeline" />
public class ControllerResolutionPipeline(IReadOnlyList<IControllerResolutionStage> stages) : IControllerResolutionPipeline
{
	/// <summary>
	/// Executes this pipeline.
	/// </summary>
	/// <param name="initialController">The initial controller.</param>
	/// <param name="context">The context.</param>
	public IControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context)
	{
		var state = new ControllerResolutionState(initialController);

		var stopProcessing = false;

		foreach (var item in stages)
		{
			item.Execute(state, context, StopProcessing);

			if (stopProcessing) //-V3022
				break;
		}

		return state;

		void StopProcessing() => stopProcessing = true;
	}
}