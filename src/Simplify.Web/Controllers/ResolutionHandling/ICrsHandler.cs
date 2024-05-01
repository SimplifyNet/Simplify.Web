using Simplify.Web.Controllers.ExecutionWorkOrder;

namespace Simplify.Web.Controllers.Resolution;

public interface ICrsHandler
{
	bool IsTerminal { get; }

	bool CanHandle(ControllerResolutionState state);

	void Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}