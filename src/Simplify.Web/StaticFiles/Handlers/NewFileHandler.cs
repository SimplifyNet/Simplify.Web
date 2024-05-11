using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.StaticFiles.Handlers;

public class NewFileHandler(IResponseWriter responseWriter, IStaticFile fileHandler) : IStaticFileRequestHandler
{
	public bool CanHandle(IStaticFileProcessingContext context) => !context.CanBeCached;

	public async Task Execute(IStaticFileProcessingContext context, HttpResponse response)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);

		await responseWriter.WriteAsync(response, await fileHandler.GetDataAsync(context.RelativeFilePath));
	}
}