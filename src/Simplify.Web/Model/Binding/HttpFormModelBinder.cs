using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simplify.Web.Model.Binding.Parsers;

namespace Simplify.Web.Model.Binding
{
	/// <summary>
	/// Provides form data to object (model) binding
	/// </summary>
	public class HttpFormModelBinder : IModelBinder
	{
		/// <summary>
		/// Binds specified form data to model asynchronously.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public async Task BindAsync<T>(ModelBinderEventArgs<T> args)
		{
			if (!args.Context.Request.ContentType.Contains("application/x-www-form-urlencoded"))
				return;

			var form = await args.Context.Context.Request.ReadFormAsync();

			args.SetModel(ListToModelParser.Parse<T>(form.Select(x => new KeyValuePair<string, string[]>(x.Key, x.Value)).ToList()));
		}
	}
}