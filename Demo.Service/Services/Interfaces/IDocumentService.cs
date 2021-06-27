using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Service.Services.Interfaces
{
    public interface IDocumentService
    {
        string PutDocument(MemoryStream stream, string fileName, string contentType);
        MemoryStream GetDocument(string fileName);
    }
}
