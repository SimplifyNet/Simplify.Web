using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web;

/// <summary>
/// Provides the action modules accessor base class.
/// </summary>
public abstract class ActionModulesAccessor : ModulesAccessor
{
	/// <summary>
	/// Gets the current web context
	/// </summary>
	public virtual IWebContext Context { get; internal set; } = null!;

	/// <summary>
	/// Gets the data collector.
	/// </summary>
	public virtual IDataCollector DataCollector { get; internal set; } = null!;

	/// <summary>
	/// Gets the redirector.
	/// </summary>
	public virtual IRedirector Redirector { get; internal set; } = null!;

	/// <summary>
	/// Gets the language manager.
	/// </summary>
	public virtual ILanguageManager LanguageManager { get; internal set; } = null!;

	/// <summary>
	/// Gets the text files reader.
	/// </summary>
	public virtual IFileReader FileReader { get; internal set; } = null!;
}