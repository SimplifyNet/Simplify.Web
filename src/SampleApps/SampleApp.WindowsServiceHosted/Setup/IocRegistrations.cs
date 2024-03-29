﻿using Simplify.DI;
using Simplify.Web;

namespace SampleApp.WindowsServiceHosted.Setup;

public static class IocRegistrations
{
	public static IDIContainerProvider RegisterAll(this IDIContainerProvider container)
	{
		// Manual Simplify.Web bootstrapper registration
		container.RegisterSimplifyWeb()
			.Register<WebApplicationStartup>();

		return container;
	}
}