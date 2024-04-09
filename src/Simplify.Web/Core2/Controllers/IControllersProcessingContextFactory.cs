using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers;

public interface IControllersProcessingContextFactory
{
	IControllersProcessingContext Create(IHttpContext context);
}