using Ai_Panel.Application.Enum;
using Ai_Panel.Application.Tools;
using System.Globalization;
namespace Ai_Panel.Application.DTOs;

public class DateValueDto
{
    public List<IdTitleTimeBased> DateValue { get; set; }
    public TimeType TimeType { get; set; }
    public static PersianCalendar PC = new PersianCalendar();
    public string Titles => string.Join(',', GetGroupedData().Select(g => g.Title));
    public string Values => string.Join(',', GetGroupedData().Select(g => g.Id));
    public double TotalAverage => GetGroupedData().Select(g => g.Id).Count() > 0 ? Convert.ToDouble(GetGroupedData().Select(g => g.Id).Sum()) / GetGroupedData().Select(g => g.Id).Count() : 0;
    public string Averages => GetGroupedData().All(x => x.Avg != null) ? string.Join(',', GetGroupedData().Select(g => g.Avg)) : "";
	protected new List<IdTitleAvg> GetGroupedData()
    {
        if (DateValue == null || !DateValue.Any()) return new List<IdTitleAvg>();

        return TimeType switch
        {
            TimeType.Daily => DateValue
                .GroupBy(u => PC.GetYear(u.Title) + "/" + PC.GetMonth(u.Title) + "/" + PC.GetDayOfMonth(u.Title))
                .Select(g => new IdTitleAvg
                {
                    Title = g.Key,
                    Id = g.Sum(x => x.Id),
                    Avg = g.All(x => x.Group == null || x.GroupCount == null || x.GroupCount == 0)
                    ? null  
                    : Convert.ToDouble(g.Sum(x => x.Group)) / g.First().GroupCount

                }).ToList(),

            TimeType.Weekly => DateValue
            .GroupBy(u =>
            {
                // Align each date to the start of its Persian week
                DateTime alignedDate = AlignToPersianWeekStart(u.Title);
                // Get Persian year and week number
                int persianYear = PC.GetYear(alignedDate);
                int weekOfYear = GetPersianWeekOfYear(alignedDate);
                // Create a unique key for each Persian year-week combination
                return $"{persianYear}-{weekOfYear}";
            })
            .Select(g =>
            {
                // Format the week range for the period
                var firstDay = AlignToPersianWeekStart(g.Min(x => x.Title));
                var lastDay = firstDay.AddDays(6); // End of the week
                return new IdTitleAvg
                {
                    Title = $"{PC.GetYear(firstDay)}/{PC.GetMonth(firstDay):00}/{PC.GetDayOfMonth(firstDay):00} - " +
                             $"{PC.GetYear(lastDay)}/{PC.GetMonth(lastDay):00}/{PC.GetDayOfMonth(lastDay):00}",
                    Id = g.Sum(x => x.Id), // Sum up all values in the group,
                    Avg = g.All(x => x.Group == null || x.GroupCount == null || x.GroupCount == 0)
                    ? null
                    : Convert.ToDouble(g.Sum(x => x.Group)) / g.First().GroupCount
                };
            }).ToList(),


            TimeType.Monthly => DateValue
                .GroupBy(u =>
                PC.GetMonth(u.Title).GetShamsiMonthName() + " " + PC.GetYear(u.Title)
                )
                .Select(g => new IdTitleAvg
                {
                    Title = g.Key,
                    Id = g.Sum(x => x.Id),
                    Avg = g.All(x => x.Group == null || x.GroupCount == null || x.GroupCount == 0)
                    ? null
                    : Convert.ToDouble(g.Sum(x => x.Group)) / g.First().GroupCount
                }).ToList(),

            _ => new List<IdTitleAvg>()
        };
    }
	public static List<IdTitleTimeBased> CombineListsGroups(List<IdTitleTimeBased> list, List<IdTitleTimeBased>? avgList)
	{
        var groupDict = avgList?.ToDictionary(
            item => (item.Year, item.Month, item.Day),
            item => new { Id = item.Id, Count = item.GroupCount }
        );
		var result = new List<IdTitleTimeBased>();
        foreach (var item in list)
            if (groupDict != null && groupDict.TryGetValue((item.Year, item.Month, item.Day), out var group))
				result.Add(new IdTitleTimeBased
                {
					Year = item.Year,
					Month = item.Month,
					Day = item.Day,
					Id = item.Id,
					Group = group.Id,
                    GroupCount = group.Count,
                });
			else
				result.Add(new IdTitleTimeBased
                {
					Year = item.Year,
					Month = item.Month,
					Day = item.Day,
					Id = item.Id,
					Group = null,  // if not match, set default
                    GroupCount = null,
				});
		return result;
	}
    public static DateTime AlignToPersianWeekStart(DateTime date)
    {
        while (date.DayOfWeek != DayOfWeek.Saturday)
        {
            date = date.AddDays(-1);
        }
        return date;
    }
    public static int GetPersianWeekOfYear(DateTime date)
    {
        DateTime firstDayOfPersianYear = new DateTime(PC.GetYear(date), 1, 1, PC);
        while (firstDayOfPersianYear.DayOfWeek != DayOfWeek.Saturday)
        {
            firstDayOfPersianYear = firstDayOfPersianYear.AddDays(1);
        }
        int daysSinceStart = (int)(date - firstDayOfPersianYear).TotalDays;
        return daysSinceStart / 7 + 1;
    }
}