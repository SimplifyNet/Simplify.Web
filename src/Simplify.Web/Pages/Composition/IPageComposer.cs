namespace Simplify.Web.Pages.Composition;

/// <summary>
/// Represents a web-page composer.
/// </summary>
public interface IPageComposer
{
	/// <summary>
	/// Composes the current web-page.
	/// </summary>
	string Compose();
}