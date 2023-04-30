using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Templates;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Tests.Core.PageAssembly;

[TestFixture]
public class PageBuilderTests
{
	private PageBuilder _pageBuilder;
	private Mock<IDataCollector> _dataCollector;
	private Mock<ITemplateFactory> _templatesFactory;

	private Mock<IDIContainerProvider> _containerProvider;
	private Mock<IContextVariablesSetter> _variablesSetter;
	private Mock<IStringTableItemsSetter> _stringTableItemsSetter;
	private Mock<IEnvironment> _environment;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_templatesFactory = new Mock<ITemplateFactory>();
		_pageBuilder = new PageBuilder(_templatesFactory.Object, _dataCollector.Object);

		_variablesSetter = new Mock<IContextVariablesSetter>();
		_stringTableItemsSetter = new Mock<IStringTableItemsSetter>();
		_environment = new Mock<IEnvironment>();
		_containerProvider = new Mock<IDIContainerProvider>();

		_environment.SetupGet(x => x.MasterTemplateFileName).Returns("Master.tpl");

		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IContextVariablesSetter)))).Returns(_variablesSetter.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IStringTableItemsSetter)))).Returns(_stringTableItemsSetter.Object);
		_containerProvider.Setup(x => x.Resolve(It.Is<Type>(d => d == typeof(IEnvironment)))).Returns(_environment.Object);

		_templatesFactory.Setup(x => x.Load(It.IsAny<string>())).Returns(Template.FromString("{Foo}"));
		_dataCollector.SetupGet(x => x.Items).Returns(new Dictionary<string, string> { { "Foo", "Bar" } });
	}

	[Test]
	public void Build_NormalData_BuildCorrectly()
	{
		// Act
		var result = _pageBuilder.Build(_containerProvider.Object);

		// Assert

		_variablesSetter.Verify(x => x.SetVariables(It.Is<IDIContainerProvider>(d => d == _containerProvider.Object)));
		_templatesFactory.Verify(x => x.Load(It.Is<string>(d => d == "Master.tpl")));
		Assert.AreEqual("Bar", result);
	}
}