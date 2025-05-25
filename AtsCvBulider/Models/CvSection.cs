namespace AtsCvBuilder.Models;

// Models/CvSection.cs
public class CvSection
{
    public int Id { get; set; }
    public string SectionType { get; set; } // ��� "Education", "Experience"
    public string Content { get; set; }

    // ������� �� ������ �������
    public int CVId { get; set; }
    public CV CV { get; set; }
}