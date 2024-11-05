using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Storage.Application.Common.Interfaces;

namespace Storage.Persistence;

public class StorageDbContextFactory : DesignTimeDbContextFactoryBase<StorageDbContext>
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public StorageDbContextFactory(IDateTime dateTime, ICurrentUserService currentUserService)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    public StorageDbContextFactory()
    {
        
    }
    
    protected override StorageDbContext CreateNewInstance(DbContextOptions<StorageDbContext> options)
    {
        return new StorageDbContext(options, _currentUserService, _dateTime);
    }
}

public abstract class DesignTimeDbContextFactoryBase<TContext> :
    IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string ConnectionStringName = "LoyaltyCoreConnection";
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}LoyaltyCore.Api", Path.DirectorySeparatorChar);
        var environment = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
        Console.WriteLine($"Environment  >> '{environment}'.");
        return Create(basePath, environment);
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string basePath, string environmentName)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        return Create(connectionString);
    }

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
        }

        Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return CreateNewInstance(optionsBuilder.Options);
    }
}