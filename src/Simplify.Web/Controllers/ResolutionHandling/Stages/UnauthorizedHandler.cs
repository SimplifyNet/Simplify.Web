using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.WorkOrder;
using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.Controllers.ResolutionResultHandling.Stages;

public class UnauthorizedHandler : ICrsHandlingPipelineStage
{
	public bool IsTerminal => true;

	public bool IsApplicable(ControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.NotAuthenticated;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder) =>
		builder.Status = WorkOrderStatus.Unauthorized;
}