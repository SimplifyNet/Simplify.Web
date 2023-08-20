using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SampleApp.WindowsServiceHosted;

public class WebApplicationStartup
{
	public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>();

	public void Run()
	{
		CreateWebHostBuilder(Program.Args).Build().Start();
	}
}