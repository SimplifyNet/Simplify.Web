using System.Collections.Generic;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Execution.Args;

/// <summary>
/// Represents controller execution arguments.
/// </summary>
public interface IControllerExecutionArgs
{
	/// <summary>
	/// The controller metadata.
	/// </summary>
	IControllerMetaData Controller { get; }

	/// <summary>
	/// The context.
	/// </summary>
	IHttpContext Context { get; }

	/// <summary>
	/// he route parameters.
	/// </summary>
	IDictionary<string, object>? RouteParameters { get; }
}