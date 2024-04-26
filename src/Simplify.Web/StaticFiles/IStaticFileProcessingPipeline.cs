using System.Threading.Tasks;

namespace Simplify.Web.StaticFiles;

public interface IStaticFileProcessingPipeline
{
	Task ExecuteAsync(IStaticFileProcessingContext context);
}
