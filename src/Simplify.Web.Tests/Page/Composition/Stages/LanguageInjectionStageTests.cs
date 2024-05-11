using System.Globalization;
using System.Threading;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class LanguageInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<ILanguageManagerProvider> _languageManagerProvider = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_languageManagerProvider = new Mock<ILanguageManagerProvider>();
	}

	[Test]
	public void Execute_NormalData_DataInjectedToDataCollector()
	{
		// Arrange

		var languageManager = Mock.Of<ILanguageManager>(x => x.Language == "ru");
		Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

		_languageManagerProvider.Setup(x => x.Get()).Returns(languageManager);

		var stage = new LanguageInjectionStage(_languageManagerProvider.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguage), It.Is<string>(d => d == "ru")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageExtension), It.Is<string>(d => d == ".ru")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageCultureName), It.Is<string>(d => d == "ru-RU")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageCultureNameExtension), It.Is<string>(d => d == ".ru-RU")));
	}

	[Test]
	public void Execute_NoLanguagesSet_DotLanguagesIsEmptyDefaultLanguage()
	{
		// Arrange

		var languageManager = Mock.Of<ILanguageManager>(x => x.Language == null!);

		_languageManagerProvider.Setup(x => x.Get()).Returns(languageManager);

		var stage = new LanguageInjectionStage(_languageManagerProvider.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguage), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageExtension), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageCultureName), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == LanguageInjectionStage.VariableNameCurrentLanguageCultureNameExtension), It.Is<string>(d => d == null)));
	}
}