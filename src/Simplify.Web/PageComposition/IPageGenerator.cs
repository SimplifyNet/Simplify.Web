namespace Simplify.Web.PageComposition;

/// <summary>
/// Represent web-page generator.
/// </summary>
public interface IPageGenerator
{
	/// <summary>
	/// Generates the web page HTML code.
	/// </summary>
	string Generate();
}