using DDDSample.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.DataContext;

public class CustomerDataContext : DbContext
{
    private readonly string _dbPath;
    public CustomerDataContext()
    {
        _dbPath = Path.Join(Directory.GetCurrentDirectory(), "customer.db");
    }



    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");

    public DbSet<Model.Customer> Customer => Set<Model.Customer>();

}