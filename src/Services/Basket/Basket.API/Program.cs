using BuildingBlocks.Exceptions.Handler;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

/*
 * aqui ya hemos registrado el repository BasketRepository desde IBasketRepository
 * No se puede registrar directamente varias implementaciones del IBasketRepository
 * y genera problemas con la inyección directa de dependencias.
 * Por ejemplo si hago esto:
 *
 * builder.Services.AddScoped<IBasketRepository, BasketRepository>();
 * builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();
 *
 * Lo que pasaría es que se toma el ultimo registro (con CachedBasketRepository)
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
 * pero esto sería engorroso y no es escalable ni manejable o gestionable.
 * En lugar de esto entonces, deberíamos utilizar la library Scrutor, que simplifica el proceso de registro
 * de decoradores en el contenedor de inyección de dependencias. (www.github.com/khellang/Scrutor)
 *
 */
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    // options.InstanceName = "Basket";
});


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();



// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
