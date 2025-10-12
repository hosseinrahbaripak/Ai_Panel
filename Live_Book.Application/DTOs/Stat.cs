namespace Ai_Panel.Application.DTOs;
public class Stat
{
    public int Total { get; set; }
    public int Part { get; set; }
    //public double KPI => Total > 0 ? Convert.ToDouble(Part) / Total * 100 : 0;
    public string Title { get; set; }
}
public class StatTimeBased : Stat
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    new public DateTime Title
    {
        get
        {
            return new DateTime(Year, Month, Day);
        }
    }
}