using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure, Core services 
builder.Services.AddInfrastructure();
builder.Services.AddCoer();

// Add Controllers to the service collection
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the automapper 
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

// Register the Fluent Validations
builder.Services.AddFluentValidationAutoValidation();


// Build the web appliation
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

// Enable Swagger only in Development (recommended)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "eCommerce API v1");
        c.RoutePrefix = string.Empty; // S  wagger at root URL
    });
}


// Routing 
app.UseRouting();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Controller routes
app.MapControllers();
app.Run();
