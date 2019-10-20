using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller HTTP DELETE request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DeleteAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public DeleteAttribute(string route)
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