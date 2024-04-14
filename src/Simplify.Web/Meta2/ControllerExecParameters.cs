using System.Collections.Generic;
using Simplify.Web.Http;

namespace Simplify.Web.Meta2;

/// <summary>
/// Provides controller execution parameters,
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
	/// <value>
	/// The routes.
	/// </value>
	public IDictionary<HttpMethod, string> Routes { get; } = routes ?? new Dictionary<HttpMethod, string>();

	/// <summary>
	/// Gets the run priority.
	/// </summary>
	/// <value>
	/// The run priority.
	/// </value>
	public int RunPriority { get; } = execPriority;
}