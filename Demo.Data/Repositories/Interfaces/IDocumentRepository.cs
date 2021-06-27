using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Data.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        string PutDocument(MemoryStream stream, string fileName, string contentType);
        MemoryStream GetDocument(string fileName);
    }
}
