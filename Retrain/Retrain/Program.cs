using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Retrain.BusinessLogic;
using Retrain.BusinessService;
using Retrain.DataAccess;
using Retrain.DataContracts.Validators;
using Retrain.Persistence;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CountRequestValidator>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IWordsService, WordsService>();
builder.Services.AddScoped<IWordsRepository, WordsRepository>();
builder.Services.AddScoped<IStringProcessorFactory, StringProcessorFactory>();
builder.Services.AddScoped<IStringProcessor, PlainStringProcessor>();
builder.Services.AddScoped<IStringProcessor, PathStringProcessor>();
builder.Services.AddScoped<IStringProcessor, UrlStringProcessor>();
builder.Services.AddScoped<ISentenceProcessor, SentenceProcessor>();

builder.Services.AddDbContext<RetrainDb>(options => options.UseSqlite(builder.Configuration.GetConnectionString("RetrainDb")));

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

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Plain;
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is Exception)
        {
            await context.Response.WriteAsync($"An exception was thrown: {exceptionHandlerPathFeature?.Error.Message}");
        }
    });
});

app.Run();

