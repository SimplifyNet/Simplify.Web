using System.Collections.Generic;

namespace Simplify.Web;

/// <summary>
/// Provides the controllers base class version 2.
/// </summary>
/// <seealso cref="ResponseShortcutsControllerBase" />
public abstract class Controller2Base : ResponseShortcutsControllerBase
{
	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual IDictionary<string, string?> StringTable { get; internal set; } = null!;
}