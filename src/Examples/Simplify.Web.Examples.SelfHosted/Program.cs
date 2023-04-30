using System;
using Microsoft.Owin.Hosting;

namespace Simplify.Web.Examples.SelfHosted;

internal class Program
{
	private static void Main()
	{
		using (WebApp.Start<Startup>("http://localhost:8080"))
		{
			Console.WriteLine("Running a http server on port 8080");
			Console.ReadLine();
		}
	}
}