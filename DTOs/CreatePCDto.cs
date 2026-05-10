namespace ZadanieDomowe7.DTOs;

public class CreatePCDto
{
    public string Name { get; set; } = null!;
    public decimal Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

