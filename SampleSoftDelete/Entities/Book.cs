namespace SampleSoftDelete.Entities;

public class Book : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}
