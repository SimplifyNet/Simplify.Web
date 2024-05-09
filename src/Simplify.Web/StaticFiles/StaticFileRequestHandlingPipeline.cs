using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.StaticFiles;

public class StaticFileRequestHandlingPipeline(IReadOnlyList<IStaticFileRequestHandler> handlers) : IStaticFileRequestHandlingPipeline
{
	public async Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response) =>
		await handlers
			.First(x => x.CanHandle(context))
			.Execute(context, response);

}