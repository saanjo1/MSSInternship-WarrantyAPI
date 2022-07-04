using WarrantyAPI;
using WarrantyAPI.Repositories;
using WarrantyAPI.Models;
using AutoMapper;
using WarrantyAPI.MappingProfiles;
using WarrantyAPI.Contracts;

var builder = WebApplication.CreateBuilder(args);
string WarrantyTableName = Environment.GetEnvironmentVariable("WarrantyTableName");
string connectionString = Environment.GetEnvironmentVariable("AzureTableStorage");


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new WarrantyProfile());

});

IMapper mapper = mapperConfig.CreateMapper();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IRepository<Warranty>>(x => new WarrantyRepository(connectionString, WarrantyTableName, mapper));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsapp");

app.MapControllers();

app.Run();
