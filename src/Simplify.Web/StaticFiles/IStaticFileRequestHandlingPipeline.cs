using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileRequestHandlingPipeline
{
	Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response);
}
