using System.Diagnostics;

namespace Asc2Img;

public static class Program
{
	public static void Main(string[]? args)
	{
		var (sourceFile, destinationFile) = args switch
		{
			null or [] => (ReadFileName("Source file", true), ReadFileName("Destination file", false)),
			[var src, var dst] => (src, dst),
			_
				=> throw new(
					"Invalid number of arguments. Either run with zero arguments, or with exactly two, where the first is the source file and the second is the destination file."
				),
		};

		Console.WriteLine();
		Console.Write("Working... ");

		var stopWatch = Stopwatch.StartNew();

		// open source file
		using var fileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
		using var reader = new StreamReader(fileStream);

		// read esri data
		var esri = EsriReader.Read(reader);

		// convert esri data to image
		var bitmap = ImageFromSamples.FromValues(esri.Values);

		// write to destination
		bitmap.Save(destinationFile);

		stopWatch.Stop();

		Console.WriteLine("done.");
		Console.WriteLine(
			$"Completed {esri.Header.ColumnCount}x{esri.Header.RowCount} map in {stopWatch.Elapsed.TotalSeconds:n} seconds."
		);
		Console.WriteLine(
			$"({esri.Header.ColumnCount * esri.Header.RowCount / stopWatch.Elapsed.TotalSeconds:n} pixels per second)"
		);
	}

	private static string ReadFileName(string prompt, bool mustExist)
	{
		while (true)
		{
			Console.Write(prompt);
			Console.Write(": ");

			var input = Console.ReadLine() ?? throw new NullReferenceException();

			if (!string.IsNullOrWhiteSpace(input))
			{
				var fileName = Path.GetFullPath(input);

				if (!mustExist || File.Exists(fileName))
					return fileName;
			}

			Console.WriteLine("File does not exist.");
			Console.WriteLine();
			
		}
	}
}
