using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

/// <summary>
/// Provides the execution work order
/// </summary>
/// <seealso cref="IExecutionWorkOrder" />
public class ExecutionWorkOrder(IReadOnlyList<IMatchedController> controllers, HttpStatusCode? httpStatusCode = null) : IExecutionWorkOrder
{
	/// <summary>
	/// Gets the controllers.
	/// </summary>
	/// <value>
	/// The controllers.
	/// </value>
	public IReadOnlyList<IMatchedController> Controllers { get; } = controllers;

	/// <summary>
	/// Gets the HTTP status code.
	/// </summary>
	/// <value>
	/// The HTTP status code.
	/// </value>
	public HttpStatusCode? HttpStatusCode { get; } = httpStatusCode;
}