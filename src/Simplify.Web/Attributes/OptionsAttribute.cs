using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Set controller HTTP OPTIONS request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class OptionsAttribute : ControllerRouteAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionsAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public OptionsAttribute(string route) : base(route)
		{
		}
	}
}