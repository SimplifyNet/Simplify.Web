using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Tests.Views.TestViews;
using Simplify.Web.Views;

namespace Simplify.Web.Tests.Views;

[TestFixture]
public class ViewFactoryTests
{
	[Test]
	public void CreateController_Controller_PropertiesAssigned()
	{
		// Arrange

		var provider = new DryIocDIProvider();

		var webContextProvider = new Mock<IWebContextProvider>();

		var webContext = Mock.Of<IWebContext>(x => x.Response == Mock.Of<HttpResponse>() &&
		x.SiteUrl == "http://my-site.com");

		var environment = Mock.Of<IEnvironment>();
		var listsGenerator = Mock.Of<IListsGenerator>();
		var languageManagerProvider = new Mock<ILanguageManagerProvider>();
		var languageManager = Mock.Of<ILanguageManager>(x => x.Language == "tr");
		var stringTable = Mock.Of<IStringTable>(x => x.Items == new Dictionary<string, object?> { { "foo", "bar" } });
		var templateFactory = Mock.Of<ITemplateFactory>();

		provider.Register<FooView>(LifetimeType.Transient);

		provider.Register(_ => webContextProvider.Object);
		provider.Register(_ => Mock.Of<IViewFactory>());
		provider.Register(_ => environment);
		provider.Register(_ => stringTable);
		provider.Register(_ => templateFactory);
		provider.Register(_ => listsGenerator);
		provider.Register(_ => languageManagerProvider.Object);

		webContextProvider.Setup(x => x.Get()).Returns(webContext);
		languageManagerProvider.Setup(x => x.Get()).Returns(languageManager);

		// Act

		using var scope = provider.BeginLifetimeScope();

		var view = new ViewFactory(scope.Resolver).CreateView(typeof(FooView));

		// Assert

		Assert.That(view, Is.Not.Null);

		Assert.That(view.Language, Is.EqualTo("tr"));
		Assert.That(view.SiteUrl, Is.EqualTo("http://my-site.com"));
		Assert.That(view.StringTable["foo"], Is.EqualTo("bar"));
		Assert.That(view.StringTableManager, Is.Not.Null);

		Assert.That(view.Environment, Is.EqualTo(environment));
		Assert.That(view.Html.ListsGenerator, Is.EqualTo(listsGenerator));
		Assert.That(view.StringTableManager, Is.EqualTo(stringTable));
		Assert.That(view.TemplateFactory, Is.EqualTo(templateFactory));
	}
}