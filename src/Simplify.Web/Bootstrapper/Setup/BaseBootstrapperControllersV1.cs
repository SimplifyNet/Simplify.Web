using System;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.V1.Matcher;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper v1 controllers registration.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterController1PathParser()
	{
		if (TypesToExclude.Contains(typeof(IController1PathParser)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1PathParser, Controller1PathParser>(LifetimeType.Singleton);
	}
}