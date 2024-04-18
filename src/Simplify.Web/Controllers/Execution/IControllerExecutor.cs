using System.Threading.Tasks;
using Simplify.Web.Controllers.Execution.Args;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution;

/// <summary>
/// Represents a controller executor, handles creation and execution of controllers.
/// </summary>
public interface IControllerExecutor
{
	/// <summary>
	/// Determines whether this executor can execute controller.
	/// </summary>
	bool CanHandle(IControllerMetadata args);

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	Task<ControllerResponse?> Execute(IControllerExecutionArgs args);
}