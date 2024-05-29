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
		LoadDataSettings(config);
		LoadStyleSettings(config);
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

	public bool StaticFilesEnabled { get; private set; }

	public IReadOnlyList<string> StaticFilesPaths { get; private set; } = ["styles", "scripts", "images", "content", "fonts"];

	public IReadOnlyList<string> StringTableFiles { get; private set; } = ["StringTable.xml"];

	public bool DisableAutomaticSiteTitleSet { get; private set; }

	public bool HideExceptionDetails { get; private set; }

	public bool ErrorPageDarkStyle { get; private set; }

	public bool TemplatesMemoryCache { get; private set; }

	public bool StringTableMemoryCache { get; private set; }

	public bool DisableFileReaderCache { get; private set; }

	public bool MeasurementsEnabled { get; private set; }

	public bool ConsoleTracing { get; private set; }

	private void LoadLanguageManagerSettings(IConfiguration config)
	{
		DefaultLanguage = config.TrySetNotNullOrEmptyString(nameof(DefaultLanguage), DefaultLanguage);
		AcceptCookieLanguage = config.GetValue<bool>(nameof(AcceptCookieLanguage));
		AcceptHeaderLanguage = config.GetValue<bool>(nameof(AcceptHeaderLanguage));
	}

	private void LoadTemplatesSettings(IConfiguration config)
	{
		DefaultTemplatesPath = config.TrySetNotNullOrEmptyString(nameof(DefaultTemplatesPath), DefaultTemplatesPath);
		LoadTemplatesFromAssembly = config.GetValue<bool>(nameof(LoadTemplatesFromAssembly));
		DefaultMasterTemplateFileName = config.TrySetNotNullOrEmptyString(nameof(DefaultMasterTemplateFileName), DefaultMasterTemplateFileName);
	}

	private void LoadDataCollectorSettings(IConfiguration config)
	{
		DefaultMainContentVariableName = config.TrySetNotNullOrEmptyString(nameof(DefaultMainContentVariableName), DefaultMainContentVariableName);
		DefaultTitleVariableName = config.TrySetNotNullOrEmptyString(nameof(DefaultTitleVariableName), DefaultTitleVariableName);
	}

	private void LoadDataSettings(IConfiguration config)
	{
		DataPath = config.TrySetNotNullOrEmptyString(nameof(DataPath), DataPath);

		var stringTableFiles = config[nameof(StringTableFiles)];

		if (string.IsNullOrEmpty(stringTableFiles))
			return;

		StringTableFiles = stringTableFiles!.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
	}

	private void LoadStyleSettings(IConfiguration config) =>
		DefaultStyle = config.TrySetNotNullOrEmptyString(nameof(DefaultStyle), DefaultStyle);

	private void LoadStaticFilesSettings(IConfiguration config)
	{
		StaticFilesEnabled = config.GetValue<bool>(nameof(StaticFilesEnabled));

		var staticFilesPaths = config[nameof(StaticFilesPaths)];

		if (string.IsNullOrEmpty(staticFilesPaths))
			return;

		StaticFilesPaths = staticFilesPaths!.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
		DisableFileReaderCache = config.GetValue<bool>(nameof(DisableFileReaderCache));
	}

	private void LoadDiagnosticSettings(IConfiguration config)
	{
		MeasurementsEnabled = config.GetValue<bool>(nameof(MeasurementsEnabled));
		ConsoleTracing = config.GetValue<bool>(nameof(ConsoleTracing));
	}
}