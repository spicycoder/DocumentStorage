using DocumentStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController(ILogger<DocumentsController> logger) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            logger.LogError("⚠️ File appears to be empty / invalid");
            return BadRequest("No file uploaded.");
        }

        // Actual upload logic go here
        string documentId = Guid.NewGuid().ToString();
        logger.LogInformation("📩 File upload successful: {FileId} - {FileName}", documentId, file.FileName);
        return Ok(new DocumentUploadResponse(documentId, file.FileName, "uploaded"));
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListDocuments()
    {
        logger.LogInformation("📃 Listing all files");

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
        logger.LogInformation("📥 Downloading file: {Id}", id);

        // Actual download logic go here
        byte[] fileBytes = Array.Empty<byte>(); // Placeholder for file bytes
        string fileName = "document.pdf"; // Placeholder for file name
        return File(fileBytes, "application/pdf", fileName);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteDocument(string id)
    {
        logger.LogInformation("🗑️ Deleting file: {Id}", id);

        // Actual delete logic go here
        return Ok("File deleted successfully.");
    }
}
