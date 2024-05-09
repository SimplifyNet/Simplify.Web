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
	/// Initializes a new instance of the <see cref="SimplifyWebSettings"/> class.
	/// </summary>
	public SimplifyWebSettings(IConfiguration configuration)
	{
		var config = configuration.GetSection(nameof(SimplifyWebSettings));

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

	public string DefaultLanguage { get; private set; } = "en";

	public bool AcceptCookieLanguage { get; private set; }

	public bool AcceptHeaderLanguage { get; private set; }

	public string DefaultTemplatesPath { get; private set; } = "Templates";

	public bool LoadTemplatesFromAssembly { get; private set; }

	public string DefaultMasterTemplateFileName { get; private set; } = "Master.tpl";

	public string DefaultMainContentVariableName { get; private set; } = "MainContent";

	public string DefaultTitleVariableName { get; private set; } = "Title";

	public string DefaultStyle { get; private set; } = "Main";

	public string DataPath { get; private set; } = "App_Data";

	public bool StaticFilesEnabled { get; private set; } = true;

	public IList<string> StaticFilesPaths { get; } = ["styles", "scripts", "images", "content", "fonts"];

	public IList<string> StringTableFiles { get; } = ["StringTable.xml"];

	public bool DisableAutomaticSiteTitleSet { get; private set; }

	public bool HideExceptionDetails { get; private set; }

	public bool ErrorPageDarkStyle { get; private set; }

	public bool TemplatesMemoryCache { get; private set; }

	public bool StringTableMemoryCache { get; private set; }

	public bool DisableFileReaderCache { get; private set; }

	public bool ConsoleTracing { get; private set; }

	private void LoadLanguageManagerSettings(IConfiguration config)
	{
		var defaultLanguage = config[nameof(DefaultLanguage)];

		if (!string.IsNullOrEmpty(defaultLanguage))
			DefaultLanguage = defaultLanguage;

		var acceptCookieLanguage = config[nameof(AcceptCookieLanguage)];

		bool buffer;

		if (!string.IsNullOrEmpty(acceptCookieLanguage))
			if (bool.TryParse(acceptCookieLanguage, out buffer))
				AcceptCookieLanguage = buffer;

		var acceptHeaderLanguage = config[nameof(AcceptHeaderLanguage)];

		if (string.IsNullOrEmpty(acceptHeaderLanguage))
			return;

		if (bool.TryParse(acceptHeaderLanguage, out buffer))
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