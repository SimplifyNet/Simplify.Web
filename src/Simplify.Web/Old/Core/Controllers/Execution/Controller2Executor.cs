using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Meta;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
///  Provides v2 controllers executor.
/// </summary>
/// <param name="controllerFactory">The controller factory.</param>
public class Controller2Executor(IController2Factory controllerFactory) : IVersionedControllerExecutor
{
	private readonly IController2Factory _controllerFactory = controllerFactory;

	/// <summary>
	/// Gets the controller version
	/// </summary>
	public ControllerVersion Version => ControllerVersion.V2;

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response.</returns>
	/// <returns></returns>
	public Task<ControllerResponse?> Execute(IControllerExecutionArgs args)
	{
		throw new NotImplementedException();
	}
}
