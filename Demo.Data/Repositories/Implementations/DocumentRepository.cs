using Demo.Core.Config;
using Demo.Data.Repositories.Interfaces;
using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Data.Repositories.Implementations
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MinioClient _minioClient;
        private readonly string _endPoint;
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;

        public DocumentRepository()
        {
            _endPoint = EnvVars.GetEnvironmentVariable(EnvVars.MinIOEndPoint);
            _accessKey = EnvVars.GetEnvironmentVariable(EnvVars.MinIOAccessKey);
            _secretKey = EnvVars.GetEnvironmentVariable(EnvVars.MinIOSecretKey);
            _bucketName = EnvVars.GetEnvironmentVariable(EnvVars.MinIOBucketName);

            _minioClient = new MinioClient(_endPoint, _accessKey, _secretKey);
        }

        public MemoryStream GetDocument(string fileName)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                //Eğer ilgili bucket altında ismi verilen object yer almıyorsa bu metod bize hata fırlatacaktır.
                _minioClient.StatObjectAsync(_bucketName, fileName).Wait();

                _minioClient.GetObjectAsync(_bucketName, fileName,
                                    (stream) =>
                                    {
                                        stream.CopyTo(memoryStream);
                                    }).Wait();
                memoryStream.Position = 0;

            }
            catch(Exception ex)
            {
                throw new FileNotFoundException(string.Format("File Not Found {0} at bucket {1}", fileName, _bucketName));
            }
            return memoryStream;
        }

        public string PutDocument(MemoryStream stream, string fileName, string contentType)
        {
            bool isFound = _minioClient.BucketExistsAsync(_bucketName).Result;
            if (!isFound)
            {
                _minioClient.MakeBucketAsync(_bucketName).Wait();
            }

            _minioClient.PutObjectAsync(_bucketName, fileName, stream, stream.Length, contentType).Wait();
            return fileName;
        }
    }
}
