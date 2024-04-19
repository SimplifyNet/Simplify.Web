using System;

namespace Simplify.Web.Meta.Controllers.V1;

/// <summary>
/// Provides the controller v1 metadata information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerMetaData" /> class.
/// </remarks>
/// <param name="controllerType">Type of the controller.</param>
public class Controller1Metadata(Type controllerType) : ControllerMetadata(controllerType), IController1Metadata
{
}