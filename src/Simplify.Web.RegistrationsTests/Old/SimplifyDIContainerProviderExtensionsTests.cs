using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Old;
using Simplify.Web.Old.Core.Controllers.Execution;
using Simplify.Web.RegistrationsTests.Old.CustomTypes;

namespace Simplify.Web.RegistrationsTests.Old;

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
			x.OverrideControllerExecutor(r => DIRegistratorExtensions.Register<IControllerExecutor, CustomControllerExecutor>(r));
		});

		// Act

		using var scope = container.BeginLifetimeScope();

		var result = scope.Resolver.Resolve<IControllerExecutor>();

		// Assert

		Assert.AreEqual(typeof(CustomControllerExecutor), result.GetType());
	}
}