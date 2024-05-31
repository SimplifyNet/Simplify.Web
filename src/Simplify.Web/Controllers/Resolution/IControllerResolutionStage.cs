using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution;

/// <summary>
/// Represents a controller resolution stage
/// </summary>
public interface IControllerResolutionStage
{
	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="context">The context.</param>
	/// <param name="stopExecution">The action to stop execution.</param>
	void Execute(IControllerResolutionState state, HttpContext context, Action stopExecution);
}