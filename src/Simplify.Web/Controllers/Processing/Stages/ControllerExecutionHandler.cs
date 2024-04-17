using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Extensions;
using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Controllers.Response.Injectors;

namespace Simplify.Web.Controllers.Processing.Stages;

public class ControllerExecutionHandler(IControllerExecutorResolver resolver, IControllerResponsePropertiesInjector propertiesInjector)
	: BaseControllerProcessor(resolver, propertiesInjector), IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing) => ExecuteAndHandleResponse(context.ToControllerExecutionArgs(), stopProcessing);
}