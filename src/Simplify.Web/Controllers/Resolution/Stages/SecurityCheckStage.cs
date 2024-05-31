using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.Stages;

/// <summary>
/// Provides the security check stage.
/// </summary>
/// <seealso cref="IControllerResolutionStage" />
public class SecurityCheckStage(ISecurityChecker securityChecker) : IControllerResolutionStage
{
	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="context">The context.</param>
	/// <param name="stopExecution">The action to stop execution.</param>
	public void Execute(IControllerResolutionState state, HttpContext context, Action stopExecution)
	{
		state.SecurityStatus = securityChecker.CheckSecurityRules(state.Controller, context.User);

		if (state.SecurityStatus != SecurityStatus.Ok)
			stopExecution();
	}
}