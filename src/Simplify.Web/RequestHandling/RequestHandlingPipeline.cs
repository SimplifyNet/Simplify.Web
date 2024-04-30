using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling;

public class RequestHandlingPipeline(IReadOnlyList<IRequestHandler> handlers) : IRequestHandlingPipeline
{
	public async Task ExecuteAsync(HttpContext context)
	{
		var stopProcessing = false;

		foreach (var item in handlers)
		{
			await item.HandleAsync(context, StopProcessing);

			if (stopProcessing)
				break;
		}

		return;

		void StopProcessing() => stopProcessing = true;
	}
}