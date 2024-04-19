using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Meta.Controllers;

public class ControllerMetaDataFactoryResolver(IEnumerable<IControllerMetaDataFactory> factories) : IControllerMetaDataFactoryResolver
{
	public IControllerMetaDataFactory Resolve(Type controllerType) =>
		factories.FirstOrDefault(x => x.CanHandle(controllerType))
		?? throw new InvalidOperationException("No matching controller metadata factory found for controller type: " + controllerType);
}
