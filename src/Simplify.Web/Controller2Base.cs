using System.Collections.Generic;

namespace Simplify.Web;

/// <summary>
/// Controllers base class version 2.
/// </summary>
public abstract class Controller2Base : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual IDictionary<string, object> StringTable { get; internal set; } = null!;
}