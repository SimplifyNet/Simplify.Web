using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplify.Web.Model.Binding.Parsers;

namespace Simplify.Web.Model.Binding.Binders;

/// <summary>
/// Provides the HTTP form data to object (model) binding.
/// </summary>
public class HttpFormModelBinder : IModelBinder
{
	/// <summary>
	/// Binds the specified form data to model asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public async Task BindAsync<T>(ModelBinderEventArgs<T> args)
	{
		if (args.Context.Request.ContentType == null || !args.Context.Request.ContentType.Contains("application/x-www-form-urlencoded"))
			return;

		await args.Context.ReadFormAsync();

		args.SetModel(ListToModelParser.Parse<T>(args.Context.Form.Select(x => new KeyValuePair<string, string[]>(x.Key, x.Value!)).ToList()));
	}
}