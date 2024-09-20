using Microsoft.EntityFrameworkCore;
using Supplier_A.ProductService.Data;
using Supplier_A.ProductService.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Kafka Consumer as hosted service
builder.Services.AddHostedService<KafkaConsumer>();

// Add the DbContext with SQL Server configuration
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-A2RESSU\\SQLEXPRESS; Initial Catalog=supplier_a_product; Integrated Security=True; TrustServerCertificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable static files for serving images and other assets from wwwroot
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
