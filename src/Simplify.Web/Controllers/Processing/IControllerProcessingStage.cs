using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Processing.Context;

namespace Simplify.Web.Controllers.Processing;

public interface IControllerProcessingStage
{
	public Task<ResponseBehavior> Execute(IControllerProcessingContext context, Action stopProcessing);
}