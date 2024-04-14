using System.Collections.Generic;
using Simplify.Web.Http;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching;

public interface IMatchedControllersFactory
{
	IReadOnlyList<IMatchedController> Create(IHttpContext context);
}