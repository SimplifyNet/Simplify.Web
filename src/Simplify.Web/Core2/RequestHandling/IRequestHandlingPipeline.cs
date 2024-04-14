using System.Threading.Tasks;

namespace Simplify.Web.Core2.RequestHandling;

public interface IRequestHandlingPipeline
{
	Task ExecuteAsync(IHttpContext context);
}