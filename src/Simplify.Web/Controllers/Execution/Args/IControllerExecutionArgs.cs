using System.Collections.Generic;
using Simplify.Web.Http;
using Simplify.Web.Meta;

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
	IHttpContext Context { get; }

	/// <summary>
	/// Gets the route parameters.
	/// </summary>
	IDictionary<string, object>? RouteParameters { get; }
}