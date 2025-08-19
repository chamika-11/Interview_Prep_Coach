using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace InterviewPrepCoach.Infrastructure;


public class LLMService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;


    public LLMService(HttpClient http, string? baseUrl = null)
    {
        _http = http;
        _baseUrl = baseUrl ?? "http://localhost:11434"; // Ollama default
    }


    public async Task<string> GetJsonAsync(string systemPrompt, string userPrompt)
    {
        // We build a strict prompt asking for JSON only
        var prompt = "" + systemPrompt + "\n\nReturn only valid JSON with no extra commentary.\n\n" + userPrompt;
        var payload = new { model = "llama3", prompt = prompt, stream = false };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(_baseUrl + "/api/generate", content);
        response.EnsureSuccessStatusCode();
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return doc.RootElement.GetProperty("response").GetString() ?? "{}";
    }
}