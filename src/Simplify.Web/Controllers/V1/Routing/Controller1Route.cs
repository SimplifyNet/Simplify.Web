using System.Collections.Generic;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Routing;

public class Controller1Route(string path) : IControllerRoute
{
    /// <summary>
    /// Gets the controller path items.
    /// </summary>
    public IList<PathItem> Items { get; } = Controller1PathParser.Parse(path);

    public string Path { get; } = path;
}