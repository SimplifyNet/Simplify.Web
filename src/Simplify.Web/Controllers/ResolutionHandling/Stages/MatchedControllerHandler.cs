using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.Controllers.ResolutionHandling.Stages;

public class MatchedControllerHandler : ICrsHandlingPipelineStage
{
	public bool IsTerminal => false;

	public bool IsApplicable(ControllerResolutionState state) => state.IsMatched;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder) => builder.Controllers.Add(state.ToMatchedController());
}