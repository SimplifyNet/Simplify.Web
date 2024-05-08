using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public class CrsHandlingPipeline(IReadOnlyList<ICrsHandler> handlers) : ICrsHandlingPipeline
{
	public bool Execute(ControllerResolutionState state, ExecutionWorkOrderBuilder builder)
	{
		var handler = handlers.FirstOrDefault(x => x.CanHandle(state));

		if (handler == null)
			return false;

		handler.Execute(state, builder);

		return handler.IsTerminal;
	}
}