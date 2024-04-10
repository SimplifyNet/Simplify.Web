using System.Collections.Generic;
using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers;

public interface IControllersProcessingContext
{
	public IHttpContext Context { get; }

	public IReadOnlyList<IMatchedController> MatchedControllers { get; }
}