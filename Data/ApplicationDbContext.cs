
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
        public DbSet<NTR2.Models.Note> Notes { get; set; }
        public DbSet<NTR2.Models.Category> Categories { get; set; }
        public DbSet<NTR2.Models.NoteCategory> NoteCategories { get; set; }


    }
}