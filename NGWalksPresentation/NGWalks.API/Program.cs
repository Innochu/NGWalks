using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NGWalks.Presentation.Automapper;
using NGWalksApplication;
using NGWalksDTOValidations;
using NGWalksPersistence;
using NGWalksPersistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRegionDTOValidation>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NGDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NGWalksConnextionString")));
builder.Services.AddScoped<IRegionRepo, RegionRepo>();
builder.Services.AddAutoMapper(typeof(RegionAutomapper));
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
