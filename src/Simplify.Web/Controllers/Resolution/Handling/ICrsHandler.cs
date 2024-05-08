using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public interface ICrsHandler
{
	bool IsTerminal { get; }

	bool CanHandle(ControllerResolutionState state);

	void Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}