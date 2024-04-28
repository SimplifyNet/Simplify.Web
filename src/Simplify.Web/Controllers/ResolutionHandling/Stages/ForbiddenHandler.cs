using Simplify.Web.Controllers.Extensions;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.WorkOrder.Construction;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.ResolutionHandling.Stages;

public class ForbiddenHandler(IControllersMetaStore metaStore) : ICrsHandlingPipelineStage
{
	public bool IsTerminal => true;

	public bool IsApplicable(ControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Forbidden;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		builder.Controllers.Clear();

		if (metaStore.Controller403 == null)
			builder.Status = WorkOrder.WorkOrderStatus.Forbidden;
		else
		{
			builder.Controllers.Add(metaStore.Controller403.ToMatchedController());
		}
	}
}