using System.Text.RegularExpressions;

namespace Ai_Panel.Application.Tools
{
	public static class StringExtensions
	{
		public static string LimitSentenceLength(this string paragraph, int maximumLenght)
		{
			//null check
			if (paragraph == null) return null;

			//less than maximum length, return as it is
			if (paragraph.Length <= maximumLenght) return paragraph;
			//split the paragraph into indvidual words 
			string[] words = paragraph.Split(' ');
			//initialize return variable 
			string paragraphToReturn = string.Empty;
			//construct the return word 
			foreach (string word in words)
			{
				//check if adding 3 to current length and next word is more than maximum length. 
				if (paragraphToReturn.Length + word.Length + 3 > maximumLenght)
				{
					//append "..."
					paragraphToReturn = paragraphToReturn.Trim() + " ...";
					//exit foreach loop
					break;
				}
				//add next word and continue
				paragraphToReturn += word + " ";
			}
			return paragraphToReturn;
		}
		public static string FormatPrompt(this string prompt, Dictionary<string, string> promptDictionary)
		{
			if (string.IsNullOrEmpty(prompt))
				return prompt;
			string pattern = @"\{\{(.*?)\}\}";

			return Regex.Replace(prompt, pattern, match =>
			{
				string key = match.Groups[1].Value;
				if (promptDictionary.TryGetValue(key, out var value))
				{
					return value;
				}
				return match.Value;
			});
		}
	}
}
