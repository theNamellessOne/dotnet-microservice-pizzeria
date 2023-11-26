using Microsoft.EntityFrameworkCore;
using PizzaService.AsyncDataServices;
using PizzaService.Data;
using PizzaService.Data.Repositories.Implementations;
using PizzaService.Data.Repositories.Interfaces;
using PizzaService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//підключення до дб в залежності від того чи це запущено через кубернет чи 'локально'
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("PizzaDatabase")!));

//додати репозиторії для dependency injection
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IPizzaSizeOptionsRepository, PizzaSizeOptionsRepostiory>();
builder.Services.AddScoped<IPizzaBorderOptionsRepository, PizzaBorderOptionsRepository>();

//шина повідомленнь (сінглтон)
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

//Grpc сервіс
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

//підготувати дб до використання
PrepDb.PreparePopulation(app, app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
//add endpoints
app.UseEndpoints(endpoints =>
{
    //add controller endpoints
    endpoints.MapControllers();
    //add grpc endpoints
    endpoints.MapGrpcService<GrpcPizzaService>();
    //add pizze.proto file as endpoint for future client to read
    endpoints.MapGet("protos/pizze.proto",
        async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/pizza.proto")); });
});

app.Run();