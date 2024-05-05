using System.Collections.Generic;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Meta;

/// <summary>
/// Provides the controller execution parameters,
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerExecParameters" /> class.
/// </remarks>
/// <param name="routes">The routes.</param>
/// <param name="execPriority">The execute priority.</param>
public class ControllerExecParameters(IDictionary<HttpMethod, string>? routes, int execPriority = 0)
{
	/// <summary>
	/// Gets the controller handling routes.
	/// </summary>
	public IDictionary<HttpMethod, string> Routes { get; } = routes ?? new Dictionary<HttpMethod, string>();

	/// <summary>
	/// Gets the run priority.
	/// </summary>
	public int RunPriority { get; } = execPriority;
}