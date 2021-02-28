
namespace AttacheCase
{
  partial class Form5
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabelDecryptionTime = new System.Windows.Forms.ToolStripStatusLabel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonClose = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageAtc2 = new System.Windows.Forms.TabPage();
      this.textBoxOutputFileList2 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.textBoxAtcHeaderSize2 = new System.Windows.Forms.TextBox();
      this.labelAtcHeaderSize2 = new System.Windows.Forms.Label();
      this.textBoxTypeAlgorism = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.textBoxDataFileVersion2 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxlFileSignature2 = new System.Windows.Forms.TextBox();
      this.labeFileSignature = new System.Windows.Forms.Label();
      this.textBoxfBroken2 = new System.Windows.Forms.TextBox();
      this.labelfBroken = new System.Windows.Forms.Label();
      this.textBoxMissTypeLimit2 = new System.Windows.Forms.TextBox();
      this.labelMissTypeLimit = new System.Windows.Forms.Label();
      this.textBoxReserved = new System.Windows.Forms.TextBox();
      this.labelReserved = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.labelDataSubVersion = new System.Windows.Forms.Label();
      this.textBoxDataSubVersion = new System.Windows.Forms.TextBox();
      this.tabPageAtc3 = new System.Windows.Forms.TabPage();
      this.textBoxMissTypeLimit3 = new System.Windows.Forms.TextBox();
      this.labelMissTypeLimit3 = new System.Windows.Forms.Label();
      this.textBoxFileSignature3 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.textBoxOutputFileList = new System.Windows.Forms.TextBox();
      this.labelOutputFileList = new System.Windows.Forms.Label();
      this.textBoxRfc2898DeriveBytes = new System.Windows.Forms.TextBox();
      this.labelRfc2898DeriveBytes = new System.Windows.Forms.Label();
      this.textBoxSalt = new System.Windows.Forms.TextBox();
      this.labelSalt = new System.Windows.Forms.Label();
      this.textBoxAtcHeaderSize = new System.Windows.Forms.TextBox();
      this.labelAtcHeaderSize = new System.Windows.Forms.Label();
      this.textBoxDataFileVersion3 = new System.Windows.Forms.TextBox();
      this.labelDataFileVersion = new System.Windows.Forms.Label();
      this.textBroken3 = new System.Windows.Forms.TextBox();
      this.labelBroken = new System.Windows.Forms.Label();
      this.labelAppFileVersion = new System.Windows.Forms.Label();
      this.textBoxAppFileVersion = new System.Windows.Forms.TextBox();
      this.statusStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPageAtc2.SuspendLayout();
      this.tabPageAtc3.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDecryptionTime});
      this.statusStrip1.Location = new System.Drawing.Point(0, 419);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(624, 22);
      this.statusStrip1.TabIndex = 0;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabelDecryptionTime
      // 
      this.toolStripStatusLabelDecryptionTime.Name = "toolStripStatusLabelDecryptionTime";
      this.toolStripStatusLabelDecryptionTime.Size = new System.Drawing.Size(96, 17);
      this.toolStripStatusLabelDecryptionTime.Text = "DecryptionTime: ";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 376);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(624, 43);
      this.panel1.TabIndex = 1;
      // 
      // buttonClose
      // 
      this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonClose.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonClose.Location = new System.Drawing.Point(518, 7);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(83, 29);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "&Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPageAtc2);
      this.tabControl1.Controls.Add(this.tabPageAtc3);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(624, 376);
      this.tabControl1.TabIndex = 2;
      // 
      // tabPageAtc2
      // 
      this.tabPageAtc2.Controls.Add(this.textBoxOutputFileList2);
      this.tabPageAtc2.Controls.Add(this.label4);
      this.tabPageAtc2.Controls.Add(this.textBoxAtcHeaderSize2);
      this.tabPageAtc2.Controls.Add(this.labelAtcHeaderSize2);
      this.tabPageAtc2.Controls.Add(this.textBoxTypeAlgorism);
      this.tabPageAtc2.Controls.Add(this.label3);
      this.tabPageAtc2.Controls.Add(this.textBoxDataFileVersion2);
      this.tabPageAtc2.Controls.Add(this.label2);
      this.tabPageAtc2.Controls.Add(this.textBoxlFileSignature2);
      this.tabPageAtc2.Controls.Add(this.labeFileSignature);
      this.tabPageAtc2.Controls.Add(this.textBoxfBroken2);
      this.tabPageAtc2.Controls.Add(this.labelfBroken);
      this.tabPageAtc2.Controls.Add(this.textBoxMissTypeLimit2);
      this.tabPageAtc2.Controls.Add(this.labelMissTypeLimit);
      this.tabPageAtc2.Controls.Add(this.textBoxReserved);
      this.tabPageAtc2.Controls.Add(this.labelReserved);
      this.tabPageAtc2.Controls.Add(this.label1);
      this.tabPageAtc2.Controls.Add(this.labelDataSubVersion);
      this.tabPageAtc2.Controls.Add(this.textBoxDataSubVersion);
      this.tabPageAtc2.Location = new System.Drawing.Point(4, 22);
      this.tabPageAtc2.Name = "tabPageAtc2";
      this.tabPageAtc2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageAtc2.Size = new System.Drawing.Size(616, 350);
      this.tabPageAtc2.TabIndex = 0;
      this.tabPageAtc2.Text = "AttacheCase2";
      this.tabPageAtc2.UseVisualStyleBackColor = true;
      // 
      // textBoxOutputFileList2
      // 
      this.textBoxOutputFileList2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutputFileList2.Location = new System.Drawing.Point(16, 201);
      this.textBoxOutputFileList2.Multiline = true;
      this.textBoxOutputFileList2.Name = "textBoxOutputFileList2";
      this.textBoxOutputFileList2.ReadOnly = true;
      this.textBoxOutputFileList2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxOutputFileList2.Size = new System.Drawing.Size(581, 132);
      this.textBoxOutputFileList2.TabIndex = 28;
      this.textBoxOutputFileList2.TextChanged += new System.EventHandler(this.textBoxOutputFileList2_TextChanged);
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(17, 186);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(77, 12);
      this.label4.TabIndex = 27;
      this.label4.Text = "OutputFileList";
      // 
      // textBoxAtcHeaderSize2
      // 
      this.textBoxAtcHeaderSize2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxAtcHeaderSize2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxAtcHeaderSize2.Location = new System.Drawing.Point(336, 93);
      this.textBoxAtcHeaderSize2.Name = "textBoxAtcHeaderSize2";
      this.textBoxAtcHeaderSize2.ReadOnly = true;
      this.textBoxAtcHeaderSize2.Size = new System.Drawing.Size(133, 19);
      this.textBoxAtcHeaderSize2.TabIndex = 26;
      // 
      // labelAtcHeaderSize2
      // 
      this.labelAtcHeaderSize2.AutoSize = true;
      this.labelAtcHeaderSize2.Location = new System.Drawing.Point(336, 78);
      this.labelAtcHeaderSize2.Name = "labelAtcHeaderSize2";
      this.labelAtcHeaderSize2.Size = new System.Drawing.Size(80, 12);
      this.labelAtcHeaderSize2.TabIndex = 25;
      this.labelAtcHeaderSize2.Text = "AtcHeaderSize";
      // 
      // textBoxTypeAlgorism
      // 
      this.textBoxTypeAlgorism.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxTypeAlgorism.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxTypeAlgorism.Location = new System.Drawing.Point(336, 35);
      this.textBoxTypeAlgorism.Name = "textBoxTypeAlgorism";
      this.textBoxTypeAlgorism.ReadOnly = true;
      this.textBoxTypeAlgorism.Size = new System.Drawing.Size(133, 19);
      this.textBoxTypeAlgorism.TabIndex = 24;
      // 
      // label3
      // 
      this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(334, 20);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 12);
      this.label3.TabIndex = 23;
      this.label3.Text = "TypeAlgorism";
      // 
      // textBoxDataFileVersion2
      // 
      this.textBoxDataFileVersion2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxDataFileVersion2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxDataFileVersion2.Location = new System.Drawing.Point(177, 145);
      this.textBoxDataFileVersion2.Name = "textBoxDataFileVersion2";
      this.textBoxDataFileVersion2.ReadOnly = true;
      this.textBoxDataFileVersion2.Size = new System.Drawing.Size(133, 19);
      this.textBoxDataFileVersion2.TabIndex = 22;
      // 
      // label2
      // 
      this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(175, 130);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 12);
      this.label2.TabIndex = 21;
      this.label2.Text = "DataFileVersion";
      // 
      // textBoxlFileSignature2
      // 
      this.textBoxlFileSignature2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxlFileSignature2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxlFileSignature2.Location = new System.Drawing.Point(177, 93);
      this.textBoxlFileSignature2.Name = "textBoxlFileSignature2";
      this.textBoxlFileSignature2.ReadOnly = true;
      this.textBoxlFileSignature2.Size = new System.Drawing.Size(133, 19);
      this.textBoxlFileSignature2.TabIndex = 20;
      // 
      // labeFileSignature
      // 
      this.labeFileSignature.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labeFileSignature.AutoSize = true;
      this.labeFileSignature.Location = new System.Drawing.Point(175, 78);
      this.labeFileSignature.Name = "labeFileSignature";
      this.labeFileSignature.Size = new System.Drawing.Size(72, 12);
      this.labeFileSignature.TabIndex = 19;
      this.labeFileSignature.Text = "FileSignature";
      // 
      // textBoxfBroken2
      // 
      this.textBoxfBroken2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxfBroken2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxfBroken2.Location = new System.Drawing.Point(177, 35);
      this.textBoxfBroken2.Name = "textBoxfBroken2";
      this.textBoxfBroken2.ReadOnly = true;
      this.textBoxfBroken2.Size = new System.Drawing.Size(133, 19);
      this.textBoxfBroken2.TabIndex = 18;
      // 
      // labelfBroken
      // 
      this.labelfBroken.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelfBroken.AutoSize = true;
      this.labelfBroken.Location = new System.Drawing.Point(175, 20);
      this.labelfBroken.Name = "labelfBroken";
      this.labelfBroken.Size = new System.Drawing.Size(45, 12);
      this.labelfBroken.TabIndex = 17;
      this.labelfBroken.Text = "fBroken";
      // 
      // textBoxMissTypeLimit2
      // 
      this.textBoxMissTypeLimit2.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxMissTypeLimit2.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxMissTypeLimit2.Location = new System.Drawing.Point(17, 145);
      this.textBoxMissTypeLimit2.Name = "textBoxMissTypeLimit2";
      this.textBoxMissTypeLimit2.ReadOnly = true;
      this.textBoxMissTypeLimit2.Size = new System.Drawing.Size(133, 19);
      this.textBoxMissTypeLimit2.TabIndex = 16;
      // 
      // labelMissTypeLimit
      // 
      this.labelMissTypeLimit.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelMissTypeLimit.AutoSize = true;
      this.labelMissTypeLimit.Location = new System.Drawing.Point(15, 130);
      this.labelMissTypeLimit.Name = "labelMissTypeLimit";
      this.labelMissTypeLimit.Size = new System.Drawing.Size(79, 12);
      this.labelMissTypeLimit.TabIndex = 15;
      this.labelMissTypeLimit.Text = "MissTypeLimit";
      // 
      // textBoxReserved
      // 
      this.textBoxReserved.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxReserved.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxReserved.Location = new System.Drawing.Point(16, 93);
      this.textBoxReserved.Name = "textBoxReserved";
      this.textBoxReserved.ReadOnly = true;
      this.textBoxReserved.Size = new System.Drawing.Size(133, 19);
      this.textBoxReserved.TabIndex = 14;
      // 
      // labelReserved
      // 
      this.labelReserved.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelReserved.AutoSize = true;
      this.labelReserved.Location = new System.Drawing.Point(14, 78);
      this.labelReserved.Name = "labelReserved";
      this.labelReserved.Size = new System.Drawing.Size(49, 12);
      this.labelReserved.TabIndex = 13;
      this.labelReserved.Text = "reserved";
      // 
      // label1
      // 
      this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.DarkGreen;
      this.label1.Location = new System.Drawing.Point(15, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(141, 12);
      this.label1.TabIndex = 12;
      this.label1.Text = "ver.2.00~ \"5\", ver.2.70~ \"6\"";
      // 
      // labelDataSubVersion
      // 
      this.labelDataSubVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelDataSubVersion.AutoSize = true;
      this.labelDataSubVersion.Location = new System.Drawing.Point(15, 20);
      this.labelDataSubVersion.Name = "labelDataSubVersion";
      this.labelDataSubVersion.Size = new System.Drawing.Size(87, 12);
      this.labelDataSubVersion.TabIndex = 11;
      this.labelDataSubVersion.Text = "DataSubVersion";
      // 
      // textBoxDataSubVersion
      // 
      this.textBoxDataSubVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxDataSubVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxDataSubVersion.Location = new System.Drawing.Point(17, 35);
      this.textBoxDataSubVersion.Name = "textBoxDataSubVersion";
      this.textBoxDataSubVersion.ReadOnly = true;
      this.textBoxDataSubVersion.Size = new System.Drawing.Size(133, 19);
      this.textBoxDataSubVersion.TabIndex = 10;
      // 
      // tabPageAtc3
      // 
      this.tabPageAtc3.Controls.Add(this.textBoxMissTypeLimit3);
      this.tabPageAtc3.Controls.Add(this.labelMissTypeLimit3);
      this.tabPageAtc3.Controls.Add(this.textBoxFileSignature3);
      this.tabPageAtc3.Controls.Add(this.label5);
      this.tabPageAtc3.Controls.Add(this.textBoxOutputFileList);
      this.tabPageAtc3.Controls.Add(this.labelOutputFileList);
      this.tabPageAtc3.Controls.Add(this.textBoxRfc2898DeriveBytes);
      this.tabPageAtc3.Controls.Add(this.labelRfc2898DeriveBytes);
      this.tabPageAtc3.Controls.Add(this.textBoxSalt);
      this.tabPageAtc3.Controls.Add(this.labelSalt);
      this.tabPageAtc3.Controls.Add(this.textBoxAtcHeaderSize);
      this.tabPageAtc3.Controls.Add(this.labelAtcHeaderSize);
      this.tabPageAtc3.Controls.Add(this.textBoxDataFileVersion3);
      this.tabPageAtc3.Controls.Add(this.labelDataFileVersion);
      this.tabPageAtc3.Controls.Add(this.textBroken3);
      this.tabPageAtc3.Controls.Add(this.labelBroken);
      this.tabPageAtc3.Controls.Add(this.labelAppFileVersion);
      this.tabPageAtc3.Controls.Add(this.textBoxAppFileVersion);
      this.tabPageAtc3.Location = new System.Drawing.Point(4, 22);
      this.tabPageAtc3.Name = "tabPageAtc3";
      this.tabPageAtc3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageAtc3.Size = new System.Drawing.Size(616, 350);
      this.tabPageAtc3.TabIndex = 1;
      this.tabPageAtc3.Text = "AttacheCase3";
      this.tabPageAtc3.UseVisualStyleBackColor = true;
      // 
      // textBoxMissTypeLimit3
      // 
      this.textBoxMissTypeLimit3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxMissTypeLimit3.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxMissTypeLimit3.Location = new System.Drawing.Point(178, 35);
      this.textBoxMissTypeLimit3.Name = "textBoxMissTypeLimit3";
      this.textBoxMissTypeLimit3.ReadOnly = true;
      this.textBoxMissTypeLimit3.Size = new System.Drawing.Size(133, 19);
      this.textBoxMissTypeLimit3.TabIndex = 29;
      // 
      // labelMissTypeLimit3
      // 
      this.labelMissTypeLimit3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelMissTypeLimit3.AutoSize = true;
      this.labelMissTypeLimit3.Location = new System.Drawing.Point(176, 20);
      this.labelMissTypeLimit3.Name = "labelMissTypeLimit3";
      this.labelMissTypeLimit3.Size = new System.Drawing.Size(79, 12);
      this.labelMissTypeLimit3.TabIndex = 28;
      this.labelMissTypeLimit3.Text = "MissTypeLimit";
      // 
      // textBoxFileSignature3
      // 
      this.textBoxFileSignature3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxFileSignature3.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxFileSignature3.Location = new System.Drawing.Point(19, 81);
      this.textBoxFileSignature3.Name = "textBoxFileSignature3";
      this.textBoxFileSignature3.ReadOnly = true;
      this.textBoxFileSignature3.Size = new System.Drawing.Size(133, 19);
      this.textBoxFileSignature3.TabIndex = 27;
      // 
      // label5
      // 
      this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(17, 66);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 12);
      this.label5.TabIndex = 26;
      this.label5.Text = "FileSignature";
      // 
      // textBoxOutputFileList
      // 
      this.textBoxOutputFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutputFileList.Location = new System.Drawing.Point(15, 219);
      this.textBoxOutputFileList.Multiline = true;
      this.textBoxOutputFileList.Name = "textBoxOutputFileList";
      this.textBoxOutputFileList.ReadOnly = true;
      this.textBoxOutputFileList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxOutputFileList.Size = new System.Drawing.Size(582, 117);
      this.textBoxOutputFileList.TabIndex = 25;
      this.textBoxOutputFileList.TextChanged += new System.EventHandler(this.textBoxOutputFileList_TextChanged);
      // 
      // labelOutputFileList
      // 
      this.labelOutputFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelOutputFileList.AutoSize = true;
      this.labelOutputFileList.Location = new System.Drawing.Point(17, 204);
      this.labelOutputFileList.Name = "labelOutputFileList";
      this.labelOutputFileList.Size = new System.Drawing.Size(77, 12);
      this.labelOutputFileList.TabIndex = 24;
      this.labelOutputFileList.Text = "OutputFileList";
      // 
      // textBoxRfc2898DeriveBytes
      // 
      this.textBoxRfc2898DeriveBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxRfc2898DeriveBytes.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxRfc2898DeriveBytes.Location = new System.Drawing.Point(19, 170);
      this.textBoxRfc2898DeriveBytes.Name = "textBoxRfc2898DeriveBytes";
      this.textBoxRfc2898DeriveBytes.ReadOnly = true;
      this.textBoxRfc2898DeriveBytes.Size = new System.Drawing.Size(578, 19);
      this.textBoxRfc2898DeriveBytes.TabIndex = 23;
      // 
      // labelRfc2898DeriveBytes
      // 
      this.labelRfc2898DeriveBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelRfc2898DeriveBytes.AutoSize = true;
      this.labelRfc2898DeriveBytes.Location = new System.Drawing.Point(17, 155);
      this.labelRfc2898DeriveBytes.Name = "labelRfc2898DeriveBytes";
      this.labelRfc2898DeriveBytes.Size = new System.Drawing.Size(110, 12);
      this.labelRfc2898DeriveBytes.TabIndex = 22;
      this.labelRfc2898DeriveBytes.Text = "Rfc2898DeriveBytes";
      // 
      // textBoxSalt
      // 
      this.textBoxSalt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxSalt.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxSalt.Location = new System.Drawing.Point(19, 127);
      this.textBoxSalt.Name = "textBoxSalt";
      this.textBoxSalt.ReadOnly = true;
      this.textBoxSalt.Size = new System.Drawing.Size(578, 19);
      this.textBoxSalt.TabIndex = 21;
      // 
      // labelSalt
      // 
      this.labelSalt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelSalt.AutoSize = true;
      this.labelSalt.Location = new System.Drawing.Point(17, 112);
      this.labelSalt.Name = "labelSalt";
      this.labelSalt.Size = new System.Drawing.Size(25, 12);
      this.labelSalt.TabIndex = 20;
      this.labelSalt.Text = "Salt";
      // 
      // textBoxAtcHeaderSize
      // 
      this.textBoxAtcHeaderSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxAtcHeaderSize.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxAtcHeaderSize.Location = new System.Drawing.Point(337, 81);
      this.textBoxAtcHeaderSize.Name = "textBoxAtcHeaderSize";
      this.textBoxAtcHeaderSize.ReadOnly = true;
      this.textBoxAtcHeaderSize.Size = new System.Drawing.Size(133, 19);
      this.textBoxAtcHeaderSize.TabIndex = 19;
      // 
      // labelAtcHeaderSize
      // 
      this.labelAtcHeaderSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelAtcHeaderSize.AutoSize = true;
      this.labelAtcHeaderSize.Location = new System.Drawing.Point(335, 66);
      this.labelAtcHeaderSize.Name = "labelAtcHeaderSize";
      this.labelAtcHeaderSize.Size = new System.Drawing.Size(80, 12);
      this.labelAtcHeaderSize.TabIndex = 18;
      this.labelAtcHeaderSize.Text = "AtcHeaderSize";
      // 
      // textBoxDataFileVersion3
      // 
      this.textBoxDataFileVersion3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxDataFileVersion3.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxDataFileVersion3.Location = new System.Drawing.Point(178, 81);
      this.textBoxDataFileVersion3.Name = "textBoxDataFileVersion3";
      this.textBoxDataFileVersion3.ReadOnly = true;
      this.textBoxDataFileVersion3.Size = new System.Drawing.Size(133, 19);
      this.textBoxDataFileVersion3.TabIndex = 15;
      // 
      // labelDataFileVersion
      // 
      this.labelDataFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelDataFileVersion.AutoSize = true;
      this.labelDataFileVersion.Location = new System.Drawing.Point(174, 66);
      this.labelDataFileVersion.Name = "labelDataFileVersion";
      this.labelDataFileVersion.Size = new System.Drawing.Size(87, 12);
      this.labelDataFileVersion.TabIndex = 14;
      this.labelDataFileVersion.Text = "DataFileVersion";
      // 
      // textBroken3
      // 
      this.textBroken3.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBroken3.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBroken3.Location = new System.Drawing.Point(337, 35);
      this.textBroken3.Name = "textBroken3";
      this.textBroken3.ReadOnly = true;
      this.textBroken3.Size = new System.Drawing.Size(133, 19);
      this.textBroken3.TabIndex = 11;
      // 
      // labelBroken
      // 
      this.labelBroken.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelBroken.AutoSize = true;
      this.labelBroken.Location = new System.Drawing.Point(335, 20);
      this.labelBroken.Name = "labelBroken";
      this.labelBroken.Size = new System.Drawing.Size(45, 12);
      this.labelBroken.TabIndex = 10;
      this.labelBroken.Text = "fBroken";
      // 
      // labelAppFileVersion
      // 
      this.labelAppFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelAppFileVersion.AutoSize = true;
      this.labelAppFileVersion.Location = new System.Drawing.Point(17, 20);
      this.labelAppFileVersion.Name = "labelAppFileVersion";
      this.labelAppFileVersion.Size = new System.Drawing.Size(83, 12);
      this.labelAppFileVersion.TabIndex = 9;
      this.labelAppFileVersion.Text = "AppFileVersion";
      // 
      // textBoxAppFileVersion
      // 
      this.textBoxAppFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxAppFileVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxAppFileVersion.Location = new System.Drawing.Point(19, 35);
      this.textBoxAppFileVersion.Name = "textBoxAppFileVersion";
      this.textBoxAppFileVersion.ReadOnly = true;
      this.textBoxAppFileVersion.Size = new System.Drawing.Size(133, 19);
      this.textBoxAppFileVersion.TabIndex = 8;
      // 
      // Form5
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 441);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(640, 480);
      this.Name = "Form5";
      this.Text = "Developer Console";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form5_FormClosed);
      this.Load += new System.EventHandler(this.Form5_Load);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPageAtc2.ResumeLayout(false);
      this.tabPageAtc2.PerformLayout();
      this.tabPageAtc3.ResumeLayout(false);
      this.tabPageAtc3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageAtc2;
    private System.Windows.Forms.TabPage tabPageAtc3;
    private System.Windows.Forms.TextBox textBoxDataFileVersion3;
    private System.Windows.Forms.Label labelDataFileVersion;
    private System.Windows.Forms.TextBox textBroken3;
    private System.Windows.Forms.Label labelBroken;
    private System.Windows.Forms.Label labelAppFileVersion;
    private System.Windows.Forms.TextBox textBoxAppFileVersion;
    private System.Windows.Forms.TextBox textBoxAtcHeaderSize;
    private System.Windows.Forms.Label labelAtcHeaderSize;
    private System.Windows.Forms.Label labelSalt;
    private System.Windows.Forms.TextBox textBoxSalt;
    private System.Windows.Forms.Label labelRfc2898DeriveBytes;
    private System.Windows.Forms.TextBox textBoxRfc2898DeriveBytes;
    private System.Windows.Forms.TextBox textBoxOutputFileList;
    private System.Windows.Forms.Label labelOutputFileList;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDecryptionTime;
    private System.Windows.Forms.TextBox textBoxMissTypeLimit2;
    private System.Windows.Forms.Label labelMissTypeLimit;
    private System.Windows.Forms.TextBox textBoxReserved;
    private System.Windows.Forms.Label labelReserved;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label labelDataSubVersion;
    private System.Windows.Forms.TextBox textBoxDataSubVersion;
    private System.Windows.Forms.TextBox textBoxfBroken2;
    private System.Windows.Forms.Label labelfBroken;
    private System.Windows.Forms.TextBox textBoxlFileSignature2;
    private System.Windows.Forms.Label labeFileSignature;
    private System.Windows.Forms.TextBox textBoxDataFileVersion2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxTypeAlgorism;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBoxOutputFileList2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBoxAtcHeaderSize2;
    private System.Windows.Forms.Label labelAtcHeaderSize2;
    private System.Windows.Forms.TextBox textBoxMissTypeLimit3;
    private System.Windows.Forms.Label labelMissTypeLimit3;
    private System.Windows.Forms.TextBox textBoxFileSignature3;
    private System.Windows.Forms.Label label5;
  }
}