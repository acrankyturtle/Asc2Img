using System.Drawing.Imaging;
using System.Security;
using Asc2Img;

namespace Asc2Img_Gui;

public partial class MainForm : Form
{
	private string? _sourceFile;

	public MainForm()
	{
		InitializeComponent();
	}

	private async Task SelectSourceFile(string sourceFile)
	{
		btnSourceFileBrowse.Enabled = false;
		_sourceFile = sourceFile;
		ShowProgress();
		await GeneratePreview();
		HideProgress();
		lblSourceFile.Text = _sourceFile;
		btnSourceFileBrowse.Enabled = true;
	}

	private void ShowProgress()
	{
		pnlProgress.Visible = true;
	}

	private void HideProgress()
	{
		pnlProgress.Visible = false;
	}

	private async Task GeneratePreview()
	{
		if (string.IsNullOrWhiteSpace(_sourceFile))
		{
			previewRenderer.Image = null;
			return;
		}

		TextReader textReader;
		try
		{
			var fileStream = new FileStream(_sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			textReader = new StreamReader(fileStream);
		}
		catch (Exception ex) when (ex is DirectoryNotFoundException or FileNotFoundException)
		{
			lblSourceFile.ForeColor = Color.Red;
			previewRenderer.Image = null;
			return;
		}
		catch (Exception ex) when (ex is IOException or SecurityException or UnauthorizedAccessException)
		{
			MessageBox.Show(
				$"Erorr reading source file:\r\n{ex.Message}",
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
			previewRenderer.Image = null;
			return;
		}

		EsriFile esriFile;
		try
		{
			esriFile = await Task.Run(() => Esri.Read(textReader));
		}
		finally
		{
			textReader.Dispose();
		}

		previewRenderer.Image = await Task.Run(() => HeightMap.CreateImage(esriFile.Values, default));
	}

	private void btnSourceFileBrowse_Click(object sender, EventArgs e)
	{
		using var ofd = new OpenFileDialog
		{
			Title = "Select ESRI file...",
			Filter = "ASCII ESRI file (*.asc)|*.asc",
			CheckFileExists = true,
		};

		if (ofd.ShowDialog(this) != DialogResult.OK)
			return;

		SelectSourceFile(ofd.FileName);
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		if (previewRenderer.Image is null)
			return;

		using var sfd = new SaveFileDialog
		{
			Title = "Select output file...",
			Filter = "PNG (*.png)|*.png|TIFF (*.tif, *.tiff)|*.tif;*.tiff"
		};

		if (sfd.ShowDialog(this) != DialogResult.OK)
			return;

		Stream stream;
		try
		{
			stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
		}
		catch (Exception ex) when (ex is IOException or SecurityException or UnauthorizedAccessException)
		{
			MessageBox.Show(
				$"Erorr opening destination file:\r\n{ex.Message}",
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
			return;
		}

		try
		{
			previewRenderer.Image.Save(
				stream,
				sfd.FilterIndex switch
				{
					0 => ImageFormat.Png,
					1 => ImageFormat.Tiff,
					_ => throw new NotImplementedException(),
				}
			);
		}
		finally
		{
			stream.Dispose();
		}

		MessageBox.Show("Operation completed successfully.", Application.ProductName);
	}
}
