using Microsoft.AspNetCore.Identity;

namespace AtsCvBuilder.Models;

// Models/ApplicationUser.cs
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Description { get; set; }
    public bool IsEmailVerified { get; set; } = false;

    // ⁄·«ﬁ… »Ì‰ «·„” Œœ„ Ê«·”Ì—… «·–« Ì…
    public ICollection<CV> CVs { get; set; }
}