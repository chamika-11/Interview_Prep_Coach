using InterviewPrepCoach.Core;
using InterviewPrepCoach.Infrastructure;
using InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace InterviewPrepCoach.MCP;

public class QuestionGeneratorTool : IMcpTool<QuestionInput, List<QuestionItem>>
{
    private readonly LLMService _llm;
    public string Name => "question_generator";

    public QuestionGeneratorTool(LLMService llm) => _llm = llm;

    public async Task<List<QuestionItem>> ExecuteAsync(QuestionInput input)
    {
        // Guard & clamp
        var count = Math.Clamp(input.Count <= 0 ? 8 : input.Count, 1, 30);

        var system = "You are an interview coach that creates well-structured interview questions.";

        var user =
$@"Generate {count} diverse interview questions tailored to the candidate. 
Cover both behavioral and technical topics relevant to the resume and job description.

Return ONLY a JSON array (no code fences, no prose). Each item must be an object with keys:
- ""question"": string
- ""answerHint"": string (1–2 sentences)
- ""topic"": string (e.g., "".NET"", ""API Design"", ""Behavioral"")
- ""difficulty"": one of [""Easy"", ""Medium"", ""Hard""]

Resume JSON:
{JsonSerializer.Serialize(input.Resume)}

Job Description JSON:
{JsonSerializer.Serialize(input.JobDescription)}";

        // Ask the model (Ollama via LLMService). It already instructs "Return only JSON".
        var raw = await _llm.GetJsonAsync(system, user);

        // Clean up and coerce to a JSON array if needed
        var json = CoerceToJsonArray(raw);

        // Try to deserialize normally
        try
        {
            var list = JsonSerializer.Deserialize<List<QuestionItem>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            if (list != null) return list;
        }
        catch { /* fall through to fallback */ }

        // Fallback: if it's a single object, wrap it into a list
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                var obj = doc.RootElement;

                string GetStr(string name, string def = "") =>
                    obj.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.String ? v.GetString() ?? def : def;

                var one = new QuestionItem
                {
                    Question = GetStr("question"),
                    AnswerHint = GetStr("answerHint"),
                    Topic = GetStr("topic"),
                    Difficulty = GetStr("difficulty", "Medium")
                };
                return new List<QuestionItem> { one };
            }
        }
        catch { /* ignore */ }

        // Last resort: empty
        return new List<QuestionItem>();
    }

    // Helper: strip code fences and extract the JSON array if present; otherwise wrap object as array
    private static string CoerceToJsonArray(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "[]";
        s = s.Trim();

        // Strip markdown code fences if present
        if (s.StartsWith("```"))
        {
            var firstNewLine = s.IndexOf('\n');
            if (firstNewLine >= 0) s = s[(firstNewLine + 1)..];
            if (s.EndsWith("```")) s = s[..^3];
            s = s.Trim();
        }

        // Prefer array slice
        var startArr = s.IndexOf('[');
        var endArr = s.LastIndexOf(']');
        if (startArr >= 0 && endArr > startArr)
        {
            return s.Substring(startArr, endArr - startArr + 1);
        }

        // Otherwise try object and wrap it
        var startObj = s.IndexOf('{');
        var endObj = s.LastIndexOf('}');
        if (startObj >= 0 && endObj > startObj)
        {
            var obj = s.Substring(startObj, endObj - startObj + 1);
            return "[" + obj + "]";
        }

        return "[]";
    }
}
