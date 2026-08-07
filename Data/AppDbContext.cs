using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<IdentityUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Health" },
                new Category { Id = 3, Name = "Lifestyle" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post 
                {
                    Id = 1,
                    Title = "Technology Post 1",
                    Content = "This is the content of the Technology post.",
                    CategoryId = 1,
                    PublishedDate = new DateTime(2023, 1, 1),
                    Author = "John Doe",
                    FeatureImagePath = "https://plus.unsplash.com/premium_photo-1683121716061-3faddf4dc504?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHRlY2hub2xvZ3l8ZW58MHx8MHx8fDA%3D"
                },
                new Post 
                { 
                    Id = 2,
                    Title = "Health Post 1",
                    Content = "This is the content of the Health post.",
                    CategoryId = 2,
                    PublishedDate = new DateTime(2023, 2, 1),
                    Author = "Jane Smith",
                    FeatureImagePath = "https://images.unsplash.com/photo-1477332552946-cfb384aeaf1c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8SGVhbHRofGVufDB8fDB8fHww",
                    
                },
                new Post 
                { 
                    Id = 3,
                    Title = "Lifestyle Post 1",
                    Content = "This is the content of the Lifestyle post.",
                    CategoryId = 3,
                    PublishedDate = new DateTime(2023, 3, 1),
                    Author = "Alice Johnson",
                    FeatureImagePath = "https://images.unsplash.com/photo-1511988617509-a57c8a288659?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8TGlmZXN0eWxlfGVufDB8fDB8fHww"
                }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
