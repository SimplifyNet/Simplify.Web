using Simplify.DI;
using Simplify.Web.Bootstrapper;

namespace SampleApp.WindowsServiceHosted.Setup
{
	public class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<WebApplicationStartup>();

			// Manual Simplify.Web bootstrapper registration
			DIContainer.Current.RegisterSimplifyWeb();
		}
	}
}