using System;
using System.Globalization;
using System.Threading;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Core.PageAssembly;

[TestFixture]
public class ContextVariablesSetterTests
{
	private Mock<IDataCollector> _dataCollector;
	private ContextVariablesSetter _setter;
	private Mock<IDIContainerProvider> _containerProvider;

	private Mock<IEnvironment> _environment;
	private Mock<ILanguageManagerProvider> _languageManagerProvider;
	private Mock<ILanguageManager> _languageManager;
	private Mock<IWebContextProvider> _contextProvider;
	private Mock<IWebContext> _context;
	private Mock<IStringTable> _stringTable;
	private Mock<IStopwatchProvider> _stopwatchProvider;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_setter = new ContextVariablesSetter(_dataCollector.Object, true);
		_containerProvider = new Mock<IDIContainerProvider>();

		_environment = new Mock<IEnvironment>();
		_languageManagerProvider = new Mock<ILanguageManagerProvider>();
		_languageManager = new Mock<ILanguageManager>();
		_contextProvider = new Mock<IWebContextProvider>();
		_context = new Mock<IWebContext>();
		_stringTable = new Mock<IStringTable>();
		_stopwatchProvider = new Mock<IStopwatchProvider>();

		_dataCollector.SetupGet(x => x.TitleVariableName).Returns("Title");

		_environment.SetupGet(x => x.TemplatesPath).Returns("Templates");
		_environment.SetupGet(x => x.SiteStyle).Returns("Main");

		// Language-country code used in test but not suppored in LanguageManager for now
		Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

		_languageManager.SetupGet(x => x.Language).Returns("ru");
		_languageManagerProvider.Setup(x => x.Get()).Returns(_languageManager.Object);

		_context.SetupGet(x => x.SiteUrl).Returns("http://localhost/mysite/");
		_context.SetupGet(x => x.VirtualPath).Returns("/mysite");
		_contextProvider.Setup(x => x.Get()).Returns(_context.Object);
		_context.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mysite"));

		_stopwatchProvider.Setup(x => x.StopAndGetMeasurement()).Returns(new TimeSpan(0, 0, 1, 15, 342));

		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IEnvironment)))).Returns(_environment.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(ILanguageManagerProvider)))).Returns(_languageManagerProvider.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IWebContextProvider)))).Returns(_contextProvider.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IStringTable)))).Returns(_stringTable.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IStopwatchProvider)))).Returns(_stopwatchProvider.Object);
	}

	[Test]
	public void SetVariables_NormalData_AllVariablesSetToDataCollector()
	{
		// Act

		_setter.SetVariables(_containerProvider.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameTemplatesPath), It.Is<string>(d => d == "Templates")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteStyle), It.Is<string>(d => d == "Main")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguage), It.Is<string>(d => d == "ru")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageExtension), It.Is<string>(d => d == ".ru")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageCultureName), It.Is<string>(d => d == "ru-RU")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageCultureNameExtension), It.Is<string>(d => d == ".ru-RU")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteUrl), It.Is<string>(d => d == "http://localhost/mysite/")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameSiteVirtualPath), It.Is<string>(d => d == "/mysite")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameExecutionTime), It.Is<string>(d => d == "01:15:342")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.IsAny<string>()), Times.Never);
	}

	[Test]
	public void SetVariables_NoLanguagesSet_DotLanguagesIsEmptyDefaltLanguage()
	{
		// Assign
		_languageManager.SetupGet(x => x.Language).Returns((string)null);

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguage), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageExtension), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageCultureName), It.Is<string>(d => d == null)));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesSetter.VariableNameCurrentLanguageCultureNameExtension), It.Is<string>(d => d == null)));
	}

	[Test]
	public void SetVariables_TitleNoTitleInStringTable_AddNotInvoked()
	{
		// Assign
		_setter = new ContextVariablesSetter(_dataCollector.Object, false);

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.IsAny<string>()), Times.Never);
	}

	[Test]
	public void SetVariables_TitleDefaultPage_AddedTitleFromStringTable()
	{
		// Assign

		_setter = new ContextVariablesSetter(_dataCollector.Object, false);
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == ContextVariablesSetter.SiteTitleStringTableVariableName)))
			.Returns("Test!");
		_context.SetupGet(x => x.Request.Path).Returns(new PathString("/"));

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Test!")));
	}

	[Test]
	public void SetVariables_TitleDefaultPageWithQueryString_AddedTitleFromStringTable()
	{
		// Assign

		_setter = new ContextVariablesSetter(_dataCollector.Object, false);
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == ContextVariablesSetter.SiteTitleStringTableVariableName)))
			.Returns("Test!");
		_context.SetupGet(x => x.Request.Path).Returns(new PathString("/?=lang=ru"));

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Test!")));
	}

	[Test]
	public void SetVariables_TitleSpecificActionNoTitleInDataCollector_AddedTitleFromStringTable()
	{
		// Assign

		_setter = new ContextVariablesSetter(_dataCollector.Object, false);
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == ContextVariablesSetter.SiteTitleStringTableVariableName)))
			.Returns("Test!");
		_context.SetupGet(x => x.Request.Path).Returns(new PathString("/foo"));

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Test!")));
	}

	[Test]
	public void SetVariables_TitleSpecificActionNoTitleInDataCollector_TitleAddedAfterDataInDataCollector()
	{
		// Assign

		_setter = new ContextVariablesSetter(_dataCollector.Object, false);
		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == ContextVariablesSetter.SiteTitleStringTableVariableName)))
			.Returns("Test!");
		_context.SetupGet(x => x.Request.Path).Returns(new PathString("/foo"));
		_dataCollector.Setup(x => x.IsDataExist(It.Is<string>(d => d == "Title"))).Returns(true);

		// Act
		_setter.SetVariables(_containerProvider.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == " - Test!")));
	}
}