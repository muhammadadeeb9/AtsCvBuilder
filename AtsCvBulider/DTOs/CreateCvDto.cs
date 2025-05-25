namespace AtsCvBuilder;
// Dtos.cs
public class CvDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public List<SectionDto> Sections { get; set; }
}

public class SectionDto
{
    public string SectionType { get; set; }
    public string Content { get; set; }
}
