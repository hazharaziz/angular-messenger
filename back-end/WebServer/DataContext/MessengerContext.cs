using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebServer;
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

        public virtual DbSet<Direct> Directs { get; set; }
        public virtual DbSet<DirectMessage> DirectMessages { get; set; }
        public virtual DbSet<Follower> Followers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<GroupMessage> GroupMessages { get; set; }
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

            modelBuilder.Entity<DirectMessage>(entity =>
            {
                entity.HasOne(d => d.Direct)
                    .WithMany(p => p.DirectMessages)
                    .HasForeignKey(d => d.DirectId)
                    .HasConstraintName("FK_DirectMessages_Directs");
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.Property(e => e.Pending).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupMembers_GroupMembers");
            });

            modelBuilder.Entity<GroupMessage>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMessages)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupMessages_GroupMessages");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.ReplyToId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Composer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ComposerId)
                    .HasConstraintName("FK_Messages_Users");
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
