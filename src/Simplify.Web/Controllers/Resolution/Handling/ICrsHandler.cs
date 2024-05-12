using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public interface ICrsHandler
{
	bool IsTerminal { get; }

	bool CanHandle(IControllerResolutionState state);

	void Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder);
}