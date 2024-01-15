using NUnit.Framework;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Meta;

[TestFixture]
public class SimplifyWebTypesFinderTests
{
	[SetUp]
	public void Initialize()
	{
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("DynamicProxyGenAssembly2");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();
	}

	[Test]
	public void FindTypeDerivedFrom_BaseBootstrapper_TestBootstrapperReturned()
	{
		// Act
		var type = SimplifyWebTypesFinder.FindTypeDerivedFrom<BaseBootstrapper>();

		// Assert
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestBootstrapper", type!.FullName);
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
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>();

		// Assert

		Assert.AreEqual(3, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController1", types[0].FullName);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController3", types[1].FullName);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController6", types[2].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_ControllerWithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller<>));

		// Assert

		Assert.AreEqual(1, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController4", types[0].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_AsyncControllerWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>();

		// Assert

		Assert.AreEqual(1, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController2", types[0].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_AsyncControllerWithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(AsyncController<>));

		// Assert

		Assert.AreEqual(1, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController5", types[0].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_Controller2With1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller2>();

		// Assert

		Assert.AreEqual(1, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestControllerV2", types[0].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_Controller2WithModelWith1TypeDerived_1TestControllersReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller2<>));

		// Assert

		Assert.AreEqual(1, types.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestControllerV2WithModel", types[0].FullName);
	}

	[Test]
	public void FindTypesDerivedFrom_NoDerivedTypes_NullReturned()
	{
		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<TestBootstrapper>();

		// Assert
		Assert.AreEqual(0, types.Count);
	}
}