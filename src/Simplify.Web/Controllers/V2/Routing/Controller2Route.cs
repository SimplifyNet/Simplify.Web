using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V2.Routing;

public class Controller2Route(string path, IDictionary<string, Type> invokeMethodParameters) : IControllerRoute
{
	public string Path { get; } = path;

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	public IList<PathItem> Items { get; } = Controller2PathParser.Parse(path, invokeMethodParameters);
}