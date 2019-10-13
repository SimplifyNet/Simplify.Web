namespace Simplify.Web.Model.Validation
{
	/// <summary>
	/// Represent model validator
	/// </summary>
	public interface IModelValidator
	{
		/// <summary>
		/// Validates the specified model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="model">The model.</param>
		void Validate<T>(T model);
	}
}