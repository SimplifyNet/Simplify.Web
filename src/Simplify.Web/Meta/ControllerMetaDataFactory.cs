using System;

namespace Simplify.Web.Meta;

/// <summary>
/// Creates controller meta-data
/// </summary>
public class ControllerMetaDataFactory : IControllerMetaDataFactory
{
	/// <summary>
	/// Creates the controller meta data.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <returns></returns>
	public IControllerMetaData CreateControllerMetaData(Type controllerType) => new ControllerMetaData(controllerType);
}