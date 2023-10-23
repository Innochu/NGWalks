using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NGWalks.Presentation.Automapper;
using NGWalksApplication;
using NGWalksDTOValidations;
using NGWalksPersistence;
using NGWalksPersistence.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRegionDTOValidation>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NGDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NGWalksConnextionString")));
builder.Services.AddScoped<IRegionRepo, RegionRepo>();
builder.Services.AddAutoMapper(typeof(RegionAutomapper));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
	ValidateIssuer = true,
	ValidateAudience = true,
	ValidateLifetime = true,
	ValidateIssuerSigningKey = true,
	ValidIssuer = builder.Configuration["Jwt:Issuer"],
	ValidAudience = builder.Configuration["Jwt:Audence"],
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

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
