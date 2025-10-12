namespace Ai_Panel.Application.Tools;

using System.IO;
using ImageMagick;
public static class ImageConverter
{
	public static async Task<Stream?> ConvertToPngStreamAsync(Stream inputStream)
	{
		inputStream.Position = 0;

		using var image = new MagickImage(inputStream);
		image.Format = MagickFormat.Png;

		var outputStream = new MemoryStream();
		image.Write(outputStream);

		outputStream.Position = 0;
		return outputStream;
	}

}
