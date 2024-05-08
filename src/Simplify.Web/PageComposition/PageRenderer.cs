using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.PageComposition;

public class PageRenderer(IReadOnlyList<IPageRenderingStage> stages) : IPageRenderer
{
	public async Task RenderAsync(HttpContext context)
	{
		foreach (var item in stages)
			await item.ExecuteAsync(context);
	}
}