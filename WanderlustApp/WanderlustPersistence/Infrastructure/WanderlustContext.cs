using Microsoft.EntityFrameworkCore;
using WanderlustPersistence.Entity;

namespace WanderlustPersistence.Infrastructure
{
    /// <summary>
    /// Database context for the travel log application
    /// </summary>
    public class WanderlustContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">Database context options</param>
        public WanderlustContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Database set for users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Database set for countries
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Database set for towns
        /// </summary>
        public DbSet<Town> Towns { get; set; }

        /// <summary>
        /// Database set for traditional foods
        /// </summary>
        public DbSet<TraditionalFood> Foods { get; set; }

        /// <summary>
        /// Database set for sights
        /// </summary>
        public DbSet<Sight> Sight { get; set; }

        /// <summary>
        /// Database set for regions
        /// </summary>
        public DbSet<Region> Regions { get; set; }
    }
}
