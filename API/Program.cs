
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Middlewares;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//-----------//
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

//Handle Internal server Error
app.UseMiddleware<ExceptionMiddleware>();

//Handle Not Found Error (Route Not Found) 
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseSwagger();
app.UseSwaggerUI();


//To Deal with images and other static files
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    logger.LogError(ex , "An error occurred while migrating");
}

app.Run();
