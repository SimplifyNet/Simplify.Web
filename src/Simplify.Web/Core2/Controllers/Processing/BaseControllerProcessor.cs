using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Execution;
using Simplify.Web.Core2.Controllers.Execution.Args;
using Simplify.Web.Core2.Controllers.Response.Injectors;

namespace Simplify.Web.Core2.Controllers.Processing;

public abstract class BaseControllerProcessor(IControllerExecutorResolver resolver, IControllerResponsePropertiesInjector propertiesInjector)
{
	protected async Task ExecuteAndHandleResponse(IControllerExecutionArgs args, Action stopProcessing)
	{
		var response = await resolver.Resolve(args.Controller).Execute(args);

		if (response == null)
			return;

		await HandleControllerResponseAsync(response, stopProcessing);
	}

	private async Task HandleControllerResponseAsync(ControllerResponse response, Action stopProcessing)
	{
		propertiesInjector.Inject(response);

		var responseResult = await response.ExecuteAsync();

		switch (responseResult)
		{
			case ControllerResponseResult.RawOutput:
				stopProcessing();
				break;

			case ControllerResponseResult.Redirect:
				stopProcessing();
				break;
		}
	}
}
