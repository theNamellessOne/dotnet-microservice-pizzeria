using Microsoft.EntityFrameworkCore;
using UserService.AsyncDataServices;
using UserService.Data;
using UserService.Data.Repositories;
using UserService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//підключення до дб в залежності від того чи це запущено через кубернет чи 'локально'
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("UsersDatabase")!));

//додати репозиторії для dependency injection
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

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
PrepDb.PrepPop(app, app.Environment);

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
    endpoints.MapGrpcService<GrpcUserService>();
    //add user.proto file as endpoint for future client to read
    endpoints.MapGet("protos/user.proto",
        async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/user.proto")); });
});

app.Run();