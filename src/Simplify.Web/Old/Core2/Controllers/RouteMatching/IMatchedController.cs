using System.Collections.Generic;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching;

public interface IMatchedController
{
	public IControllerMetaData MetaData { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}