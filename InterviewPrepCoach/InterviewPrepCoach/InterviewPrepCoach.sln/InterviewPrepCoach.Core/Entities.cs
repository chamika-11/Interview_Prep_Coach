namespace InterviewPrepCoach.Core;


public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class Session
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "active";
}


public class Resume
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string StoragePath { get; set; } = string.Empty; 
    public string ParsedJson { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class JobDescription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string SourceType { get; set; } = "text"; 
    public string Source { get; set; } = string.Empty; 
    public string ParsedJson { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class QuestionSet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SessionId { get; set; }
    public Guid ResumeId { get; set; }
    public Guid JobDescriptionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid QuestionSetId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string AnswerHint { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Medium";
}


public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public double? Score { get; set; }
    public string FeedbackJson { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}