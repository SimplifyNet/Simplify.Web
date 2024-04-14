using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;
using Simplify.Web.Old.Core2.Controllers.Security;
using Simplify.Web.Old.Modules;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Stages;

public class NotAuthenticatedHandler(IRedirector redirector) : IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing)
	{
		if (context.SecurityStatus == SecurityStatus.Ok)
			return Task.CompletedTask;

		context.SetResponseStatusCode(401);
		redirector.SetLoginReturnUrlFromCurrentUri();

		stopProcessing();

		return Task.CompletedTask;
	}
}