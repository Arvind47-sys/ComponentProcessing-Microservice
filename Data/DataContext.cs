using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    /// <summary>
    /// DataContext contains all the Db sets of the defined entities
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<ProcessResponse> ProcessResponses { get; set; }
        public DbSet<ProcessRequest> ProcessRequest { get; set; }
    }
}