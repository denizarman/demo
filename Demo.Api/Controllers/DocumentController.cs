using Demo.Api.Dtos;
using Demo.Api.Helpers;
using Demo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public IActionResult UploadDocument([FromForm] IFormFile file)
        {
            var docName = _documentService.PutDocument(FileToStreamHelper.FileToStream(file), file.FileName, file.ContentType);
            return Ok(docName);
        }

        [HttpGet]
        public FileResult DownloadDocument([FromHeader] string fileName)
        {
            var documentStream = _documentService.GetDocument(fileName);
            return File(documentStream, MimeTypeHelper.GetMimeType(fileName), fileName);
        }
    }
}
