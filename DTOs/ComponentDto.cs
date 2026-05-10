namespace ZadanieDomowe7.DTOs;

public class ComponentDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Amount { get; set; }
}

