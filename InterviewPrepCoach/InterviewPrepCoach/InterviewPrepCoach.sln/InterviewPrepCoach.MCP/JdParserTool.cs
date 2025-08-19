using InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP;

public class JdInput
{
    public string JobDescriptionText { get; set; } = string.Empty;
}

public class JdOutput
{
    public List<string> Skills { get; set; } = new();
    public List<string> Responsibilities { get; set; } = new();
    public List<string> NiceToHaves { get; set; } = new();
}

public class JdParserTool : IMcpTool<JdInput, JdOutput>
{
    public string Name => "jd_parser";

    public async Task<JdOutput> ExecuteAsync(JdInput input)
    {
        // Call LLM to parse job description (stubbed for now)
        return new JdOutput
        {
            Skills = new List<string> { "Angular", ".NET", "REST APIs" },
            Responsibilities = new List<string> { "Develop APIs", "Write tests" },
            NiceToHaves = new List<string> { "Azure knowledge" }
        };
    }
}
