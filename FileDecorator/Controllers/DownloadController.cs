using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace FileDecorator.Controllers;

[ApiController]
[Route("d/{id}")]
public class DownloadController : ControllerBase
{
    private readonly IContentService contentService;
    private static readonly Regex IdValidator = new("[a-zA-Z0-9-_]+", RegexOptions.Compiled);

    public DownloadController(IContentService contentService)
    {
        this.contentService = contentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string id)
    {
        if (string.IsNullOrWhiteSpace(id) || !IdValidator.IsMatch(id))
            return BadRequest();
        if (!contentService.TryGet(id, out var metadata) || metadata == null)
            return NotFound();

        var file = new FileStream(metadata.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return new FileStreamResult(file, "application/octet-stream")
        {
            EnableRangeProcessing = true,
            FileDownloadName = metadata.FileName ?? Path.GetFileName(metadata.FilePath)
        };
    }
}
