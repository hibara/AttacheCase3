namespace AttacheCase
{
  partial class Form2
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
      this.labelAppName = new System.Windows.Forms.Label();
      this.buttonOK = new System.Windows.Forms.Button();
      this.labelVersion = new System.Windows.Forms.Label();
      this.pictureBoxApplicationIcon = new System.Windows.Forms.PictureBox();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.pictureBoxProgressCircle = new System.Windows.Forms.PictureBox();
      this.linkLabelCheckForUpdates = new System.Windows.Forms.LinkLabel();
      this.pictureBoxCheckMark = new System.Windows.Forms.PictureBox();
      this.pictureBoxExclamationMark = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressCircle)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckMark)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark)).BeginInit();
      this.SuspendLayout();
      // 
      // labelAppName
      // 
      resources.ApplyResources(this.labelAppName, "labelAppName");
      this.labelAppName.Name = "labelAppName";
      // 
      // buttonOK
      // 
      resources.ApplyResources(this.buttonOK, "buttonOK");
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // labelVersion
      // 
      resources.ApplyResources(this.labelVersion, "labelVersion");
      this.labelVersion.Name = "labelVersion";
      // 
      // pictureBoxApplicationIcon
      // 
      resources.ApplyResources(this.pictureBoxApplicationIcon, "pictureBoxApplicationIcon");
      this.pictureBoxApplicationIcon.Name = "pictureBoxApplicationIcon";
      this.pictureBoxApplicationIcon.TabStop = false;
      // 
      // labelCopyright
      // 
      resources.ApplyResources(this.labelCopyright, "labelCopyright");
      this.labelCopyright.Name = "labelCopyright";
      // 
      // linkLabel1
      // 
      resources.ApplyResources(this.linkLabel1, "linkLabel1");
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.TabStop = true;
      this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // pictureBoxProgressCircle
      // 
      this.pictureBoxProgressCircle.BackColor = System.Drawing.Color.Transparent;
      resources.ApplyResources(this.pictureBoxProgressCircle, "pictureBoxProgressCircle");
      this.pictureBoxProgressCircle.Name = "pictureBoxProgressCircle";
      this.pictureBoxProgressCircle.TabStop = false;
      // 
      // linkLabelCheckForUpdates
      // 
      resources.ApplyResources(this.linkLabelCheckForUpdates, "linkLabelCheckForUpdates");
      this.linkLabelCheckForUpdates.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabelCheckForUpdates.Name = "linkLabelCheckForUpdates";
      this.linkLabelCheckForUpdates.TabStop = true;
      this.linkLabelCheckForUpdates.VisitedLinkColor = System.Drawing.Color.Blue;
      this.linkLabelCheckForUpdates.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCheckForUpdates_LinkClicked);
      // 
      // pictureBoxCheckMark
      // 
      resources.ApplyResources(this.pictureBoxCheckMark, "pictureBoxCheckMark");
      this.pictureBoxCheckMark.Name = "pictureBoxCheckMark";
      this.pictureBoxCheckMark.TabStop = false;
      // 
      // pictureBoxExclamationMark
      // 
      resources.ApplyResources(this.pictureBoxExclamationMark, "pictureBoxExclamationMark");
      this.pictureBoxExclamationMark.Name = "pictureBoxExclamationMark";
      this.pictureBoxExclamationMark.TabStop = false;
      // 
      // Form2
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonOK;
      this.Controls.Add(this.pictureBoxExclamationMark);
      this.Controls.Add(this.pictureBoxCheckMark);
      this.Controls.Add(this.linkLabelCheckForUpdates);
      this.Controls.Add(this.pictureBoxProgressCircle);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.pictureBoxApplicationIcon);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.labelAppName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Form2";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
      this.Load += new System.EventHandler(this.Form2_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressCircle)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckMark)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExclamationMark)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelAppName;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.PictureBox pictureBoxApplicationIcon;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.PictureBox pictureBoxProgressCircle;
    private System.Windows.Forms.LinkLabel linkLabelCheckForUpdates;
    private System.Windows.Forms.PictureBox pictureBoxCheckMark;
    private System.Windows.Forms.PictureBox pictureBoxExclamationMark;
  }
}