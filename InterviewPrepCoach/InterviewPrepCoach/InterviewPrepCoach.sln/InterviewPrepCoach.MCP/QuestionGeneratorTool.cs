using InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP;

public class QuestionInput
{
    public ResumeOutput Resume { get; set; } = new();
    public JdOutput JobDescription { get; set; } = new();
    public int Count { get; set; } = 5;
}

public class QuestionItem
{
    public string Question { get; set; } = string.Empty;
    public string AnswerHint { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Medium";
}

public class QuestionGeneratorTool : IMcpTool<QuestionInput, List<QuestionItem>>
{
    public string Name => "question_generator";

    public async Task<List<QuestionItem>> ExecuteAsync(QuestionInput input)
    {
        // Call LLM to generate questions (stubbed for now)
        return new List<QuestionItem>
        {
            new QuestionItem
            {
                Question = "Tell me about a .NET project you built.",
                AnswerHint = "Mention backend APIs, Entity Framework.",
                Topic = ".NET Development"
            },
            new QuestionItem
            {
                Question = "How do you handle API versioning?",
                AnswerHint = "Discuss semantic versioning or controllers.",
                Topic = "API Design"
            }
        };
    }
}
