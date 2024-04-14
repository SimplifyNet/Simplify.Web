using System.Collections.Generic;

namespace Simplify.Web.Old.Settings;

/// <summary>
/// Represent Simplify.Web settings.
/// </summary>
public interface ISimplifyWebSettings
{
	/// <summary>
	/// Default language, for example: "en", "ru", "de" etc., default value is "en".
	/// </summary>
	string DefaultLanguage { get; }

	/// <summary>
	/// Gets a value indicating whether cookie language should be accepted.
	/// </summary>
	/// <value>
	/// <c>true</c> if cookie language should be accepted; otherwise, <c>false</c>.
	/// </value>
	bool AcceptCookieLanguage { get; }

	/// <summary>
	/// Gets a value indicating whether HTTP header language should be accepted.
	/// </summary>
	/// <value>
	/// <c>true</c> if HTTP header language should be accepted; otherwise, <c>false</c>.
	/// </value>
	bool AcceptHeaderLanguage { get; }

	/// <summary>
	/// Default templates directory path, for example: Templates, default value is "Templates".
	/// </summary>
	string DefaultTemplatesPath { get; }

	/// <summary>
	/// Gets a value indicating whether all templates should be loaded from assembly.
	/// </summary>
	/// <value>
	/// <c>true</c> if all templates should be loaded from assembly; otherwise, <c>false</c>.
	/// </value>
	bool LoadTemplatesFromAssembly { get; }

	/// <summary>
	/// Gets or sets the master page template file name.
	/// </summary>
	/// <value>
	/// The name of the master page template file.
	/// </value>
	string DefaultMasterTemplateFileName { get; }

	/// <summary>
	/// Gets or sets the master template main content variable name.
	/// </summary>
	/// <value>
	/// The  master template main content variable name.
	/// </value>
	string DefaultMainContentVariableName { get; }

	/// <summary>
	/// Gets or sets the master template title variable name.
	/// </summary>
	/// <value>
	/// The title variable name.
	/// </value>
	string DefaultTitleVariableName { get; }

	/// <summary>
	/// Default site style
	/// </summary>
	string DefaultStyle { get; }

	/// <summary>
	/// Data directory path, for example: default value is "App_Data".
	/// </summary>
	string DataPath { get; }

	/// <summary>
	/// Gets a value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.
	/// </summary>
	bool StaticFilesEnabled { get; }

	/// <summary>
	/// Gets the static files paths.
	/// </summary>
	/// <value>
	/// The static files paths.
	/// </value>
	IList<string> StaticFilesPaths { get; }

	/// <summary>
	/// Gets the string table files.
	/// </summary>
	/// <value>
	/// The string table files.
	/// </value>
	IList<string> StringTableFiles { get; }

	/// <summary>
	/// Gets or sets a value indicating whether site title postfix should be set automatically.
	/// </summary>
	bool DisableAutomaticSiteTitleSet { get; }

	/// <summary>
	/// Gets a value indicating whether exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred.
	/// </summary>
	bool HideExceptionDetails { get; }

	/// <summary>
	/// Gets a value indicating whether Simplify.Web HTTP 500 error page should be displayed in dark style.
	/// </summary>
	bool ErrorPageDarkStyle { get; }

	/// <summary>
	/// Gets a value indicating whether templates memory cache enabled or disabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
	/// </value>
	bool TemplatesMemoryCache { get; }

	/// <summary>
	/// Gets a value indicating whether string table memory cache enabled or disabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if string table memory cache enabled; otherwise, <c>false</c>.
	/// </value>
	bool StringTableMemoryCache { get; }

	/// <summary>
	/// Gets a value indicating whether file reader caching should be disable.
	/// </summary>
	bool DisableFileReaderCache { get; }

	/// <summary>
	/// Gets a value indicating whether console tracing is enabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if console tracing is enabled; otherwise, <c>false</c>.
	/// </value>
	bool ConsoleTracing { get; }
}