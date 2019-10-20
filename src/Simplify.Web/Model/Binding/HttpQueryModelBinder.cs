using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Model.Binding.Parsers;

namespace Simplify.Web.Model.Binding
{
	/// <summary>
	/// Provides HTTP query to model binding
	/// </summary>
	public class HttpQueryModelBinder : IModelBinder
	{
		/// <summary>
		/// Binds specified HTTP query to model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="args">The <see cref="ModelBinderEventArgs{T}" /> instance containing the event data.</param>
		public void Bind<T>(ModelBinderEventArgs<T> args)
		{
			if (args.Context.Request.Method == "GET")
				args.SetModel(
					ListToModelParser.Parse<T>(
						args.Context.Query.Select(x => new KeyValuePair<string, string[]>(x.Key, x.Value)).ToList()));
		}
	}
}