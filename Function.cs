using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared;

var builder = WebApplication.CreateSlimBuilder(args);
            
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = ApiSerializerContext.Default;
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi, options =>
{
    options.Serializer = new SourceGeneratorLambdaJsonSerializer<ApiSerializerContext>();
});
            
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(options =>
{
    options.IncludeScopes = true;
    options.UseUtcTimestamp = true;
    options.TimestampFormat = "hh:mm:ss ";
});

var app = builder.Build();

//Handlers.DataAccess = app.Services.GetRequiredService<ProductsDAO>();
Handlers.Logger = app.Logger;

app.MapGet("/", Handlers.GetAllProducts);

app.MapDelete("/{id}", Handlers.DeleteProduct);

app.MapPut("/{id}", Handlers.PutProduct);

app.MapGet("/{id}", Handlers.GetProduct);

app.Run();

static class Handlers
{
    internal static ILogger Logger;
    
    public static async Task GetAllProducts(HttpContext context)
    {

        Logger.LogInformation($"First native aot implementation");
        await context.WriteResponse(HttpStatusCode.OK, "First native aot implementation");
    }

    public static async Task DeleteProduct(HttpContext context)
    {
        Logger.LogInformation($"You little bo, whayoudoing trayna deleting ma products");
        await context.WriteResponse(HttpStatusCode.OK, "You little bo, whayoudoing trayna deleting ma products");
    }

    public static async Task GetProduct(HttpContext context)
    {
        var id = context.Request.RouteValues["id"].ToString();

        Logger.LogInformation($"Dont thinks is that easy to get my product nnumber {id}");
        await context.WriteResponse(HttpStatusCode.OK, $"Dont thinks is that easy to get my product nnumber {id}");
    }
    
    public static async Task PutProduct(HttpContext context)
    {


        var id = context.Request.RouteValues["id"].ToString();

        Logger.LogInformation($"are ya trying to put praducts in ma db?!?! {id}");
        await context.WriteResponse(HttpStatusCode.OK, $"are ya trying to put praducts in ma db?!?! {id}");

    }
}

static class ResponseWriter
{
    public static async Task WriteResponse(this HttpContext context, HttpStatusCode statusCode)
    {
        await context.WriteResponse<string>(statusCode, "");
    }
    
    public static async Task WriteResponse<TResponseType>(this HttpContext context, HttpStatusCode statusCode, TResponseType body) where TResponseType : class
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body, typeof(TResponseType), ApiSerializerContext.Default));
    }
}