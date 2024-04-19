using System;

namespace Simplify.Web.Meta.Controllers;

/// <summary>
/// Represents controller metadata factories resolver
/// </summary>
public interface IControllerMetaDataFactoryResolver
{
	/// <summary>
	/// Resolves the controller metadata factory for specified type.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	IControllerMetaDataFactory Resolve(Type controllerType);
}