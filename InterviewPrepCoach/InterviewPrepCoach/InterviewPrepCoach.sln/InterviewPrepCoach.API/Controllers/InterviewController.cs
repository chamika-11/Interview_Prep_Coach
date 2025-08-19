using InterviewPrepCoach.Core;
using InterviewPrepCoach.MCP;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly ResumeAnalyzerTool _resumeTool;
    private readonly JdParserTool _jdTool;
    private readonly QuestionGeneratorTool _questionTool;

    // Tools are injected by ASP.NET Core
    public InterviewController(
        ResumeAnalyzerTool resumeTool,
        JdParserTool jdTool,
        QuestionGeneratorTool questionTool)
    {
        _resumeTool = resumeTool;
        _jdTool = jdTool;
        _questionTool = questionTool;
    }

    [HttpPost("analyze-resume")]
    public async Task<IActionResult> AnalyzeResume([FromBody] ResumeInput input)
    {
        var result = await _resumeTool.ExecuteAsync(input);
        return Ok(result);
    }

    [HttpPost("parse-jd")]
    public async Task<IActionResult> ParseJd([FromBody] JdInput input)
    {
        var result = await _jdTool.ExecuteAsync(input);
        return Ok(result);
    }

    [HttpPost("generate-questions")]
    public async Task<IActionResult> GenerateQuestions([FromBody] QuestionInput input)
    {
        var result = await _questionTool.ExecuteAsync(input);
        return Ok(result);
    }
}
