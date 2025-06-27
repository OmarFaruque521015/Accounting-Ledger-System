using Infrastructure;
using Infrastructure.Features.Accounts.Handlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPlay",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
builder.Services.AddMediatR(typeof(AddAccountHandler).Assembly);

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddAccountValidator>();

//Add ConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPlay");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
