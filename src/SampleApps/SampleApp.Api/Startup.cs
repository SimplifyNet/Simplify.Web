using Simplify.Web;

var app = WebApplication.CreateBuilder(args).Build();

app.UseSimplifyWeb();

app.Run();
