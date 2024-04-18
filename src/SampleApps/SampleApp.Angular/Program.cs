using Simplify.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	//	Configure the HTTP request pipeline.

	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

	app.UseHttpsRedirection();
}

// Production use: proxying from .NET to Angular
if (!app.Environment.IsDevelopment())
{
	app.UseStaticFiles();
	app.UseSpaStaticFiles();
}

app.UseSimplifyWebNonTerminal();

// Production use: proxying from .NET to Angular
if (!app.Environment.IsDevelopment())
	app.UseSpa(spa => spa.Options.SourcePath = "ClientApp");

app.Run();