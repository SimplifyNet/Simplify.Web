using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.Controllers.Resolution;

public interface ICrsHandlingPipelineStage
{
	bool IsTerminal { get; }

	bool IsApplicable(ControllerResolutionState state);

	void Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}