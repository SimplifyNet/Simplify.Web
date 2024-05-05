using System;

namespace Simplify.Web.Controllers.Meta.Factory;

/// <summary>
/// Represents controller metadata factories resolver
/// </summary>
public interface IControllerMetadataFactoryResolver
{
	/// <summary>
	/// Resolves the controller metadata factory for specified type.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	IControllerMetadataFactory Resolve(Type controllerType);
}