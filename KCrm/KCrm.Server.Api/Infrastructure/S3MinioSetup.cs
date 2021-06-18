using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class S3MinioSetup {

        public static IServiceCollection AddS3Minio(this IServiceCollection collection, IConfiguration configuration) {
            
            var minioSecret = configuration["Minio:MinioSecret"];
            var minioKey = configuration["Minio:MinioKey"];
            var credentials = new BasicAWSCredentials (minioKey, minioSecret);
            
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.EUWest1, // MUST set this before setting ServiceURL and it should match the `MINIO_REGION` environment variable.
                ServiceURL =  configuration["Minio:MinioEndpoint"], // replace http://localhost:9000 with URL of your MinIO server
                ForcePathStyle = true, // MUST be true to work correctly with MinIO server, 
            };
            
            collection.AddScoped<IAmazonS3> (x=>new AmazonS3Client (credentials, config));
            
            return collection;
        }
    }
}
