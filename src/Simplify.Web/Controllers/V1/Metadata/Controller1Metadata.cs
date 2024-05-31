using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V1.Routing;

namespace Simplify.Web.Controllers.V1.Metadata;

/// <summary>
/// Provides the controller v1 metadata.
/// </summary>
/// <seealso cref="ControllerMetadata" />
/// <seealso cref="IController1Metadata" />
public class Controller1Metadata : ControllerMetadata, IController1Metadata
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Controller1Metadata"/> class.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ControllerMetadata" /> class.
	/// </remarks>
	/// <seealso cref="IControllerMetadata" />
	public Controller1Metadata(Type controllerType) : base(controllerType) =>
		ExecParameters = BuildControllerExecParameters(controllerType);

	/// <summary>
	/// Builds the controller route.
	/// </summary>
	/// <param name="path">The path.</param>
	protected override IControllerRoute BuildControllerRoute(string path) => new Controller1Route(path);
}