using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Controllers.Response;
using Simplify.Web.RegistrationsTests.CustomTypes;

namespace Simplify.Web.RegistrationsTests;

[TestFixture]
public class SimplifyDIContainerProviderExtensionsTests
{
	[Test]
	public void RegisterSimplifyWeb_OverrideControllerResponseExecutorAndCustomProvider_TypeOverriddenAndCustomProviderUsed()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container.RegisterSimplifyWeb(registrationsOverride: x =>
		{
			x.OverrideControllerResponseExecutor(r => r.Register<IControllerResponseExecutor, CustomControllerResponseExecutor>());
		},
		containerProvider: container);

		// Act

		using var scope = container.BeginLifetimeScope();
		var result = scope.Resolver.Resolve<IControllerResponseExecutor>();

		// Assert
		Assert.That(result.GetType(), Is.EqualTo(typeof(CustomControllerResponseExecutor)));
	}
}