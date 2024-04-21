using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Http;
using Simplify.Web.Meta.Controllers.Extensions;

namespace Simplify.Web.Controllers.RouteMatching.Extensions;

public static class MatchedControllersExtensions
{
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> items) =>
		items.OrderBy(x => x.MetaData.ExecParameters?.RunPriority ?? 0);

	public static bool IsHandledRoute(this IEnumerable<IMatchedController> items) =>
		items.Any(x => x.MetaData.ContainsRoute() || x.MetaData.Is404Controller());

	public static bool TryProcessUnhandledRoute(this IEnumerable<IMatchedController> controllers, IHttpContext context)
	{
		if (controllers.IsHandledRoute())
			return false;

		context.SetResponseStatusCode(404);

		return true;
	}
}