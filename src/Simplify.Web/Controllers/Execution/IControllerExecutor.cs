using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
	bool CanHandle(IControllerMetadata args);

	/// <summary>
	/// Creates the actual controller and executes it.
	/// </summary>
	/// <param name="args">The matched controller.</param>
	/// <param name="context">The HTTP contexts.</param>
	Task<ControllerResponse?> ExecuteAsync(IMatchedController controller, HttpContext context);
}