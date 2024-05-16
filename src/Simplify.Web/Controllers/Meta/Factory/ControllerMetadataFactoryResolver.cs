using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.Meta.Factory;

public class ControllerMetadataFactoryResolver(IEnumerable<IControllerMetadataFactory> factories) : IControllerMetadataFactoryResolver
{
	public IControllerMetadataFactory Resolve(Type controllerType) =>
		factories.FirstOrDefault(x => x.CanHandle(controllerType))
		?? throw new InvalidOperationException("No matching controller metadata factory found for controller type: " + controllerType);
}