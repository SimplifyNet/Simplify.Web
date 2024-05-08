using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public interface ICrsHandler
{
	bool IsTerminal { get; }

	bool CanHandle(ControllerResolutionState state);

	void Execute(ControllerResolutionState state, ExecutionWorkOrderBuilder builder);
}