using System.Linq;
using NUnit.Framework;
using Simplify.Web.Bootstrapper.Setup;
using Simplify.Web.Meta.Tests.TestTypes.Bootstrapper;
using Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;
using Simplify.Web.Meta.Tests.TestTypes.Controllers.V2;
using Simplify.Web.System;

namespace Simplify.Web.Meta.Tests.System;

[TestFixture]
public class SimplifyWebTypesFinderTests
{
	[SetUp]
	public void Initialize()
	{
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();
	}

	[Test]
	public void FindTypeDerivedFrom_BaseBootstrapper_TestBootstrapperReturned()
	{
		// Act
		var type = SimplifyWebTypesFinder.FindTypeDerivedFrom<BaseBootstrapper>();

		// Assert
		Assert.That(type, Is.EqualTo(typeof(TestBootstrapper)));
	}

	[Test]
	public void FindTypeDerivedFrom_TestBootstrapperAndNoDerivedTypes_NullReturned()
	{
		// Act
		var type = SimplifyWebTypesFinder.FindTypeDerivedFrom<TestBootstrapper>();

		// Assert
		Assert.IsNull(type);
	}

	[Test]
	public void FindTypesDerivedFrom_ControllerWith3TypesDerived_3TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>().ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(3));

		Assert.That(types[0], Is.EqualTo(typeof(AllAttributesController)));
		Assert.That(types[1], Is.EqualTo(typeof(TestController)));
		Assert.That(types[2], Is.EqualTo(typeof(TestControllerViaIntermediateBaseClass)));
	}

	[Test]
	public void FindTypesDerivedFrom_ControllerWithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller<>)).ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(1));
		Assert.That(types[0], Is.EqualTo(typeof(TestControllerWithModel)));
	}

	[Test]
	public void FindTypesDerivedFrom_AsyncControllerWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>().ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(1));
		Assert.That(types[0], Is.EqualTo(typeof(TestAsyncController)));
	}

	[Test]
	public void FindTypesDerivedFrom_AsyncControllerWithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(AsyncController<>)).ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(1));
		Assert.That(types[0], Is.EqualTo(typeof(TestAsyncWithModelController)));
	}

	[Test]
	public void FindTypesDerivedFrom_Controller2With1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller2>().ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(1));
		Assert.That(types[0], Is.EqualTo(typeof(TestControllerV2)));
	}

	[Test]
	public void FindTypesDerivedFrom_Controller2WithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller2<>)).ToList();

		// Assert

		Assert.That(types.Count, Is.EqualTo(1));
		Assert.That(types[0], Is.EqualTo(typeof(TestControllerV2WithModel)));
	}

	[Test]
	public void FindTypesDerivedFrom_NoDerivedTypes_NullReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<TestBootstrapper>();

		// Assert
		Assert.That(types.Count, Is.EqualTo(0));
	}
}