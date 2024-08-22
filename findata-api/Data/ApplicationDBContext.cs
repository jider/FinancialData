using Microsoft.EntityFrameworkCore;
using findata_api.Models;

namespace findata_api.Data;

public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}


