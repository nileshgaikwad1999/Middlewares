using Microsoft.Extensions.FileProviders;
using WebApplication3;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath= "staticfile"
});
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.Use(async (ctx, next) =>
{
    var endpoint = ctx.GetEndpoint();
    await next(ctx);
});
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath , "newstatic"))
});
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/product/{id:int}", async (ctx) =>
    {
        var id = Convert.ToInt32(ctx.Request.RouteValues["id"]);
      await  ctx.Response.WriteAsync("this is product id" + id );
    });
    endpoints.MapGet("/books/auther/{authername}/{id}", async (ctx) =>
    {
        var id =Convert.ToInt32(ctx.Request.RouteValues["id"]);
        var authName = ctx.Request.RouteValues["authername"];
        await ctx.Response.WriteAsync($"{authName} {id}");

    });
});
//app.Use(async (ctx, next) =>
//{
    
//    var endpoint = ctx.GetEndpoint();
//    if (endpoint != null)
//       await ctx.Response.WriteAsync(endpoint.DisplayName);
//    await next(ctx);
//});
//app.UseEndpoints(endpoint =>
//{
//    endpoint.Map("/Home",async (ctx) =>
//    {
//      await  ctx.Response.WriteAsync("url home page");
//    });
//    endpoint.MapGet("/product", async (ctx) =>
//    {
//        await ctx.Response.WriteAsync("url get product page");
//    });
    
//});
//app.Use(async (ctx, next) =>
//{
//    await ctx.Response.WriteAsync("first middleware\n\n");
//    await next(ctx);
//});

//app.UseMiddleware();
//app.UseWhen((ctx) =>
//    ctx.Request.Query.ContainsKey("isAuth")
//,
//app => app.Use(async (ctx, next) =>
//{
//    await ctx.Response.WriteAsync("second middleware\n\n");
//    await next(ctx);
//})
//);

//app.Run(async (ctx) =>
//{
//   await ctx.Response.WriteAsync("short circit middleware");
//});
app.Run();
