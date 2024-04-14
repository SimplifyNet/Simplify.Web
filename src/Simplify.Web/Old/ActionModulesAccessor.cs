using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;

namespace Simplify.Web.Old;

/// <summary>
/// Action modules accessor base class.
/// </summary>
public class ActionModulesAccessor : ModulesAccessor
{
	/// <summary>
	/// Current web context
	/// </summary>
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
	/// Text files reader.
	/// </summary>
	public virtual IFileReader FileReader { get; internal set; } = null!;
}