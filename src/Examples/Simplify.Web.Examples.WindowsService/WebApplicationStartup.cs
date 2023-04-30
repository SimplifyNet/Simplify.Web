using System;
using Microsoft.Owin.Hosting;

namespace Simplify.Web.Examples.WindowsService;

public class WebApplicationStartup
{
	public void Run()
	{
		Console.WriteLine("Running a http server on port 8080");
		WebApp.Start<Startup>("http://localhost:8080");
	}
}