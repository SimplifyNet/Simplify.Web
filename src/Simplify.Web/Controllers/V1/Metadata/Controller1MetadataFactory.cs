using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Factory;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V1.Metadata;

public class Controller1MetadataFactory : IControllerMetadataFactory
{
	public bool CanHandle(Type controllerType) => controllerType.IsDerivedFrom(Controller1Types.Types);

	public IControllerMetadata Create(Type controllerType) => new Controller1Metadata(controllerType);
}