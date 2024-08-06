using Microsoft.EntityFrameworkCore;
using Jobber.Data;
using Jobeer.Workers;
using Jobeer.Services;
using Jobber.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss";
});


builder.Services.AddDbContext<DataContext>(options => options.UseSqlite($"Data Source=db.db"));

builder.Services.AddSingleton<HHruService>();
builder.Services.AddSingleton<SeleniumFactory>();
builder.Services.AddSingleton<HHruEmployeeData>();
builder.Services.AddSingleton<TelegramService>();

builder.Services.AddTransient<SearchModelsService>();

builder.Services.AddHostedService<HHruSender>();

var app = builder.Build();

app.Run();
