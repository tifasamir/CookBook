using CodeBook.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace CodeBook.db
{
    public class CodeBookContext : DbContext
    {
     


        //private readonly IConfiguration _configuration;
        //public SalesContext() : base("name=SalesSystemDBConnectionString")
        //{
        //}

        public CodeBookContext(DbContextOptions<CodeBookContext> options) : base(options)
        {

        }

        public CodeBookContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings-sqlite.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
                   // optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseSqlite(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
               // .OwnsOne(p => p.AuthorName)
                .HasData(
             new { SettingId = 1, key = "sLang", value = "" ,desc ="" },
             new { SettingId = 2, key = "sChapter", value = "", desc = "" },
             new { SettingId = 3, key = "sLesson", value = "", desc = "" },
             new { SettingId = 4, key = "sView", value = "", desc = "" },
             new { SettingId = 5, key = "materialurl", value = "", desc = "" },
             new { SettingId = 6, key = "addLangShortCut", value = "", desc = "" },
             new { SettingId = 7, key = "addChapterShortCut", value = "", desc = "" },
             new { SettingId = 8, key = "addLessonShortCut", value = "", desc = "" }
           //  new { SettingId = 9, key = "", value = "", desc = "" }

             );
        }
        public DbSet<Lang> Lang { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Chapter> Chapter { get; set; }
        public DbSet<Viewer> Viewer { get; set; }
        public DbSet<Setting> Setting { get; set; }
        
    }
}
