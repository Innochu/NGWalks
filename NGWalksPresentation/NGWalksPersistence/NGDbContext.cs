using Microsoft.EntityFrameworkCore;
using NGWalksDomain.Models;

namespace NGWalksPersistence
{
	public class NGDbContext : DbContext
	{
        public NGDbContext(DbContextOptions<NGDbContext> options) : base(options)
        {
            

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}