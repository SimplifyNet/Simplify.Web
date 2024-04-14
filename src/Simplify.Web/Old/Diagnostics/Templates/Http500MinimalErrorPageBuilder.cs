using Simplify.Templates;

namespace Simplify.Web.Old.Diagnostics.Templates;

/// <summary>
/// Provides HTTP 500 minimal error page builder.
/// </summary>
public class Http500MinimalErrorPageBuilder
{
	/// <summary>
	/// Builds the error page.
	/// </summary>
	/// <param name="exceptionText">The exception text.</param>
	/// <returns></returns>
	public static string Build(string? exceptionText = null) =>
		TemplateBuilder.FromCurrentAssembly("Diagnostics.Templates.Http500MinimalErrorPage.html")
			.Build()
			.Set("ExceptionText", exceptionText)
			.Get();
}