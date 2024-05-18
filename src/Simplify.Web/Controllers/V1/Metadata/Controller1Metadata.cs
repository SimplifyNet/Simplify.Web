using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V1.Routing;

namespace Simplify.Web.Controllers.V1.Metadata;

public class Controller1Metadata(Type controllerType) : ControllerMetadata(controllerType), IController1Metadata
{
	protected override IControllerRoute ParseControllerRoute(string path) => new Controller1Route(path);
}