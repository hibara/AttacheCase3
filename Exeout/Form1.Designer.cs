namespace Exeout
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.buttonDecrypt = new System.Windows.Forms.Button();
      this.buttonExit = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.labelMessage = new System.Windows.Forms.Label();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.checkBoxNotMaskPassword = new System.Windows.Forms.CheckBox();
      this.labelPercent = new System.Windows.Forms.Label();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonDecrypt
      // 
      resources.ApplyResources(this.buttonDecrypt, "buttonDecrypt");
      this.buttonDecrypt.Name = "buttonDecrypt";
      this.buttonDecrypt.UseVisualStyleBackColor = true;
      this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
      // 
      // buttonExit
      // 
      resources.ApplyResources(this.buttonExit, "buttonExit");
      this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // textBox1
      // 
      this.textBox1.AllowDrop = true;
      resources.ApplyResources(this.textBox1, "textBox1");
      this.textBox1.Name = "textBox1";
      this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
      this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
      this.textBox1.DragLeave += new System.EventHandler(this.textBox1_DragLeave);
      this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Name = "statusStrip1";
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
      this.toolStripStatusLabel2.Spring = true;
      // 
      // toolStripStatusLabel1
      // 
      resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
      // 
      // labelMessage
      // 
      resources.ApplyResources(this.labelMessage, "labelMessage");
      this.labelMessage.Name = "labelMessage";
      // 
      // progressBar1
      // 
      resources.ApplyResources(this.progressBar1, "progressBar1");
      this.progressBar1.Name = "progressBar1";
      // 
      // checkBoxNotMaskPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskPassword, "checkBoxNotMaskPassword");
      this.checkBoxNotMaskPassword.Name = "checkBoxNotMaskPassword";
      this.checkBoxNotMaskPassword.UseVisualStyleBackColor = true;
      this.checkBoxNotMaskPassword.CheckedChanged += new System.EventHandler(this.checkBoxNotMaskPassword_CheckedChanged);
      // 
      // labelPercent
      // 
      resources.ApplyResources(this.labelPercent, "labelPercent");
      this.labelPercent.Name = "labelPercent";
      // 
      // Form1
      // 
      this.AllowDrop = true;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.CancelButton = this.buttonExit;
      resources.ApplyResources(this, "$this");
      this.Controls.Add(this.checkBoxNotMaskPassword);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.labelMessage);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.buttonExit);
      this.Controls.Add(this.buttonDecrypt);
      this.Controls.Add(this.labelPercent);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDecrypt;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.CheckBox checkBoxNotMaskPassword;
		private System.Windows.Forms.Label labelPercent;
	}
}

