using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public class CrsHandlingPipeline(IReadOnlyList<ICrsHandler> handlers) : ICrsHandlingPipeline
{
	public bool Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		var handler = handlers.FirstOrDefault(x => x.CanHandle(state));

		if (handler == null)
			return false;

		handler.Execute(state, builder);

		return handler.IsTerminal;
	}
}