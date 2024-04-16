using System.Linq;
using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.RequestHandling;

public class RequestHandlingPipeline(IOrderedEnumerable<IRequestHandler> handlers) : IRequestHandlingPipeline
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