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

        // Êßæíä ÇáÚáÇŞÇÊ
        builder.Entity<CV>()
            .HasOne<ApplicationUser>() // ÇáÚáÇŞÉ ãÚ ApplicationUser
            .WithMany() // áÇ ÍÇÌÉ Åáì ÊÍÏíÏ ÎÇÕíÉ ÇáÊäŞá İí ApplicationUser
            .HasForeignKey(c => c.UserId) // ÇÓÊÎÏÇã UserId ßãİÊÇÍ ÃÌäÈí
            .OnDelete(DeleteBehavior.Cascade); // ÊÍÏíÏ ÇáÓáæß ÚäÏ ÇáÍĞİ

        // ÇáÚáÇŞÉ Èíä CvSection æ CV
        builder.Entity<CvSection>()
            .HasOne(s => s.CV) // ÇáÚáÇŞÉ ãÚ CV
            .WithMany(c => c.Sections) // ŞÇÆãÉ ÇáÃŞÓÇã ÇáãÑÊÈØÉ ÈÇáÓíÑÉ ÇáĞÇÊíÉ
            .HasForeignKey(s => s.CVId) // ÇÓÊÎÏÇã CVId ßãİÊÇÍ ÃÌäÈí
            .OnDelete(DeleteBehavior.Cascade); // ÊÍÏíÏ ÇáÓáæß ÚäÏ ÇáÍĞİ
    }
}