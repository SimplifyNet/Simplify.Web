using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller HTTP DELETE request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DeleteAttribute : ControllerRouteAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public DeleteAttribute(string route) : base(route)
		{
		}
	}
}