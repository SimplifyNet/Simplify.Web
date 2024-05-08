using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.PageComposition;

public interface IPageRenderingStage
{
	Task ExecuteAsync(HttpContext context);
}