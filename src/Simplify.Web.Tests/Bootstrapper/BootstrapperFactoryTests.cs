using NUnit.Framework;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Bootstrapper.Setup;
using Simplify.Web.System;
using Simplify.Web.Tests.Bootstrapper.TestTypes;

namespace Simplify.Web.Tests.Bootstrapper;

[TestFixture]
public class BootstrapperFactoryTests
{
	[Test]
	public void CreateBootstrapper_NoUserType_BaseBootstrapperReturned()
	{
		// Arrange

		if (!SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Contains("Simplify"))
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("Simplify");

		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var bootstrapper = BootstrapperFactory.CreateBootstrapper();

		// Assert
		Assert.That(bootstrapper.GetType(), Is.EqualTo(typeof(BaseBootstrapper)));
	}

	[Test]
	public void CreateBootstrapper_HasUserType_TestBootstrapperReturned()
	{
		// Arrange

		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		// Act
		var bootstrapper = BootstrapperFactory.CreateBootstrapper();

		// Assert
		Assert.That(bootstrapper.GetType(), Is.EqualTo(typeof(TestBootstrapper)));
	}
}