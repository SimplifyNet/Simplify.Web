using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers;

public class ControllersProcessingContextFactory : IControllersProcessingContextFactory
{
	public IControllersProcessingContext Create(IHttpContext context)
	{
		return new ControllersProcessingContext(context, null!, null!, null);
	}
}