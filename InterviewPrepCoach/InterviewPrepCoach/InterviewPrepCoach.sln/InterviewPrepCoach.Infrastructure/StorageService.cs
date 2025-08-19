namespace InterviewPrepCoach.Infrastructure;


public class StorageService
{
    private readonly string _root;
    public StorageService(IWebHostEnvironment env)
    {
        _root = Path.Combine(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
        Directory.CreateDirectory(_root);
    }
    public string SaveFile(string fileName, Stream stream)
    {
        var safeName = string.Concat(Guid.NewGuid().ToString("N"), "_", Path.GetFileName(fileName));
        var fullPath = Path.Combine(_root, safeName);
        using var fs = File.Create(fullPath);
        stream.CopyTo(fs);
        return fullPath; // absolute path for PDFPig
    }
}