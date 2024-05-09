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
	public void RegisterSimplifyWeb_OverrideControllerResponseExecutor_TypeOverridden()
	{
		// Arrange

		var container = new DryIocDIProvider();

		container.RegisterSimplifyWeb(x =>
		{
			x.OverrideControllerResponseExecutor(r => r.Register<IControllerResponseExecutor, CustomControllerResponseExecutor>());
		});

		// Act

		using var scope = container.BeginLifetimeScope();
		var result = scope.Resolver.Resolve<IControllerResponseExecutor>();

		// Assert
		Assert.That(result.GetType(), Is.EqualTo(typeof(CustomControllerResponseExecutor)));
	}
}