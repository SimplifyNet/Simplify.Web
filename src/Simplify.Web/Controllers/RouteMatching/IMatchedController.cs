using System.Collections.Generic;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedController
{
	public IControllerMetadata MetaData { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}