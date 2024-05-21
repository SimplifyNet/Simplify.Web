using System.Threading.Tasks;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution;

/// <summary>
/// Represents a controller executor, handles creation and execution of controllers.
/// </summary>
public interface IControllerExecutor
{
	/// <summary>
	/// Determines whether this executor can execute controller.
	/// </summary>
	bool CanHandle(IControllerMetadata controllerMetadata);

	/// <summary>
	/// Creates the actual controller and executes it.
	/// </summary>
	/// <param name="args">The matched controller.</param>
	Task<ControllerResponse?> ExecuteAsync(IMatchedController matchedController);
}