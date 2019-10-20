namespace Simplify.Web.Model.Binding
{
	/// <summary>
	/// Represent model binder
	/// </summary>
	public interface IModelBinder
	{
		/// <summary>
		/// Binds the model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="args">The <see cref="ModelBinderEventArgs{T}"/> instance containing the event data.</param>
		void Bind<T>(ModelBinderEventArgs<T> args);
	}
}