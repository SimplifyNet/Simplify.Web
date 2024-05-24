using System.Collections.Generic;

namespace Simplify.Web;

/// <summary>
/// Provides the controllers base class version 2.
/// </summary>
public abstract class Controller2Base : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the string table.
	/// </summary>
	public virtual IDictionary<string, string?> StringTable { get; internal set; } = null!;
}