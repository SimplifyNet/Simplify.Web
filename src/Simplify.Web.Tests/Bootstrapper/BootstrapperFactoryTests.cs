using NUnit.Framework;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Meta;

namespace Simplify.Web.Tests.Bootstrapper;

[TestFixture]
public class BootstrapperFactoryTests
{
	[Test]
	public void CreateBootstrapper_NoUserType_BaseBootstrapperReturned()
	{
		// Assign

		if (!SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Contains("Simplify"))
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("Simplify");

		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var bootstrapper = BootstrapperFactory.CreateBootstrapper();

		// Assert

		Assert.AreEqual("Simplify.Web.Bootstrapper.BaseBootstrapper", bootstrapper.GetType().FullName);
	}

	[Test]
	public void CreateBootstrapper_HaveUserType_TestBootstrapperReturned()
	{
		// Assign

		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var bootstrapper = BootstrapperFactory.CreateBootstrapper();

		// Assert

		Assert.AreEqual("Simplify.Web.Tests.TestEntities.TestBootstrapper", bootstrapper.GetType().FullName);
	}
}