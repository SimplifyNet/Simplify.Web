namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Various HTML generation classes container.
/// </summary>
public sealed class HtmlWrapper : IHtmlWrapper
{
	/// <summary>
	/// HTML ComboBox lists generator.
	/// </summary>
	public IListsGenerator ListsGenerator { get; internal set; } = null!;
}