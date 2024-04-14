namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Represents a various HTML generation classes container.
/// </summary>
public interface IHtmlWrapper
{
	/// <summary>
	/// Gets the HTML ComboBox lists generator.
	/// </summary>
	IListsGenerator ListsGenerator { get; }
}