namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Provides the various HTML generation classes container.
/// </summary>
public sealed class HtmlWrapper : IHtmlWrapper
{
	/// <summary>
	/// The HTML ComboBox lists generator.
	/// </summary>
	public IListsGenerator ListsGenerator { get; internal set; } = null!;
}