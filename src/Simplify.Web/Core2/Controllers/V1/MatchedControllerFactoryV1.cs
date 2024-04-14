using System;
using Simplify.Web.Core2.Controllers.RouteMatching;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.V1;

public class MatchedControllerFactoryV1 : IMatchedControllerFactory
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
