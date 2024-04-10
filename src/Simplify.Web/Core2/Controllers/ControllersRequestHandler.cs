using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing;

namespace Simplify.Web.Core2.Controllers;

public class ControllersRequestHandler(IControllerProcessingPipeline processingPipeline, IControllerProcessingContextFactory argsFactory) : IControllersRequestHandler
{
	public async Task HandleAsync(IControllersProcessingContext context)
	{
		foreach (var controller in context.MatchedControllers)
			await processingPipeline.Execute(argsFactory.Create(controller, context.Context));
	}
}