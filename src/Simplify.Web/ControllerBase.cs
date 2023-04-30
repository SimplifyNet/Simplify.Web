#nullable disable

namespace Simplify.Web
{
	/// <summary>
	/// Controllers base class
	/// </summary>
	public abstract class ControllerBase : ActionModulesAccessor
	{
		/// <summary>
		/// Gets the route parameters.
		/// </summary>
		/// <value>
		/// The route parameters.
		/// </value>
		public virtual dynamic RouteParameters { get; internal set; }
	}
}