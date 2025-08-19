using InterviewPrepCoach.Core;
using InterviewPrepCoach.Infrastructure;
using InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP;


namespace InterviewPrepCoach.MCP;


public class JdParserTool : IMcpTool<JdInput, JdOutput>
{
    private readonly LLMService _llm;
    public string Name => "jd_parser";


    public JdParserTool(LLMService llm) => _llm = llm;


    public async Task<JdOutput> ExecuteAsync(JdInput input)
    {
        var system = "You parse job descriptions into structured JSON for recruiters.";
        var user = "Parse into JSON with keys: skills[], responsibilities[], niceToHaves[].\nJD Text:\n" + input.JobDescriptionText;
        var json = await _llm.GetJsonAsync(system, user);
        try { return System.Text.Json.JsonSerializer.Deserialize<JdOutput>(json) ?? new(); }
        catch { return new JdOutput(); }
    }
}