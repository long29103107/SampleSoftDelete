namespace SampleSoftDelete.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "unknown";
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = "unknown";
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
