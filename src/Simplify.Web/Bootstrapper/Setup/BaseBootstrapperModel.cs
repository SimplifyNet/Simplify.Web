using System.Linq;
using Simplify.DI;
using Simplify.Web.Model;
using Simplify.Web.Model.Binding.Binders;
using Simplify.Web.Model.Validation;
using Simplify.Web.Modules.Context;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper model registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the default model binders.
	/// </summary>
	public virtual void RegisterDefaultModelBinders()
	{
		BootstrapperFactory.ContainerProvider.Register<JsonModelBinder>(LifetimeType.Singleton);
		BootstrapperFactory.ContainerProvider.Register<HttpQueryModelBinder>(LifetimeType.Singleton);
		BootstrapperFactory.ContainerProvider.Register<HttpFormModelBinder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the default model validators.
	/// </summary>
	public virtual void RegisterDefaultModelValidators() =>
		BootstrapperFactory.ContainerProvider.Register(r => new ValidationAttributesExecutor(), LifetimeType.Singleton);

	/// <summary>
	/// Registers the model handler.
	/// </summary>
	public virtual void RegisterModelHandler()
	{
		if (TypesToExclude.Contains(typeof(IModelHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IModelHandler>(r => new HttpModelHandler(r, r.Resolve<IWebContextProvider>().Get()));
	}
}