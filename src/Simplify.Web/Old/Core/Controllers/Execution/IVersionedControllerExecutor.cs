using System.Threading.Tasks;
using Simplify.Web.Old.Meta;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
/// Represents versioned controller executor, handles creation and execution of controllers.
/// </summary>
public interface IVersionedControllerExecutor
{
	/// <summary>
	/// Gets the supported controller version.
	/// </summary>
	public ControllerVersion Version { get; }

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response.</returns>
	Task<ControllerResponse?> Execute(IControllerExecutionArgs args);
}