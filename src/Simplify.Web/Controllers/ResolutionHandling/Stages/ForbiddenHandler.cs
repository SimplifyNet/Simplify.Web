using System.Net;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.ResolutionHandling.Stages;

public class ForbiddenHandler : ICrsHandler
{
	public bool IsTerminal => true;

	public bool CanHandle(ControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Forbidden;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		builder.Controllers.Clear();

		if (ControllersMetaStore.Current.ForbiddenController == null)
			builder.HttpStatusCode = HttpStatusCode.Forbidden;
		else
			builder.Controllers.Add(ControllersMetaStore.Current.ForbiddenController.ToMatchedController());
	}
}