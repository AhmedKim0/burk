using System;
using System.Text;

using Burk.Mapper;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Imp;
using Burk.DAL.Repository.Interface;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Burk.BL.Interface;
using Burk.BL.Imp;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BurkDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnetion")));

builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<BurkDbContext>();


builder.Services.AddScoped<IAsyncRepository<WaitingList>, Repository<WaitingList>>();
builder.Services.AddScoped<IAsyncRepository<Client>, Repository<Client>>();
builder.Services.AddScoped<IAsyncRepository<TempUser>, Repository<TempUser>>();
builder.Services.AddScoped<IAsyncRepository<Review>, Repository<Review>>();
builder.Services.AddScoped<IAsyncRepository<Question>, Repository<Question>>();



builder.Services.AddScoped<IReserveService, ReserveService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IReviewService, ReviewService>();



builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddAuthentication(o =>
{
	o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.RequireHttpsMetadata = false;
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidateAudience = false,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
	};
});
builder.Services.AddSwaggerGen(o =>
{
	o.SwaggerDoc("v1", new OpenApiInfo()
	{
		Version = "v1",
		Title = "Burk api",
		Description = "adasdsad",
		Contact = new OpenApiContact()
		{
			Name = "Ahmedkimo",
			Email = "a.medhat.kimo@gmail.com",
			Url = new Uri("https://mydomain.com")
		}
	});

	o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter the JWT Key"
	});

	o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
					{
					   new OpenApiSecurityScheme()
					   {
						  Reference = new OpenApiReference()
						  {
							 Type = ReferenceType.SecurityScheme,
							 Id = "Bearer"
						  },
						  Name = "Bearer",
						  In = ParameterLocation.Header
					   },
					   new List<string>()
					}
				});
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	await SeedRole.SeedRoles(roleManager);
}

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
