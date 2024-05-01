using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Resolution;

public class ControllerResolutionState(IControllerMetadata initialController)
{
	public IControllerMetadata ControllerMetadata { get; } = initialController;

	public bool IsMatched { get; set; }

	public IReadOnlyDictionary<string, object>? RouteParameters { get; set; }

	public SecurityStatus SecurityStatus { get; set; }

	public IMatchedController ToMatchedController()
	{
		if (ControllerMetadata == null)
			throw new InvalidOperationException("ControllerMetadata is null");

		return new MatchedController(ControllerMetadata, RouteParameters);
	}
}