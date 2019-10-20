using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller HTTP PATCH request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PatchAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PutAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public PatchAttribute(string route)
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