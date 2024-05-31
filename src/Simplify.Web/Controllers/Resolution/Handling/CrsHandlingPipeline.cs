using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

/// <summary>
/// Provides the CRS handling pipeline.
/// </summary>
/// <seealso cref="ICrsHandlingPipeline" />
public class CrsHandlingPipeline(IReadOnlyList<ICrsHandler> handlers) : ICrsHandlingPipeline
{
	/// <summary>
	/// Executes this pipeline.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="builder">The builder.</param>
	public bool Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder)
	{
		var handler = handlers.FirstOrDefault(x => x.CanHandle(state));

		if (handler == null)
			return false;

		handler.Execute(state, builder);

		return handler.IsTerminal;
	}
}