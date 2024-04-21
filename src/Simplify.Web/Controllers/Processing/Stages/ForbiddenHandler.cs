using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Extensions;
using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Controllers.Response.Injectors;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Processing.Stages;

public class ForbiddenHandler(IControllerExecutorResolver resolver,
	IControllerResponsePropertiesInjector propertiesInjector,
	IControllersMetaStore metaStore)
	: BaseControllerProcessor(resolver, propertiesInjector), IControllerProcessingStage
{
	public async Task Execute(IControllerProcessingContext context, Action stopProcessing)
	{
		if (context.SecurityStatus == SecurityStatus.Ok)
			return;

		if (metaStore.Controller403 == null)
			context.Context.SetResponseStatusCode(403);
		else
			await ExecuteAndHandleResponse(metaStore.Controller403.ToControllerExecutionArgs(context.Context), stopProcessing);
	}
}