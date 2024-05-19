using System;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Factory;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V2.Metadata;

public class Controller2MetadataFactory : IControllerMetadataFactory
{
	public bool CanHandle(Type controllerType) => controllerType.IsDerivedFrom(Controller2Types.Types);

	public IControllerMetadata Create(Type controllerType) => new Controller2Metadata(controllerType);
}