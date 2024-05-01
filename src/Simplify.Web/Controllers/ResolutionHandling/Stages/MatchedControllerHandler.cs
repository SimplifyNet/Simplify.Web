using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;

namespace Simplify.Web.Controllers.ResolutionHandling.Stages;

public class MatchedControllerHandler : ICrsHandler
{
	public bool IsTerminal => false;

	public bool CanHandle(ControllerResolutionState state) => state.IsMatched;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder) => builder.Controllers.Add(state.ToMatchedController());
}