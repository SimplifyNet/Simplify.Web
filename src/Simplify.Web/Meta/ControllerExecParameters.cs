#nullable disable

namespace Simplify.Web.Meta;

/// <summary>
/// Provides controller execution parameters
/// </summary>
public class ControllerExecParameters
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerExecParameters" /> class.
	/// </summary>
	/// <param name="routeInfo">The route information.</param>
	/// <param name="execPriority">The execute priority.</param>
	public ControllerExecParameters(ControllerRouteInfo routeInfo = null, int execPriority = 0)
	{
		RouteInfo = routeInfo;
		RunPriority = execPriority;
	}

	/// <summary>
	/// Gets the route information.
	/// </summary>
	/// <value>
	/// The route information.
	/// </value>
	public ControllerRouteInfo RouteInfo { get; private set; }

	/// <summary>
	/// Gets the run priority.
	/// </summary>
	/// <value>
	/// The run priority.
	/// </value>
	public int RunPriority { get; private set; }
}