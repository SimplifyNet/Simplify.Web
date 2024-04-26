using Simplify.Web.Http;

namespace Simplify.Web.StaticFiles.Context;

public interface IStaticFileProcessingContextFactory
{
	IStaticFileProcessingContext Create(IHttpContext context);
}