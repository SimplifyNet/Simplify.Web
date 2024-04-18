using System;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.V2;

public class MatchedControllerFactoryV2 : IMatchedControllerFactory
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