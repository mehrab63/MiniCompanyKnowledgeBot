using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Middleware;
using MiniCompanyKnowledgeBot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IKnowledgeService, KnowledgeService>();
builder.Services.AddSingleton<IConversationStore, InMemoryConversationStore>();
builder.Services.AddSingleton<IDocumentStore, InMemoryDocumentStore>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCustomExceptionHandler();
app.MapControllers();

app.Run();
