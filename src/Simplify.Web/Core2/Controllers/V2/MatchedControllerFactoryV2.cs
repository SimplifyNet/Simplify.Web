using System;
using Simplify.Web.Core2.Controllers.RouteMatching;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.V2;

public class MatchedControllerFactoryV2 : IMatchedControllerFactory
{
	public bool CanCreate(IControllerMetaData metaData)
	{
		throw new NotImplementedException();
	}

	public IMatchedController Create(IControllerMetaData metaData, IHttpContext context)
	{
		throw new NotImplementedException();
	}
}
