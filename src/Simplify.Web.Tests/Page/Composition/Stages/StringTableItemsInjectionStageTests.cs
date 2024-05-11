using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class StringTableItemsInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;

	[SetUp]
	public void Initialize() => _dataCollector = new Mock<IDataCollector>();

	[Test]
	public void Execute_DataExistsInStringTable_DataInjectedToDataCollector()
	{
		// Arrange

		var stringTable = Mock.Of<IStringTable>(x => x.Items == new Dictionary<string, object>
		{
			{ "Foo", "Bar" }
		});

		var stage = new StringTableItemsInjectionStage(stringTable);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == StringTableItemsInjectionStage.StringTablePrefix + "Foo"), It.Is<string>(d => d == "Bar")));
	}
}