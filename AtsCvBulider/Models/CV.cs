namespace AtsCvBuilder.Models;

// Models/CV.cs
public class CV
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }

    // ����� ��� ������ ������� ���������
    public ICollection<CvSection> Sections { get; set; }

    // ������� �� ��������
    public string UserId { get; set; }
    //public ApplicationUser User { get; set; }
}