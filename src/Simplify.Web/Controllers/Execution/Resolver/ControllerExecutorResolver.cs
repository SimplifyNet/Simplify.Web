using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution.Resolver;

/// <summary>
/// Provides the controller execution resolver
/// </summary>
/// <seealso cref="IControllerExecutorResolver" />
public class ControllerExecutorResolver(IReadOnlyList<IControllerExecutor> executors) : IControllerExecutorResolver
{
	/// <summary>
	/// Resolves the specified controller executor.
	/// </summary>
	/// <param name="controller">The controller type.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException">No matching controller executor found for controller type: " + controller.ControllerType</exception>
	public IControllerExecutor Resolve(IControllerMetadata controller) =>
		executors.FirstOrDefault(x => x.CanHandle(controller))
		?? throw new InvalidOperationException("No matching controller executor found for controller type: " + controller.ControllerType);
}