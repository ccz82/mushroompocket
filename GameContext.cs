using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket
{
    public class GameContext : DbContext
    {
        /// <summary>
        /// Represents the Character table in the database.
        /// </summary>
        public DbSet<Character> Characters { get; set; }

        /// <summary>
        /// Path to the local SQLite database file.
        /// </summary>
        public string DbPath { get; }

        /// <summary>
        /// Constructor for GameContext.
        /// </summary>
        public GameContext()
        {
            // Path to local SQLite database is set to "./game.db".
            string path = Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Join(path, "game.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
