using System.Threading.Tasks;
using Simplify.Web.Controllers.Processing.Context;

namespace Simplify.Web.Controllers.Processing;

public interface IControllerProcessingPipeline
{
	Task Execute(IControllerProcessingContext context);
}