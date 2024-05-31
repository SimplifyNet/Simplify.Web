using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.Meta.Factory;

/// <summary>
/// Provides the controller metadata factories resolver
/// </summary>
/// <seealso cref="IControllerMetadataFactoryResolver" />
public class ControllerMetadataFactoryResolver(IEnumerable<IControllerMetadataFactory> factories) : IControllerMetadataFactoryResolver
{
	/// <summary>
	/// Resolves the controller metadata factory for specified type.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException">No matching controller metadata factory found for controller type: " + controllerType</exception>
	public IControllerMetadataFactory Resolve(Type controllerType) =>
		factories.FirstOrDefault(x => x.CanHandle(controllerType))
		?? throw new InvalidOperationException("No matching controller metadata factory found for controller type: " + controllerType);
}