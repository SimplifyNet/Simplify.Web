using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Execution.Resolver;

public class ControllerExecutorResolver(IReadOnlyList<IControllerExecutor> executors) : IControllerExecutorResolver
{
	public IControllerExecutor Resolve(IControllerMetadata controller) =>
		executors.FirstOrDefault(x => x.CanHandle(controller))
		?? throw new InvalidOperationException("No matching controller executor found for controller type: " + controller.ControllerType);
}