using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// con el metodo AddCarter() se añaden los servicios necesarios para Carter
// dentro de la inyeccion de dependencias de ASP.net
builder.Services.AddCarter();
// para agregar los servicios de mediatr vamos a usar el metodo config
// y vamos a especificar la configuracion requerida, por ejemplo, 
// vamos a registrar todos los esrvicios en este proyecto en la bibiliteca de 
// clases MediatR, por eso typeof(Program) es esta clase y le paso el Assembly name.
// esto es para decirle al MediatR donde enccontrar nuestras clases de comandos y handlers de queries.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add Validators
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.

// En el metodo MapCarter() Carter mapea las rutas definidas en la implementación del modulo ICarter
app.MapCarter();

app.Run();
