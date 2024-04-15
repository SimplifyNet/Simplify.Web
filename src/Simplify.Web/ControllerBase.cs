namespace Simplify.Web;

/// <summary>
/// Provides the controllers base class.
/// </summary>
public abstract class ControllerBase : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	public virtual dynamic RouteParameters { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	public virtual dynamic StringTable { get; internal set; } = null!;
}