using System.Collections.Generic;

namespace Simplify.Web.Meta;

/// <summary>
/// Provides controller execution parameters
/// </summary>
public class ControllerExecParameters
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerExecParameters" /> class.
	/// </summary>
	/// <param name="routes">The routes.</param>
	/// <param name="execPriority">The execute priority.</param>
	public ControllerExecParameters(IDictionary<HttpMethod, string>? routes, int execPriority = 0)
	{
		Routes = routes ?? new Dictionary<HttpMethod, string>();
		RunPriority = execPriority;
	}

	/// <summary>
	/// Gets the controller handling routes.
	/// </summary>
	/// <value>
	/// The routes.
	/// </value>
	public IDictionary<HttpMethod, string> Routes { get; }

	/// <summary>
	/// Gets the run priority.
	/// </summary>
	/// <value>
	/// The run priority.
	/// </value>
	public int RunPriority { get; }
}