using System.Threading;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Pages.Composition;

namespace Simplify.Web.Pages.Composition.Stages;

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