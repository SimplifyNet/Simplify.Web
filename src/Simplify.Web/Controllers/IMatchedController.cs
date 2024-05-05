using System.Collections.Generic;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers;

public interface IMatchedController
{
	public IControllerMetadata Controller { get; }

	public IReadOnlyDictionary<string, object>? RouteParameters { get; }
}