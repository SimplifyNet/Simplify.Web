using System.Threading.Tasks;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represents controller executor, handles creation and execution of controllers.
/// </summary>
public interface IControllerExecutor
{
	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response result.</returns>
	Task<ControllerResponseResult> Execute(IControllerExecutionArgs args);
}