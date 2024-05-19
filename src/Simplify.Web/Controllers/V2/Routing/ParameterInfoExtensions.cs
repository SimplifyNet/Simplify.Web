using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplify.Web.Controllers.V2.Routing;

public static class ParameterInfoExtensions
{
	public static IDictionary<string, Type> ToLowercaseNameTypeDictionary(this IEnumerable<ParameterInfo> items) =>
		items.ToDictionary(x => (x.Name ?? throw new InvalidOperationException("Parameters name is null")).ToLowerInvariant(), x => x.ParameterType);
}