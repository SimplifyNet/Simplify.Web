using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

public interface IMatchedController
{
	public IControllerMetadata Controller { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}