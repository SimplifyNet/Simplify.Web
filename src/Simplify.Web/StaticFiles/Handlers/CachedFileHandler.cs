using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles.Handlers;

public class CachedFileHandler : IStaticFileRequestHandler
{
	public bool CanHandle(IStaticFileProcessingContext context) => context.CanBeCached;

	public Task Execute(IStaticFileProcessingContext context, HttpResponse response)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);
		response.StatusCode = (int)HttpStatusCode.NotModified;

		return Task.CompletedTask;
	}
}