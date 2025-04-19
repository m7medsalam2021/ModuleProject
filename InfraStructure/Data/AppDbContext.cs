using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Tech" },
                new Category { Id = 2, Name = "Prog" }
            );
            // Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Post 1",
                    Image = "x.jpg",
                    Content = "New Content",
                    CreatedAt = new DateTime(2025,4,2),
                    CategoryId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "Post 2",
                    Image = "y.jpg",
                    Content = "New Content 2",
                    CreatedAt = new DateTime(2025,4,2),
                    CategoryId = 2
                }
            );
            // Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    UserName = "User 1",
                    Content = "Content 1",
                    CommentDate = new DateTime(2025,4,2),
                    PostId = 1
                },
                new Comment
                {
                    Id = 2,
                    UserName = "User 2",
                    Content = "Content 2",
                    CommentDate = new DateTime(2025,4,2),
                    PostId = 2
                },
                new Comment
                {
                    Id = 3,
                    UserName = "User 3",
                    Content = "Content 3",
                    CommentDate = new DateTime(2025,4,2),
                    PostId = 2
                },
                new Comment
                {
                    Id = 4,
                    UserName = "User 4",
                    Content = "Content 4",
                    CommentDate = new DateTime(2025,4,2),
                    PostId = 1
                }
            );
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<User> Users { get; set; }

    }
}
