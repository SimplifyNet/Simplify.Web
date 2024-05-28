using SampleApp.Api.Setup;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web;

var builder = WebApplication.CreateBuilder(args);

(DIContainer.Current = new SimpleInjectorDIProvider())
	.RegisterAll(builder.Services)
	.Verify();

var app = builder.Build();

app.UseSimplifyWeb();

app.Run();