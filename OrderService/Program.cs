using Microsoft.EntityFrameworkCore;
using OrderService.AsyncDataServices;
using OrderService.Data;
using OrderService.Data.Repositories;
using OrderService.EventProcessing;
using OrderService.SyncDataServices.Grpc;
using OrderService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//connect to database
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("OrderDatabase")!));

// Add services to the container for dependency injection.
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Add http data service
builder.Services.AddHttpClient<IHttpPizzaDataClient, HttpPizzaDataClient>();

//Add GRPC services
builder.Services.AddScoped<IGrpcPizzaDataClient, GrpcPizzaDataClient>();

//Add event processor for async data services as Singleton
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

//Add event listener as background service
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

//prepare database
PrepareDb.PreparePopulation(app, app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();