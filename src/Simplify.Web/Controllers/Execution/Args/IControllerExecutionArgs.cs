using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution.Args;

/// <summary>
/// Represents a controller execution arguments.
/// </summary>
public interface IControllerExecutionArgs
{
	/// <summary>
	/// Gets the controller metadata.
	/// </summary>
	IControllerMetadata Controller { get; }

	/// <summary>
	/// Gets the context.
	/// </summary>
	HttpContext Context { get; }

	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	IReadOnlyDictionary<string, object>? RouteParameters { get; }
}