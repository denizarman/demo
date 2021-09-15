using Demo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Config
{
    public static class EnvVars
    {
        public const string DatabaseProvider = "DATABASE_PROVIDER";
        public const string DemoContextConnectionString = "DEMOCONTEXT_CONNECTIONSTRING";
        public const string RedisAddress = "REDIS_ADDRESS";
        public const string MinIOEndPoint = "MINIO_ENDPOINT";
        public const string MinIOAccessKey = "MINIO_ACCESSKEY";
        public const string MinIOSecretKey = "MINIO_SECRETKEY";
        public const string MinIOBucketName = "MINIO_BUCKETNAME";
        public const string ElasticSearchUri = "ELASTICSEARCH_URI";
        public const string LogStashUri = "LOGSTASH_URI";

        public static string GetEnvironmentVariable(string environmentVariable)
        {
            var envVar = Environment.GetEnvironmentVariable(environmentVariable);
            if (string.IsNullOrEmpty(envVar))
            {
                throw new EnvironmentVariableException(environmentVariable);
            }
            return envVar;
        }
    }

    public static class DatabaseProviders
    {
        public const string InMemory = "InMemory";
        public const string PgSql = "PgSql";
    }
}
