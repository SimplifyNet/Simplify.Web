using System;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.V1.Metadata;

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