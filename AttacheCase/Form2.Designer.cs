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
      this.labelBeta = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).BeginInit();
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
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // labelBeta
      // 
      resources.ApplyResources(this.labelBeta, "labelBeta");
      this.labelBeta.Name = "labelBeta";
      // 
      // Form2
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonOK;
      this.Controls.Add(this.labelBeta);
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
      this.Load += new System.EventHandler(this.Form2_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationIcon)).EndInit();
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
    private System.Windows.Forms.Label labelBeta;
  }
}