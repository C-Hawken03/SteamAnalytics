using Microsoft.EntityFrameworkCore;
//Talks to Database and therefore infrastructure
using SteamAnalytics.Domain;

namespace SteamAnalytics.Infrastructure.Persistence {
    public sealed class SteamAnalyticsDbContext : DbContext {
        public SteamAnalyticsDbContext(DbContextOptions<SteamAnalyticsDbContext> options)
            : base(options) {
        }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<PlayerSnapshot> PlayerSnapshots => Set<PlayerSnapshot>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            ConfigureGame(modelBuilder);
            ConfigureTag(modelBuilder);
            ConfigurePlayerSnapshot(modelBuilder); 


        }
        private static void ConfigureGame(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasMany(g => g.Tags)
                  .WithMany(t => t.Games)
                  .UsingEntity<Dictionary<string, object>>(
                      "gametags",
                      j => j
                          .HasOne<Tag>()
                          .WithMany()
                          .HasForeignKey("TagsId"),
                      j => j
                          .HasOne<Game>()
                          .WithMany()
                          .HasForeignKey("GamesId")
                  );

            });
        }
        private static void ConfigureTag(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasIndex(t => t.Name)
                      .IsUnique();

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
        private static void ConfigurePlayerSnapshot(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PlayerSnapshot>(entity =>
            {
                entity.ToTable("game_player_snapshots");

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.GameId, e.Timestamp });

                entity.Property(e => e.Timestamp)
                      .HasColumnType("datetime(6)");

                entity.HasOne(e => e.Game)
                      .WithMany()
                      .HasForeignKey(e => e.GameId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }


}
