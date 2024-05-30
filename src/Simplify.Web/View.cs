namespace Simplify.Web;

/// <summary>
/// Provides the view base class.
/// </summary>
/// <seealso cref="ModulesAccessor" />
public abstract class View : ModulesAccessor
{
	/// <summary>
	/// Gets the current language.
	/// </summary>
	/// <value>
	/// The language.
	/// </value>
	public virtual string Language { get; internal set; } = null!;

	/// <summary>
	/// Gets the site root URL, for example: http://mysite.com or http://localhost/mysite/.
	/// </summary>
	/// <value>
	/// The site URL.
	/// </value>
	public string SiteUrl { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	/// <value>
	/// The string table.
	/// </value>
	public virtual dynamic StringTable { get; internal set; } = null!;
}