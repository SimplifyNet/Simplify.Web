using System;

namespace Simplify.Web.Meta.Controllers.Factory;

/// <summary>
/// Represents a controller metadata creator.
/// </summary>
public interface IControllerMetadataFactory
{
	/// <summary>
	/// Determines whether this factory can create a metadata fro specified type
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	bool CanHandle(Type controllerType);

	/// <summary>
	/// Creates the controller metadata.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	IControllerMetadata Create(Type controllerType);
}