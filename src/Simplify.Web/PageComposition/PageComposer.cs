using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.PageComposition;

public class PageComposer(IReadOnlyList<IPageCompositionStage> stages, IPageGenerator pageGenerator) : IPageComposer
{
	public async Task<string> ComposeAsync(HttpContext context)
	{
		foreach (var item in stages)
			await item.ExecuteAsync(context);

		return pageGenerator.Generate();
	}
}