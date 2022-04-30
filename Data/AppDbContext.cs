namespace V3.Data;
using V3.Models;
using Microsoft.EntityFrameworkCore;
public class AppDataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings
        var connectionString = Configuration.GetConnectionString("conexaoMySQL");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    

    public DbSet<Cliente>? Clientes { get; set; } 
    public DbSet<Produto>? Produtos { get; set; }  
    public DbSet<Carrinho>? Carrinhos { get; set; } 
    public DbSet<Compra>? Compras { get; set; } 

}