using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.V1.Execution;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;
using Simplify.Web.Tests.Controllers.V1.Execution.TestControllers;
using Simplify.Web.Views;

namespace Simplify.Web.Tests.Controllers.V1.Execution;

[TestFixture]
public class Controller1FactoryTests
{
	[Test]
	public void CreateController_Controller_PropertiesAssigned()
	{
		// Arrange

		var mc = new MatchedController(Mock.Of<IController1Metadata>(m => m.ControllerType == typeof(FooController)), new Dictionary<string, object>());

		var provider = new DryIocDIProvider();

		var webContextProvider = new Mock<IWebContextProvider>();
		var webContext = Mock.Of<IWebContext>(x => x.Response == Mock.Of<HttpResponse>());
		var dataCollector = Mock.Of<IDataCollector>();
		var environment = Mock.Of<IEnvironment>();
		var fileReader = Mock.Of<IFileReader>();
		var listsGenerator = Mock.Of<IListsGenerator>();
		var languageManagerProvider = new Mock<ILanguageManagerProvider>();
		var languageManager = Mock.Of<ILanguageManager>();
		var redirector = Mock.Of<IRedirector>();
		var stringTable = Mock.Of<IStringTable>();
		var templateFactory = Mock.Of<ITemplateFactory>();

		provider.Register<FooController>(LifetimeType.Transient);

		provider.Register(_ => webContextProvider.Object);
		provider.Register(_ => Mock.Of<IViewFactory>());
		provider.Register(_ => environment);
		provider.Register(_ => stringTable);
		provider.Register(_ => templateFactory);
		provider.Register(_ => listsGenerator);
		provider.Register(_ => dataCollector);
		provider.Register(_ => redirector);
		provider.Register(_ => languageManagerProvider.Object);
		provider.Register(_ => fileReader);

		webContextProvider.Setup(x => x.Get()).Returns(webContext);
		languageManagerProvider.Setup(x => x.Get()).Returns(languageManager);

		// Act

		using var scope = provider.BeginLifetimeScope();

		var r = new Controller1Factory(scope.Resolver).CreateController(mc);

		// Assert

		Assert.That(r, Is.Not.Null);

		Assert.That(r.RouteParameters, Is.Not.Null);
		Assert.That(r.StringTable, Is.Not.Null);
		Assert.That(r.Context, Is.EqualTo(webContext));
		Assert.That(r.DataCollector, Is.EqualTo(dataCollector));
		Assert.That(r.Environment, Is.EqualTo(environment));
		Assert.That(r.FileReader, Is.EqualTo(fileReader));
		Assert.That(r.Html.ListsGenerator, Is.EqualTo(listsGenerator));
		Assert.That(r.LanguageManager, Is.EqualTo(languageManager));
		Assert.That(r.Redirector, Is.EqualTo(redirector));
		Assert.That(r.StringTableManager, Is.EqualTo(stringTable));
		Assert.That(r.TemplateFactory, Is.EqualTo(templateFactory));
	}
}