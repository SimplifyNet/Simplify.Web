using System.Collections.Generic;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.Controllers.Processing;

public class CrsHandlingPipeline(IReadOnlyList<ICrsHandlingPipelineStage> stages) : ICrsHandlingPipeline
{
	public bool Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		foreach (var item in stages)
		{
			if (item.IsApplicable(state))
			{
				item.Execute(state, builder);

				if (item.IsTerminal)
					return true;
			}
		}

		return false;
	}
}