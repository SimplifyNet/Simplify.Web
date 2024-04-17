using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Controllers.Processing.Stages;

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