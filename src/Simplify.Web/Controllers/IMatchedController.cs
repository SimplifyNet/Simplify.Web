using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

/// <summary>
/// Represents a matcher controller.
/// </summary>
public interface IMatchedController
{
	/// <summary>
	/// Gets the controller.
	/// </summary>
	/// <value>
	/// The controller.
	/// </value>
	public IControllerMetadata Controller { get; }

	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public IReadOnlyDictionary<string, object>? RouteParameters { get; }
}