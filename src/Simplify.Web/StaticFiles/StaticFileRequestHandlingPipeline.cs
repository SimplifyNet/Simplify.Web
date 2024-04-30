using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Processing;

namespace Simplify.Web.StaticFiles;

public class StaticFileRequestHandlingPipeline(IReadOnlyList<IStaticFileRequestHandler> handlers) : IStaticFileRequestHandlingPipeline
{
	public async Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response)
	{
		foreach (var item in handlers)
		{
			if (!item.CanHandle(context))
				continue;

			await item.Execute(context, response);

			return;
		}
	}
}