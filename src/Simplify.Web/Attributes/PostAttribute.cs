using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller HTTP POST request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PostAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PostAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public PostAttribute(string route)
		{
			Route = route;
		}

		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <value>
		/// The route.
		/// </value>
		public string Route { get; }
	}
}