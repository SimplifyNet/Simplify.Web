using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simplify.Web;
using Simplify.Web.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new PathString("/login");
		options.Cookie.Name = "AppCookie";
	});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseAuthentication();
app.UseAuthRedirect("/login");

app.UseSimplifyWebNonTerminal();

app.Run();