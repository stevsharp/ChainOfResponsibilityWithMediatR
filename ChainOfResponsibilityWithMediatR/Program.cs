using MediatR;
using ChainOfResponsibility.Infrastructure.DependencyInjection;
using Carter;
using FluentValidation;
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
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssembly(typeof(ChainOfResponsibility.Application.AssemblyMarker).Assembly);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

