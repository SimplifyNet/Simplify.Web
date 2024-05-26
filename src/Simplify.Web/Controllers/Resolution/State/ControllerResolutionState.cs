using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.State;

public class ControllerResolutionState(IControllerMetadata initialController) : IControllerResolutionState
{
	public IControllerMetadata Controller { get; } = initialController ?? throw new ArgumentNullException(nameof(initialController));

	public bool IsMatched { get; set; }

	public IReadOnlyDictionary<string, object>? RouteParameters { get; set; }

	public SecurityStatus SecurityStatus { get; set; }

	public IMatchedController ToMatchedController() => new MatchedController(Controller, RouteParameters);
}