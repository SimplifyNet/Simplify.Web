using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.Web.Settings;

/// <summary>
/// Provides Simplify.Web settings.
/// </summary>
/// <seealso cref="ISimplifyWebSettings" />
public sealed class SimplifyWebSettings : ISimplifyWebSettings
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SimplifyWebSettings" /> class.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	public SimplifyWebSettings(IConfiguration configuration)
	{
		var config = configuration.GetSection(nameof(SimplifyWebSettings));

		if (!config.GetChildren().Any())
			return;

		LoadLanguageManagerSettings(config);
		LoadTemplatesSettings(config);
		LoadDataCollectorSettings(config);
		LoadDataSettings(config);
		LoadStyleSettings(config);
		LoadStaticFilesSettings(config);
		LoadEngineBehaviorSettings(config);
		LoadCacheSettings(config);
		LoadDiagnosticSettings(config);
	}

	/// <summary>
	/// Gets the default language, for example: "en", "ru", "de" etc., default value is "en".
	/// </summary>
	/// <value>
	/// The default language.
	/// </value>
	public string DefaultLanguage { get; private set; } = "en";

	/// <summary>
	/// Gets the value indicating whether cookie language should be accepted.
	/// </summary>
	/// <value>
	///   <c>true</c> if cookie language should be accepted; otherwise, <c>false</c>.
	/// </value>
	public bool AcceptCookieLanguage { get; private set; }

	/// <summary>
	/// Gets the value indicating whether HTTP header language should be accepted.
	/// </summary>
	/// <value>
	///   <c>true</c> if header language should be accepted; otherwise, <c>false</c>.
	/// </value>
	public bool AcceptHeaderLanguage { get; private set; }

	/// <summary>
	/// Gets the default templates directory path, for example: Templates, default value is "Templates".
	/// </summary>
	/// <value>
	/// The default templates path.
	/// </value>
	public string DefaultTemplatesPath { get; private set; } = "Templates";

	/// <summary>
	/// Gets the value indicating whether all templates should be loaded from assembly.
	/// </summary>
	/// <value>
	///   <c>true</c> if all templates should be loaded from assembly; otherwise, <c>false</c>.
	/// </value>
	public bool LoadTemplatesFromAssembly { get; private set; }

	/// <summary>
	/// Gets the master page template file name.
	/// </summary>
	/// <value>
	/// The default name of the master template file.
	/// </value>
	public string DefaultMasterTemplateFileName { get; private set; } = "Master.tpl";

	/// <summary>
	/// Gets the master template main content variable name.
	/// </summary>
	/// <value>
	/// The default name of the main content variable.
	/// </value>
	public string DefaultMainContentVariableName { get; private set; } = "MainContent";

	/// <summary>
	/// Gets the master template title variable name.
	/// </summary>
	/// <value>
	/// The default name of the title variable.
	/// </value>
	public string DefaultTitleVariableName { get; private set; } = "Title";

	/// <summary>
	/// Gets the default site style
	/// </summary>
	/// <value>
	/// The default style.
	/// </value>
	public string DefaultStyle { get; private set; } = "Main";

	/// <summary>
	/// Gets the data directory path, for example: default value is "App_Data".
	/// </summary>
	/// <value>
	/// The data path.
	/// </value>
	public string DataPath { get; private set; } = "App_Data";

	/// <summary>
	/// Gets the value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only.
	/// </summary>
	/// <value>
	///   <c>true</c> if only controllers should be processed, processing of static files disabled; otherwise, <c>false</c>.
	/// </value>
	public bool StaticFilesEnabled { get; private set; }

	/// <summary>
	/// Gets the static files paths.
	/// </summary>
	/// <value>
	/// The static files paths.
	/// </value>
	public IReadOnlyList<string> StaticFilesPaths { get; private set; } = ["styles", "scripts", "images", "content", "fonts"];

	/// <summary>
	/// Gets the string table files.
	/// </summary>
	/// <value>
	/// The string table files.
	/// </value>
	public IReadOnlyList<string> StringTableFiles { get; private set; } = ["StringTable.xml"];

	/// <summary>
	/// Gets the value indicating whether site title postfix should be set automatically.
	/// </summary>
	/// <value>
	///   <c>true</c> if site title postfix should be set automatically; otherwise, <c>false</c>.
	/// </value>
	public bool DisableAutomaticSiteTitleSet { get; private set; }

	/// <summary>
	/// Gets the value indicating whether exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred.
	/// </summary>
	/// <value>
	///   <c>true</c> if exception details on Simplify.Web HTTP 500 error page should be hidden when some unhandled exception occurred; otherwise, <c>false</c>.
	/// </value>
	public bool HideExceptionDetails { get; private set; }

	/// <summary>
	/// Gets the value indicating whether Simplify.Web HTTP 500 error page should be displayed in dark style.
	/// </summary>
	/// <value>
	///   <c>true</c> if Simplify.Web HTTP 500 error page should be displayed in dark style; otherwise, <c>false</c>.
	/// </value>
	public bool ErrorPageDarkStyle { get; private set; }

	/// <summary>
	/// Gets the value indicating whether templates memory cache enabled or disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if templates memory cache is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool TemplatesMemoryCache { get; private set; }

	/// <summary>
	/// Gets the value indicating whether string table memory cache enabled or disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if string table memory cache is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool StringTableMemoryCache { get; private set; }

	/// <summary>
	/// Gets the value indicating whether static files memory cache enabled or disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if static files memory cache is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool StaticFilesMemoryCache { get; private set; }

	/// <summary>
	/// Gets the value indicating whether file reader caching should be disabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if file reader caching should be disabled; otherwise, <c>false</c>.
	/// </value>
	public bool DisableFileReaderCache { get; private set; }

	/// <summary>
	/// Gets the value indicating whether measurements is enabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if measurements is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool MeasurementsEnabled { get; private set; }

	/// <summary>
	/// Gets the value indicating whether console tracing is enabled.
	/// </summary>
	/// <value>
	///   <c>true</c> if console tracing is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool ConsoleTracing { get; private set; }

	private void LoadLanguageManagerSettings(IConfiguration config)
	{
		DefaultLanguage = config.GetValueOrDefaultValue(nameof(DefaultLanguage), DefaultLanguage);
		AcceptCookieLanguage = config.GetValue<bool>(nameof(AcceptCookieLanguage));
		AcceptHeaderLanguage = config.GetValue<bool>(nameof(AcceptHeaderLanguage));
	}

	private void LoadTemplatesSettings(IConfiguration config)
	{
		DefaultTemplatesPath = config.GetValueOrDefaultValue(nameof(DefaultTemplatesPath), DefaultTemplatesPath);
		LoadTemplatesFromAssembly = config.GetValue<bool>(nameof(LoadTemplatesFromAssembly));
		DefaultMasterTemplateFileName = config.GetValueOrDefaultValue(nameof(DefaultMasterTemplateFileName), DefaultMasterTemplateFileName);
	}

	private void LoadDataCollectorSettings(IConfiguration config)
	{
		DefaultMainContentVariableName = config.GetValueOrDefaultValue(nameof(DefaultMainContentVariableName), DefaultMainContentVariableName);
		DefaultTitleVariableName = config.GetValueOrDefaultValue(nameof(DefaultTitleVariableName), DefaultTitleVariableName);
	}

	private void LoadDataSettings(IConfiguration config)
	{
		DataPath = config.GetValueOrDefaultValue(nameof(DataPath), DataPath);

		var stringTableFiles = config[nameof(StringTableFiles)];

		if (string.IsNullOrEmpty(stringTableFiles))
			return;

		StringTableFiles = [.. stringTableFiles!
			.Replace(" ", "")
			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)];
	}

	private void LoadStyleSettings(IConfiguration config) =>
		DefaultStyle = config.GetValueOrDefaultValue(nameof(DefaultStyle), DefaultStyle);

	private void LoadStaticFilesSettings(IConfiguration config)
	{
		StaticFilesEnabled = config.GetValue<bool>(nameof(StaticFilesEnabled));

		var staticFilesPaths = config[nameof(StaticFilesPaths)];

		if (string.IsNullOrEmpty(staticFilesPaths))
			return;

		StaticFilesPaths = [.. staticFilesPaths!
			.Replace(" ", "")
			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)];
	}

	private void LoadEngineBehaviorSettings(IConfiguration config)
	{
		DisableAutomaticSiteTitleSet = config.GetValue<bool>(nameof(DisableAutomaticSiteTitleSet));
		HideExceptionDetails = config.GetValue<bool>(nameof(HideExceptionDetails));
		ErrorPageDarkStyle = config.GetValue<bool>(nameof(ErrorPageDarkStyle));
	}

	private void LoadCacheSettings(IConfiguration config)
	{
		TemplatesMemoryCache = config.GetValue<bool>(nameof(TemplatesMemoryCache));
		StringTableMemoryCache = config.GetValue<bool>(nameof(StringTableMemoryCache));
		StaticFilesMemoryCache = config.GetValue<bool>(nameof(StaticFilesMemoryCache));
		DisableFileReaderCache = config.GetValue<bool>(nameof(DisableFileReaderCache));
	}

	private void LoadDiagnosticSettings(IConfiguration config)
	{
		MeasurementsEnabled = config.GetValue<bool>(nameof(MeasurementsEnabled));
		ConsoleTracing = config.GetValue<bool>(nameof(ConsoleTracing));
	}
}