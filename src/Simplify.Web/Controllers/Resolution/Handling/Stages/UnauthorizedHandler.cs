using System.Net;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling.Stages;

public class UnauthorizedHandler : ICrsHandler
{
	public bool IsTerminal => true;

	public bool CanHandle(IControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Unauthorized;

	public void Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder) =>
		builder.HttpStatusCode = HttpStatusCode.Unauthorized;
}