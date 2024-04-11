using System.Collections.Generic;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Routing;

public class MatchedControllersFactory(IControllersMetaStore metaStore, IMatchedControllerFactoryResolver factoryResolver) : IMatchedControllersFactory
{
	public IReadOnlyList<IMatchedController> Create(IHttpContext context)
	{
		var result = new List<IMatchedController>();

		foreach (var item in metaStore.RoutedControllers)
			result.Add(factoryResolver.Resolve(item).Create(item, context));

		return result.AsReadOnly();
	}
}
