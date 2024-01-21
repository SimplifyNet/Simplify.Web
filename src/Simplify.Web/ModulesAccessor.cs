using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;

namespace Simplify.Web;

/// <summary>
/// Provides modules accessor base class.
/// </summary>
public abstract class ModulesAccessor : ViewAccessor
{
	/// <summary>
	/// Various HTML generation classes container.
	/// </summary>
	/// <value>
	/// The various HTML generation classes container.
	/// </value>
	public virtual IHtmlWrapper Html { get; internal set; } = null!;

	/// <summary>
	/// Current request environment data.
	/// </summary>
	public virtual IEnvironment Environment { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual dynamic StringTable { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table manager.
	/// </summary>
	/// <value>
	/// The string table manager.
	/// </value>
	public virtual IStringTable StringTableManager { get; internal set; } = null!;

	/// <summary>
	/// Text templates loader.
	/// </summary>
	public virtual ITemplateFactory TemplateFactory { get; internal set; } = null!;
}