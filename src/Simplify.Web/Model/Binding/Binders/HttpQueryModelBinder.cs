using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplify.Web.Model.Binding.Parsers;

namespace Simplify.Web.Model.Binding.Binders;

/// <summary>
/// Provides the HTTP query to model binding.
/// </summary>
public class HttpQueryModelBinder : IModelBinder
{
	/// <summary>
	/// Binds the specified HTTP query to model asynchronously.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public Task BindAsync<T>(ModelBinderEventArgs<T> args)
	{
		if (args.Context.Request.Method == "GET")
			args.SetModel(ListToModelParser.Parse<T>(args.Context.Query.Select(x => new KeyValuePair<string, string[]>(x.Key, x.Value!))
				.ToList()));

		return Task.CompletedTask;
	}
}