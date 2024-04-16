using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Extensions;
using Simplify.Web.Controllers.RouteMatching.Extensions;
using Simplify.Web.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.RouteMatching;

public class MatchedControllersFactory(IControllersMetaStore metaStore, IMatchedControllerFactoryResolver factoryResolver) : IMatchedControllersFactory
{
	private readonly IEnumerable<IMatchedController> _globalMatchedControllers = metaStore.GlobalControllers.Select(x => x.ToMatchedController());

	public IReadOnlyList<IMatchedController> Create(IHttpContext context)
	{
		var result = new List<IMatchedController>();

		result.AddRange(_globalMatchedControllers);
		result.AddRange(CreateRoutedMatchedControllers(context));

		return result
			.SortByRunPriority()
			.ToList()
			.AsReadOnly();
	}

	private IEnumerable<IMatchedController> CreateRoutedMatchedControllers(IHttpContext context) =>
		metaStore.RoutedControllers.Select(x => factoryResolver.Resolve(x).Create(x, context));
}