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
      this.tabPageInvalidChar = new System.Windows.Forms.TabPage();
      this.panelInvalidChar = new System.Windows.Forms.Panel();
      this.buttonInvalidCharYes = new System.Windows.Forms.Button();
      this.buttonInvalidCharCancel = new System.Windows.Forms.Button();
      this.labelInvalidChar = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.tabPageConfirmToReadIniFile = new System.Windows.Forms.TabPage();
      this.panelConfirmToReadIniFile = new System.Windows.Forms.Panel();
      this.checkBoxConfirmToReadIniFile = new System.Windows.Forms.CheckBox();
      this.labelConfirmToReadIniFileAlert = new System.Windows.Forms.Label();
      this.labelIniFilePath = new System.Windows.Forms.Label();
      this.buttonConfirmToReadIniFileYes = new System.Windows.Forms.Button();
      this.buttonConfirmToReadIniFileNo = new System.Windows.Forms.Button();
      this.labelConfirmToReadIniFile = new System.Windows.Forms.Label();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
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
      this.tabPageInvalidChar.SuspendLayout();
      this.panelInvalidChar.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.tabPageConfirmToReadIniFile.SuspendLayout();
      this.panelConfirmToReadIniFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.SuspendLayout();
      // 
      // panelOuter
      // 
      this.panelOuter.Controls.Add(this.tabControl1);
      resources.ApplyResources(this.panelOuter, "panelOuter");
      this.panelOuter.Name = "panelOuter";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPageInputPassword);
      this.tabControl1.Controls.Add(this.tabPageOverwriteConfirm);
      this.tabControl1.Controls.Add(this.tabPageAskEncryptOrDecrypt);
      this.tabControl1.Controls.Add(this.tabPageInvalidChar);
      this.tabControl1.Controls.Add(this.tabPageConfirmToReadIniFile);
      resources.ApplyResources(this.tabControl1, "tabControl1");
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      // 
      // tabPageInputPassword
      // 
      this.tabPageInputPassword.Controls.Add(this.panelInputPassword);
      resources.ApplyResources(this.tabPageInputPassword, "tabPageInputPassword");
      this.tabPageInputPassword.Name = "tabPageInputPassword";
      this.tabPageInputPassword.UseVisualStyleBackColor = true;
      // 
      // panelInputPassword
      // 
      this.panelInputPassword.Controls.Add(this.checkBoxNotMaskEncryptedPassword);
      this.panelInputPassword.Controls.Add(this.labelPasswordValid);
      this.panelInputPassword.Controls.Add(this.pictureBoxPasswordValid);
      this.panelInputPassword.Controls.Add(this.label2);
      this.panelInputPassword.Controls.Add(this.label1);
      this.panelInputPassword.Controls.Add(this.textBoxRePassword);
      this.panelInputPassword.Controls.Add(this.textBoxPassword);
      this.panelInputPassword.Controls.Add(this.buttonPasswordCancel);
      this.panelInputPassword.Controls.Add(this.buttonPasswordOK);
      resources.ApplyResources(this.panelInputPassword, "panelInputPassword");
      this.panelInputPassword.Name = "panelInputPassword";
      // 
      // checkBoxNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskEncryptedPassword, "checkBoxNotMaskEncryptedPassword");
      this.checkBoxNotMaskEncryptedPassword.Name = "checkBoxNotMaskEncryptedPassword";
      this.checkBoxNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
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
      // 
      // textBoxPassword
      // 
      resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.UseSystemPasswordChar = true;
      // 
      // buttonPasswordCancel
      // 
      resources.ApplyResources(this.buttonPasswordCancel, "buttonPasswordCancel");
      this.buttonPasswordCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonPasswordCancel.Name = "buttonPasswordCancel";
      this.buttonPasswordCancel.UseVisualStyleBackColor = true;
      // 
      // buttonPasswordOK
      // 
      resources.ApplyResources(this.buttonPasswordOK, "buttonPasswordOK");
      this.buttonPasswordOK.Name = "buttonPasswordOK";
      this.buttonPasswordOK.UseVisualStyleBackColor = true;
      // 
      // tabPageOverwriteConfirm
      // 
      this.tabPageOverwriteConfirm.Controls.Add(this.panelOverwriteConfirm);
      resources.ApplyResources(this.tabPageOverwriteConfirm, "tabPageOverwriteConfirm");
      this.tabPageOverwriteConfirm.Name = "tabPageOverwriteConfirm";
      this.tabPageOverwriteConfirm.UseVisualStyleBackColor = true;
      // 
      // panelOverwriteConfirm
      // 
      this.panelOverwriteConfirm.Controls.Add(this.splitButton2);
      this.panelOverwriteConfirm.Controls.Add(this.splitButton1);
      this.panelOverwriteConfirm.Controls.Add(this.buttonOverwriteCancel);
      this.panelOverwriteConfirm.Controls.Add(this.labelMessageText);
      this.panelOverwriteConfirm.Controls.Add(this.pictureBoxQuestionIcon);
      resources.ApplyResources(this.panelOverwriteConfirm, "panelOverwriteConfirm");
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
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSkip,
            this.ToolStripMenuItemSkipAll});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
      // 
      // ToolStripMenuItemSkip
      // 
      this.ToolStripMenuItemSkip.Name = "ToolStripMenuItemSkip";
      resources.ApplyResources(this.ToolStripMenuItemSkip, "ToolStripMenuItemSkip");
      this.ToolStripMenuItemSkip.Click += new System.EventHandler(this.ToolStripMenuItemSkip_Click);
      // 
      // ToolStripMenuItemSkipAll
      // 
      this.ToolStripMenuItemSkipAll.Name = "ToolStripMenuItemSkipAll";
      resources.ApplyResources(this.ToolStripMenuItemSkipAll, "ToolStripMenuItemSkipAll");
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
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOverwrite,
            this.ToolStripMenuItemOverwriteAll,
            this.toolStripMenuItem1,
            this.ToolStripMenuItemKeepNewer,
            this.ToolStripMenuItemkeepNewerAll});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
      // 
      // ToolStripMenuItemOverwrite
      // 
      this.ToolStripMenuItemOverwrite.Name = "ToolStripMenuItemOverwrite";
      resources.ApplyResources(this.ToolStripMenuItemOverwrite, "ToolStripMenuItemOverwrite");
      this.ToolStripMenuItemOverwrite.Click += new System.EventHandler(this.ToolStripMenuItemOverwrite_Click);
      // 
      // ToolStripMenuItemOverwriteAll
      // 
      this.ToolStripMenuItemOverwriteAll.Name = "ToolStripMenuItemOverwriteAll";
      resources.ApplyResources(this.ToolStripMenuItemOverwriteAll, "ToolStripMenuItemOverwriteAll");
      this.ToolStripMenuItemOverwriteAll.Click += new System.EventHandler(this.ToolStripMenuItemOverwriteAll_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      // 
      // ToolStripMenuItemKeepNewer
      // 
      this.ToolStripMenuItemKeepNewer.Name = "ToolStripMenuItemKeepNewer";
      resources.ApplyResources(this.ToolStripMenuItemKeepNewer, "ToolStripMenuItemKeepNewer");
      this.ToolStripMenuItemKeepNewer.Click += new System.EventHandler(this.ToolStripMenuItemKeepNewer_Click);
      // 
      // ToolStripMenuItemkeepNewerAll
      // 
      this.ToolStripMenuItemkeepNewerAll.Name = "ToolStripMenuItemkeepNewerAll";
      resources.ApplyResources(this.ToolStripMenuItemkeepNewerAll, "ToolStripMenuItemkeepNewerAll");
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
      this.tabPageAskEncryptOrDecrypt.Controls.Add(this.panelAskEncryptOrDecrypt);
      resources.ApplyResources(this.tabPageAskEncryptOrDecrypt, "tabPageAskEncryptOrDecrypt");
      this.tabPageAskEncryptOrDecrypt.Name = "tabPageAskEncryptOrDecrypt";
      this.tabPageAskEncryptOrDecrypt.UseVisualStyleBackColor = true;
      // 
      // panelAskEncryptOrDecrypt
      // 
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonAskEncryptOrDecryptCancel);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonDecrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.buttonEncrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.labelAskEncryptOrDecrypt);
      this.panelAskEncryptOrDecrypt.Controls.Add(this.pictureBox1);
      resources.ApplyResources(this.panelAskEncryptOrDecrypt, "panelAskEncryptOrDecrypt");
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
      // tabPageInvalidChar
      // 
      this.tabPageInvalidChar.Controls.Add(this.panelInvalidChar);
      resources.ApplyResources(this.tabPageInvalidChar, "tabPageInvalidChar");
      this.tabPageInvalidChar.Name = "tabPageInvalidChar";
      this.tabPageInvalidChar.UseVisualStyleBackColor = true;
      // 
      // panelInvalidChar
      // 
      this.panelInvalidChar.Controls.Add(this.buttonInvalidCharYes);
      this.panelInvalidChar.Controls.Add(this.buttonInvalidCharCancel);
      this.panelInvalidChar.Controls.Add(this.labelInvalidChar);
      this.panelInvalidChar.Controls.Add(this.pictureBox2);
      resources.ApplyResources(this.panelInvalidChar, "panelInvalidChar");
      this.panelInvalidChar.Name = "panelInvalidChar";
      // 
      // buttonInvalidCharYes
      // 
      resources.ApplyResources(this.buttonInvalidCharYes, "buttonInvalidCharYes");
      this.buttonInvalidCharYes.Name = "buttonInvalidCharYes";
      this.buttonInvalidCharYes.UseVisualStyleBackColor = true;
      this.buttonInvalidCharYes.Click += new System.EventHandler(this.buttonInvalidCharYes_Click);
      // 
      // buttonInvalidCharCancel
      // 
      resources.ApplyResources(this.buttonInvalidCharCancel, "buttonInvalidCharCancel");
      this.buttonInvalidCharCancel.Name = "buttonInvalidCharCancel";
      this.buttonInvalidCharCancel.UseVisualStyleBackColor = true;
      this.buttonInvalidCharCancel.Click += new System.EventHandler(this.buttonInvalidCharCancel_Click);
      // 
      // labelInvalidChar
      // 
      resources.ApplyResources(this.labelInvalidChar, "labelInvalidChar");
      this.labelInvalidChar.Name = "labelInvalidChar";
      // 
      // pictureBox2
      // 
      resources.ApplyResources(this.pictureBox2, "pictureBox2");
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.TabStop = false;
      // 
      // tabPageConfirmToReadIniFile
      // 
      this.tabPageConfirmToReadIniFile.Controls.Add(this.panelConfirmToReadIniFile);
      resources.ApplyResources(this.tabPageConfirmToReadIniFile, "tabPageConfirmToReadIniFile");
      this.tabPageConfirmToReadIniFile.Name = "tabPageConfirmToReadIniFile";
      this.tabPageConfirmToReadIniFile.UseVisualStyleBackColor = true;
      // 
      // panelConfirmToReadIniFile
      // 
      this.panelConfirmToReadIniFile.Controls.Add(this.checkBoxConfirmToReadIniFile);
      this.panelConfirmToReadIniFile.Controls.Add(this.labelConfirmToReadIniFileAlert);
      this.panelConfirmToReadIniFile.Controls.Add(this.labelIniFilePath);
      this.panelConfirmToReadIniFile.Controls.Add(this.buttonConfirmToReadIniFileYes);
      this.panelConfirmToReadIniFile.Controls.Add(this.buttonConfirmToReadIniFileNo);
      this.panelConfirmToReadIniFile.Controls.Add(this.labelConfirmToReadIniFile);
      this.panelConfirmToReadIniFile.Controls.Add(this.pictureBox3);
      resources.ApplyResources(this.panelConfirmToReadIniFile, "panelConfirmToReadIniFile");
      this.panelConfirmToReadIniFile.Name = "panelConfirmToReadIniFile";
      // 
      // checkBoxConfirmToReadIniFile
      // 
      resources.ApplyResources(this.checkBoxConfirmToReadIniFile, "checkBoxConfirmToReadIniFile");
      this.checkBoxConfirmToReadIniFile.Name = "checkBoxConfirmToReadIniFile";
      this.checkBoxConfirmToReadIniFile.UseVisualStyleBackColor = true;
      // 
      // labelConfirmToReadIniFileAlert
      // 
      resources.ApplyResources(this.labelConfirmToReadIniFileAlert, "labelConfirmToReadIniFileAlert");
      this.labelConfirmToReadIniFileAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.labelConfirmToReadIniFileAlert.Name = "labelConfirmToReadIniFileAlert";
      // 
      // labelIniFilePath
      // 
      resources.ApplyResources(this.labelIniFilePath, "labelIniFilePath");
      this.labelIniFilePath.Name = "labelIniFilePath";
      // 
      // buttonConfirmToReadIniFileYes
      // 
      resources.ApplyResources(this.buttonConfirmToReadIniFileYes, "buttonConfirmToReadIniFileYes");
      this.buttonConfirmToReadIniFileYes.Name = "buttonConfirmToReadIniFileYes";
      this.buttonConfirmToReadIniFileYes.UseVisualStyleBackColor = true;
      this.buttonConfirmToReadIniFileYes.Click += new System.EventHandler(this.buttonConfirmToReadIniFileYes_Click);
      // 
      // buttonConfirmToReadIniFileNo
      // 
      resources.ApplyResources(this.buttonConfirmToReadIniFileNo, "buttonConfirmToReadIniFileNo");
      this.buttonConfirmToReadIniFileNo.Name = "buttonConfirmToReadIniFileNo";
      this.buttonConfirmToReadIniFileNo.UseVisualStyleBackColor = true;
      this.buttonConfirmToReadIniFileNo.Click += new System.EventHandler(this.buttonConfirmToReadIniFileNo_Click);
      // 
      // labelConfirmToReadIniFile
      // 
      resources.ApplyResources(this.labelConfirmToReadIniFile, "labelConfirmToReadIniFile");
      this.labelConfirmToReadIniFile.Name = "labelConfirmToReadIniFile";
      // 
      // pictureBox3
      // 
      resources.ApplyResources(this.pictureBox3, "pictureBox3");
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.TabStop = false;
      // 
      // Form4
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
      this.tabPageInvalidChar.ResumeLayout(false);
      this.panelInvalidChar.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.tabPageConfirmToReadIniFile.ResumeLayout(false);
      this.panelConfirmToReadIniFile.ResumeLayout(false);
      this.panelConfirmToReadIniFile.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelOuter;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOverwriteAll;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemKeepNewer;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOverwrite;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemkeepNewerAll;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSkip;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSkipAll;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageInputPassword;
    private System.Windows.Forms.TabPage tabPageOverwriteConfirm;
    private System.Windows.Forms.TabPage tabPageAskEncryptOrDecrypt;
    private System.Windows.Forms.TabPage tabPageInvalidChar;
    private System.Windows.Forms.Panel panelInvalidChar;
    private System.Windows.Forms.Button buttonInvalidCharYes;
    private System.Windows.Forms.Button buttonInvalidCharCancel;
    private System.Windows.Forms.Label labelInvalidChar;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.TabPage tabPageConfirmToReadIniFile;
    private System.Windows.Forms.Panel panelConfirmToReadIniFile;
    private System.Windows.Forms.CheckBox checkBoxConfirmToReadIniFile;
    private System.Windows.Forms.Label labelConfirmToReadIniFileAlert;
    private System.Windows.Forms.Label labelIniFilePath;
    private System.Windows.Forms.Button buttonConfirmToReadIniFileYes;
    private System.Windows.Forms.Button buttonConfirmToReadIniFileNo;
    private System.Windows.Forms.Label labelConfirmToReadIniFile;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Panel panelInputPassword;
    private System.Windows.Forms.CheckBox checkBoxNotMaskEncryptedPassword;
    private System.Windows.Forms.Label labelPasswordValid;
    private System.Windows.Forms.PictureBox pictureBoxPasswordValid;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxRePassword;
    private System.Windows.Forms.TextBox textBoxPassword;
    private System.Windows.Forms.Button buttonPasswordCancel;
    private System.Windows.Forms.Button buttonPasswordOK;
    private System.Windows.Forms.Panel panelOverwriteConfirm;
    private SplitButton splitButton2;
    private SplitButton splitButton1;
    private System.Windows.Forms.Button buttonOverwriteCancel;
    private System.Windows.Forms.Label labelMessageText;
    private System.Windows.Forms.Panel panelAskEncryptOrDecrypt;
    private System.Windows.Forms.Button buttonAskEncryptOrDecryptCancel;
    private System.Windows.Forms.Button buttonDecrypt;
    private System.Windows.Forms.Button buttonEncrypt;
    private System.Windows.Forms.Label labelAskEncryptOrDecrypt;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBoxQuestionIcon;
  }
}