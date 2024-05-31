using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.State;

/// <summary>
/// Provides the controller resolution state.
/// </summary>
/// <seealso cref="IControllerResolutionState" />
public class ControllerResolutionState(IControllerMetadata initialController) : IControllerResolutionState
{
	/// <summary>
	/// Gets the controller.
	/// </summary>
	/// <value>
	/// The controller.
	/// </value>
	public IControllerMetadata Controller { get; } = initialController ?? throw new ArgumentNullException(nameof(initialController));

	/// <summary>
	/// Gets or sets a value indicating whether the controller route is matched.
	/// </summary>
	/// <value>
	///   <c>true</c> if the controller route is matched; otherwise, <c>false</c>.
	/// </value>
	public bool IsMatched { get; set; }

	/// <summary>
	/// Gets or sets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public IReadOnlyDictionary<string, object>? RouteParameters { get; set; }

	/// <summary>
	/// Gets or sets the security status.
	/// </summary>
	/// <value>
	/// The security status.
	/// </value>
	public SecurityStatus SecurityStatus { get; set; }

	/// <summary>
	/// Converts to matched controller.
	/// </summary>
	public IMatchedController ToMatchedController() => new MatchedController(Controller, RouteParameters);
}