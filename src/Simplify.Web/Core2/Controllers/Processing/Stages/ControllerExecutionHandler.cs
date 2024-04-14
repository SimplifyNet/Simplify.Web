using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Execution;
using Simplify.Web.Core2.Controllers.Execution.Extensions;
using Simplify.Web.Core2.Controllers.Response.Injectors;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

public class ControllerExecutionHandler(IControllerExecutorResolver resolver, IControllerResponsePropertiesInjector propertiesInjector) : IControllerProcessingStage
{
	public async Task Execute(IControllerProcessingContext context, Action stopProcessing)
	{
		var response = await resolver.Resolve(context.Controller.MetaData).Execute(context.ToControllerExecutionArgs());

		if (response == null)
			return;

		propertiesInjector.Inject(response);

		var responseResult = await response.ExecuteAsync();
	}
}