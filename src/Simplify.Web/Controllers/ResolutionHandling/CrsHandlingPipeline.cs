using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;

namespace Simplify.Web.Controllers.ResolutionHandling;

public class CrsHandlingPipeline(IReadOnlyList<ICrsHandler> handlers) : ICrsHandlingPipeline
{
	public bool Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		var handler = handlers.First(x => x.CanHandle(state));

		handler.Execute(state, builder);

		return handler.IsTerminal;
	}
}