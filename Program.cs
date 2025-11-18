using Microsoft.EntityFrameworkCore;
using ReportesAPI.Data;
using ReportesAPI.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=LINQExample;Username=postgres;Password=123456"));
builder.Services.AddScoped<IReportService, ReportService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi(); 
app.MapGet("/reporte/pedidos-cliente",
        async (IReportService reportService) =>
            await reportService.GeneratePedidosPorClienteAsync())
    .WithName("PedidosPorCliente")
    .WithOpenApi();

app.MapGet("/reporte/productos-vendidos",
        async (IReportService reportService) =>
            await reportService.GenerateTopProductosAsync())
    .WithName("TopProductos")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
