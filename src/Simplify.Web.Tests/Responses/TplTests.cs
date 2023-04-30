﻿using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses;

[TestFixture]
public class TplTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IWebContext> _context = null!;
	private Mock<IOwinResponse> _response = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_context = new Mock<IWebContext>();
		_response = new Mock<IOwinResponse>();

		_context.SetupGet(x => x.Response).Returns(_response.Object);
	}

	[Test]
	public void Process_NormalData_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", null, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		var result = tplData.Object.Process();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		Assert.AreEqual(ControllerResponseResult.Default, result);
	}

	[Test]
	public void Process_NormalTemplate_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>(TemplateBuilder.FromString("test").Build(), null, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		tplData.Object.Process();

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
	}

	[Test]
	public void Process_NormalTemplateAndTitle_DataAndTitleAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>(TemplateBuilder.FromString("test").Build(), "foo title", 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		tplData.Object.Process();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
	}

	[Test]
	public void Process_NormalDataAndTitle_DataAndTitleAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", "foo title", 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		tplData.Object.Process();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
	}

	[Test]
	public void Process_NormalDataAndNullTitle_DataAddedTitleNotAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", null, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		tplData.Object.Process();

		// Assert

		_dataCollector.Verify(x => x.AddTitle(It.IsAny<string>()), Times.Never);
	}
}