namespace Simplify.Web;

/// <summary>
/// Provides the view base class.
/// </summary>
public abstract class View : ModulesAccessor
{
	/// <summary>
	/// Gets the current language.
	/// </summary>
	public virtual string Language { get; internal set; } = null!;

	/// <summary>
	/// Gets the site root url, for example: http://mysite.com or http://localhost/mysite/.
	/// </summary>
	public string SiteUrl { get; internal set; } = null!;

	/// <summary>
	/// Gets the string table.
	/// </summary>
	public virtual dynamic StringTable { get; internal set; } = null!;
}