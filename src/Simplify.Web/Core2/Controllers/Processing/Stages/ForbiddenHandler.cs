using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing.Security;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

public class ForbiddenHandler : IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing)
	{
		if (context.SecurityStatus == SecurityStatus.Ok)
			return Task.CompletedTask;

		return Task.CompletedTask;
	}
}