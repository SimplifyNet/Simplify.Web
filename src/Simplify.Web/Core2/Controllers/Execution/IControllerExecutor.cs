using System.Threading.Tasks;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Execution;

/// <summary>
/// Represents controller executor, handles creation and execution of controllers.
/// </summary>
public interface IControllerExecutor
{
	/// <summary>
	/// Determines whether this executor can execute controller.
	/// </summary>
	bool CanHandle(IControllerMetaData args);

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response.</returns>
	Task<ControllerResponse?> Execute(IControllerExecutionArgs args);
}