namespace Simplify.Web.Modules.Data.Html;

/// <summary>
/// Various HTML generation classes container
/// </summary>
public interface IHtmlWrapper : IHideObjectMembers
{
	/// <summary>
	/// HTML ComboBox lists generator.
	/// </summary>
	IListsGenerator ListsGenerator { get; }
}