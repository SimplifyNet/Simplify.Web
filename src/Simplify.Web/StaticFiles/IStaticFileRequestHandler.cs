using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileRequestHandler
{
	bool CanHandle(IStaticFileProcessingContext context);

	public Task Execute(IStaticFileProcessingContext context, HttpResponse response);
}