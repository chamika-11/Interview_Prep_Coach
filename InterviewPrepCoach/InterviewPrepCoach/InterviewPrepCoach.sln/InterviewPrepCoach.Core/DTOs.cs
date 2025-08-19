namespace InterviewPrepCoach.Core;


public class ResumeInput
{
    public string ResumePath { get; set; } = string.Empty; // used internally after upload
}


public class ResumeOutput
{
    public List<string> Skills { get; set; } = new();
    public string ExperienceSummary { get; set; } = string.Empty;
    public List<string> Gaps { get; set; } = new();
}


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


public class QuestionInput
{
    public ResumeOutput Resume { get; set; } = new();
    public JdOutput JobDescription { get; set; } = new();
    public int Count { get; set; } = 8;
}


public class QuestionItem
{
    public string Question { get; set; } = string.Empty;
    public string AnswerHint { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Medium";
}