using System.Collections.Generic;
using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.ProcessingContext;

public class ControllersProcessingContext(
	IHttpContext context,
	IReadOnlyList<IMatchedController> matchedControllers) : IControllersProcessingContext
{
	public IHttpContext Context { get; } = context;

	public IReadOnlyList<IMatchedController> MatchedControllers { get; } = matchedControllers;
}
