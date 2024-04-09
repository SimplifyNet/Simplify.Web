using System;

namespace Simplify.Web.Meta2;

/// <summary>
/// Represents controller metadata factories director
/// </summary>
public interface IControllerMetaDataFactoriesDirector
{
	/// <summary>
	/// Creates the controller metadata for specified type.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	IControllerMetaData Create(Type controllerType);
}