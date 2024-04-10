using System.Collections.Generic;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Routing;

public interface IMatchedController
{
	public IControllerMetaData MetaData { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}