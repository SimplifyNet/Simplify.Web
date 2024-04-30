using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Processing;

public class ControllerResolutionPipeline(IReadOnlyList<IControllerResolutionStage> stages) : IControllerResolutionPipeline
{
	public ControllerResolutionState Execute(IControllerMetadata initialController, HttpContext context)
	{
		var state = new ControllerResolutionState(initialController);

		var stopProcessing = false;

		foreach (var item in stages)
		{
			item.Execute(state, context, StopProcessing);

			if (stopProcessing)
				break;
		}

		return state;

		void StopProcessing() => stopProcessing = true;
	}
}