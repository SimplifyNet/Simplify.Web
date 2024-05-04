using System.Net;
using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.ResolutionHandling;

namespace Simplify.Web.Controllers.ResolutionResultHandling.Stages;

public class UnauthorizedHandler : ICrsHandler
{
	public bool IsTerminal => true;

	public bool CanHandle(ControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Unauthorized;

	public void Execute(ControllerResolutionState state, WorkOrderBuilder builder) =>
		builder.HttpStatusCode = HttpStatusCode.Unauthorized;
}