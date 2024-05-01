using System;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Http.ResponseWriting;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the bootstrapper configuration registration.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the response writer.
	/// </summary>
	public virtual void RegisterResponseWriter()
	{
		if (TypesToExclude.Contains(typeof(IResponseWriter)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IResponseWriter, ResponseWriter>(LifetimeType.Singleton);
	}
}