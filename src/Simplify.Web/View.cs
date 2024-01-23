namespace Simplify.Web;

/// <summary>
/// View base class.
/// </summary>
public abstract class View : ModulesAccessor
{
	/// <summary>
	/// Gets the current language.
	/// </summary>
	/// <value>
	/// The current language.
	/// </value>
	public virtual string Language { get; internal set; } = null!;

	/// <summary>
	/// Site root url, for example: http://mysite.com or http://localhost/mysite/.
	/// </summary>
	public string SiteUrl { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual dynamic StringTable { get; internal set; } = null!;
}