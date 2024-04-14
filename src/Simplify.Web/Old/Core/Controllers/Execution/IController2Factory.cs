namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represent controller v2 factory.
/// </summary>
public interface IController2Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller.</returns>
	ResponseShortcutsControllerBase CreateController(IControllerExecutionArgs args);
}