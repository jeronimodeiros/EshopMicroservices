using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Application Services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

/*
 * aqui ya hemos registrado el repository BasketRepository desde IBasketRepository
 * No se puede registrar directamente varias implementaciones del IBasketRepository
 * y genera problemas con la inyecci�n directa de dependencias.
 * Por ejemplo si hago esto:
 *
 * builder.Services.AddScoped<IBasketRepository, BasketRepository>();
 * builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();
 *
 * Lo que pasar�a es que se toma el ultimo registro (con CachedBasketRepository)
 * y se pierde el primero (de BasketRepository)
 *
 * Para solucionar esto, podemos usar el patron Decorator. Entonces
 * podemos 'decorar' manualmente el CachedBasketRepository de la siguiente manera:
 *
 *  builder.Services.AddScoped<IBasketRepository>(provider =>
 *  {
 *      var basketRepository = provider.GetRequiredService<BasketRepository>();
 *      return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
 *  });
 *
 * pero esto ser�a engorroso y no es escalable ni manejable o gestionable.
 * En lugar de esto entonces, deber�amos utilizar la library Scrutor, que simplifica el proceso de registro
 * de decoradores en el contenedor de inyecci�n de dependencias. (www.github.com/khellang/Scrutor)
 *
 */
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    // options.InstanceName = "Basket";
});

//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
 .ConfigurePrimaryHttpMessageHandler(() =>
 {
     var handler = new HttpClientHandler
     {
         ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
     };
     return handler;
 });

//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
