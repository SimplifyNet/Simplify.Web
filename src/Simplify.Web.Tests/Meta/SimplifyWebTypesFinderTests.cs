using NUnit.Framework;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Meta;

[TestFixture]
public class SimplifyWebTypesFinderTests
{
	[Test]
	public void FindTypeDerivedFrom_BaseBootstrapper_TestBootstrapperReturned()
	{
		// Assign

		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var type = SimplifyWebTypesFinder.FindTypeDerivedFrom<BaseBootstrapper>();

		// Assert
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestBootstrapper", type!.FullName);
	}

	[Test]
	public void FindTypeDerivedFrom_NoDerivedTypes_NullReturned()
	{
		// Act
		var type = SimplifyWebTypesFinder.FindTypeDerivedFrom<TestBootstrapper>();

		// Assert
		Assert.IsNull(type);
	}

	[Test]
	public void FindTypesDerivedFrom_TypeHave3TypesDerived_3TestControllersReturned()
	{
		// Assign

		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("DynamicProxyGenAssembly2");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>();
		var types2 = SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>();

		// Assert

		Assert.AreEqual(3, types.Count);
		Assert.AreEqual(1, types2.Count);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController1", types[0].FullName);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController3", types[1].FullName);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController6", types[2].FullName);
		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestController2", types2[0].FullName);
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