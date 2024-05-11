using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class SiteTitleInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IWebContextProvider> _contextProvider = null!;
	private Mock<IStringTable> _stringTable = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_contextProvider = new Mock<IWebContextProvider>();
		_stringTable = new Mock<IStringTable>();

		_dataCollector.SetupGet(x => x.TitleVariableName).Returns("Title");
	}

	[Test]
	public void Execute_NoTitleInStringTable_AddNotInvoked()
	{
		// Arrange

		_dataCollector.SetupGet(x => x.TitleVariableName).Returns("Title");

		var stage = new SiteTitleInjectionStage(null!, Mock.Of<IStringTable>());

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.IsAny<string>()), Times.Never);
	}

	[Test]
	public void Execute_DefaultPageTitleInStringTableExists_AddedTitleFromStringTable()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.Path == new PathString("/"));

		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == SiteTitleInjectionStage.SiteTitleStringTableVariableName))).Returns("Foo");
		_contextProvider.Setup(x => x.Get()).Returns(context);

		var stage = new SiteTitleInjectionStage(_contextProvider.Object, _stringTable.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Foo")));
	}

	[Test]
	public void Execute_DefaultPageWithQueryStringTitleInStringTableExists_AddedTitleFromStringTable()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.Path == new PathString("/?=lang=ru"));

		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == SiteTitleInjectionStage.SiteTitleStringTableVariableName))).Returns("Foo");
		_contextProvider.Setup(x => x.Get()).Returns(context);

		var stage = new SiteTitleInjectionStage(_contextProvider.Object, _stringTable.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Foo")));
	}

	[Test]
	public void Execute_NonDefaultPageAndNoTitleInInDataCollector_AddedTitleFromStringTable()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.Path == new PathString("/foo"));

		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == SiteTitleInjectionStage.SiteTitleStringTableVariableName))).Returns("Foo");
		_contextProvider.Setup(x => x.Get()).Returns(context);

		var stage = new SiteTitleInjectionStage(_contextProvider.Object, _stringTable.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == "Foo")));
	}

	[Test]
	public void Execute_NonDefaultPageAndTitleInDataCollectorAlreadyExists_TitleAddedAfterDataInDataCollector()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.Path == new PathString("/foo"));

		_stringTable.Setup(x => x.GetItem(It.Is<string>(d => d == SiteTitleInjectionStage.SiteTitleStringTableVariableName))).Returns("Foo");
		_dataCollector.Setup(x => x.IsDataExist(It.Is<string>(d => d == "Title"))).Returns(true);
		_contextProvider.Setup(x => x.Get()).Returns(context);

		var stage = new SiteTitleInjectionStage(_contextProvider.Object, _stringTable.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "Title"), It.Is<string>(d => d == " - Foo")));
	}
}