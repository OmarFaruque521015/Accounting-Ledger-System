using AccountingLedgerSystem.Controllers;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Features.Accounts.Command;
using Infrastructure.Features.Accounts.Handlers;
using Infrastructure.Features.Accounts.Queries;
using Infrastructure.Services;
using Infrastructure.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPlay",
        b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//Connect with Angular project start
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader());
});
//Connect with Angular project End

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(AccountController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Infrastructure services
builder.Services.AddScoped<IStoredProcedureService, StoredProcedureService>();

// Register MediatR
builder.Services.AddMediatR(typeof(AddAccountHandler).Assembly);
builder.Services.AddMediatR(typeof(AddJournalEntryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAccountsCommandHandler).Assembly);

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
app.UseCors("AllowAngular");
app.MapControllers();

app.Run();
