namespace Asc2Img_Gui;

partial class MainForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		grpSourceFile = new GroupBox();
		btnSourceFileBrowse = new Button();
		lblSourceFile = new Label();
		grpPreview = new GroupBox();
		pnlProgress = new TableLayoutPanel();
		progressBar = new ProgressBar();
		previewRenderer = new PreviewRenderer();
		grpExport = new GroupBox();
		btnExport = new Button();
		grpSourceFile.SuspendLayout();
		grpPreview.SuspendLayout();
		pnlProgress.SuspendLayout();
		grpExport.SuspendLayout();
		SuspendLayout();
		// 
		// grpSourceFile
		// 
		grpSourceFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		grpSourceFile.Controls.Add(btnSourceFileBrowse);
		grpSourceFile.Controls.Add(lblSourceFile);
		grpSourceFile.Location = new Point(12, 12);
		grpSourceFile.Name = "grpSourceFile";
		grpSourceFile.Size = new Size(756, 76);
		grpSourceFile.TabIndex = 0;
		grpSourceFile.TabStop = false;
		grpSourceFile.Text = "Source File";
		// 
		// btnSourceFileBrowse
		// 
		btnSourceFileBrowse.Anchor = AnchorStyles.Right;
		btnSourceFileBrowse.Location = new Point(656, 28);
		btnSourceFileBrowse.Name = "btnSourceFileBrowse";
		btnSourceFileBrowse.Size = new Size(94, 29);
		btnSourceFileBrowse.TabIndex = 1;
		btnSourceFileBrowse.Text = "Browse...";
		btnSourceFileBrowse.UseVisualStyleBackColor = true;
		btnSourceFileBrowse.Click += btnSourceFileBrowse_Click;
		// 
		// lblSourceFile
		// 
		lblSourceFile.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		lblSourceFile.Location = new Point(6, 25);
		lblSourceFile.Name = "lblSourceFile";
		lblSourceFile.Size = new Size(644, 35);
		lblSourceFile.TabIndex = 0;
		lblSourceFile.Text = "(no file selected)";
		lblSourceFile.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// grpPreview
		// 
		grpPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		grpPreview.Controls.Add(pnlProgress);
		grpPreview.Controls.Add(previewRenderer);
		grpPreview.Location = new Point(12, 94);
		grpPreview.Name = "grpPreview";
		grpPreview.Size = new Size(756, 525);
		grpPreview.TabIndex = 2;
		grpPreview.TabStop = false;
		grpPreview.Text = "Preview";
		// 
		// pnlProgress
		// 
		pnlProgress.ColumnCount = 3;
		pnlProgress.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		pnlProgress.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		pnlProgress.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		pnlProgress.Controls.Add(progressBar, 1, 0);
		pnlProgress.Dock = DockStyle.Fill;
		pnlProgress.Location = new Point(3, 23);
		pnlProgress.Name = "pnlProgress";
		pnlProgress.RowCount = 1;
		pnlProgress.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		pnlProgress.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		pnlProgress.Size = new Size(750, 499);
		pnlProgress.TabIndex = 1;
		pnlProgress.Visible = false;
		// 
		// progressBar
		// 
		progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		progressBar.Location = new Point(190, 228);
		progressBar.MarqueeAnimationSpeed = 17;
		progressBar.Name = "progressBar";
		progressBar.Size = new Size(369, 42);
		progressBar.Step = 1;
		progressBar.Style = ProgressBarStyle.Marquee;
		progressBar.TabIndex = 0;
		// 
		// previewRenderer
		// 
		previewRenderer.BackColor = Color.Black;
		previewRenderer.Dock = DockStyle.Fill;
		previewRenderer.Image = null;
		previewRenderer.Location = new Point(3, 23);
		previewRenderer.Name = "previewRenderer";
		previewRenderer.Size = new Size(750, 499);
		previewRenderer.TabIndex = 0;
		previewRenderer.Text = "previewRenderer1";
		// 
		// grpExport
		// 
		grpExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		grpExport.Controls.Add(btnExport);
		grpExport.Location = new Point(12, 625);
		grpExport.Name = "grpExport";
		grpExport.Size = new Size(756, 76);
		grpExport.TabIndex = 2;
		grpExport.TabStop = false;
		grpExport.Text = "Export";
		// 
		// btnExport
		// 
		btnExport.Anchor = AnchorStyles.Left;
		btnExport.Location = new Point(6, 28);
		btnExport.Name = "btnExport";
		btnExport.Size = new Size(94, 28);
		btnExport.TabIndex = 1;
		btnExport.Text = "Save as...";
		btnExport.UseVisualStyleBackColor = true;
		btnExport.Click += btnExport_Click;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(780, 713);
		Controls.Add(grpExport);
		Controls.Add(grpPreview);
		Controls.Add(grpSourceFile);
		MinimumSize = new Size(393, 458);
		Name = "MainForm";
		Text = "Asc2Img";
		grpSourceFile.ResumeLayout(false);
		grpPreview.ResumeLayout(false);
		pnlProgress.ResumeLayout(false);
		grpExport.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private GroupBox grpSourceFile;
	private Button btnSourceFileBrowse;
	private Label lblSourceFile;
	private GroupBox grpPreview;
	private GroupBox grpExport;
	private Button btnExportTiff;
	private Button btnExport;
	private PreviewRenderer previewRenderer;
	private ProgressBar progressBar;
	private TableLayoutPanel pnlProgress;
}