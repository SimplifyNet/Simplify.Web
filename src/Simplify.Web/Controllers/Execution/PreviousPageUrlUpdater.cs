using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Controllers.Execution;

/// <summary>
/// Provides the previous page URL updater
/// </summary>
/// <seealso cref="IControllersExecutor" />
public class PreviousPageUrlUpdater(IControllersExecutor baseExecutor, IRedirector redirector) : IControllersExecutor
{
	/// <summary>
	/// Executes the controllers asynchronously.
	/// </summary>
	/// <param name="controllers">The controllers.</param>
	public async Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> controllers)
	{
		var result = await baseExecutor.ExecuteAsync(controllers);

		if (result == ResponseBehavior.Default)
			redirector.SetPreviousPageUrlToCurrentPage();

		return result;
	}
}