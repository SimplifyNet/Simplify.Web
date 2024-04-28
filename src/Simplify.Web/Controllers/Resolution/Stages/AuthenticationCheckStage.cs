using System;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Resolution.Stages;

public class AuthenticationCheckStage(ISecurityChecker securityChecker) : IControllerResolutionStage
{
	public void Execute(ControllerResolutionState state, IHttpContext context, Action stopExecution)
	{
		state.SecurityStatus = securityChecker.CheckSecurityRules(state.ControllerMetadata, context.User);

		if (state.SecurityStatus != SecurityStatus.Ok)
			stopExecution();
	}
}