using Infrastructure;
using Microsoft.Extensions.FileProviders;
using WebApi;
using WebApi.Extensions.Services;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandlerMiddleware();
app.UseStatusCodePagesWithReExecute("/errors/{0}"); //doesn't work

app.UseCorsService();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    app.Services.InitialiseDB();
}
catch(Exception ex)
{
    logger.LogError($"Error occured while initialising the database, message: {ex.Message}");
    throw;
}

app.Run();
