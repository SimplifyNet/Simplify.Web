namespace Simplify.Web;

/// <summary>
/// Provides the controllers base class.
/// </summary>
/// <seealso cref="ResponseShortcutsControllerBase" />
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