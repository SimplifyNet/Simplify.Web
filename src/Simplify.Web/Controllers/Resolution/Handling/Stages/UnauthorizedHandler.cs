using System.Net;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling.Stages;

/// <summary>
/// Provides the unauthorized handler.
/// </summary>
/// <seealso cref="ICrsHandler" />
public class UnauthorizedHandler : ICrsHandler
{
	/// <summary>
	/// Gets a value indicating whether this handler is terminal.
	/// </summary>
	/// <value>
	///   <c>true</c> if this handler is terminal; otherwise, <c>false</c>.
	/// </value>
	public bool IsTerminal => true;

	/// <summary>
	/// Determines whether this handler can handle the specified state.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <returns>
	///   <c>true</c> if this handler can handle the specified state; otherwise, <c>false</c>.
	/// </returns>
	public bool CanHandle(IControllerResolutionState state) => state.SecurityStatus == Security.SecurityStatus.Unauthorized;

	/// <summary>
	/// Executes the handler.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="builder">The builder.</param>
	public void Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder) =>
		builder.HttpStatusCode = HttpStatusCode.Unauthorized;
}