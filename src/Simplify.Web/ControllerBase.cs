namespace Simplify.Web;

/// <summary>
/// Controllers base class.
/// </summary>
public abstract class ControllerBase : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	public virtual dynamic RouteParameters { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual dynamic StringTable { get; internal set; } = null!;
}