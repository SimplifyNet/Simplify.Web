﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;

namespace Simplify.Web.RequestHandling.Handlers;

/// <summary>
/// Provides the controllers handler.
/// </summary>
/// <seealso cref="IRequestHandler" />
public class ControllersHandler(IExecutionWorkOrderBuildDirector workOrderBuildDirector, IControllersExecutor controllersExecutor) : IRequestHandler
{
	/// <summary>
	/// Handle the request.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	public async Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		var workOrder = workOrderBuildDirector.CreateWorkOrder(context);

		if (workOrder.HttpStatusCode != null)
		{
			context.Response.StatusCode = (int)workOrder.HttpStatusCode;
			return;
		}

		var result = await controllersExecutor.ExecuteAsync(workOrder.Controllers);

		if (result != ResponseBehavior.Default)
			return;

		await next();
	}
}