using AspNetCore.Swagger.Themes;
using DocumentStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsDB"));
});

var app = builder.Build();

app.MapDefaultEndpoints();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
db.Database.Migrate();

app.MapOpenApi();
app.UseSwaggerUI(
    Theme.Futuristic,
    options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Document Storage API");
        options.EnableAllAdvancedOptions();
        options.ShowBackToTopButton();
        options.EnableThemeSwitcher();
    });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();