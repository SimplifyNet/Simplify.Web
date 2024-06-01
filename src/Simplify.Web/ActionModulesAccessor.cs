using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web;

/// <summary>
/// Provides the action modules accessor base class.
/// </summary>
/// <seealso cref="ModulesAccessor" />
public abstract class ActionModulesAccessor : ModulesAccessor
{
	/// <summary>
	/// Gets the current web context
	/// </summary>
	/// <value>
	/// The context.
	/// </value>
	public virtual IWebContext Context { get; internal set; } = null!;

	/// <summary>
	/// Gets the data collector.
	/// </summary>
	/// <value>
	/// The data collector.
	/// </value>
	public virtual IDataCollector DataCollector { get; internal set; } = null!;

	/// <summary>
	/// Gets the redirector.
	/// </summary>
	/// <value>
	/// The redirector.
	/// </value>
	public virtual IRedirector Redirector { get; internal set; } = null!;

	/// <summary>
	/// Gets the language manager.
	/// </summary>
	/// <value>
	/// The language manager.
	/// </value>
	public virtual ILanguageManager LanguageManager { get; internal set; } = null!;

	/// <summary>
	/// Gets the text files reader.
	/// </summary>
	/// <value>
	/// The file reader.
	/// </value>
	public virtual IFileReader FileReader { get; internal set; } = null!;
}