using System.Drawing;

namespace Asc2Img;

public static class HeightMap
{
	public static Image CreateImage(double?[,] values, Color defaultColor)
	{
		var normalized = NormalizeHeightMap(values);

		var width = values.GetLength(0);
		var height = values.GetLength(1);

		var bitmap = new Bitmap(width, height);

		for (var y = 0; y < height; y++)
			for (var x = 0; x < width; x++)
			{
				var maybeValue = normalized[x, y];
				var color = GetColor(maybeValue);
				bitmap.SetPixel(x, y, color);
			}

		Color GetColor(double? maybeValue)
		{
			if (maybeValue is not { } value)
				return defaultColor;

			var colorValue = (int)Math.Round(value * 255);
			return Color.FromArgb(colorValue, colorValue, colorValue);
		}

		return bitmap;
	}

	private static double?[,] NormalizeHeightMap(double?[,] values)
	{
		var width = values.GetLength(0);
		var height = values.GetLength(1);

		var result = new double?[width, height];

		var min = double.PositiveInfinity;
		var max = double.NegativeInfinity;

		foreach (var value in values)
		{
			if (value is null)
				continue;

			min = Math.Min(value.Value, min);
			max = Math.Max(value.Value, max);
		}

		for (var y = 0; y < height; y++)
			for (var x = 0; x < width; x++)
			{
				var value = GetValue(x, y);
				result[x, y] = value;
			}

		double? GetValue(int x, int y) =>
			values[x, y] is { } rawValue ? (rawValue - min) / (max - min) : null;

		return result;
	}
}
