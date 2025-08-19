using System.Text;
using InterviewPrepCoach.Core;
using InterviewPrepCoach.Infrastructure;
using InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP;
using UglyToad.PdfPig;

namespace InterviewPrepCoach.MCP;

public class ResumeAnalyzerTool : IMcpTool<ResumeInput, ResumeOutput>
{
    private readonly LLMService _llm;
    public string Name=> "resume_analyzer";
    public ResumeAnalyzerTool(LLMService llm)=> _llm = llm;

    public async Task<ResumeOutput> ExecuteAsync (ResumeInput input)
    {
        var text = new StringBuilder();
        using (var pdf = PdfDocument.Open(input.ResumePath))
        {
            foreach (var page in pdf.GetPages()) text.AppendLine(page.Text);
        }


        var system = "You are an expert career coach that extracts structured data from resumes.";
        var user = "Extract the following strictly as JSON with keys: skills (array of strings), experienceSummary (string), gaps (array of strings).\nResume Text:\n" + text.ToString();


        var json = await _llm.GetJsonAsync(system, user);
        try { return System.Text.Json.JsonSerializer.Deserialize<ResumeOutput>(json) ?? new(); }
        catch { return new ResumeOutput(); }
    }
}