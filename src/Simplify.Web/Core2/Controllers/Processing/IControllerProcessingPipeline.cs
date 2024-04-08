using System.Threading.Tasks;

namespace Simplify.Web.Core2.Controllers.Processing;

public interface IControllerProcessingPipeline
{
	Task Execute(IControllerProcessingContext context);
}