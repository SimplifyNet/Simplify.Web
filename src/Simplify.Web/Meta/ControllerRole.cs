namespace Simplify.Web.Meta
{
	/// <summary>
	/// Controller specific roles
	/// </summary>
	public class ControllerRole
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerRole" /> class.
		/// </summary>
		/// <param name="is400Handler">if set to <c>true</c> then indicates what controller handles HTTP 400 errors.</param>
		/// <param name="is403Handler">if set to <c>true</c> then indicates what controller handles HTTP 403 errors.</param>
		/// <param name="is404Handler">if set to <c>true</c> then indicates what controller handles HTTP 404 errors.</param>
		public ControllerRole(bool is400Handler = false, bool is403Handler = false, bool is404Handler = false)
		{
			Is400Handler = is400Handler;
			Is403Handler = is403Handler;
			Is404Handler = is404Handler;
		}

		/// <summary>
		/// Gets or sets a value indicating whether controller is HTTP 400 error handler.
		/// </summary>
		public bool Is400Handler { get; }

		/// <summary>
		/// Gets or sets a value indicating whether controller is HTTP 403 error handler.
		/// </summary>
		public bool Is403Handler { get; }

		/// <summary>
		/// Gets or sets a value indicating whether controller is HTTP 404 error handler.
		/// </summary>
		public bool Is404Handler { get; }
	}
}