using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileRequestHandlingPipeline
{
	Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response);
}