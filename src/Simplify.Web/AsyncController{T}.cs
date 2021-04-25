#nullable disable

using System.Threading.Tasks;
using Simplify.DI;
using Simplify.Web.Model;

namespace Simplify.Web
{
	/// <summary>
	/// Asynchronous model controllers base class
	/// </summary>
	public abstract class AsyncController<T> : AsyncControllerBase
		where T : class
	{
		private T _model;

		/// <summary>
		/// Gets the model of current request.
		/// </summary>
		/// <value>
		/// The current request model.
		/// </value>
		public virtual T Model
		{
			get
			{
				if (_model != null)
					return _model;

				ReadModelAsync().Wait();

				return _model;
			}
		}

		/// <summary>
		/// Reads the model asynchronously.
		/// </summary>
		public virtual async Task ReadModelAsync()
		{
			var handler = Resolver.Resolve<IModelHandler>();

			if (!handler.Processed)
				await handler.ProcessAsync<T>(Resolver);

			_model = handler.GetModel<T>();
		}
	}
}