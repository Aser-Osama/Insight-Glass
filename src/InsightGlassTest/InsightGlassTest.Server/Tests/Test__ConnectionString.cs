using System;
using System.IO;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Moq;
using Xunit;
using NuGet.ContentModel;

public class DatabaseServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseService _databaseService;

    public DatabaseServiceTests()
    {
        _configuration = BuildConfiguration();
        _databaseService = new DatabaseService(_configuration);
    }

    [Fact]
    public void ShouldRetrieveDatabaseConnectionStringFromKeyVault()
    {
        // Act
        var connectionString = _databaseService.GetConnectionString();

        // Assert
        Assert.Equal(Environment.GetEnvironmentVariable("DBLiveConn"), connectionString);
    }

    private IConfiguration BuildConfiguration()
    {
        var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        var configurationBuilder = new ConfigurationBuilder();

        if (File.Exists(envFilePath))
        {
            var envVariables = File.ReadAllLines(envFilePath);
            foreach (var line in envVariables)
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
        }

        var keyVaultUri = new Uri(Environment.GetEnvironmentVariable("KeyVaultUrl"));
        var credential = new DefaultAzureCredential();

        configurationBuilder
            .AddEnvironmentVariables()
            .AddAzureKeyVault(keyVaultUri, credential);

        return configurationBuilder.Build();
    }
}

public class DatabaseService
{
    private readonly IConfiguration _configuration;

    public DatabaseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetSection("DBLiveConn").Value;
    }
}