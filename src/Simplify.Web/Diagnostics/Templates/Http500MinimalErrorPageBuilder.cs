﻿using Simplify.Templates;

namespace Simplify.Web.Diagnostics.Templates;

/// <summary>
/// Provides the HTTP 500 minimal error page builder.
/// </summary>
public static class Http500MinimalErrorPageBuilder
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