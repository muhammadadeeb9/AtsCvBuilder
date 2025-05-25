using Microsoft.EntityFrameworkCore;
using AtsCvBuilder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AtsCvBuilder.Data;

// Data/ApplicationDbContext.cs
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CV> CVs { get; set; }
    public DbSet<CvSection> CvSections { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ����� ��������
        builder.Entity<CV>()
            .HasOne<ApplicationUser>() // ������� �� ApplicationUser
            .WithMany() // �� ���� ��� ����� ����� ������ �� ApplicationUser
            .HasForeignKey(c => c.UserId) // ������� UserId ������ �����
            .OnDelete(DeleteBehavior.Cascade); // ����� ������ ��� �����

        // ������� ��� CvSection � CV
        builder.Entity<CvSection>()
            .HasOne(s => s.CV) // ������� �� CV
            .WithMany(c => c.Sections) // ����� ������� �������� ������� �������
            .HasForeignKey(s => s.CVId) // ������� CVId ������ �����
            .OnDelete(DeleteBehavior.Cascade); // ����� ������ ��� �����
    }
}