namespace Simplify.Web.Controllers.V2.Execution;

/// <summary>
/// Represents a v2 controller factory.
/// </summary>
public interface IController2Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="matchedController">The matched controller.</param>
	/// <returns>
	/// The controller.
	/// </returns>
	ResponseShortcutsControllerBase CreateController(IMatchedController matchedController);
}