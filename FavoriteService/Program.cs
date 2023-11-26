using FavoriteService.AsyncDataServices;
using FavoriteService.Data;
using FavoriteService.Data.Repositories;
using FavoriteService.EventProcessing;
using FavoriteService.SyncDataServices.Grpc.Pizza;
using FavoriteService.SyncDataServices.Grpc.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//select database based on environment
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("FavoriteDatabase")!));

// Add services to the container for dependency injection.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

//Add GRPC services
builder.Services.AddScoped<IUserDataClient, UserDataClient>();
builder.Services.AddScoped<IPizzaDataClient, PizzaDataClient>();

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