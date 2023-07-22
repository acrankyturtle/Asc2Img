using System.Drawing;

namespace Asc2Img;

public static class ImageFromSamples
{
	private static readonly Color _defaultColor = default;

	public static Image FromValues(double?[,] values)
	{
		var width = values.GetLength(0);
		var height = values.GetLength(1);

		var min = double.PositiveInfinity;
		var max = double.NegativeInfinity;

		foreach (var value in values)
		{
			if (value is null)
				continue;

			min = Math.Min(value.Value, min);
			max = Math.Max(value.Value, max);
		}

		var bitmap = new Bitmap(width, height);

		for (var y = 0; y < height; y++)
			for (var x = 0; x < width; x++)
			{
				var color = GetColor(x, y);
				bitmap.SetPixel(x, y, color);
			}

		Color GetColor(int x, int y)
		{
			if (values[x, y] is not { } rawValue)
				return _defaultColor;

			var range = max - min;
			var colorValue = (int)Math.Round((rawValue - min) / range * 255);

			return Color.FromArgb(colorValue, colorValue, colorValue);
		}

		return bitmap;
	}
}
