using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Processing;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;

namespace Simplify.Web.StaticFiles.Stages;

public class CachedFileHandler : IStaticFileRequestHandler
{
	public bool CanHandle(IStaticFileProcessingContext context) => context.IsCached;

	public Task Execute(IStaticFileProcessingContext context, HttpResponse response)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);
		response.StatusCode = (int)HttpStatusCode.NotModified;

		return Task.CompletedTask;
	}
}