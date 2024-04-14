using Simplify.Web;
using Simplify.Web.Old;

var app = WebApplication.CreateBuilder(args).Build();

app.UseSimplifyWeb();

app.Run();
