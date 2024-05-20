using NUnit.Framework;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.Controllers.Meta.Loader;
using Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;
using Simplify.Web.Meta.Tests.TestTypes.Controllers.V2;
using Simplify.Web.System;

namespace Simplify.Web.Meta.Tests.Controllers.Meta.Loader;

[TestFixture]
[IgnoreControllers(typeof(TestController))]
public class MetadataLoaderTests
{
	[SetUp]
	public void Initialize()
	{
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();
	}

	[Test]
	public void Load_CurrentAssemblyControllers_GetWithoutIgnored()
	{
		// Arrange
		var loader = MetadataLoader.Current;

		// Act
		var items = loader.Load();

		// Assert

		Assert.That(items.Count, Is.EqualTo(7));

		Assert.That(items[0].ControllerType, Is.EqualTo(typeof(TestControllerV2)));
		Assert.That(items[1].ControllerType, Is.EqualTo(typeof(TestControllerV2WithModel)));
		Assert.That(items[2].ControllerType, Is.EqualTo(typeof(AllAttributesController)));
		Assert.That(items[3].ControllerType, Is.EqualTo(typeof(TestControllerViaIntermediateBaseClass)));
		Assert.That(items[4].ControllerType, Is.EqualTo(typeof(TestAsyncController)));
		Assert.That(items[5].ControllerType, Is.EqualTo(typeof(TestControllerWithModel)));
		Assert.That(items[6].ControllerType, Is.EqualTo(typeof(TestAsyncWithModelController)));
	}
}