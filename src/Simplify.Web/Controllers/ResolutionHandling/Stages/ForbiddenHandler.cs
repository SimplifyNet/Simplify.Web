using System.Net;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.ResolutionHandling.Stages;

public class ForbiddenHandler(IControllersMetaStore metaStore) : ICrsHandler
{
	public bool IsTerminal => true;

	public bool CanHandle(ControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Forbidden;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder)
	{
		builder.Controllers.Clear();

		if (metaStore.Controller403 == null)
			builder.HttpStatusCode = HttpStatusCode.Forbidden;
		else
			builder.Controllers.Add(metaStore.Controller403.ToMatchedController());
	}
}