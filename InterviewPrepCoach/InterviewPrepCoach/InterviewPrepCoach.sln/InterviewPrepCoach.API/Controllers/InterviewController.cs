using InterviewPrepCoach.Core;
using InterviewPrepCoach.MCP;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using UglyToad.PdfPig;
using Xceed.Words.NET;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly ResumeAnalyzerTool _resumeTool;

    public InterviewController(ResumeAnalyzerTool resumeTool)
    {
        _resumeTool = resumeTool;
    }

    [HttpPost("upload-resume")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(10_000_000)] // ~10MB limit
    public async Task<IActionResult> UploadResume([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "resumes");
        Directory.CreateDirectory(uploadFolder);

        var filePath = Path.Combine(uploadFolder, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        string extractedText = await ExtractTextFromFile(filePath);
        var input = new ResumeInput { Text = extractedText };
        var result = await _resumeTool.ExecuteAsync(input);

        return Ok(result);
    }


    private async Task<string> ExtractTextFromFile(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();

        if (ext == ".txt")
            return await System.IO.File.ReadAllTextAsync(filePath);

        if (ext == ".pdf")
        {
            using var pdf = PdfDocument.Open(filePath);
            var text = new StringBuilder();
            foreach (var page in pdf.GetPages())
                text.AppendLine(page.Text);
            return text.ToString();
        }

        if (ext == ".docx")
        {
            using var doc = DocX.Load(filePath);
            return doc.Text;
        }

        return $"Unsupported file format: {ext}";
    }
}
