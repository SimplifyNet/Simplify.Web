using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Environment;

namespace Simplify.Web;

/// <summary>
/// Provides the modules accessor base class.
/// </summary>
public abstract class ModulesAccessor : ViewAccessor
{
	/// <summary>
	/// Gets the various HTML generation classes container.
	/// </summary>
	public virtual IHtmlWrapper Html { get; internal set; } = null!;

	/// <summary>
	/// Gets the current request and framework environment data.
	/// </summary>
	public virtual IEnvironment Environment { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table manager.
	/// </summary>
	public virtual IStringTable StringTableManager { get; internal set; } = null!;

	/// <summary>
	/// Gets the text templates loader.
	/// </summary>
	public virtual ITemplateFactory TemplateFactory { get; internal set; } = null!;
}