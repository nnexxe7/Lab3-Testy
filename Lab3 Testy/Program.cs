var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dodaj us³ugê Swaggera
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Lab 3", Version = "v1" });
});

var app = builder.Build();

// Dodaj obs³ugê Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab 3");
});

app.UseAuthorization();
app.MapControllers();
app.Run();
