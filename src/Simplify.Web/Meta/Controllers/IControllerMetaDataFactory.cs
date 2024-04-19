using System;

namespace Simplify.Web.Meta.Controllers;

/// <summary>
/// Represents a controller metadata creator.
/// </summary>
public interface IControllerMetaDataFactory
{
	/// <summary>
	/// Determines whether this factory can create a metadata fro specified type
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	bool CanHandleType(Type controllerType);

	/// <summary>
	/// Creates the controller metadata.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	IControllerMetadata Create(Type controllerType);
}