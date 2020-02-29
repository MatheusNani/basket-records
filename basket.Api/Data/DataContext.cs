using basket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace basket.Api.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) {

        }

        public DbSet<BasketGame> BasketGame { get; set; }
        public DbSet<RecordGame> RecordGame { get; set; }
    }
}