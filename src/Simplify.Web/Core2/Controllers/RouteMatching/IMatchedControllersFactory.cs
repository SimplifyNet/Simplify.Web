using System.Collections.Generic;

namespace Simplify.Web.Core2.Controllers.RouteMatching;

public interface IMatchedControllersFactory
{
	IReadOnlyList<IMatchedController> Create(IHttpContext context);
}
