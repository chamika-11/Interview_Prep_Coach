using InterviewPrepCoach.MCP;
using InterviewPrepCoach.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddHttpClient<LLMService>(); // for Ollama
builder.Services.AddScoped<ResumeAnalyzerTool>();
builder.Services.AddScoped<JdParserTool>();
builder.Services.AddScoped<QuestionGeneratorTool>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
