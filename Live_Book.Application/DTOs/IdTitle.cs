namespace Live_Book.Application.DTOs;

public class IdTitle
{
    public int Id { get; set; }
    public string Title { get; set; }
}
public class GuidTitle
{
	public Guid Id { get; set; }
	public string Title { get; set; }
}
public class IdTitleAvg : IdTitle
{
	public double? Avg { get; set; }	
}