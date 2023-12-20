using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.Web.Settings;

/// <summary>
/// Simplify.Web settings
/// </summary>
/// <seealso cref="ISimplifyWebSettings" />
public sealed class SimplifyWebSettings : ISimplifyWebSettings
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SimplifyWebSettings"/> class.
	/// </summary>
	public SimplifyWebSettings(IConfiguration configuration)
	{
		var config = configuration.GetSection("SimplifyWebSettings");

		if (!config.GetChildren().Any())
			return;

		LoadLanguageManagerSettings(config);
		LoadTemplatesSettings(config);
		LoadDataCollectorSettings(config);
		LoadStyleSettings(config);
		LoadOtherSettings(config);
		LoadStaticFilesSettings(config);
		LoadEngineBehaviorSettings(config);
		LoadCacheSettings(config);
		LoadDiagnosticSettings(config);
	}

	/// <summary>
	/// Default language, for example: "en", "ru", "de" etc., default value is "en"
	/// </summary>
	public string DefaultLanguage { get; private set; } = "en";

	/// <summary>
	/// Gets a value indicating whether HTTP header language should be accepted
	/// </summary>
	/// <value>
	/// <c>true</c> if HTTP header language should be accepted; otherwise, <c>false</c>.
	/// </value>
	public bool AcceptHeaderLanguage { get; private set; }

	/// <summary>
	/// Default templates directory path, for example: Templates, default value is "Templates"
	/// </summary>
	public string DefaultTemplatesPath { get; private set; } = "Templates";

	/// <summary>
	/// Gets a value indicating whether all templates should be loaded from assembly
	/// </summary>
	/// <value>
	/// <c>true</c> if all templates should be loaded from assembly; otherwise, <c>false</c>.
	/// </value>
	public bool LoadTemplatesFromAssembly { get; private set; }

	/// <summary>
	/// Gets or sets the master page template file name
	/// </summary>
	/// <value>
	/// The name of the master page template file
	/// </value>
	public string DefaultMasterTemplateFileName { get; private set; } = "Master.tpl";

	/// <summary>
	/// Gets or sets the master template main content variable name.
	/// </summary>
	/// <value>
	/// The  master template main content variable name.
	/// </value>
	public string DefaultMainContentVariableName { get; private set; } = "MainContent";

	/// <summary>
	/// Gets or sets the master template title variable name.
	/// </summary>
	/// <value>
	/// The title variable name.
	/// </value>
	public string DefaultTitleVariableName { get; private set; } = "Title";

	/// <summary>
	/// Default site style
	/// </summary>
	public string DefaultStyle { get; private set; } = "Main";

	/// <summary>
	/// Data directory path, default value is "App_Data"
	/// </summary>
	public string DataPath { get; private set; } = "App_Data";

	/// <summary>
	/// Gets a value indicating whether Simplify.Web static files processing is enabled or controllers requests should be processed only
	/// </summary>
	public bool StaticFilesEnabled { get; private set; } = true;

	/// <summary>
	/// Gets the static files paths.
	/// </summary>
	/// <value>
	/// The static files paths.
	/// </value>
	public IList<string> StaticFilesPaths { get; }
		= new List<string> { "styles", "scripts", "images", "content", "fonts" };

	/// <summary>
	/// Gets the string table files.
	/// </summary>
	/// <value>
	/// The string table files.
	/// </value>
	public IList<string> StringTableFiles { get; }
		= new List<string> { "StringTable.xml" };

	/// <summary>
	/// Gets or sets a value indicating whether site title postfix should be set automatically
	/// </summary>
	public bool DisableAutomaticSiteTitleSet { get; private set; }

	/// <summary>
	/// Gets a value indicating whether exception details should be hide when some unhandled exception occurred.
	/// </summary>
	public bool HideExceptionDetails { get; private set; }

	/// <summary>
	/// Gets a value indicating whether Simplify.Web HTTP 500 error page should be displayed in dark style.
	/// </summary>
	public bool ErrorPageDarkStyle { get; private set; }

	/// <summary>
	/// Gets a value indicating whether templates memory cache enabled or disabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
	/// </value>
	public bool TemplatesMemoryCache { get; private set; }

	/// <summary>
	/// Gets a value indicating whether string table memory cache enabled or disabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if string table memory cache enabled; otherwise, <c>false</c>.
	/// </value>
	public bool StringTableMemoryCache { get; private set; }

	/// <summary>
	/// Gets a value indicating whether file reader caching should be disable.
	/// </summary>
	public bool DisableFileReaderCache { get; private set; }

	/// <summary>
	/// Gets a value indicating whether console tracing is enabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if console tracing is enabled; otherwise, <c>false</c>.
	/// </value>
	public bool ConsoleTracing { get; private set; }

	private void LoadLanguageManagerSettings(IConfiguration config)
	{
		var defaultLanguage = config[nameof(DefaultLanguage)];

		if (!string.IsNullOrEmpty(defaultLanguage))
			DefaultLanguage = defaultLanguage;

		var acceptHeaderLanguage = config[nameof(AcceptHeaderLanguage)];

		if (string.IsNullOrEmpty(acceptHeaderLanguage))
			return;

		if (bool.TryParse(acceptHeaderLanguage, out var buffer))
			AcceptHeaderLanguage = buffer;
	}

	private void LoadTemplatesSettings(IConfiguration config)
	{
		var defaultTemplatesPath = config[nameof(DefaultTemplatesPath)];

		if (!string.IsNullOrEmpty(defaultTemplatesPath))
			DefaultTemplatesPath = defaultTemplatesPath;

		var loadTemplatesFromAssembly = config[nameof(LoadTemplatesFromAssembly)];

		if (!string.IsNullOrEmpty(loadTemplatesFromAssembly))
			if (bool.TryParse(loadTemplatesFromAssembly, out var buffer))
				LoadTemplatesFromAssembly = buffer;

		var defaultMasterTemplateFileName = config[nameof(DefaultMasterTemplateFileName)];

		if (!string.IsNullOrEmpty(defaultMasterTemplateFileName))
			DefaultMasterTemplateFileName = defaultMasterTemplateFileName;
	}

	private void LoadDataCollectorSettings(IConfiguration config)
	{
		var defaultMainContentVariableName = config[nameof(DefaultMainContentVariableName)];

		if (!string.IsNullOrEmpty(defaultMainContentVariableName))
			DefaultMainContentVariableName = defaultMainContentVariableName;

		var defaultTitleVariableName = config[nameof(DefaultTitleVariableName)];

		if (!string.IsNullOrEmpty(defaultTitleVariableName))
			DefaultTitleVariableName = defaultTitleVariableName;
	}

	private void LoadStyleSettings(IConfiguration config)
	{
		var defaultStyle = config[nameof(DefaultStyle)];

		if (!string.IsNullOrEmpty(defaultStyle))
			DefaultStyle = defaultStyle;
	}

	private void LoadOtherSettings(IConfiguration config)
	{
		var dataPath = config[nameof(DataPath)];

		if (!string.IsNullOrEmpty(dataPath))
			DataPath = dataPath;

		var stringTableFiles = config[nameof(StringTableFiles)];

		if (string.IsNullOrEmpty(stringTableFiles))
			return;

		{
			StringTableFiles.Clear();
			var items = stringTableFiles.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var item in items)
				StringTableFiles.Add(item);
		}
	}

	private void LoadStaticFilesSettings(IConfiguration config)
	{
		var staticFilesEnabled = config[nameof(StaticFilesEnabled)];

		if (!string.IsNullOrEmpty(staticFilesEnabled))
			if (bool.TryParse(staticFilesEnabled, out var buffer))
				StaticFilesEnabled = buffer;

		var staticFilesPaths = config[nameof(StaticFilesPaths)];

		if (!string.IsNullOrEmpty(staticFilesPaths))
		{
			StaticFilesPaths.Clear();
			var items = staticFilesPaths.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var item in items)
				StaticFilesPaths.Add(item);
		}
	}

	private void LoadEngineBehaviorSettings(IConfiguration config)
	{
		var disableAutomaticSiteTitleSet = config[nameof(DisableAutomaticSiteTitleSet)];

		if (!string.IsNullOrEmpty(disableAutomaticSiteTitleSet))
			if (bool.TryParse(disableAutomaticSiteTitleSet, out var buffer))
				DisableAutomaticSiteTitleSet = buffer;

		var hideExceptionDetails = config[nameof(HideExceptionDetails)];

		if (!string.IsNullOrEmpty(hideExceptionDetails))
			if (bool.TryParse(hideExceptionDetails, out var buffer))
				HideExceptionDetails = buffer;

		var errorPageDarkStyle = config[nameof(ErrorPageDarkStyle)];

		if (!string.IsNullOrEmpty(errorPageDarkStyle))
			if (bool.TryParse(errorPageDarkStyle, out var buffer))
				ErrorPageDarkStyle = buffer;
	}

	private void LoadCacheSettings(IConfiguration config)
	{
		var templatesMemoryCache = config[nameof(TemplatesMemoryCache)];

		if (!string.IsNullOrEmpty(templatesMemoryCache))
			if (bool.TryParse(templatesMemoryCache, out var buffer))
				TemplatesMemoryCache = buffer;

		var stringTableMemoryCache = config[nameof(StringTableMemoryCache)];

		if (!string.IsNullOrEmpty(stringTableMemoryCache))
			if (bool.TryParse(stringTableMemoryCache, out var buffer))
				StringTableMemoryCache = buffer;

		var disableFileReaderCache = config[nameof(DisableFileReaderCache)];

		if (!string.IsNullOrEmpty(disableFileReaderCache))
			if (bool.TryParse(disableFileReaderCache, out var buffer))
				DisableFileReaderCache = buffer;
	}

	private void LoadDiagnosticSettings(IConfiguration config)
	{
		var consoleTracing = config[nameof(ConsoleTracing)];

		if (string.IsNullOrEmpty(consoleTracing))
			return;

		if (bool.TryParse(consoleTracing, out var buffer))
			ConsoleTracing = buffer;
	}
}