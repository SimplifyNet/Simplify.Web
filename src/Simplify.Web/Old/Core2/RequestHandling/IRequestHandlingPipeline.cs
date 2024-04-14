using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.Old.Core2.RequestHandling;

public interface IRequestHandlingPipeline
{
	Task ExecuteAsync(IHttpContext context);
}