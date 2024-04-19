using System.Linq;

namespace Simplify.Web.Meta.Controllers.Extensions;

public static class ControllerMetadataRouteExtensions
{
	public static bool ContainsRoute(this IControllerMetadata x) =>
		x.ExecParameters!.Routes.Any(x => !string.IsNullOrEmpty(x.Value));
}
