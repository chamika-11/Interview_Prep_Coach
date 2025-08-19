using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly ResumeAnalyzerTool _resumeTool;
    private readonly JdParserTool _jdTool;
    private readonly QuestionGeneratorTool _questionTool;

    public InterviewController()
    {
        _resumeTool = new ResumeAnalyzerTool();
        _jdTool = new JdParserTool();
        _questionTool = new QuestionGeneratorTool();
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
