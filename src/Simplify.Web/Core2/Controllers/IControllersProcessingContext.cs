using System.Collections.Generic;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers;

public interface IControllersProcessingContext
{
	public IHttpContext Context { get; set; }

	public IReadOnlyList<IControllerMetaData> AllMatchedControllers { get; }
	public IReadOnlyList<IControllerMetaData> RouteSpecificControllers { get; }

	public IDictionary<string, object>? RouteParameters { get; }
}