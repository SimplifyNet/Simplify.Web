using Microsoft.AspNetCore.Builder;

namespace Simplify.Web.Auth;

/// <summary>
/// Provides authentication redirection extensions
/// </summary>
public static class AuthRedirectExtensions
{
	/// <summary>
	/// Adds the authentication redirect middleware to the request pipeline (will redirect request to `redirectUrl` in case of HTTP 401 status code returned by the one of the subsequent middlewares in the pipeline).
	/// </summary>
	/// <param name="app">The application.</param>
	/// <param name="redirectUrl">The redirect URL.</param>
	public static void UseAuthRedirect(this IApplicationBuilder app, string redirectUrl)
	{
		app.Use(async (context, next) =>
		{
			await next();

			if (context.Response.StatusCode == 401)
				context.Response.Redirect(redirectUrl);
		});
	}
}