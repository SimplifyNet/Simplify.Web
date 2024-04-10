using System.Collections.Generic;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.Routing;

public interface IMatchedControllersFactory
{
	IReadOnlyList<IMatchedController> Create(IHttpContext context);
}
