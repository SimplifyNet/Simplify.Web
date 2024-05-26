using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Controllers.Response;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;
using Simplify.Web.Responses;
using Simplify.Web.Views;

namespace Simplify.Web.Tests.Controllers.Response;

[TestFixture]
public class ControllerResponseExecutorTests
{
	[Test]
	public async Task ExecuteAsync_ContentResponse_PropertiesAssignedAndResponseExecuted()
	{
		// Arrange

		var controllerResponse = new Mock<Content>("Foo", 200, "text/plain") { CallBase = true };

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
		var responseWriter = Mock.Of<IResponseWriter>();
		var stringTable = Mock.Of<IStringTable>();
		var templateFactory = Mock.Of<ITemplateFactory>();

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
		provider.Register(_ => responseWriter);

		controllerResponse.Setup(x => x.ExecuteAsync()).ReturnsAsync(ResponseBehavior.RawOutput);
		webContextProvider.Setup(x => x.Get()).Returns(webContext);
		languageManagerProvider.Setup(x => x.Get()).Returns(languageManager);

		var r = controllerResponse.Object;

		// Act

		using var scope = provider.BeginLifetimeScope();
		var executor = new ControllerResponseExecutor(scope.Resolver);

		var result = await executor.ExecuteAsync(controllerResponse.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));

		Assert.That(r.Context, Is.EqualTo(webContext));
		Assert.That(r.DataCollector, Is.EqualTo(dataCollector));
		Assert.That(r.Environment, Is.EqualTo(environment));
		Assert.That(r.FileReader, Is.EqualTo(fileReader));
		Assert.That(r.Html.ListsGenerator, Is.EqualTo(listsGenerator));
		Assert.That(r.LanguageManager, Is.EqualTo(languageManager));
		Assert.That(r.Redirector, Is.EqualTo(redirector));
		Assert.That(r.ResponseWriter, Is.EqualTo(responseWriter));
		Assert.That(r.StringTableManager, Is.EqualTo(stringTable));
		Assert.That(r.TemplateFactory, Is.EqualTo(templateFactory));

		controllerResponse.Verify(x => x.ExecuteAsync());
	}
}