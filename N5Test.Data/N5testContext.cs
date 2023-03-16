using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using N5Test.Data.Models.Permissions;
using N5Test.Data.Models.PermissionTypes;
using N5Test.Util.Configurations;

namespace N5Test.Data;

public partial class N5testContext : DbContext
{
    private readonly IOptions<DataBaseConfiguration> options;

    public N5testContext(IOptions<DataBaseConfiguration> options)
    {
        this.options = options;
    }

    public virtual DbSet<PermisionType> PermisionTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        string connectionString = this.options.Value.DefaultConnection;
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermisionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permisio__3214EC072651D04F");

            entity.Property(e => e.Description).HasMaxLength(150);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC074D25020C");

            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.EmpleyeeForename).HasMaxLength(150);
            entity.Property(e => e.EnployeeSurname).HasMaxLength(150);
            entity.Property(e => e.PermissionDate).HasColumnType("date");

            entity.HasOne(d => d.PermissionType).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permissio__Permi__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
