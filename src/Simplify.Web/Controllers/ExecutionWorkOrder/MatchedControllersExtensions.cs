using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public static class MatchedControllersExtensions
{
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> items) =>
		items.OrderBy(x => x.Controller.ExecParameters?.RunPriority ?? 0);
}