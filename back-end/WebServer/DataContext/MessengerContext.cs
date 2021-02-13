using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebServer.Models.DBModels;

#nullable disable

namespace WebServer.DataContext
{
    public partial class MessengerContext : DbContext
    {
        public MessengerContext()
        {
        }

        public MessengerContext(DbContextOptions<MessengerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Follower> Followers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.Property(e => e.Pending).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.Followers)
                    .HasForeignKey(d => d.FollowerId)
                    .HasConstraintName("FK_Followers_Followers");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.ReplyToId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Composer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ComposerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Users");

                entity.HasOne(d => d.ReplyTo)
                    .WithMany(p => p.InverseReplyTo)
                    .HasForeignKey(d => d.ReplyToId)
                    .HasConstraintName("FK_Messages_Messages1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
