using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Loader;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Http;

namespace Simplify.Web.Meta.Tests.Controllers.Meta.MetaStore;

[TestFixture]
public class ControllersMetaStoreTests
{
	[Test]
	public void Ctor_AllTypesControllers_LoadToRespectiveCategory()
	{
		// Arrange

		var globalController = Mock.Of<IControllerMetadata>();

		var regularController = Mock.Of<IControllerMetadata>(x =>
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, IControllerRoute> { { HttpMethod.Get, Mock.Of<IControllerRoute>() } }, 0));

		var forbiddenController = Mock.Of<IControllerMetadata>(x =>
			x.Role == new ControllerRole(true, false));

		var notFoundController = Mock.Of<IControllerMetadata>(x =>
			x.Role == new ControllerRole(false, true));

		var loader = new Mock<IMetadataLoader>();

		loader.Setup(x => x.Load()).Returns(
		[
			globalController,
			regularController,
			forbiddenController,
			notFoundController,
		]);

		// Act
		var store = new ControllersMetaStore(loader.Object);

		// Assert

		Assert.That(store.AllControllers.Count, Is.EqualTo(4));
		Assert.That(store.StandardControllers.Count, Is.EqualTo(2));
		Assert.That(store.GlobalControllers.Count, Is.EqualTo(1));
		Assert.That(store.RoutedControllers.Count, Is.EqualTo(1));

		Assert.That(store.StandardControllers[0], Is.EqualTo(globalController));
		Assert.That(store.StandardControllers[1], Is.EqualTo(regularController));
		Assert.That(store.GlobalControllers[0], Is.EqualTo(globalController));
		Assert.That(store.RoutedControllers[0], Is.EqualTo(regularController));
		Assert.That(store.ForbiddenController, Is.EqualTo(forbiddenController));
		Assert.That(store.NotFoundController, Is.EqualTo(notFoundController));
	}
}