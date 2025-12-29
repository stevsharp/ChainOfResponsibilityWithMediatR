using Carter;

using ChainOfResponsibility.Application.Common.Behaviors;
using ChainOfResponsibility.Infrastructure.DependencyInjection;
using ChainOfResponsibility.Infrastructure.Persistence;

using FluentValidation;

using MediatR;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(ChainOfResponsibility.Application.AssemblyMarker).Assembly);
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(ChainOfResponsibility.Application.AssemblyMarker).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();
app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await DbSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();

app.Run();

