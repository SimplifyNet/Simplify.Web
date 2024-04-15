using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching.Extensions;

public static class MatchedControllersExtensions
{
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> controllersMetaContainers) =>
		controllersMetaContainers.OrderBy(x => x.MetaData.ExecParameters?.RunPriority ?? 0);
}