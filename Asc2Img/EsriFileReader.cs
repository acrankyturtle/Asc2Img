namespace Asc2Img;

public static class EsriReader
{
	public static EsriFile Read(TextReader reader)
	{
		var header = ReadFileInfo(reader);

		var values = new double?[header.ColumnCount, header.RowCount];
		foreach (var (x, y, value) in ReadSamples(reader, header))
			values[x, y] = value;

		return new(header, values);
	}

	private static EsriFileHeader ReadFileInfo(TextReader reader) =>
		new(
			ReadPropertyValueInt(reader, "ncols"),
			ReadPropertyValueInt(reader, "nrows"),
			ReadPropertyValueDouble(reader, "xllcenter", "xllcorner"),
			ReadPropertyValueDouble(reader, "yllcenter", "yllcorner"),
			ReadPropertyValueDouble(reader, "cellsize"),
			ReadPropertyValueDouble(reader, "nodata_value")
		);

	private static int ReadPropertyValueInt(TextReader reader, params string[] expectedPropertyNames)
	{
		var (propName, propValue) = ReadPropertyLine(reader);

		if (
			expectedPropertyNames.Any(
				expected => !propName.Equals(expected, StringComparison.OrdinalIgnoreCase)
			)
		)
			throw new EsriFormatException($"Unexpected property: `{propName}`.");

		return int.Parse(propValue);
	}

	private static double ReadPropertyValueDouble(TextReader reader, params string[] expectedPropertyNames)
	{
		var (propName, propValue) = ReadPropertyLine(reader);

		if (
			expectedPropertyNames.All(
				expected => !propName.Equals(expected, StringComparison.OrdinalIgnoreCase)
			)
		)
			throw new EsriFormatException($"Unexpected property: `{propName}`.");

		return double.Parse(propValue);
	}

	private static (string Name, string Value) ReadPropertyLine(TextReader reader)
	{
		var line = reader.ReadLine();

		if (line == null)
			throw new EsriFormatException("Unexpected end of file.");

		var spaceIndex = line.IndexOf(' ');

		if (spaceIndex < 0)
			throw new EsriFormatException("Invalid property line.");

		var propName = line[..spaceIndex];
		var propValue = line[(spaceIndex + 1)..];

		return (propName, propValue);
	}

	private static IEnumerable<Sample> ReadSamples(TextReader reader, EsriFileHeader fileHeader)
	{
		var x = 0;
		var y = 0;

		while (reader.ReadLine() is { } line)
		{
			if (line is null)
				throw new EsriFormatException("Unexpected end of file.");

			var previousSpace = -1;
			var nextSpace = -1;

			while (true)
			{
				nextSpace = line.IndexOf(' ', nextSpace + 1);

				if (nextSpace == -1)
				{
					if (previousSpace != -1)
						yield return Sample(line.AsSpan()[(previousSpace + 1)..]);

					break;
				}

				yield return Sample(line.AsSpan()[(previousSpace + 1)..nextSpace]);

				previousSpace = nextSpace;
			}
		}

		if (x != 0 && y != fileHeader.RowCount)
			throw new EsriFormatException("Too few samples.");

		Sample Sample(ReadOnlySpan<char> strValue)
		{
			var value = double.Parse(strValue.Trim());
			var sample = new Sample(
				x,
				y,
				Math.Abs(value - fileHeader.NoDataValue) > 0.000001d ? value : null
			);

			Increment();
			return sample;

			void Increment()
			{
				x++;

				if (x < fileHeader.ColumnCount)
					return;

				x = 0;
				y++;
			}
		}
	}

	private readonly record struct Sample(int X, int Y, double? Value);
}

public record EsriFile(EsriFileHeader Header, double?[,] Values);

public record EsriFileHeader(
	int ColumnCount,
	int RowCount,
	double OffsetX, // unused
	double OffsetY, // unused
	double CellSize, // unused
	double NoDataValue
);

public class EsriFormatException : Exception
{
	public EsriFormatException(string message) : base(message) { }
}
