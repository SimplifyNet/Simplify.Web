namespace Simplify.Web.Controllers.V1.Execution;

/// <summary>
/// Represents a v1 controller factory.
/// </summary>
public interface IController1Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The matched controller.</param>
	/// <returns>The controller.</returns>
	ControllerBase CreateController(IMatchedController matchedController);
}