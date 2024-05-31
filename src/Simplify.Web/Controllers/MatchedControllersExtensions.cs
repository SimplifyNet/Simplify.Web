using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers;

/// <summary>
/// Provides the matcher controllers extensions.
/// </summary>
public static class MatchedControllersExtensions
{
	/// <summary>
	/// Sorts the matched controllers by run priority.
	/// </summary>
	/// <param name="items">The items.</param>
	public static IOrderedEnumerable<IMatchedController> SortByRunPriority(this IEnumerable<IMatchedController> items) =>
		items.OrderBy(x => x.Controller.ExecParameters?.RunPriority ?? 0);
}