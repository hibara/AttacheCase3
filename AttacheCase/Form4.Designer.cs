namespace AttacheCase
{
	partial class Form4
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
      this.panelOuter = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageInputPassword = new System.Windows.Forms.TabPage();
      this.panelInputPassword = new System.Windows.Forms.Panel();
      this.checkBoxNotMaskEncryptedPassword = new System.Windows.Forms.CheckBox();
      this.labelPasswordValid = new System.Windows.Forms.Label();
      this.pictureBoxPasswordValid = new System.Windows.Forms.PictureBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxRePassword = new System.Windows.Forms.TextBox();
      this.textBoxPassword = new System.Windows.Forms.TextBox();
      this.buttonPasswordCancel = new System.Windows.Forms.Button();
      this.buttonPasswordOK = new System.Windows.Forms.Button();
      this.tabPageOverwriteConfirm = new System.Windows.Forms.TabPage();
      this.panelOverwriteConfirm = new System.Windows.Forms.Panel();
      this.splitButton2 = new AttacheCase.SplitButton();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ToolStripMenuItemSkip = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemSkipAll = new System.Windows.Forms.ToolStripMenuItem();
      this.splitButton1 = new AttacheCase.SplitButton();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ToolStripMenuItemOverwrite = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemOverwriteAll = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemKeepNewer = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemkeepNewerAll = new System.Windows.Forms.ToolStripMenuItem();
      this.buttonOverwriteCancel = new System.Windows.Forms.Button();
      this.labelMessageText = new System.Windows.Forms.Label();
      this.pictureBoxQuestionIcon = new System.Windows.Forms.PictureBox();
      this.tabPageAskEncryptOrDecrypt = new System.Windows.Forms.TabPage();
      this.panelAskEncryptOrDecrypt = new System.Windows.Forms.Panel();
      this.buttonAskEncryptOrDecryptCancel = new System.Windows.Forms.Button();
      this.buttonDecrypt = new System.Windows.Forms.Button();
      this.buttonEncrypt = new System.Windows.Forms.Button();
      this.labelAskEncryptOrDecrypt = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panelOuter.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPageInputPassword.SuspendLayout();
      this.panelInputPassword.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordValid)).BeginInit();
      this.tabPageOverwriteConfirm.SuspendLayout();
      this.panelOverwriteConfirm.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionIcon)).BeginInit();
      this.tabPageAskEncryptOrDecrypt.SuspendLayout();
      this.panelAskEncryptOrDecrypt.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // panelOuter
      // 
      resources.ApplyResources(this.panelOuter, "panelOuter");
      this.panelOuter.Controls.Add(this.tabControl1);
      this.panelOuter.Name = "panelOuter";
      // 
      // tabControl1
      // 
      resources.ApplyResources(this.tabControl1, "tabControl1");
      this.tabControl1.Controls.Add(this.tabPageInputPassword);
      this.tabControl1.Controls.Add(this.tabPageOverwriteConfirm);
      this.tabControl1.Controls.Add(this.tabPageAskEncryptOrDecrypt);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      // 
      // tabPageInputPassword
      // 
      resources.ApplyResources(this.tabPageInputPassword, "tabPageInputPassword");
      this.tabPageInputPassword.Controls.Add(this.panelInputPassword);
      this.tabPageInputPassword.Name = "tabPageInputPassword";
      this.tabPageInputPassword.UseVisualStyleBackColor = true;
      // 
      // panelInputPassword
      // 
      resources.ApplyResources(this.panelInputPassword, "panelInputPassword");
      this.panelInputPassword.Controls.Add(this.checkBoxNotMaskEncryptedPassword);
      this.panelInputPassword.Controls.Add(this.labelPasswordValid);
      this.panelInputPassword.Controls.Add(this.pictureBoxPasswordValid);
      this.panelInputPassword.Controls.Add(this.label2);
      this.panelInputPassword.Controls.Add(this.label1);
      this.panelInputPassword.Controls.Add(this.textBoxRePassword);
      this.panelInputPassword.Controls.Add(this.textBoxPassword);
      this.panelInputPassword.Controls.Add(this.buttonPasswordCancel);
      this.panelInputPassword.Controls.Add(this.buttonPasswordOK);
      this.panelInputPassword.Name = "panelInputPassword";
      // 
      // checkBoxNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskEncryptedPassword, "checkBoxNotMaskEncryptedPassword");
      this.checkBoxNotMaskEncryptedPassword.Name = "checkBoxNotMaskEncryptedPassword";
      this.checkBoxNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxNotMaskEncryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxNotMaskEncryptedPassword_CheckedChanged);
      // 
      // labelPasswordValid
      // 
      resources.ApplyResources(this.labelPasswordValid, "labelPasswordValid");
      this.labelPasswordValid.Name = "labelPasswordValid";
      // 
      // pictureBoxPasswordValid
      // 
      resources.ApplyResources(this.pictureBoxPasswordValid, "pictureBoxPasswordValid");
      this.pictureBoxPasswordValid.Name = "pictureBoxPasswordValid";
      this.pictureBoxPasswordValid.TabStop = false;
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.Name = "label2";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // textBoxRePassword
      // 
      resources.ApplyResources(this.textBoxRePassword, "textBoxRePassword");
      this.textBoxRePassword.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.textBoxRePassword.Name = "textBoxRePassword";
      this.textBoxRePassword.UseSystemPasswordChar = true;
      this.textBoxRePassword.TextChanged += new System.EventHandler(this.textBoxRePassword_TextChanged);
      // 
      // textBoxPassword
      // 
      resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.UseSystemPasswordChar = true;
      this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
      // 
      // buttonPasswordCancel
      // 
      resources.ApplyResources(this.buttonPasswordCancel, "buttonPasswordCancel");
      this.buttonPasswordCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonPasswordCancel.Name = "buttonPasswordCancel";
      this.buttonPasswordCancel.UseVisualStyleBackColor = true;
      this.buttonPasswordCancel.Click += new System.EventHandler(this.buttonPasswordCancel_Click);
      // 
      // buttonPasswordOK
      // 
      resources.ApplyResources(this.buttonPasswordOK, "buttonPasswordOK");
      this.buttonPasswordOK.Name = "buttonPasswordOK";
      this.buttonPasswordOK.UseVisualStyleBackColor = true;
      this.buttonPasswordOK.Click += new System.EventHandler(this.buttonPasswordOK_Click);
      // 
      // tabPageOverwriteConfirm
      // 
      resources.ApplyResources(this.tabPageOverwriteConfirm, "tabPageOverwriteConfirm");
      this.tabPageOverwriteConfirm.Controls.Add(this.panelOverwriteConfirm);
      this.tabPageOverwriteConfirm.Name = "tabPageOverwriteConfirm";
      this.tabPageOverwriteConfirm.UseVisualStyleBackColor = true;
      // 
      // panelOverwriteConfirm
      // 
      resources.ApplyResources(this.panelOverwriteConfirm, "panelOverwriteConfirm");
      this.panelOverwriteConfirm.Controls.Add(this.splitButton2);
      this.panelOverwriteConfirm.Controls.Add(this.splitButton1);
      this.panelOverwriteConfirm.Controls.Add(this.buttonOverwriteCancel);
      this.panelOverwriteConfirm.Controls.Add(this.labelMessageText);
      this.panelOverwriteConfirm.Controls.Add(this.pictureBoxQuestionIcon);
      this.panelOverwriteConfirm.Name = "panelOverwriteConfirm";
      // 
      // splitButton2
      // 
      resources.ApplyResources(this.splitButton2, "splitButton2");
      this.splitButton2.Menu = this.contextMenuStrip2;
      this.splitButton2.Name = "splitButton2";
      this.splitButton2.UseVisualStyleBackColor = true;
      this.splitButton2.Click += new System.EventHandler(this.splitButton2_Click);
      // 
      // contextMenuStrip2
      // 
      resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSkip,
            this.ToolStripMenuItemSkipAll});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      // 
      // ToolStripMenuItemSkip
      // 
      resources.ApplyResources(this.ToolStripMenuItemSkip, "ToolStripMenuItemSkip");
      this.ToolStripMenuItemSkip.Name = "ToolStripMenuItemSkip";
      this.ToolStripMenuItemSkip.Click += new System.EventHandler(this.ToolStripMenuItemSkip_Click);
      // 
      // ToolStripMenuItemSkipAll
      // 
      resources.ApplyResources(this.ToolStripMenuItemSkipAll, "ToolStripMenuItemSkipAll");
      this.ToolStripMenuItemSkipAll.Name = "ToolStripMenuItemSkipAll";
      this.ToolStripMenuItemSkipAll.Click += new System.EventHandler(this.ToolStripMenuItemSkipAll_Click);
      // 
      // splitButton1
      // 
      resources.ApplyResources(this.splitButton1, "splitButton1");
      this.splitButton1.Menu = this.contextMenuStrip1;
      this.splitButton1.Name = "splitButton1";
      this.splitButton1.UseVisualStyleBackColor = true;
      this.splitButton1.Click += new System.EventHandler(this.splitButton1_Click);
      // 
      // contextMenuStrip1
      // 
      resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOverwrite,
            this.ToolStripMenuItemOverwriteAll,
            this.toolStripMenuItem1,
            this.ToolStripMenuItemKeepNewer,
            this.ToolStripMenuItemkeepNewerAll});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      // 
      // ToolStripMenuItemOverwrite
      // 
      resources.ApplyResources(this.ToolStripMenuItemOverwrite, "ToolStripMenuItemOverwrite");
      this.ToolStripMenuItemOverwrite.Name = "ToolStripMenuItemOverwrite";
      this.ToolStripMenuItemOverwrite.Click += new System.EventHandler(this.ToolStripMenuItemOverwrite_Click);
      // 
      // ToolStripMenuItemOverwriteAll
      // 
      resources.ApplyResources(this.ToolStripMenuItemOverwriteAll, "ToolStripMenuItemOverwriteAll");
      this.ToolStripMenuItemOverwriteAll.Name = "ToolStripMenuItemOverwriteAll";
      this.ToolStripMenuItemOverwriteAll.Click += new System.EventHandler(this.ToolStripMenuItemOverwriteAll_Click);
      // 
      // toolStripMenuItem1
      // 
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      // 
      // ToolStripMenuItemKeepNewer
      // 
      resources.ApplyResources(this.ToolStripMenuItemKeepNewer, "ToolStripMenuItemKeepNewer");
      this.ToolStripMenuItemKeepNewer.Name = "ToolStripMenuItemKeepNewer";
      this.ToolStripMenuItemKeepNewer.Click += new System.EventHandler(this.ToolStripMenuItemKeepNewer_Click);
      // 
      // ToolStripMenuItemkeepNewerAll
      // 
      resources.ApplyResources(this.ToolStripMenuItemkeepNewerAll, "ToolStripMenuItemkeepNewerAll");
      this.ToolStripMenuItemkeepNewerAll.Name = "ToolStripMenuItemkeepNewerAll";
      this.ToolStripMenuItemkeepNewerAll.Click += new System.EventHandler(this.ToolStripMenuItemkeepNewerAll_Click);
      // 
      // buttonOverwriteCancel
      // 
      resources.ApplyResources(this.buttonOverwriteCancel, "buttonOverwriteCancel");
      this.buttonOverwriteCancel.Name = "buttonOverwriteCancel";
      this.buttonOverwriteCancel.UseVisualStyleBackColor = true;
      this.buttonOverwriteCancel.Click += new System.EventHandler(this.buttonOverwriteCancel_Click);
      // 
      // labelMessageText
      // 
      resources.ApplyResources(this.labelMessageText, "labelMessageText");
      this.labelMessageText.Name = "labelMessageText";
      // 
      // pictureBoxQuestionIcon
      // 
      resources.ApplyResources(this.pictureBoxQuestionIcon, "pictureBoxQuestionIcon");
      this.pictureBoxQuestionIcon.Name = "pictureBoxQuestionIcon";
      this.pictureBoxQuestionIcon.TabStop = false;
      // 
      // tabPageAskEncryptOrDecrypt
      // 
      resources.ApplyResources(this.tabPageAskEncryptOrDecrypt, "tabPageAskEncryptOrDecrypt");
      this.tabPageAskEncryptOrDecrypt.Controls.Add(this.panelAskEncryptOrDecrypt);
      this.tabPageAskEncryptOrDecrypt.Name = "tabPageAskEncryptOrDecrypt";
      this.tabPageAskEncryptOrDecrypt.UseVisualStyleBackColor = true;
      // 
      // panelAskEncryptOrDecrypt
      // 
      resources.ApplyResources(this.panelAskEncryptOrDecrypt, "panelAskEncryptOrDecrypt");
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonAskEncryptOrDecryptCancel);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonDecrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonEncrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.labelAskEncryptOrDecrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.pictureBox1);
      this.panelAskEncryptOrDecrypt.Name = "panelAskEncryptOrDecrypt";
      // 
      // buttonAskEncryptOrDecryptCancel
      // 
      resources.ApplyResources(this.buttonAskEncryptOrDecryptCancel, "buttonAskEncryptOrDecryptCancel");
      this.buttonAskEncryptOrDecryptCancel.Name = "buttonAskEncryptOrDecryptCancel";
      this.buttonAskEncryptOrDecryptCancel.UseVisualStyleBackColor = true;
      this.buttonAskEncryptOrDecryptCancel.Click += new System.EventHandler(this.buttonAskEncryptOrDecryptCancel_Click);
      // 
      // buttonDecrypt
      // 
      resources.ApplyResources(this.buttonDecrypt, "buttonDecrypt");
      this.buttonDecrypt.Name = "buttonDecrypt";
      this.buttonDecrypt.UseVisualStyleBackColor = true;
      this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
      // 
      // buttonEncrypt
      // 
      resources.ApplyResources(this.buttonEncrypt, "buttonEncrypt");
      this.buttonEncrypt.Name = "buttonEncrypt";
      this.buttonEncrypt.UseVisualStyleBackColor = true;
      this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
      // 
      // labelAskEncryptOrDecrypt
      // 
      resources.ApplyResources(this.labelAskEncryptOrDecrypt, "labelAskEncryptOrDecrypt");
      this.labelAskEncryptOrDecrypt.Name = "labelAskEncryptOrDecrypt";
      // 
      // pictureBox1
      // 
      resources.ApplyResources(this.pictureBox1, "pictureBox1");
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.TabStop = false;
      // 
      // Form4
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonPasswordCancel;
      this.Controls.Add(this.panelOuter);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Form4";
      this.ShowIcon = false;
      this.TopMost = true;
      this.Shown += new System.EventHandler(this.Form4_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
      this.panelOuter.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPageInputPassword.ResumeLayout(false);
      this.panelInputPassword.ResumeLayout(false);
      this.panelInputPassword.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordValid)).EndInit();
      this.tabPageOverwriteConfirm.ResumeLayout(false);
      this.panelOverwriteConfirm.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestionIcon)).EndInit();
      this.tabPageAskEncryptOrDecrypt.ResumeLayout(false);
      this.panelAskEncryptOrDecrypt.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelOuter;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageInputPassword;
		private System.Windows.Forms.Panel panelInputPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxRePassword;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Button buttonPasswordCancel;
		private System.Windows.Forms.Button buttonPasswordOK;
		private System.Windows.Forms.Label labelPasswordValid;
		private System.Windows.Forms.PictureBox pictureBoxPasswordValid;
		private System.Windows.Forms.TabPage tabPageOverwriteConfirm;
		private System.Windows.Forms.Panel panelOverwriteConfirm;
		private System.Windows.Forms.Button buttonOverwriteCancel;
		private System.Windows.Forms.Label labelMessageText;
		private System.Windows.Forms.PictureBox pictureBoxQuestionIcon;
		private System.Windows.Forms.TabPage tabPageAskEncryptOrDecrypt;
		private System.Windows.Forms.Panel panelAskEncryptOrDecrypt;
		private System.Windows.Forms.Button buttonAskEncryptOrDecryptCancel;
		private System.Windows.Forms.Button buttonDecrypt;
		private System.Windows.Forms.Button buttonEncrypt;
		private System.Windows.Forms.Label labelAskEncryptOrDecrypt;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox checkBoxNotMaskEncryptedPassword;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOverwriteAll;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemKeepNewer;
    private SplitButton splitButton1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOverwrite;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemkeepNewerAll;
    private SplitButton splitButton2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSkip;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSkipAll;
  }
}