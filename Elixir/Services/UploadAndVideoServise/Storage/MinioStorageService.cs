using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Elixir.Services.StaticService.Storage;

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _minioEndpoint = "192.168.1.24:49906"; // Change for your setup
    private readonly string _accessKey = "VNOvEPKsq29OpJZPW45O";
    private readonly string _secretKey = "ujbdm2PyFRplXqIGpPEtPy7Ark7FRu6hX3w0ta69";

    public MinioStorageService()
    {
        _minioClient = new MinioClient()
            .WithEndpoint(_minioEndpoint)
            .WithCredentials(_accessKey, _secretKey)
            .WithSSL(false)
            .Build();
    }

    public async Task UploadFileAsync(string bucketName, string key, string filePath, string contentType)
    {
        try
        {
            bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!found)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(key)
                .WithFileName(filePath)
                .WithContentType(contentType));
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"MinIO Upload Error: {ex.Message}");
        }
    }

    public string GetFileUrl(string bucketName, string key)
    {
        return $"{_minioEndpoint}/{bucketName}/{key}";
    }
}