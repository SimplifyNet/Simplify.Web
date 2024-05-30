using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

/// <summary>
/// Represents an execution work order
/// </summary>
public interface IExecutionWorkOrder
{
	/// <summary>
	/// Gets the controllers.
	/// </summary>
	/// <value>
	/// The controllers.
	/// </value>
	IReadOnlyList<IMatchedController> Controllers { get; }

	/// <summary>
	/// Gets the HTTP status code.
	/// </summary>
	/// <value>
	/// The HTTP status code.
	/// </value>
	public HttpStatusCode? HttpStatusCode { get; }
}