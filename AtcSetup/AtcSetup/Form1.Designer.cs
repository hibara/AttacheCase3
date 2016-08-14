namespace AtcSetup
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.labelInfo = new System.Windows.Forms.Label();
      this.buttonExit = new System.Windows.Forms.Button();
      this.labelPercent = new System.Windows.Forms.Label();
      this.buttonAssociation = new System.Windows.Forms.Button();
      this.buttonUnAssociation = new System.Windows.Forms.Button();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      resources.ApplyResources(this.progressBar1, "progressBar1");
      this.progressBar1.Name = "progressBar1";
      // 
      // labelInfo
      // 
      resources.ApplyResources(this.labelInfo, "labelInfo");
      this.labelInfo.Name = "labelInfo";
      // 
      // buttonExit
      // 
      resources.ApplyResources(this.buttonExit, "buttonExit");
      this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // labelPercent
      // 
      resources.ApplyResources(this.labelPercent, "labelPercent");
      this.labelPercent.Name = "labelPercent";
      // 
      // buttonAssociation
      // 
      resources.ApplyResources(this.buttonAssociation, "buttonAssociation");
      this.buttonAssociation.Name = "buttonAssociation";
      this.buttonAssociation.UseVisualStyleBackColor = true;
      this.buttonAssociation.Click += new System.EventHandler(this.buttonAssociation_Click);
      // 
      // buttonUnAssociation
      // 
      resources.ApplyResources(this.buttonUnAssociation, "buttonUnAssociation");
      this.buttonUnAssociation.Name = "buttonUnAssociation";
      this.buttonUnAssociation.UseVisualStyleBackColor = true;
      this.buttonUnAssociation.Click += new System.EventHandler(this.buttonUnAssociation_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Name = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
      // 
      // Form1
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonExit;
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.buttonUnAssociation);
      this.Controls.Add(this.buttonAssociation);
      this.Controls.Add(this.labelPercent);
      this.Controls.Add(this.buttonExit);
      this.Controls.Add(this.labelInfo);
      this.Controls.Add(this.progressBar1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.Form1_Load);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label labelPercent;
		private System.Windows.Forms.Button buttonAssociation;
		private System.Windows.Forms.Button buttonUnAssociation;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
	}
}

