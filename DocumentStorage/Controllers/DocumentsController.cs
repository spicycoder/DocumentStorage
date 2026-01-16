using DocumentStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        
        // Actual upload logic go here
        return Ok(new DocumentUploadResponse(Guid.NewGuid().ToString(), file.FileName, "uploaded"));
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListDocuments()
    {
        // Actual listing logic go here
        Dictionary<string, string> documents = new()
        {
            { Guid.NewGuid().ToString(), "document1.pdf" },
            { Guid.NewGuid().ToString(), "document2.pdf" },
        };
        return Ok(new DocumentsListResponse(documents));
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadDocument(string id)
    {
        // Actual download logic go here
        byte[] fileBytes = Array.Empty<byte>(); // Placeholder for file bytes
        string fileName = "document.pdf"; // Placeholder for file name
        return File(fileBytes, "application/pdf", fileName);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDocument(string id)
    {
        // Actual delete logic go here
        return Ok("File deleted successfully.");
    }
}
