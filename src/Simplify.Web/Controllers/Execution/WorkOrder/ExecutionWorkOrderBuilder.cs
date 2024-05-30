using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

/// <summary>
/// Provides the execution work order builder
/// </summary>
public sealed class ExecutionWorkOrderBuilder
{
	/// <summary>
	/// Gets the controllers.
	/// </summary>
	/// <value>
	/// The controllers.
	/// </value>
	public List<IMatchedController> Controllers { get; } = [];

	/// <summary>
	/// Gets or sets the HTTP status code.
	/// </summary>
	/// <value>
	/// The HTTP status code.
	/// </value>
	public HttpStatusCode? HttpStatusCode { get; set; }

	/// <summary>
	/// Builds the execution work order.
	/// </summary>
	public IExecutionWorkOrder Build() =>
		new ExecutionWorkOrder(
			Controllers
				.SortByRunPriority()
				.ToList()
				.AsReadOnly(),
			HttpStatusCode);
}