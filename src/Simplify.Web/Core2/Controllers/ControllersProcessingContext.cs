using System.Collections.Generic;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers;

public class ControllersProcessingContext : IControllersProcessingContext
{
	public ControllersProcessingContext(
		IHttpContext context,
	 	IReadOnlyList<IControllerMetaData> allMatchedControllers,
		IReadOnlyList<IControllerMetaData> routeSpecificControllers,
		IDictionary<string, object>? routeParameters)
	{
		Context = context;
		AllMatchedControllers = allMatchedControllers;
		RouteSpecificControllers = routeSpecificControllers;
		RouteParameters = routeParameters;
	}

	public IHttpContext Context { get; }

	public IReadOnlyList<IControllerMetaData> AllMatchedControllers { get; }
	public IReadOnlyList<IControllerMetaData> RouteSpecificControllers { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}
