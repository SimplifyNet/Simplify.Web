using System.Collections.Generic;
using Simplify.Web.Http;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Execution.Args;

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