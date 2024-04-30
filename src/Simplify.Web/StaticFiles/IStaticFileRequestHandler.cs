using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles;

namespace Simplify.Web.Controllers.Processing;

public interface IStaticFileRequestHandler
{
	bool CanHandle(IStaticFileProcessingContext context);

	public Task Execute(IStaticFileProcessingContext context, HttpResponse response);
}