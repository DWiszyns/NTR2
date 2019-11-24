
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NTR2.Models;

namespace NTR2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
            .HasIndex(b => b.Title).IsUnique();
            builder.Entity<Note>()
            .HasIndex(b => b.Title).IsUnique();
            builder.Entity<NoteCategory>()
            .HasKey(c => new { c.CategoryID, c.NoteID });
        }
        public DbSet<NTR2.Models.Note> Notes { get; set; }
        public DbSet<NTR2.Models.Category> Categories { get; set; }
        public DbSet<NTR2.Models.NoteCategory> NoteCategories { get; set; }

    }
}