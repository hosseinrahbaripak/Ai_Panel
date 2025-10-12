namespace Live_Book.Application.DTOs;
public class IdTitleTimeBased
{
	public int Year { get; set; }
	public int? Month { get; set; }
	public int? Day { get; set; }
	public virtual int Id { get; set; }
    public virtual int? Group { get; set; }
	public int? GroupCount { get; set; }
    public DateTime Title
	{
		get
		{
			return new DateTime(Year, Month ?? 1, Day ?? 1);
		}
	}
}
