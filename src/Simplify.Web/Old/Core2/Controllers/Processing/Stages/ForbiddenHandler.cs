using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core2.Controllers.Execution;
using Simplify.Web.Old.Core2.Controllers.Execution.Extensions;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;
using Simplify.Web.Old.Core2.Controllers.Response.Injectors;
using Simplify.Web.Old.Core2.Controllers.Security;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Stages;

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
			context.SetResponseStatusCode(403);
		else
			await ExecuteAndHandleResponse(metaStore.Controller403.ToControllerExecutionArgs(context.Context), stopProcessing);
	}
}