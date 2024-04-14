using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing.Context;
using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Modules;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

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