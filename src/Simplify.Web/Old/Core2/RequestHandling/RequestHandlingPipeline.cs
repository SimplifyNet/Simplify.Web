using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.Old.Core2.RequestHandling;

public class RequestHandlingPipeline(IList<IRequestHandler> handlers) : IRequestHandlingPipeline
{
	public async Task ExecuteAsync(IHttpContext context)
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