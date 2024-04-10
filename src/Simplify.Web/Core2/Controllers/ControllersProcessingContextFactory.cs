using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers;

public class ControllersProcessingContextFactory(IMatchedControllersFactory matchedControllersFactory) : IControllersProcessingContextFactory
{
	public IControllersProcessingContext Create(IHttpContext context) => new ControllersProcessingContext(context, matchedControllersFactory.Create(context));
}