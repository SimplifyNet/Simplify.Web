﻿namespace Simplify.Web.Core2.Controllers.Routing.Matcher;

/// <summary>
/// Represent controller path parser.
/// </summary>
public interface IControllerPathParser
{
	/// <summary>
	/// Parses the specified controller path.
	/// </summary>
	/// <param name="controllerPath">The controller path.</param>
	/// <returns></returns>
	IControllerPath Parse(string controllerPath);
}