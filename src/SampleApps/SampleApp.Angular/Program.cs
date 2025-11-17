using System.Text.Json;
using Simplify.Web;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Responses;

Json.DefaultOptions = new JsonSerializerOptions
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseHttpsRedirection();

app.UseSimplifyWeb(true);

BootstrapperFactory.ContainerProvider.Verify();

await app.RunAsync();