using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core2.Controllers.Execution;
using Simplify.Web.Old.Core2.Controllers.Execution.Extensions;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;
using Simplify.Web.Old.Core2.Controllers.Response.Injectors;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Stages;

public class ControllerExecutionHandler(IControllerExecutorResolver resolver, IControllerResponsePropertiesInjector propertiesInjector)
	: BaseControllerProcessor(resolver, propertiesInjector), IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing) => ExecuteAndHandleResponse(context.ToControllerExecutionArgs(), stopProcessing);
}