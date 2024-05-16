using Simplify.Web;
using Simplify.Web.Bootstrapper;

var app = WebApplication.CreateBuilder(args).Build();

app.UseSimplifyWeb();

BootstrapperFactory.ContainerProvider.Verify();

app.Run();