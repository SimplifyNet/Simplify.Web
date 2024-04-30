using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers.Extensions;

namespace Simplify.Web.Controllers.RouteMatching.Extensions;

public static class MatchedControllersExtensions
{
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> items) =>
		items.OrderBy(x => x.Controller.ExecParameters?.RunPriority ?? 0);

	public static bool IsHandledRoute(this IEnumerable<IMatchedController> items) =>
		items.Any(x => x.Controller.ContainsRoute() || x.Controller.Is404Controller());

	public static bool TryProcessUnhandledRoute(this IEnumerable<IMatchedController> controllers, HttpResponse response)
	{
		if (controllers.IsHandledRoute())
			return false;

		response.StatusCode = 404;

		return true;
	}
}