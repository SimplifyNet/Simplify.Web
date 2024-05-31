using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Resolution.State;

/// <summary>
/// Represents a controller resolution state.
/// </summary>
public interface IControllerResolutionState
{
	/// <summary>
	/// Gets the controller.
	/// </summary>
	/// <value>
	/// The controller.
	/// </value>
	IControllerMetadata Controller { get; }

	/// <summary>
	/// Gets or sets a value indicating whether the controller route is matched.
	/// </summary>
	/// <value>
	///   <c>true</c> if the controller route is matched; otherwise, <c>false</c>.
	/// </value>
	bool IsMatched { get; set; }

	/// <summary>
	/// Gets or sets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	IReadOnlyDictionary<string, object>? RouteParameters { get; set; }

	/// <summary>
	/// Gets or sets the security status.
	/// </summary>
	/// <value>
	/// The security status.
	/// </value>
	SecurityStatus SecurityStatus { get; set; }

	/// <summary>
	/// Converts to matched controller.
	/// </summary>
	IMatchedController ToMatchedController();
}