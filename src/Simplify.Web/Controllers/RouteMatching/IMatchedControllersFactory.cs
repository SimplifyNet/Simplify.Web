using System.Collections.Generic;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllersFactory
{
	IReadOnlyList<IMatchedController> Create(IHttpContext context);
}