using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebServer.Models;

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
                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.FollowerFollowerNavigations)
                    .HasForeignKey(d => d.FollowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Followers__Follo__32E0915F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowerUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Followers__UserI__31EC6D26");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Text).HasDefaultValueSql("('')");

                entity.HasOne(d => d.Composer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ComposerId)
                    .HasConstraintName("FK__Messages__Compos__276EDEB3");

                entity.HasOne(d => d.ReplyTo)
                    .WithMany(p => p.InverseReplyTo)
                    .HasForeignKey(d => d.ReplyToId)
                    .HasConstraintName("FK__Messages__ReplyT__286302EC");
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
