using System;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.V1;

public class MatchedControllerFactoryV1 : IMatchedControllerFactory
{
	public bool CanCreate(IControllerMetadata metaData)
	{
		throw new NotImplementedException();
	}

	public IMatchedController Create(IControllerMetadata metaData, IHttpContext context)
	{
		throw new NotImplementedException();
	}
}