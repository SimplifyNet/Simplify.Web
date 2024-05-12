using System.Net;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling.Stages;

public class ForbiddenHandler : ICrsHandler
{
	public bool IsTerminal => true;

	public bool CanHandle(IControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Forbidden;

	public void Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder)
	{
		builder.Controllers.Clear();

		if (ControllersMetaStore.Current.ForbiddenController == null)
			builder.HttpStatusCode = HttpStatusCode.Forbidden;
		else
			builder.Controllers.Add(ControllersMetaStore.Current.ForbiddenController.ToMatchedController());
	}
}