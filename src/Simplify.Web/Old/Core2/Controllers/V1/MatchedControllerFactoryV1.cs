using System;
using Simplify.Web.Http;
using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.V1;

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