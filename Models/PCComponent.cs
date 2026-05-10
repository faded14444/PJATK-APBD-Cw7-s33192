namespace ZadanieDomowe7.Models;

public class PCComponent
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; } = null!;
    public int Amount { get; set; }

    public virtual PC PC { get; set; } = null!;
    public virtual Component Component { get; set; } = null!;
}

