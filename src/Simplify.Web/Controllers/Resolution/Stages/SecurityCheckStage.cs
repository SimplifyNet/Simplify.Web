using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.Stages;

public class SecurityCheckStage(ISecurityChecker securityChecker) : IControllerResolutionStage
{
	public void Execute(IControllerResolutionState state, HttpContext context, Action stopExecution)
	{
		state.SecurityStatus = securityChecker.CheckSecurityRules(state.Controller, context.User);

		if (state.SecurityStatus != SecurityStatus.Ok)
			stopExecution();
	}
}