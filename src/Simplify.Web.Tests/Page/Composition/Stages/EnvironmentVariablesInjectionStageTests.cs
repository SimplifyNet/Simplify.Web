using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class EnvironmentVariablesInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;

	[SetUp]
	public void Initialize() => _dataCollector = new Mock<IDataCollector>();

	[Test]
	public void Execute_NormalData_DataInjectedToDataCollector()
	{
		// Arrange

		// ReSharper disable once ComplexConditionExpression
		var environment = Mock.Of<IDynamicEnvironment>(x =>
			x.TemplatesPath == "Templates" &&
			x.SiteStyle == "Main");

		var stage = new EnvironmentVariablesInjectionStage(environment);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == EnvironmentVariablesInjectionStage.VariableNameTemplatesPath), It.Is<string>(d => d == "Templates")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == EnvironmentVariablesInjectionStage.VariableNameSiteStyle), It.Is<string>(d => d == "Main")));
	}
}