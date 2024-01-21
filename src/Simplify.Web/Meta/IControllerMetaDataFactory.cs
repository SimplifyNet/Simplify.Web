using System;

namespace Simplify.Web.Meta;

/// <summary>
/// Represent controller meta-data creator.
/// </summary>
public interface IControllerMetaDataFactory
{
	/// <summary>
	/// Creates the controller meta data.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <returns></returns>
	IControllerMetaData CreateControllerMetaData(Type controllerType);
}