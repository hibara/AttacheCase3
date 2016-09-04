namespace Exeout
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
      this.buttonOK = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.labelApplicationName = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.linkLabelURL = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      resources.ApplyResources(this.buttonOK, "buttonOK");
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // pictureBox1
      // 
      resources.ApplyResources(this.pictureBox1, "pictureBox1");
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.TabStop = false;
      // 
      // labelApplicationName
      // 
      resources.ApplyResources(this.labelApplicationName, "labelApplicationName");
      this.labelApplicationName.Name = "labelApplicationName";
      // 
      // labelVersion
      // 
      resources.ApplyResources(this.labelVersion, "labelVersion");
      this.labelVersion.Name = "labelVersion";
      // 
      // labelCopyright
      // 
      resources.ApplyResources(this.labelCopyright, "labelCopyright");
      this.labelCopyright.Name = "labelCopyright";
      // 
      // linkLabelURL
      // 
      resources.ApplyResources(this.linkLabelURL, "linkLabelURL");
      this.linkLabelURL.Name = "linkLabelURL";
      this.linkLabelURL.TabStop = true;
      this.linkLabelURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
      // 
      // Form2
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonOK;
      this.Controls.Add(this.linkLabelURL);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.labelApplicationName);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "Form2";
      this.Load += new System.EventHandler(this.Form2_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelApplicationName;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.LinkLabel linkLabelURL;
  }
}