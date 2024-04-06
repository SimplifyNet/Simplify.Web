using System.Threading.Tasks;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.RequestHandling;

public interface IRequestHandler
{
	Task Execute(IHttpContext context, RequestHandler next);
}