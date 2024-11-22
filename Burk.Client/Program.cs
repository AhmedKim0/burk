using Burk.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Burk.DAL.Repository.Interface;
using Burk.DAL.Repository.Imp;
using Burk.Client.DTO;
using Burk.DAL.Entity;
using Burk.Client.BL.Interfaces;
using Burk.Client.BL.Imp;
using Burk.Client.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BurkDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ClientConnection")));
builder.Services.AddScoped<IAsyncRepository<WaitingList>, Repository<WaitingList>>();
builder.Services.AddScoped<IAsyncRepository<Client>, Repository<Client>>();
builder.Services.AddScoped<IReserveService, ReserveService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
