
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonClose = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageAtc2 = new System.Windows.Forms.TabPage();
      this.tabPageAtc3 = new System.Windows.Forms.TabPage();
      this.textBoxOutputFileList = new System.Windows.Forms.TextBox();
      this.labelOutputFileList = new System.Windows.Forms.Label();
      this.textBoxRfc2898DeriveBytes = new System.Windows.Forms.TextBox();
      this.labelRfc2898DeriveBytes = new System.Windows.Forms.Label();
      this.textBoxSalt = new System.Windows.Forms.TextBox();
      this.labelSalt = new System.Windows.Forms.Label();
      this.textBoxAtcHeaderSize = new System.Windows.Forms.TextBox();
      this.labelAtcHeaderSize = new System.Windows.Forms.Label();
      this.textBoxTypeAlgorism = new System.Windows.Forms.TextBox();
      this.labelTypeAlgorism = new System.Windows.Forms.Label();
      this.textBoxDataFileVersion = new System.Windows.Forms.TextBox();
      this.labelDataFileVersion = new System.Windows.Forms.Label();
      this.textBoxTokenStr = new System.Windows.Forms.TextBox();
      this.TokenStr = new System.Windows.Forms.Label();
      this.textBroken = new System.Windows.Forms.TextBox();
      this.labelBroken = new System.Windows.Forms.Label();
      this.labelDataSebVersion = new System.Windows.Forms.Label();
      this.textBoxDataSebVersion = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPageAtc3.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 419);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(624, 22);
      this.statusStrip1.TabIndex = 0;
      this.statusStrip1.Text = "statusStrip1";
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
      this.tabPageAtc2.Location = new System.Drawing.Point(4, 22);
      this.tabPageAtc2.Name = "tabPageAtc2";
      this.tabPageAtc2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageAtc2.Size = new System.Drawing.Size(643, 350);
      this.tabPageAtc2.TabIndex = 0;
      this.tabPageAtc2.Text = "AttacheCase2";
      this.tabPageAtc2.UseVisualStyleBackColor = true;
      // 
      // tabPageAtc3
      // 
      this.tabPageAtc3.Controls.Add(this.textBoxOutputFileList);
      this.tabPageAtc3.Controls.Add(this.labelOutputFileList);
      this.tabPageAtc3.Controls.Add(this.textBoxRfc2898DeriveBytes);
      this.tabPageAtc3.Controls.Add(this.labelRfc2898DeriveBytes);
      this.tabPageAtc3.Controls.Add(this.textBoxSalt);
      this.tabPageAtc3.Controls.Add(this.labelSalt);
      this.tabPageAtc3.Controls.Add(this.textBoxAtcHeaderSize);
      this.tabPageAtc3.Controls.Add(this.labelAtcHeaderSize);
      this.tabPageAtc3.Controls.Add(this.textBoxTypeAlgorism);
      this.tabPageAtc3.Controls.Add(this.labelTypeAlgorism);
      this.tabPageAtc3.Controls.Add(this.textBoxDataFileVersion);
      this.tabPageAtc3.Controls.Add(this.labelDataFileVersion);
      this.tabPageAtc3.Controls.Add(this.textBoxTokenStr);
      this.tabPageAtc3.Controls.Add(this.TokenStr);
      this.tabPageAtc3.Controls.Add(this.textBroken);
      this.tabPageAtc3.Controls.Add(this.labelBroken);
      this.tabPageAtc3.Controls.Add(this.labelDataSebVersion);
      this.tabPageAtc3.Controls.Add(this.textBoxDataSebVersion);
      this.tabPageAtc3.Location = new System.Drawing.Point(4, 22);
      this.tabPageAtc3.Name = "tabPageAtc3";
      this.tabPageAtc3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageAtc3.Size = new System.Drawing.Size(616, 350);
      this.tabPageAtc3.TabIndex = 1;
      this.tabPageAtc3.Text = "AttacheCase3";
      this.tabPageAtc3.UseVisualStyleBackColor = true;
      // 
      // textBoxOutputFileList
      // 
      this.textBoxOutputFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutputFileList.Location = new System.Drawing.Point(13, 195);
      this.textBoxOutputFileList.Multiline = true;
      this.textBoxOutputFileList.Name = "textBoxOutputFileList";
      this.textBoxOutputFileList.ReadOnly = true;
      this.textBoxOutputFileList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxOutputFileList.Size = new System.Drawing.Size(592, 134);
      this.textBoxOutputFileList.TabIndex = 25;
      this.textBoxOutputFileList.TextChanged += new System.EventHandler(this.textBoxOutputFileList_TextChanged);
      // 
      // labelOutputFileList
      // 
      this.labelOutputFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelOutputFileList.AutoSize = true;
      this.labelOutputFileList.Location = new System.Drawing.Point(11, 179);
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
      this.textBoxRfc2898DeriveBytes.Location = new System.Drawing.Point(13, 145);
      this.textBoxRfc2898DeriveBytes.Name = "textBoxRfc2898DeriveBytes";
      this.textBoxRfc2898DeriveBytes.ReadOnly = true;
      this.textBoxRfc2898DeriveBytes.Size = new System.Drawing.Size(594, 19);
      this.textBoxRfc2898DeriveBytes.TabIndex = 23;
      // 
      // labelRfc2898DeriveBytes
      // 
      this.labelRfc2898DeriveBytes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.labelRfc2898DeriveBytes.AutoSize = true;
      this.labelRfc2898DeriveBytes.Location = new System.Drawing.Point(13, 130);
      this.labelRfc2898DeriveBytes.Name = "labelRfc2898DeriveBytes";
      this.labelRfc2898DeriveBytes.Size = new System.Drawing.Size(110, 12);
      this.labelRfc2898DeriveBytes.TabIndex = 22;
      this.labelRfc2898DeriveBytes.Text = "Rfc2898DeriveBytes";
      // 
      // textBoxSalt
      // 
      this.textBoxSalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxSalt.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxSalt.Location = new System.Drawing.Point(319, 88);
      this.textBoxSalt.Name = "textBoxSalt";
      this.textBoxSalt.ReadOnly = true;
      this.textBoxSalt.Size = new System.Drawing.Size(288, 19);
      this.textBoxSalt.TabIndex = 21;
      // 
      // labelSalt
      // 
      this.labelSalt.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelSalt.AutoSize = true;
      this.labelSalt.Location = new System.Drawing.Point(319, 73);
      this.labelSalt.Name = "labelSalt";
      this.labelSalt.Size = new System.Drawing.Size(25, 12);
      this.labelSalt.TabIndex = 20;
      this.labelSalt.Text = "Salt";
      // 
      // textBoxAtcHeaderSize
      // 
      this.textBoxAtcHeaderSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxAtcHeaderSize.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxAtcHeaderSize.Location = new System.Drawing.Point(166, 88);
      this.textBoxAtcHeaderSize.Name = "textBoxAtcHeaderSize";
      this.textBoxAtcHeaderSize.ReadOnly = true;
      this.textBoxAtcHeaderSize.Size = new System.Drawing.Size(133, 19);
      this.textBoxAtcHeaderSize.TabIndex = 19;
      // 
      // labelAtcHeaderSize
      // 
      this.labelAtcHeaderSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelAtcHeaderSize.AutoSize = true;
      this.labelAtcHeaderSize.Location = new System.Drawing.Point(164, 73);
      this.labelAtcHeaderSize.Name = "labelAtcHeaderSize";
      this.labelAtcHeaderSize.Size = new System.Drawing.Size(80, 12);
      this.labelAtcHeaderSize.TabIndex = 18;
      this.labelAtcHeaderSize.Text = "AtcHeaderSize";
      // 
      // textBoxTypeAlgorism
      // 
      this.textBoxTypeAlgorism.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxTypeAlgorism.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxTypeAlgorism.Location = new System.Drawing.Point(13, 88);
      this.textBoxTypeAlgorism.Name = "textBoxTypeAlgorism";
      this.textBoxTypeAlgorism.ReadOnly = true;
      this.textBoxTypeAlgorism.Size = new System.Drawing.Size(133, 19);
      this.textBoxTypeAlgorism.TabIndex = 17;
      // 
      // labelTypeAlgorism
      // 
      this.labelTypeAlgorism.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelTypeAlgorism.AutoSize = true;
      this.labelTypeAlgorism.Location = new System.Drawing.Point(11, 73);
      this.labelTypeAlgorism.Name = "labelTypeAlgorism";
      this.labelTypeAlgorism.Size = new System.Drawing.Size(75, 12);
      this.labelTypeAlgorism.TabIndex = 16;
      this.labelTypeAlgorism.Text = "TypeAlgorism";
      // 
      // textBoxDataFileVersion
      // 
      this.textBoxDataFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxDataFileVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxDataFileVersion.Location = new System.Drawing.Point(474, 35);
      this.textBoxDataFileVersion.Name = "textBoxDataFileVersion";
      this.textBoxDataFileVersion.ReadOnly = true;
      this.textBoxDataFileVersion.Size = new System.Drawing.Size(133, 19);
      this.textBoxDataFileVersion.TabIndex = 15;
      // 
      // labelDataFileVersion
      // 
      this.labelDataFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelDataFileVersion.AutoSize = true;
      this.labelDataFileVersion.Location = new System.Drawing.Point(472, 20);
      this.labelDataFileVersion.Name = "labelDataFileVersion";
      this.labelDataFileVersion.Size = new System.Drawing.Size(87, 12);
      this.labelDataFileVersion.TabIndex = 14;
      this.labelDataFileVersion.Text = "DataFileVersion";
      // 
      // textBoxTokenStr
      // 
      this.textBoxTokenStr.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxTokenStr.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxTokenStr.Location = new System.Drawing.Point(319, 35);
      this.textBoxTokenStr.Name = "textBoxTokenStr";
      this.textBoxTokenStr.ReadOnly = true;
      this.textBoxTokenStr.Size = new System.Drawing.Size(133, 19);
      this.textBoxTokenStr.TabIndex = 13;
      // 
      // TokenStr
      // 
      this.TokenStr.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.TokenStr.AutoSize = true;
      this.TokenStr.Location = new System.Drawing.Point(317, 20);
      this.TokenStr.Name = "TokenStr";
      this.TokenStr.Size = new System.Drawing.Size(75, 12);
      this.TokenStr.TabIndex = 12;
      this.TokenStr.Text = "labelTokenStr";
      // 
      // textBroken
      // 
      this.textBroken.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBroken.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBroken.Location = new System.Drawing.Point(166, 35);
      this.textBroken.Name = "textBroken";
      this.textBroken.ReadOnly = true;
      this.textBroken.Size = new System.Drawing.Size(133, 19);
      this.textBroken.TabIndex = 11;
      // 
      // labelBroken
      // 
      this.labelBroken.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelBroken.AutoSize = true;
      this.labelBroken.Location = new System.Drawing.Point(164, 20);
      this.labelBroken.Name = "labelBroken";
      this.labelBroken.Size = new System.Drawing.Size(45, 12);
      this.labelBroken.TabIndex = 10;
      this.labelBroken.Text = "fBroken";
      // 
      // labelDataSebVersion
      // 
      this.labelDataSebVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.labelDataSebVersion.AutoSize = true;
      this.labelDataSebVersion.Location = new System.Drawing.Point(11, 20);
      this.labelDataSebVersion.Name = "labelDataSebVersion";
      this.labelDataSebVersion.Size = new System.Drawing.Size(87, 12);
      this.labelDataSebVersion.TabIndex = 9;
      this.labelDataSebVersion.Text = "DataSebVersion";
      // 
      // textBoxDataSebVersion
      // 
      this.textBoxDataSebVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.textBoxDataSebVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxDataSebVersion.Location = new System.Drawing.Point(13, 35);
      this.textBoxDataSebVersion.Name = "textBoxDataSebVersion";
      this.textBoxDataSebVersion.ReadOnly = true;
      this.textBoxDataSebVersion.Size = new System.Drawing.Size(133, 19);
      this.textBoxDataSebVersion.TabIndex = 8;
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
      this.panel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
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
    private System.Windows.Forms.TextBox textBoxDataFileVersion;
    private System.Windows.Forms.Label labelDataFileVersion;
    private System.Windows.Forms.TextBox textBoxTokenStr;
    private System.Windows.Forms.Label TokenStr;
    private System.Windows.Forms.TextBox textBroken;
    private System.Windows.Forms.Label labelBroken;
    private System.Windows.Forms.Label labelDataSebVersion;
    private System.Windows.Forms.TextBox textBoxDataSebVersion;
    private System.Windows.Forms.TextBox textBoxTypeAlgorism;
    private System.Windows.Forms.Label labelTypeAlgorism;
    private System.Windows.Forms.TextBox textBoxAtcHeaderSize;
    private System.Windows.Forms.Label labelAtcHeaderSize;
    private System.Windows.Forms.Label labelSalt;
    private System.Windows.Forms.TextBox textBoxSalt;
    private System.Windows.Forms.Label labelRfc2898DeriveBytes;
    private System.Windows.Forms.TextBox textBoxRfc2898DeriveBytes;
    private System.Windows.Forms.TextBox textBoxOutputFileList;
    private System.Windows.Forms.Label labelOutputFileList;
  }
}