using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution;

public class ControllerExecutorResolver(IEnumerable<IControllerExecutor> executors) : IControllerExecutorResolver
{
	public IControllerExecutor Resolve(IControllerMetadata controller) =>
		executors.FirstOrDefault(x => x.CanHandle(controller))
		?? throw new InvalidOperationException("No matching controller executor found for controller type: " + controller.ControllerType);
}