using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;

namespace Simplify.Web.Controllers.ResolutionHandling;

public interface ICrsHandler
{
	bool IsTerminal { get; }

	bool CanHandle(ControllerResolutionState state);

	void Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}