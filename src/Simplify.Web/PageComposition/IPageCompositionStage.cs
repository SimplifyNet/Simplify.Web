using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.PageComposition;

public interface IPageCompositionStage
{
	Task ExecuteAsync(HttpContext context);
}