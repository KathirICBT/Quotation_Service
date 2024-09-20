using Microsoft.EntityFrameworkCore;
using Supplier_A.OrderService.Data;
using Supplier_A.OrderService.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer("Data Source=DESKTOP-A2RESSU\\SQLEXPRESS; Initial Catalog=Supplier_a_order_db; Integrated Security=True; TrustServerCertificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
