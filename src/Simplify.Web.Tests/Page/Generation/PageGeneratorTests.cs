using System.Collections.Generic;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Generation;

namespace Simplify.Web.Tests.Page.Generation;

[TestFixture]
public class PageGeneratorTests
{
	private PageGenerator _pageGenerator = null!;

	private Mock<ITemplateFactory> _templateFactory = null!;

	[SetUp]
	public void Initialize() => _templateFactory = new Mock<ITemplateFactory>();

	[Test]
	public void Generate_TemplateLoadedAndDataCollectorVariablesInjected()
	{
		// Arrange

		var environment = Mock.Of<IDynamicEnvironment>(x => x.MasterTemplateFileName == "Bar");
		var tpl = TemplateBuilder.FromString("Foo{Var}").Build();
		var dataCollector = Mock.Of<IDataCollector>(x => x.Items == new Dictionary<string, string> { { "Var", "Val" } });

		_templateFactory.Setup(x => x.Load(It.Is<string>(s => s == "Bar"))).Returns(tpl);

		_pageGenerator = new PageGenerator(_templateFactory.Object, environment);

		// Act
		var result = _pageGenerator.Generate(dataCollector);

		// Assert
		Assert.That(result, Is.EqualTo("FooVal"));
	}
}