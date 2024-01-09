using Microsoft.EntityFrameworkCore;
using ZooManagment.Business.Services;
using ZooManagment.DataAccess;
using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ZooDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Posgres"), 
    x => x.MigrationsAssembly("ZooManagment.DataAccess")));


builder.Services.AddScoped<LocationObjectRepository>();
builder.Services.AddScoped<AnimalRepository>();
builder.Services.AddScoped<EnclosureRepository>();
builder.Services.AddScoped<SpecieRepository>();

builder.Services.AddScoped<EnclosureService>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<TransferService>();
builder.Services.AddScoped<SpecieService>();

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