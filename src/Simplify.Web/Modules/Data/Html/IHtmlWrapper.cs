namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Various HTML generation classes container.
/// </summary>
public interface IHtmlWrapper
{
	/// <summary>
	/// HTML ComboBox lists generator.
	/// </summary>
	IListsGenerator ListsGenerator { get; }
}