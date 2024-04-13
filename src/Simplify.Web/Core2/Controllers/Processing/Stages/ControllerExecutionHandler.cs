using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Execution;
using Simplify.Web.Core2.Controllers.Execution.Extensions;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

public class ControllerExecutionHandler(IControllerExecutorResolver resolver) : IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing)
	{
		resolver.Resolve(context.Controller.MetaData).Execute(context.ToControllerExecutionArgs());

		return Task.CompletedTask;
	}
}