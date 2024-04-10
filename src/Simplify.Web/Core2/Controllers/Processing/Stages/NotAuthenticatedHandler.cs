﻿using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Modules;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

public class NotAuthenticatedHandler(IRedirector redirector) : IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext args, Action stopProcessing)
	{
		if (args.SecurityStatus == SecurityStatus.Ok)
			return Task.CompletedTask;

		args.Context.Context.Response.StatusCode = 401;
		redirector.SetLoginReturnUrlFromCurrentUri();

		stopProcessing();

		return Task.CompletedTask;
	}
}