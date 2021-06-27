using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Api.Helpers
{
    public static class FileToStreamHelper
    {
        public static MemoryStream FileToStream(IFormFile formFile)
        {
            MemoryStream stream = new MemoryStream();
            formFile.CopyTo(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
