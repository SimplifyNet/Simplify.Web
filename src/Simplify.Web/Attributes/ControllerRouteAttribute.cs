using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ControllerRouteAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public ControllerRouteAttribute(string route) => Route = route;

		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <value>
		/// The route.
		/// </value>
		public string Route { get; }
	}
}