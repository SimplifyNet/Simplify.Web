using System;

namespace Simplify.Web.Meta2;

/// <summary>
/// Controller v1 metadata information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerMetaData" /> class.
/// </remarks>
/// <param name="controllerType">Type of the controller.</param>
public class Controller1MetaData(Type controllerType) : ControllerMetaData(controllerType), IController1MetaData
{
}