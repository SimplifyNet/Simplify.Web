using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Meta.Controllers.Extensions;

namespace Simplify.Web.Meta.Controllers.MetaStore.Extensions;

public static class ControllerMetadataFilterExtensions
{
	public static IEnumerable<IControllerMetadata> GetStandardControllers(this IEnumerable<IControllerMetadata> list) => list
		.Where(x => !x.IsSpecialController());

	public static IEnumerable<IControllerMetadata> GetRoutedControllers(this IEnumerable<IControllerMetadata> list) => list
		.GetStandardControllers()
		.Where(x => x.ContainsRoute());

	public static IEnumerable<IControllerMetadata> GetGlobalControllers(this IEnumerable<IControllerMetadata> list) => list
		.GetStandardControllers()
		.Where(x => !x.ContainsRoute());

	public static IControllerMetadata? GetHandlerController(this IEnumerable<IControllerMetadata> list, HandlerControllerType controllerType) =>
		controllerType switch
		{
			HandlerControllerType.Http403Handler => list.GetStandardControllers().FirstOrDefault(x =>
				x.Role is { Is403Handler: true }),
			HandlerControllerType.Http404Handler => list.GetStandardControllers().FirstOrDefault(x =>
				x.Role is { Is404Handler: true }),
			_ => null
		};
}
