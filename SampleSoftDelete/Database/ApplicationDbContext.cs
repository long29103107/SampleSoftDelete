using Microsoft.EntityFrameworkCore;
using SampleSoftDelete.Entities;

namespace SampleSoftDelete.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Book { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasQueryFilter(b => b.DeletedAt == null);
    }
    public override int SaveChanges()
    {
        HandleDelete();
        HandleAdd();
        HandleUpdate();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        HandleDelete();
        HandleAdd();
        HandleUpdate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void HandleDelete()
    {
        var entities = ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Deleted);

        foreach (var entity in entities)
        {
            if (entity.Entity is Book)
            {
                entity.State = EntityState.Modified;
                var book = entity.Entity as Book;
                book.DeletedAt = DateTime.UtcNow;
                book.DeletedBy = "unknow";
            }
        }
    }
    private void HandleAdd()
    {
        var entities = ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Added);

        foreach (var entity in entities)
        {
            if (entity.Entity is Book)
            {
                var book = entity.Entity as Book;
                book.CreatedAt = DateTime.UtcNow;
                book.CreatedBy = "unknow";
                book.UpdatedAt = DateTime.UtcNow;
                book.UpdatedBy = "unknow";
            }
        }
    }

    private void HandleUpdate()
    {
        var entities = ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in entities)
        {
            if (entity.Entity is Book)
            {
                var book = entity.Entity as Book;
                book.UpdatedAt = DateTime.UtcNow;
                book.UpdatedBy = "unknow";
            }
        }
    }
}
