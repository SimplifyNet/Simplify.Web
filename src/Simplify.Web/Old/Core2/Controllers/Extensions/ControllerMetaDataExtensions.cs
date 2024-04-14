using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IMatchedController ToMatchedController(this IControllerMetaData metaData) =>
		new MatchedController(metaData);
}