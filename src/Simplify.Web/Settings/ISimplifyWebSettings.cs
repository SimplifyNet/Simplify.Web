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
	/// <value>
	/// The default language.
	/// </value>
	string DefaultLanguage { get; }

	/// <summary>
	/// Gets the value indicating whether cookie language should be accepted.
	/// </summary>
	/// <value>
	///   <c>true</c> if cookie language should be accepted; otherwise, <c>false</c>.
	/// </value>
	bool AcceptCookieLanguage { get; }

	/// <summary>
	/// Gets the value indicating whether HTTP header language should be accepted.
	/// </summary>
	/// <value>
	///   <c>true</c> if header language should be accepted; otherwise, <c>false</c>.
	/// </value>
	bool AcceptHeaderLanguage { get; }

	/// <summary>
	/// Gets the default templates directory path, for example: Templates, default value is "Templates".
	/// </summary>
	/// <value>
	/// The default templates path.
	/// </value>
	string DefaultTemplatesPath { get; }

	/// <summary>
	/// Gets the value indicating whether all templates should be loaded from assembly.
	/// </summary>
	/// <value>
	///   <c>true</c> if all templates should be loaded from assembly; otherwise, <c>false</c>.
	/// </value>
	bool LoadTemplatesFromAssembly { get; }

	/// <summary>
	/// Gets the master page template file name.
	/// </summary>
	/// <value>
	/// The default name of the master template file.
	/// </value>
	string DefaultMasterTemplateFileName { get; }

	/// <summary>
	/// Gets the master template main content variable name.
	/// </summary>
	/// <value>
	/// The default name of the main content variable.
	/// </value>
	string DefaultMainContentVariableName { get; }

	/// <summary>
	/// Gets the master template title variable name.
	/// </summary>
	/// <value>
	/// The default name of the title variable.
	/// </value>
	string DefaultTitleVariableName { get; }

	/// <summary>
	/// Gets the default site style
	/// </summary>
	/// <value>
	/// The default style.
	/// </value>
	string DefaultStyle { get; }

	/// <summary>
	/// Gets the data directory path, for example: default value is "App_Data".
	/// </summary>
	/// <value>
	/// The data path.
	/// </value>
	string DataPath { get; }

	/// <summary>
	/// Gets the value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.
	/// </summary>
	/// <value>
	///   <c>true</c> if only controllers should be processed, processing of static files disabled; otherwise, <c>false</c>.
	/// </value>
	bool StaticFilesEnabled { get; }

	/// <summary>
	/// Gets the static files paths.
	/// </summary>
	/// <value>
	/// The static files paths.
	/// </value>
	IReadOnlyList<string> StaticFilesPaths { get; }

	/// <summary>
	/// Gets the string table files.
	/// </summary>
	/// <value>
	/// The string table files.
	/// </value>
	IReadOnlyList<string> StringTableFiles { get; }

	/// <summary>
	/// Gets the value indicating whether site title postfix should be set automatically.
	/// </summary>
	/// <value>
	///   <c>true</c> if site title postfix should be set automatically; otherwise, <c>false</c>.
	/// </value>
	bool DisableAutomaticSiteTitleSet { get; }

	/// <summary>
	/// Gets the value indicating whether exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred.
	/// </summary>
	/// <value>
	///   <c>true</c> if exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred; otherwise, <c>false</c>.
	/// </value>
	bool HideExceptionDetails { get; }

	/// <summary>
	/// Gets the value indicating whether Simplify.Web HTTP 500 error page should be displayed in dark style.
	/// </summary>
	/// <value>
	///   <c>true</c> if Simplify.Web HTTP 500 error page should be displayed in dark style; otherwise, <c>false</c>.
	/// </value>
	bool ErrorPageDarkStyle { get; }

	/// <summary>
	/// Gets the value indicating whether templates memory cache enabled or disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if templates memory cache is enabled; otherwise, <c>false</c>.
	/// </value>
	bool TemplatesMemoryCache { get; }

	/// <summary>
	/// Gets the value indicating whether string table memory cache enabled or disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if string table memory cache is enabled; otherwise, <c>false</c>.
	/// </value>
	bool StringTableMemoryCache { get; }

	/// <summary>
	/// Gets the value indicating whether file reader caching should be disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if file reader caching should be disabled; otherwise, <c>false</c>.
	/// </value>
	bool DisableFileReaderCache { get; }

	/// <summary>
	/// Gets the value indicating whether measurements is enabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if measurements is enabled; otherwise, <c>false</c>.
	/// </value>
	bool MeasurementsEnabled { get; }

	/// <summary>
	/// Gets the value indicating whether console tracing is enabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if console tracing is enabled; otherwise, <c>false</c>.
	/// </value>
	bool ConsoleTracing { get; }
}