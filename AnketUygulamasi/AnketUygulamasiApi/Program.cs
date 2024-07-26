using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDBContext>();

builder.Services.AddScoped<ISurveyService, SurveyManager>();
builder.Services.AddScoped<ISurveyDal, EfSurveyDal>();

builder.Services.AddScoped<IQuestionService, QuestionManager>();
builder.Services.AddScoped<IQuestionDal, EfQuestionDal>();

builder.Services.AddScoped<IAnswerService, AnswerManager>();
builder.Services.AddScoped<IAnswerDal, EfAnswerDal>();

builder.Services.AddControllers()
	.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
