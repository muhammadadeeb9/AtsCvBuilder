namespace AtsCvBuilder.Models;

// Models/CvSection.cs
public class CvSection
{
    public int Id { get; set; }
    public string SectionType { get; set; } // ãËá "Education", "Experience"
    public string Content { get; set; }

    // ÇáÚáÇŞÉ ãÚ ÇáÓíÑÉ ÇáĞÇÊíÉ
    public int CVId { get; set; }
    public CV CV { get; set; }
}