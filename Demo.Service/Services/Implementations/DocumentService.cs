using Demo.Data.Repositories.Interfaces;
using Demo.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Service.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public MemoryStream GetDocument(string objectName)
        {
            return _documentRepository.GetDocument(objectName);
        }

        public string PutDocument(MemoryStream stream, string fileName, string contentType)
        {
            var objectName = Guid.NewGuid().ToString().Substring(0, 11) + Path.GetExtension(fileName);
            return _documentRepository.PutDocument(stream, objectName, contentType);
        }
    }
}
