using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling.Stages;

public class MatchedControllerHandler : ICrsHandler
{
	public bool IsTerminal => false;

	public bool CanHandle(ControllerResolutionState state) => state.IsMatched;

	public void Execute(ControllerResolutionState state, ExecutionWorkOrderBuilder builder) => builder.Controllers.Add(state.ToMatchedController());
}