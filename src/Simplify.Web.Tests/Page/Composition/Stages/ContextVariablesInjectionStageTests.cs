using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition.Stages;

namespace Simplify.Web.Tests.Page.Composition.Stages;

[TestFixture]
public class ContextVariablesInjectionStageTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IWebContextProvider> _contextProvider = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_contextProvider = new Mock<IWebContextProvider>();
	}

	[Test]
	public void Execute_NormalData_DataInjectedToDataCollector()
	{
		// Arrange

		// ReSharper disable once ComplexConditionExpression
		var context = Mock.Of<IWebContext>(x =>
			x.SiteUrl == "http://localhost/mysite/" &&
			x.VirtualPath == "/my-site" &&
			x.Request.PathBase == new PathString("/my-site"));

		_contextProvider.Setup(x => x.Get()).Returns(context);

		var stage = new ContextVariablesInjectionStage(_contextProvider.Object);

		// Act
		stage.Execute(_dataCollector.Object);

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesInjectionStage.VariableNameSiteUrl), It.Is<string>(d => d == "http://localhost/mysite/")));
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == ContextVariablesInjectionStage.VariableNameSiteVirtualPath), It.Is<string>(d => d == "/my-site")));
	}
}