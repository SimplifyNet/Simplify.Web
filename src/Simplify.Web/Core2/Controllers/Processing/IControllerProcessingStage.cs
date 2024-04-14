using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing.Context;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingStage
{
	public Task Execute(IControllerProcessingContext context, Action stopProcessing);
}