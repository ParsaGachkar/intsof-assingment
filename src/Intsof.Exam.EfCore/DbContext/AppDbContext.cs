using Intsof.Exam.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Intsof.Exam.EfCore.DbContext;

public class AppDbContext:Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(q => q.Id);
        });
    }
}