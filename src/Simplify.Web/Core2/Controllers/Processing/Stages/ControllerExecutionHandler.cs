using System;
using System.Threading.Tasks;

namespace Simplify.Web.Core2.Controllers.Processing.Stages;

public class ControllerExecutionHandler : IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext args, Action stopProcessing)
	{
		throw new NotImplementedException();
	}
}