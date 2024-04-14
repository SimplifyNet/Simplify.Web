using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Old;
using Simplify.Web.Old.Modules.Data;
using Simplify.Web.Old.Responses;

namespace Simplify.Web.Tests.Old.Responses;

[TestFixture]
public class InlineTplTests
{
	private Mock<IDataCollector> _dataCollector = null!;

	[SetUp]
	public void Initialize() => _dataCollector = new Mock<IDataCollector>();

	[Test]
	public void Process_DataCollectorVariableNameIsNullOrEmpty_ArgumentNullException()
	{
		// ReSharper disable ObjectCreationAsStatement
		Assert.Throws<ArgumentNullException>(() => new InlineTpl(null, "foo"));
		Assert.Throws<ArgumentNullException>(() => new InlineTpl(null, TemplateBuilder.FromString("").Build()));
		// ReSharper restore ObjectCreationAsStatement
	}

	[Test]
	public async Task InlineTplProcess_NormalData_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<InlineTpl>("foo", "test") { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		// Act
		var result = await tplData.Object.ExecuteAsync();

		// Assert

		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "foo"), It.Is<string>(d => d == "test")));
		Assert.AreEqual(ControllerResponseResult.Default, result);
	}

	[Test]
	public async Task InlineTplProcess_NormalTemplate_DataAddedToDataCollector()
	{
		// Assign

		var tplData = new Mock<InlineTpl>("foo", await TemplateBuilder.FromString("test").BuildAsync()) { CallBase = true };
		tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

		// Act
		await tplData.Object.ExecuteAsync();

		// Assert
		_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "foo"), It.Is<string>(d => d == "test")));
	}
}