using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.RegistrationsTests.CustomTypes;

namespace Simplify.Web.RegistrationsTests;

[TestFixture]
public class SimplifyDIContainerProviderExtensionsTests
{
	[Test]
	public void RegisterSimplifyWeb_AllModulesOverride_Invoked()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container.RegisterSimplifyWeb(x =>
		{
			x.OverrideControllerExecutor(r => r.Register<IControllerExecutor, CustomControllerExecutor>());
		});

		// Act

		using var scope = container.BeginLifetimeScope();

		var result = scope.Resolver.Resolve<IControllerExecutor>();

		// Assert

		Assert.AreEqual(typeof(CustomControllerExecutor), result.GetType());
	}
}