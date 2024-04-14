using System.Reflection;
using Simplify.System;
using Simplify.Templates;

namespace Simplify.Web.Diagnostics.Templates;

/// <summary>
/// Provides HTTP 500 error page builder.
/// </summary>
public static class Http500ErrorPageBuilder
{
	/// <summary>
	/// Builds the error page.
	/// </summary>
	/// <param name="exceptionText">The exception text.</param>
	/// <param name="darkStyle"><c>true</c> if page style should be in dark colors.</param>
	public static string Build(string? exceptionText = null, bool darkStyle = false) =>
		TemplateBuilder.FromCurrentAssembly("Diagnostics.Templates.Http500ErrorPage.html")
			.Build()
			.SetStyle(darkStyle)
			.SetFrameworkVersion()
			.SetExceptionInfo(exceptionText)
			.Get();

	private static ITemplate SetExceptionInfo(this ITemplate tpl, string? exceptionText) =>
		tpl.Set("ExceptionDetails", exceptionText == null
			? ""
			: TemplateBuilder.FromCurrentAssembly("Diagnostics.Templates.Http500ErrorPageExceptionInfo.html")
				.Build()
				.Set("ExceptionText", exceptionText)
				.Get());

	private static ITemplate SetStyle(this ITemplate tpl, bool darkStyle) =>
		tpl.Set("Style",
			$"<style>{TemplateBuilder.FromCurrentAssembly("Diagnostics.Templates.Styles." + (darkStyle ? "Dark" : "Light") + ".css").Build().Get()}</style>");

	private static ITemplate SetFrameworkVersion(this ITemplate tpl) =>
		tpl.Set("Simplify.Web.Version", new AssemblyInfo(Assembly.GetCallingAssembly()).Version);
}