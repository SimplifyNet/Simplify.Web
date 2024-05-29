using System.Collections.Generic;

namespace Simplify.Web.Settings;

/// <summary>
/// Represent a Simplify.Web settings.
/// </summary>
public interface ISimplifyWebSettings
{
	/// <summary>
	/// Gets the default language, for example: "en", "ru", "de" etc., default value is "en".
	/// </summary>
	string DefaultLanguage { get; }

	/// <summary>
	/// Gets the value indicating whether cookie language should be accepted.
	/// </summary>
	bool AcceptCookieLanguage { get; }

	/// <summary>
	/// Gets the value indicating whether HTTP header language should be accepted.
	/// </summary>
	bool AcceptHeaderLanguage { get; }

	/// <summary>
	/// Gets the default templates directory path, for example: Templates, default value is "Templates".
	/// </summary>
	string DefaultTemplatesPath { get; }

	/// <summary>
	/// Gets the value indicating whether all templates should be loaded from assembly.
	/// </summary>
	bool LoadTemplatesFromAssembly { get; }

	/// <summary>
	/// Gets the master page template file name.
	/// </summary>
	string DefaultMasterTemplateFileName { get; }

	/// <summary>
	/// Gets the master template main content variable name.
	/// </summary>
	string DefaultMainContentVariableName { get; }

	/// <summary>
	/// Gets the master template title variable name.
	/// </summary>
	string DefaultTitleVariableName { get; }

	/// <summary>
	/// Gets the default site style
	/// </summary>
	string DefaultStyle { get; }

	/// <summary>
	/// Gets the data directory path, for example: default value is "App_Data".
	/// </summary>
	string DataPath { get; }

	/// <summary>
	/// Gets the value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.
	/// </summary>
	bool StaticFilesEnabled { get; }

	/// <summary>
	/// Gets the static files paths.
	/// </summary>
	IReadOnlyList<string> StaticFilesPaths { get; }

	/// <summary>
	/// Gets the string table files.
	/// </summary>
	IReadOnlyList<string> StringTableFiles { get; }

	/// <summary>
	/// Gets the value indicating whether site title postfix should be set automatically.
	/// </summary>
	bool DisableAutomaticSiteTitleSet { get; }

	/// <summary>
	/// Gets the value indicating whether exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred.
	/// </summary>
	bool HideExceptionDetails { get; }

	/// <summary>
	/// Gets the value indicating whether Simplify.Web HTTP 500 error page should be displayed in dark style.
	/// </summary>
	bool ErrorPageDarkStyle { get; }

	/// <summary>
	/// Gets the value indicating whether templates memory cache enabled or disabled.
	/// </summary>
	bool TemplatesMemoryCache { get; }

	/// <summary>
	/// Gets the value indicating whether string table memory cache enabled or disabled.
	/// </summary>
	bool StringTableMemoryCache { get; }

	/// <summary>
	/// Gets the value indicating whether file reader caching should be disable.
	/// </summary>
	bool DisableFileReaderCache { get; }

	/// <summary>
	/// Gets the value indicating whether measurements is enabled.
	/// </summary>
	bool MeasurementsEnabled { get; }

	/// <summary>
	/// Gets the value indicating whether console tracing is enabled.
	/// </summary>
	bool ConsoleTracing { get; }
}