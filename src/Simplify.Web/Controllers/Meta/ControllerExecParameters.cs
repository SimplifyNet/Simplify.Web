using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller execution parameters.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerExecParameters" /> class.
/// </remarks>
/// <param name="routes">The routes.</param>
/// <param name="execPriority">The execute priority.</param>
public class ControllerExecParameters(IDictionary<HttpMethod, IControllerRoute>? routes, int execPriority = 0)
{
	/// <summary>
	/// Gets the controller handling routes.
	/// </summary>
	/// <value>
	/// The routes.
	/// </value>
	public IDictionary<HttpMethod, IControllerRoute> Routes { get; } = routes ?? new Dictionary<HttpMethod, IControllerRoute>();

	/// <summary>
	/// Gets the run priority.
	/// </summary>
	public int RunPriority { get; } = execPriority;
}