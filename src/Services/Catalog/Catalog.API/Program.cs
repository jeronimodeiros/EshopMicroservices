
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// con el metodo AddCarter() se añaden los servicios necesarios para Carter
// dentro de la inyeccion de dependencias de ASP.net

// para agregar los servicios de mediatr vamos a usar el metodo config
// y vamos a especificar la configuracion requerida, por ejemplo, 
// vamos a registrar todos los esrvicios en este proyecto en la bibiliteca de 
// clases MediatR, por eso typeof(Program) es esta clase y le paso el Assembly name.
// esto es para decirle al MediatR donde enccontrar nuestras clases de comandos y handlers de queries.

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add Validators
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.

// En el metodo MapCarter() Carter mapea las rutas definidas en la implementación del modulo ICarter
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
