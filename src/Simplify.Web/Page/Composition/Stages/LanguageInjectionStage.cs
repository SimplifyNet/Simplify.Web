using System.Threading;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the language injection stage.
/// </summary>
/// <seealso cref="IPageCompositionStage" />
public class LanguageInjectionStage(ILanguageManagerProvider languageManagerProvider) : IPageCompositionStage
{
	/// <summary>
	/// The site variable name current language.
	/// </summary>
	public const string VariableNameCurrentLanguage = "SV:Language";

	/// <summary>
	/// The site variable name current language extension.
	/// </summary>
	public const string VariableNameCurrentLanguageExtension = "SV:LanguageExt";

	/// <summary>
	///  The site variable name current language culture name.
	/// </summary>
	public const string VariableNameCurrentLanguageCultureName = "SV:LanguageCultureName";

	/// <summary>
	/// The site variable name current language culture name extension.
	/// </summary>
	public const string VariableNameCurrentLanguageCultureNameExtension = "SV:LanguageCultureNameExt";

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector)
	{
		var languageManager = languageManagerProvider.Get();

		if (!string.IsNullOrEmpty(languageManager.Language))
		{
			dataCollector.Add(VariableNameCurrentLanguage, languageManager.Language);
			dataCollector.Add(VariableNameCurrentLanguageExtension, "." + languageManager.Language);
			dataCollector.Add(VariableNameCurrentLanguageCultureName, Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
			dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, "." + Thread.CurrentThread.CurrentCulture.TextInfo.CultureName);
		}
		else
		{
			dataCollector.Add(VariableNameCurrentLanguage, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageExtension, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageCultureName, (string?)null);
			dataCollector.Add(VariableNameCurrentLanguageCultureNameExtension, (string?)null);
		}
	}
}