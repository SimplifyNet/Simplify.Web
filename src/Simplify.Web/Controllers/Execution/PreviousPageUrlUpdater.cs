using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Controllers.Execution;

public class PreviousPageUrlUpdater(IControllersExecutor baseExecutor, IRedirector redirector) : IControllersExecutor
{
	public async Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> controllers, HttpContext context)
	{
		var result = await baseExecutor.ExecuteAsync(controllers, context);

		if (result == ResponseBehavior.Default)
			redirector.SetPreviousPageUrlToCurrentPage();

		return result;
	}
}