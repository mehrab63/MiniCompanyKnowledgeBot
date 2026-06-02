using MiniCompanyKnowledgeBot.Models;
using MiniCompanyKnowledgeBot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<KnowledgeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapPost("/ask",
    (AskRequest request, KnowledgeService service) =>
    {
        var (answer, source) = service.Ask(request.Question);

        return Results.Ok(new AskResponse
        {
            Answer = answer,
            Source = source
        });
    });

app.UseHttpsRedirection();

app.Run();
