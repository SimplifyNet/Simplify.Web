using System.Collections.Generic;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.RouteMatching;

public interface IMatchedController
{
	public IControllerMetaData MetaData { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}