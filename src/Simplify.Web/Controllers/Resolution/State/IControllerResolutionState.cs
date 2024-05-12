using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.State;

public interface IControllerResolutionState
{
	IControllerMetadata Controller { get; }

	bool IsMatched { get; set; }

	IReadOnlyDictionary<string, object>? RouteParameters { get; set; }

	SecurityStatus SecurityStatus { get; set; }

	IMatchedController ToMatchedController();
}