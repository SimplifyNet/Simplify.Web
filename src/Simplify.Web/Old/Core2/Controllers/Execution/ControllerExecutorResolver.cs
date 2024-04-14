using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Execution;

public class ControllerExecutorResolver(IEnumerable<IControllerExecutor> executors) : IControllerExecutorResolver
{
	public IControllerExecutor Resolve(IControllerMetaData controller) =>
		executors.FirstOrDefault(x => x.CanHandle(controller))
			?? throw new InvalidOperationException("No matching controller executor found for controller type: " + controller.ControllerType);
}
