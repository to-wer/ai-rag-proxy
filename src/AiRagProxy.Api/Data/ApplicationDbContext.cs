
using AiRagProxy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiRagProxy.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ProviderConfiguration> ProviderConfigurations { get; set; }
    public DbSet<ModelConfiguration> ModelConfigurations { get; set; }
}
