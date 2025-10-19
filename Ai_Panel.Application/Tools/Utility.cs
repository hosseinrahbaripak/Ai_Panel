using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.Enum;
using Ai_Panel.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersianAssistant.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
namespace Ai_Panel.Application.Tools
{
	public static class StaticFileHelper
	{
		public static async Task<bool> SaveFile(string folderPath, string fileName, IFormFile file)
		{
			try
			{
				if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName) || file == null)
				{
					return false;
				}
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
				string filepath = Path.Combine(folderPath, fileName);
				await using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
				{
					await file.CopyToAsync(stream);
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
		public static bool DeleteFile(string filePath = "")
		{
			try
			{
				if (string.IsNullOrEmpty(filePath))
				{
					return false;
				}
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				return false;
			}
		}
		public static async Task<bool> WriteOnTextFile(string filePath, string content)
		{
			try
			{
				if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(content))
				{
					return false;
				}
				if (!File.Exists(filePath))
				{
					File.CreateText(filePath);
				}
				content = content + " \n ";
				await File.AppendAllTextAsync(filePath, content);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
		public static void CheckDirectoryExistAndCrete(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		public static bool CheckDirectoryExist(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				return false;
			}
			return true;
		}
		public static async Task CopyFile(string path, IFormFile file)
		{
			await using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
		}
		public static async Task<bool> ZipFile(string sourcePath, string destinationPath)
		{
			try
			{
				if (!File.Exists(sourcePath))
				{
					return false;
				}
				await using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
				{
					await using (FileStream zipStream = new FileStream(destinationPath, FileMode.Create))
					{
						using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
						{
							ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(sourcePath));
							using (Stream entryStream = entry.Open())
							{
								await sourceStream.CopyToAsync(entryStream);
							}
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs", "backuplogs.txt");
				await WriteOnTextFile(logFilePath, e.Message + " : " + DateTime.UtcNow.AddHours(3.5));
				return false;
			}
		}
		public static List<IdTitleTimeBased> FillMissingDates(this List<IdTitleTimeBased> list, DateTime startDate, DateTime endDate)
		{
			var existingDates = new HashSet<DateTime>(list.Select(item => item.Title));
			var missingDates = new List<IdTitleTimeBased>();
			for (var date = startDate; date <= endDate; date = date.AddDays(1))
			{
				if (!existingDates.Contains(date))
				{
					missingDates.Add(new IdTitleTimeBased
					{
						Year = date.Year,
						Month = date.Month,
						Day = date.Day,
						Id = 0
					});
				}
			}
			var result = list.Concat(missingDates).OrderBy(item => item.Title).ToList();
			return result;
		}
	}
	public static class Utility
	{
		public static List<IdTitle> GetCartStatusIdTitle()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = 1,
					Title = "در انتظار پرداخت"
				},
				new IdTitle()
				{
					Id = 2,
					Title = "پرداخت شده"
				},
				new IdTitle()
				{
					Id = 3,
					Title = "منقضی شده"
				},
			};
		}
		public static List<IdTitle> GetReadTypeIdTitle()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = (int) ReadType.ReadTime,
					Title = "زمان"
				},
				new IdTitle()
				{
					Id = (int) ReadType.ReadCount,
					Title = "تعداد"
				},
			};
		}
		public static List<IdTitle> GetTimeTypeIdTitle()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = (int) TimeType.Daily,
					Title = "روزانه"
				},
				new IdTitle()
				{
					Id = (int) TimeType.Weekly,
					Title = "هفتگی"
				},
				new IdTitle()
				{
					Id = (int) TimeType.Monthly,
					Title = "ماهانه"
				},
			};
		}
		public static List<IdTitle> GetByTypeIdTitle()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = (int) ByTypeEnum.Grade,
					Title = "پایه"
				},
				new IdTitle()
				{
					Id = (int) ByTypeEnum.Tag,
					Title = "تگ"
				},
			};
		}
		public static int CalcPrice(int price, int percent)
		{
			if (percent == 0)
			{
				return price;
			}
			var pr = price - price * percent / 100;
			return pr;
		}
		public static int CalcSkip(int pageId = 1, int take = 20)
		{
			return (pageId - 1) * take;
		}
		public static string CartStatusClass(this int value)
		{
			switch (value)
			{
				case 1:
					return "text-warning";
				case 2:
					return "text-success";
				case 3:
					return "text-danger";
				default:
					return "";
			}
		}
		public static string TicketStatusClass(this int value)
		{
			switch (value)
			{
				case 0:
					return "text-danger";
				case 1:
					return "text-warning";
				case 2:
					return "text-success";
				default:
					return "";
			}
		}
		public static long? VersionCalculator(string version = "")
		{
			long sum = 0;
			string[] versionArray = version.Split(".");
			if (versionArray.Length != 3)
			{
				return null;
			}
			long firstVersionValue = long.Parse(versionArray[0]);
			long firstValue = firstVersionValue * 1000000;

			long secondVersionValue = long.Parse(versionArray[1]);
			long secondValue = secondVersionValue * 10000;

			long thirdVersionValue = long.Parse(versionArray[2]);

			sum = firstValue + secondValue + thirdVersionValue;
			return sum;
		}
		public static string HideMobileNumber(this string mobile)
		{
			if (mobile.Length != 11)
				return "";
			var firstPart = mobile.Substring(0, 4);
			var secondPart = "*****";
			var thirdPart = mobile.Substring(8, 3);
			return firstPart + secondPart + thirdPart;
		}
		public static string GetShamsiMonthName(this int month)
		{
			switch (month)
			{
				case 1: return "فررودين";
				case 2: return "ارديبهشت";
				case 3: return "خرداد";
				case 4: return "تير";
				case 5: return "مرداد";
				case 6: return "شهريور";
				case 7: return "مهر";
				case 8: return "آبان";
				case 9: return "آذر";
				case 10: return "دي";
				case 11: return "بهمن";
				case 12: return "اسفند";
				default: return "";
			}
		}
		public static MonthRange GetCurrentShamsiMonthRange()
		{
			PersianCalendar persianCalendar = new PersianCalendar();
			// Get the current date and convert to Persian date
			DateTime currentDate = DateTime.Now;
			int persianYear = persianCalendar.GetYear(currentDate);
			int persianMonth = persianCalendar.GetMonth(currentDate);

			// Get the first and last day of the current Persian month in Miladi
			DateTime startOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
			int daysOfMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);
			DateTime endOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, daysOfMonth, 23, 59, 59, 999);

			// Get the first and last day of the current Persian month
			string shamsiStart = $"{persianYear}/{persianMonth}/1";
			string shamsiEnd = $"{persianYear}/{persianMonth}/{daysOfMonth}";
			string shamsiNow = DateTime.Now.ToPersianDate();
			return new MonthRange() { Start = startOfMonth, Days = daysOfMonth, End = endOfMonth, ShamsiStart = shamsiStart, ShamsiEnd = shamsiEnd, ShamsiNow = shamsiNow };
		}
		public static int CountWords(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return 0;
			}

			// Split the string by whitespace characters
			string[] words = input.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			return words.Length;
		}
		public static int ToShamsiHour(this int hour)
		{
			if (hour < 0 || hour > 24)
				return 0;
			var pc = new PersianCalendar();
			DateTime now = DateTime.Now;
			DateTime miladiDate = new DateTime(now.Year, 1, 1, hour, 1, 1);
			return pc.GetHour(miladiDate);
		}
		public static int ToShamsiHour(this string hourStr)
		{
			int hour = 0;
			if (Int32.TryParse(hourStr, out hour))
				return hour.ToShamsiHour();
			return 0;
		}
		public static List<IdTitle> FillMissingHours(List<IdTitle> list)
		{
			var hourList = Enumerable.Range(0, 24).ToList();
			var existingHourSet = new HashSet<int>(
				list.Select(x => int.TryParse(x.Title, out int y) ? y : -1)
					.Where(x => x != -1)
			);
			var missingHours = hourList.Except(existingHourSet)
				.Select(h => new IdTitle { Id = 0, Title = h.ToString() });
			return list.Concat(missingHours)
					   .OrderBy(x => int.Parse(x.Title))
					   .ToList();
		}
		public static string ToDisplayName(this System.Enum value) => value.GetType()
			.GetMember(value.ToString())
			.First()
			.GetCustomAttribute<DisplayAttribute>()
			?.GetName() ?? string.Empty;
		public static List<SelectListItem> ToSelectList(this System.Enum enumValue)
			=> (from System.Enum e in System.Enum.GetValues(enumValue.GetType())
				select new SelectListItem
				{
					Text = e.ToDisplayName(),
					Value = Convert.ToInt16(e).ToString()
				}).ToList();
		public static string? GenerateVersion(string lastVersion)
		{
			try
			{
				if (!lastVersion.Contains("."))
					return null;
				string[] versionArray = lastVersion.Split(".");
				if (versionArray.Length != 3)
					return null;
				int major = int.Parse(versionArray[0]);
				int minor = int.Parse(versionArray[1]);
				int patch = int.Parse(versionArray[2]);
				var newVersion = "";
				if (patch >= 10)
				{
					patch = 0;
					minor++;

					if (minor > 10)
					{
						minor = 0;
						major++;
					}
				}
				else
				{
					patch++;
				}
				return $"{major}.{minor}.{patch}";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
        public static bool AreAllPropertiesNull(this object model, params string[] propertiesToIgnore)
        {
            if (model == null) return true;

            return model.GetType().GetProperties()
                .Where(prop => !propertiesToIgnore.Contains(prop.Name))
                .All(prop => prop.GetValue(model) == null);
        }
    }
	public static class EnumVal
	{
		public static string CartStatusTitle(this int value)
		{
			switch (value)
			{
				case 1:
					return "در انتظار پرداخت";
				case 2:
					return "پرداخت شده";
				case 3:
					return "منقضی شده";
				default:
					return "";
			}
		}
		public static string RequestLogStatus(int value)
		{
			return value switch
			{
				0 => "امکان ثبت نام کاربر جدید وجود ندارد",
				1 => "کد ورود ارسال شده",
				2 => "وارد اپ شده",
				3 => "خطای سیستمی",
				4 => "شماره تلفن نامعتبر",
				5 => "کد فعال سازی اشتباه وارد شده است",
				_ => "",
			};
		}
		public static List<IdTitle> GetRequestLogStatus()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = 0,
					Title = "امکان ثبت نام کاربر جدید وجود ندارد",
				},
				new IdTitle()
				{
					Id = 1,
					Title = "کد ورود ارسال شده",
				},
				new IdTitle()
				{
					Id = 2,
					Title = "وارد اپ شده",
				},
				new IdTitle()
				{
					Id = 3,
					Title = "خطای سیستمی",
				},
				new IdTitle()
				{
					Id = 4,
					Title = "شماره تلفن نامعتبر",
				},
				new IdTitle()
				{
					Id = 5,
					Title = "کد فعال سازی اشتباه وارد شده است",
				}
			};
		}
		public static string SortStr(int value)
		{
			switch (value)
			{
				case 1:
					return "نزولی به صعودی";
				case 2:
					return "صعودی به نزولی";
				default:
					return "";
			}
		}
		public static string GenderType(this int stat)
		{
			switch (stat)
			{
				case 1:
					return "مرد";
				case 2:
					return "زن";
				case 3:
					return "نامشخص";
				default: return "";
			}
		}
		public static string AdminActionType(this int stat)
		{
			switch (stat)
			{
				case 1:
					return "کاربران";
				case 2:
					return "کتاب ها";
				case 3:
					return "ادمین ها";
				default: return "";
			}
		}
		public static string AdminActionTypeToPersian(this int stat)
		{
			switch (stat)
			{
				case 1:
					return "نام کاربر";
				case 2:
					return "نام کتاب";
				case 3:
					return "نام ادمین";
				default: return "";
			}
		}
		public static string GradeType(this int stat)
		{
			switch (stat)
			{
				case 0:
					return "";
				case 1:
					return "پایه چهارم ";
				case 2:
					return "پایه پنجم ";
				case 3:
					return "پایه ششم ";
				case 4:
					return "پایه هفتم ";
				case 5:
					return "پایه هشتم";
				case 6:
					return "پایه نهم ";
				case 7:
					return "پایه دهم ";
				case 8:
					return "پایه یازدهم ";
				case 9:
					return "پایه دوازدهم ";
				default: return "";
			}
		}
		public static List<IdTitle> GetUserCommentStatus()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = 0,
					Title = "در حال انتظار برای بررسی",
				},
				new IdTitle()
				{
					Id = 1,
					Title = "تائید شده",
				},
				new IdTitle()
				{
					Id = 2,
					Title = "رد شده",
				},
			};
		}
		public static string GetVal(this BookCommentStatus val)
		{
			switch (val)
			{
				case BookCommentStatus.WaitForCheck:
					return "در حال انتظار برای بررسی";
				case BookCommentStatus.Accept:
					return "تائید شده";
				case BookCommentStatus.Reject:
					return "رد شده";
				default: return "";
			}
		}
		public static List<IdTitle> GetOfferToOthers()
		{
			return new List<IdTitle>()
			{
				new IdTitle()
				{
					Id = 0,
					Title = "بله",
				},
				new IdTitle()
				{
					Id = 1,
					Title = "مطمئن نیستم",
				},
				new IdTitle()
				{
					Id = 2,
					Title = "خیر",
				},
			};
		}
		public static string GetVal(this OfferBookToOthers val)
		{
			switch (val)
			{
				case OfferBookToOthers.Yes:
					return "بله";
				case OfferBookToOthers.NotSure:
					return "مطمئن نیستم";
				case OfferBookToOthers.No:
					return "خیر";
				default: return "";
			}
		}

	}
}
