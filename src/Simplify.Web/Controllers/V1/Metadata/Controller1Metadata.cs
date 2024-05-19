using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Metadata;

public class Controller1Metadata : ControllerMetadata, IController1Metadata
{
	public Controller1Metadata(Type controllerType) : base(controllerType)
	{
		ExecParameters = BuildControllerExecParameters(controllerType);
	}

	protected override IControllerRoute BuildControllerRoute(string path) => new Controller1Route(path);
}