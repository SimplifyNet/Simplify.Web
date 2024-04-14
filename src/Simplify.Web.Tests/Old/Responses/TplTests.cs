using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Old;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;
using Simplify.Web.Old.Responses;

namespace Simplify.Web.Tests.Old.Responses;

[TestFixture]
public class TplTests
{
	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IWebContext> _context = null!;
	private Mock<HttpResponse> _response = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_context = new Mock<IWebContext>();
		_response = new Mock<HttpResponse>();

		_context.SetupGet(x => x.Response).Returns(_response.Object);
	}

	[Test]
	public async Task Process_NormalData_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", null!, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		var result = await tplData.Object.ExecuteAsync();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		Assert.AreEqual(ControllerResponseResult.Default, result);
	}

	[Test]
	public async Task Process_NormalTemplate_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>(TemplateBuilder.FromString("test").Build(), null!, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		await tplData.Object.ExecuteAsync();

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
	}

	[Test]
	public async Task Process_NormalTemplateAndTitle_DataAndTitleAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>(TemplateBuilder.FromString("test").Build(), "foo title", 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		await tplData.Object.ExecuteAsync();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
	}

	[Test]
	public async Task Process_NormalDataAndTitle_DataAndTitleAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", "foo title", 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		await tplData.Object.ExecuteAsync();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "test")));
		_dataCollector.Verify(x => x.AddTitle(It.Is<string>(d => d == "foo title")));
	}

	[Test]
	public async Task Process_NormalDataAndNullTitle_DataAddedTitleNotAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<Tpl>("test", null!, 200) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);
		tplData.SetupGet(x => x.Context).Returns(_context.Object);

		// Act
		await tplData.Object.ExecuteAsync();

		// Assert

		_dataCollector.Verify(x => x.AddTitle(It.IsAny<string>()), Times.Never);
	}
}