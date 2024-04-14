using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represents controller execution arguments.
/// </summary>
public interface IControllerExecutionArgs
{
	/// <summary>
	/// The controller meta-data.
	/// </summary>
	IControllerMetaData ControllerMetaData { get; }

	/// <summary>
	/// The DI container resolver.
	/// </summary>
	IDIResolver Resolver { get; }

	/// <summary>
	/// The context.
	/// </summary>
	HttpContext Context { get; }

	/// <summary>
	/// he route parameters.
	/// </summary>
	IDictionary<string, object>? RouteParameters { get; }
}
