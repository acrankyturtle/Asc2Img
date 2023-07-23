namespace Asc2Img_Gui;

public class PreviewRenderer : Control
{
	private Image? _image;

	public Image? Image
	{
		get => _image;
		set
		{
			_image = value;
			Refresh();
		}
	}

	public PreviewRenderer()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		BackColor = Color.Black;
		DoubleBuffered = true;
		Size = new(512, 512);

		SetStyle(ControlStyles.DoubleBuffer, true);
		SetStyle(ControlStyles.UserPaint, true);
		SetStyle(ControlStyles.AllPaintingInWmPaint, true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		var g = e.Graphics;

		g.Clear(BackColor);

		if (Image is null)
			return;

		var clientSize = Math.Min(ClientSize.Width, ClientSize.Height);

		g.DrawImage(Image, 0f, 0, clientSize, clientSize);
	}

	protected override void OnResize(EventArgs e)
	{
		Refresh();
		base.OnResize(e);
	}
}
