using System;
using Simplify.Web.Meta.Controllers;
using Simplify.Web.Meta.Controllers.Factory;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V1.Metadata;

public class Controller1MetadataFactory : IControllerMetadataFactory
{
	public bool CanHandle(Type controllerType) => controllerType.IsDerivedFrom(Controller1Types.Types);

	public IControllerMetadata Create(Type controllerType)
	{
		throw new NotImplementedException();
	}
}