using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Routing;

public class Controller1Route : IControllerRoute
{
	public Controller1Route(string? path)
	{
		Path = path ?? "";
		Items = Controller1PathParser.Parse(Path);
	}

	public string Path { get; }

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	public IList<PathItem> Items { get; }
}