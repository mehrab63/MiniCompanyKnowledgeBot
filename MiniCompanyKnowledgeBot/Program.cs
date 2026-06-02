using MiniCompanyKnowledgeBot.Models;
using MiniCompanyKnowledgeBot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<KnowledgeService>();
 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapPost("/ask",
    (AskRequest request,
     KnowledgeService service) =>
    {
        var result = service.Ask(request.Question);

        return Results.Ok(new AskResponse
        {
            Answer = result.answer,
            Source = result.source
        });
    });

app.UseHttpsRedirection();

app.Run();
