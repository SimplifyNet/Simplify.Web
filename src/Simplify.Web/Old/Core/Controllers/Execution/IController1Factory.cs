namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represent controller v1 factory.
/// </summary>
public interface IController1Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller.</returns>
	ControllerBase CreateController(IControllerExecutionArgs args);
}