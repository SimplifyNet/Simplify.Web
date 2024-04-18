using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IMatchedController ToMatchedController(this IControllerMetadata metaData) =>
		new MatchedController(metaData);
}