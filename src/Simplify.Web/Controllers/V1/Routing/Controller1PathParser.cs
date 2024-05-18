using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.V1.Routing;

public static class Controller1PathParser
{
    /// <summary>
    /// Parses the specified controller path.
    /// </summary>
    /// <param name="controllerPath">The controller path.</param>
    /// <exception cref="ControllerRouteException">
    /// Bad controller path:  + controllerPath
    /// or
    /// </exception>
    public static IList<PathItem> Parse(string controllerPath)
    {
        var items = controllerPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        var pathItems = new List<PathItem>();

        foreach (var item in items)
        {
            if (item.Contains("{") || item.Contains("}") || item.Contains(":"))
            {
                var matches = Regex.Matches(item, @"^{[a-zA-Z0-9:_\-\[\]]+}$");

                if (matches.Count == 0)
                    throw new ControllerRouteException("Bad controller path: " + controllerPath);

                var subItem = item.Substring(1, item.Length - 2);

                if (subItem.Contains(":"))
                {
                    var parameterData = subItem.Split(':');
                    var type = ParseParameterType(parameterData[1])
                               ?? throw new ControllerRouteException(
                                   $"Undefined controller parameter type '{parameterData[1]}', path: {controllerPath}");

                    pathItems.Add(new PathParameter(parameterData[0], type));
                }
                else
                    pathItems.Add(new PathParameter(subItem, typeof(string)));
            }
            else
                pathItems.Add(new PathSegment(item));
        }

        return pathItems;
    }

    private static Type? ParseParameterType(string typeData)
    {
        return typeData switch
        {
            "int" => typeof(int),
            "decimal" => typeof(decimal),
            "bool" => typeof(bool),
            "[]" => typeof(string[]),
            "string[]" => typeof(string[]),
            "int[]" => typeof(int[]),
            "decimal[]" => typeof(decimal[]),
            "bool[]" => typeof(bool[]),
            _ => null
        };
    }
}