namespace InterviewPrepCoach.InterviewPrepCoach.sln.InterviewPrepCoach.MCP
{
    public interface IMcpTool<TInput, TOutput>
    {
        string Name { get; }
        Task<TOutput> ExecuteAsync(TInput input);
    }

}
