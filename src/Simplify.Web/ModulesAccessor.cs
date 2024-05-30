using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;

namespace Simplify.Web;

/// <summary>
/// Provides the modules accessor base class.
/// </summary>
/// <seealso cref="ViewAccessor" />
public abstract class ModulesAccessor : ViewAccessor
{
	/// <summary>
	/// Gets the various HTML generation classes container.
	/// </summary>
	/// <value>
	/// The HTML generation classes.
	/// </value>
	public virtual IHtmlWrapper Html { get; internal set; } = null!;

	/// <summary>
	/// Gets the current request and framework environment data.
	/// </summary>
	/// <value>
	/// The environment.
	/// </value>
	public virtual IEnvironment Environment { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table manager.
	/// </summary>
	/// <value>
	/// The string table manager.
	/// </value>
	public virtual IStringTable StringTableManager { get; internal set; } = null!;

	/// <summary>
	/// Gets the text templates loader.
	/// </summary>
	/// <value>
	/// The template factory.
	/// </value>
	public virtual ITemplateFactory TemplateFactory { get; internal set; } = null!;
}