using System.Threading.Tasks;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution;

/// <summary>
/// Represents a controller executor, handles creation and execution of controllers.
/// </summary>
public interface IControllerExecutor
{
	/// <summary>
	/// Determines whether this executor can execute the controller.
	/// </summary>
	/// <param name="controllerMetadata">The controller metadata.</param>
	bool CanHandle(IControllerMetadata controllerMetadata);

	/// <summary>
	/// Creates an actual controller and executes it.
	/// </summary>
	/// <param name="matchedController">The matched controller.</param>
	Task<ControllerResponse?> ExecuteAsync(IMatchedController matchedController);
}