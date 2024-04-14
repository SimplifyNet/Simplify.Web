using System.Threading.Tasks;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;

namespace Simplify.Web.Old.Core2.Controllers.Processing;

public interface IControllerProcessingPipeline
{
	Task Execute(IControllerProcessingContext context);
}