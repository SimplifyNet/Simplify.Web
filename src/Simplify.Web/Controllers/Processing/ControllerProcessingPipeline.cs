using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Processing.Context;

namespace Simplify.Web.Controllers.Processing;

public class ControllerProcessingPipeline(IReadOnlyList<IControllerProcessingStage> stages) : IControllerProcessingPipeline
{
	public async Task Execute(IControllerProcessingContext context)
	{
		var stopProcessing = false;

		foreach (var item in stages)
		{
			await item.Execute(context, StopProcessing);

			if (stopProcessing)
				break;
		}

		return;

		void StopProcessing() => stopProcessing = true;
	}
}