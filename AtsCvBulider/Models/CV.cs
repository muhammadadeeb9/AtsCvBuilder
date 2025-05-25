namespace AtsCvBuilder.Models;

// Models/CV.cs
public class CV
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }

    // ÚáÇŞÉ Èíä ÇáÓíÑÉ ÇáĞÇÊíÉ æãßæäÇÊåÇ
    public ICollection<CvSection> Sections { get; set; }

    // ÇáÚáÇŞÉ ãÚ ÇáãÓÊÎÏã
    public string UserId { get; set; }
    //public ApplicationUser User { get; set; }
}