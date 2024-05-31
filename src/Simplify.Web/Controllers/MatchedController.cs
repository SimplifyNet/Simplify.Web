using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

/// <summary>
/// Provides the matcher controller.
/// </summary>
/// <seealso cref="IMatchedController" />
public class MatchedController(IControllerMetadata metaData, IReadOnlyDictionary<string, object>? routeParameters = null) : IMatchedController
{
	/// <summary>
	/// Gets the controller.
	/// </summary>
	/// <value>
	/// The controller.
	/// </value>
	public IControllerMetadata Controller { get; } = metaData;

	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public IReadOnlyDictionary<string, object>? RouteParameters { get; } = routeParameters;
}