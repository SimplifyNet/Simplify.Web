using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.RouteMatching.Extensions;

public static class MatchedControllersExtensions
{
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> controllersMetaContainers) =>
		controllersMetaContainers.OrderBy(x => x.MetaData.ExecParameters?.RunPriority ?? 0);
}