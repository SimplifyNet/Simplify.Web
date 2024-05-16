using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class StopwatchDataInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IStopwatchProvider> _stopwatchProvider = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_stopwatchProvider = new Mock<IStopwatchProvider>();
	}

	[Test]
	public void Execute_NormalData_DataInjectedToDataCollector()
	{
		// Arrange

		_stopwatchProvider.Setup(x => x.StopAndGetMeasurement())
			.Returns(new TimeSpan(0, 0, 1, 15, 342));

		var stage = new StopwatchDataInjectionStage(_stopwatchProvider.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == StopwatchDataInjectionStage.VariableNameExecutionTime), It.Is<string>(d => d == "01:15:342")));
	}
}