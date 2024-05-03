using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Response;

namespace Simplify.Web.Controllers.Execution;

public class ControllersExecutor(IControllerExecutorResolver executorResolver, IControllerResponseExecutor responseExecutor) : IControllersExecutor
{
	public async Task<ResponseBehavior> ExecuteAsync(IReadOnlyList<IMatchedController> controllers, HttpContext context)
	{
		foreach (var controller in controllers)
		{
			var response = await executorResolver.Resolve(controller.Controller).ExecuteAsync(controller, context);

			if (response == null)
				continue;

			var result = await responseExecutor.ExecuteAsync(response);

			if (result != ResponseBehavior.Default)
				return result;
		}

		return ResponseBehavior.Default;
	}
}