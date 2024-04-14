using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplify.Web.Old.Meta;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
/// Provides controller executor, handles creation and execution of controllers.
/// </summary>
public class ControllerExecutor : IControllerExecutor
{
	private readonly IList<IVersionedControllerExecutor> _controllerExecutors;
	private readonly IControllerResponseBuilder _controllerResponseBuilder;

	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerExecutor"/> class.
	/// </summary>
	/// <param name="controllerExecutors">The controller executors</param>
	/// <param name="controllerResponseBuilder">The controller response builder</param>
	public ControllerExecutor(IList<IVersionedControllerExecutor> controllerExecutors, IControllerResponseBuilder controllerResponseBuilder)
	{
		foreach (var item in Enum.GetValues(typeof(ControllerVersion)))
			if (controllerExecutors.All(x => x.Version != (ControllerVersion)item))
				throw new InvalidOperationException($"The Simplify.Web versioned executor for version '{item}' is not found");

		_controllerExecutors = controllerExecutors;
		_controllerResponseBuilder = controllerResponseBuilder;
	}

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response.</returns>
	public async Task<ControllerResponseResult> Execute(IControllerExecutionArgs args)
	{
		var executor = _controllerExecutors.First(x => x.Version == args.ControllerMetaData.Version);

		var response = await executor.Execute(args);

		if (response == null)
			return ControllerResponseResult.Default;

		_controllerResponseBuilder.BuildControllerResponseProperties(response, args.Resolver);

		return await response.ExecuteAsync();
	}
}