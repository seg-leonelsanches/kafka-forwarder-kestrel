using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);
var bootstrapServers = builder.Configuration.GetValue<string>("Kafka:BootstrapServers");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(p => 
    new ProducerBuilder<string, string>(new ProducerConfig { BootstrapServers = bootstrapServers }).Build());

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
