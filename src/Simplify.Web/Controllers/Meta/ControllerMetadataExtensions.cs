using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.Controllers.Meta;

public static class ControllerMetadataRouteExtensions
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
			HandlerControllerType.ForbiddenHandler => list.GetStandardControllers().FirstOrDefault(x =>
				x.Role is { IsForbiddenHandler: true }),
			HandlerControllerType.NotFoundHandler => list.GetStandardControllers().FirstOrDefault(x =>
				x.Role is { IsNotFoundHandler: true }),
			_ => null
		};

	public static bool IsSpecialController(this IControllerMetadata x) =>
		x.Role is { IsForbiddenHandler: true } ||
		x.Role is { IsNotFoundHandler: true };

	public static bool ContainsRoute(this IControllerMetadata x) =>
		x.ExecParameters!.Routes.Any(x => !string.IsNullOrEmpty(x.Value));
}
