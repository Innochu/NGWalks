using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NGWalks.Presentation.Automapper;
using NGWalksApplication;
using NGWalksPersistence;
using NGWalksPersistence.Repository;
using NGWalksValidations.DTOFluentValidations;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using NGWalksCommon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("Serilog/NGWalks_Log.txt", rollingInterval:  RollingInterval.Day)
	.MinimumLevel.Information()
	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRegionDTOValidation>());
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterDTOValidations>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NG WALKS API",
		Version = "v1"
	});

	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme

	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme,
					
				},
				Scheme = "Oauth2",
				Name = JwtBearerDefaults.AuthenticationScheme,
				In = ParameterLocation.Header

				
			},
			new List<string>()
		}
	});
});
builder.Services.AddDbContext<NGDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NGWalksConnextionString")));
builder.Services.AddDbContext<NGAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NGWalksAuthConnextionString")));

builder.Services.AddScoped<IRegionRepo, RegionRepo>();
builder.Services.AddScoped<ITokenRepo, TokenRepo>();


builder.Services.AddAutoMapper(typeof(RegionAutomapper));

builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NGWalks")
	.AddEntityFrameworkStores<NGAuthDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
	
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
	ValidateIssuer = true,
	ValidateAudience = true,
	ValidateLifetime = true,
	ValidateIssuerSigningKey = true,
	ValidIssuer = builder.Configuration["Jwt:Issuer"],
	ValidAudience = builder.Configuration["Jwt:Audience"],
	IssuerSigningKey = new SymmetricSecurityKey(
		Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});
//now add authentication settings to the middle ware pipeline
//app.UseAuthentication();     as done below


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandling>();     //registered my global exception handler class
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
