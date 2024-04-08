using System;
using System.Threading.Tasks;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing);
}