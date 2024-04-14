using Simplify.Web.Core2.Controllers.RouteMatching;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IMatchedController ToMatchedController(this IControllerMetaData metaData) =>
		new MatchedController(metaData);
}