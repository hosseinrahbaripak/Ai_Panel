using System.Security.Cryptography;
using System.Text;

namespace Live_Book.Application.Tools
{
	public static class PasswordManager
	{
		/// <summary>
		/// برای امنیت بالای پسورد ادمین حتما از این متد استفاده شود.
		/// کلید ساخته شده حتما در دیتابیس ذخیره شود
		/// اگر کلید خالی باشه یعنی یک یوزر جدید میخوایم بسازیم ؛ اگر خالی نباشه برای چک کردن پسورد های قدیمی است.
		/// </summary>
		/// <param name="pass">پسورد ورودی ادمین</param>
		/// <returns>
		/// item1 = key
		/// item2 = password
		/// </returns>
		public static Tuple<string, string> GeneratePass(this string pass, string key = "")
		{
			if (string.IsNullOrEmpty(key))
			{
				key = 10.GeneratePassKey();
			}
			var password = pass.EncodePassword(key);
			return new Tuple<string, string>(key, password);
		}
		public static string GeneratePassKey(this int length) //length of salt    
		{
			try
			{
				const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
				var randNum = new Random();
				var chars = new char[length];
				for (var i = 0; i <= length - 1; i++)
				{
					chars[i] = allowedChars[Convert.ToInt32(allowedChars.Length * randNum.NextDouble())];
				}
				return new string(chars);

			}
			catch (Exception)
			{
				return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);
			}

		}
		private static string EncodePassword(this string pass, string salt) //encrypt password    
		{
			byte[] bytes = Encoding.Unicode.GetBytes(pass);
			byte[] src = Encoding.Unicode.GetBytes(salt);
			byte[] dst = new byte[src.Length + bytes.Length];
			Buffer.BlockCopy(src, 0, dst, 0, src.Length);
			Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
			HashAlgorithm? algorithm = HashAlgorithm.Create("SHA1");
			byte[]? inArray = algorithm?.ComputeHash(dst);
			return EncodePasswordMd5(Convert.ToBase64String(inArray));
		}
		private static string EncodePasswordMd5(string pass) //Encrypt using MD5    
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			var originalBytes = Encoding.Default.GetBytes(pass);
			var encodedBytes = md5.ComputeHash(originalBytes);
			return BitConverter.ToString(encodedBytes);
		}
	}
}
