using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

/// <summary>
/// Represents a CRS handler.
/// </summary>
public interface ICrsHandler
{
	/// <summary>
	/// Gets a value indicating whether this handler is terminal.
	/// </summary>
	/// <value>
	///   <c>true</c> if this handler is terminal; otherwise, <c>false</c>.
	/// </value>
	bool IsTerminal { get; }

	/// <summary>
	/// Determines whether this handler can handle the specified state.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <returns>
	///   <c>true</c> if this handler can handle the specified state; otherwise, <c>false</c>.
	/// </returns>
	bool CanHandle(IControllerResolutionState state);

	/// <summary>
	/// Executes the handler.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="builder">The builder.</param>
	void Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder);
}