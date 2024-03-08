using Microsoft.EntityFrameworkCore;
using StorageAccount.Application.IServices.AzureService;
using StorageAccount.Application.IServices.DataBaseService;
using StorageAccount.Infrastructure.DataContext;
using StorageAccount.Infrastructure.Repositories;
using StorageAccount.Infrastructure.Repositories.AzureStorage;
using StorageAccount.Infrastructure.Repositories.DataBaseStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FileContext>(optionsAction: options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectDB")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAzureService, AzureStorageRepository>();
builder.Services.AddScoped<IDataBaseService, AzureDataBaseRepository>();

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
