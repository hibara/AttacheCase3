namespace AttacheCase
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabelDataVersion = new System.Windows.Forms.ToolStripStatusLabel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemEncrypt = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemEncryptSelectFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemEncryptSelectFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemDecrypt = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemOption = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemHelpContents = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonEncryptSelectFiles = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonEncryptSelectFolder = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonDecryptSelectAtcFiles = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonOption = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
      this.panelOuter = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPageStartPage = new System.Windows.Forms.TabPage();
      this.panelStartPage = new System.Windows.Forms.Panel();
      this.pictureBoxBackButtonOff = new System.Windows.Forms.PictureBox();
      this.pictureBoxBackButtonOn = new System.Windows.Forms.PictureBox();
      this.pictureBoxHamburgerMenu = new System.Windows.Forms.PictureBox();
      this.buttonExit = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.labelDec = new System.Windows.Forms.Label();
      this.labelZip = new System.Windows.Forms.Label();
      this.labelExe = new System.Windows.Forms.Label();
      this.labelAtc = new System.Windows.Forms.Label();
      this.pictureBoxDec = new System.Windows.Forms.PictureBox();
      this.pictureBoxZip = new System.Windows.Forms.PictureBox();
      this.pictureBoxExe = new System.Windows.Forms.PictureBox();
      this.pictureBoxAtc = new System.Windows.Forms.PictureBox();
      this.pictureBoxDeleteOn = new System.Windows.Forms.PictureBox();
      this.pictureBoxDecOff = new System.Windows.Forms.PictureBox();
      this.pictureBoxDecOn = new System.Windows.Forms.PictureBox();
      this.pictureBoxZipOff = new System.Windows.Forms.PictureBox();
      this.pictureBoxZipOn = new System.Windows.Forms.PictureBox();
      this.pictureBoxExeOff = new System.Windows.Forms.PictureBox();
      this.pictureBoxExeOn = new System.Windows.Forms.PictureBox();
      this.pictureBoxAtcOff = new System.Windows.Forms.PictureBox();
      this.pictureBoxAtcOn = new System.Windows.Forms.PictureBox();
      this.labelDragAndDrop = new System.Windows.Forms.Label();
      this.tabPageEncrypt = new System.Windows.Forms.TabPage();
      this.panelEncrypt = new System.Windows.Forms.Panel();
      this.textBoxPassword = new AttacheCase.DelayTextBox();
      this.labelPasswordStrength = new System.Windows.Forms.Label();
      this.checkBoxDeleteOriginalFileAfterEncryption = new System.Windows.Forms.CheckBox();
      this.checkBoxNotMaskEncryptedPassword = new System.Windows.Forms.CheckBox();
      this.pictureBoxPassStrengthMeter = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrengthEmpty = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrength04 = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrength03 = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrength02 = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrength01 = new System.Windows.Forms.PictureBox();
      this.pictureBoxPasswordStrength00 = new System.Windows.Forms.PictureBox();
      this.pictureBoxEncryptBackButton = new System.Windows.Forms.PictureBox();
      this.buttonEncryptionPasswordOk = new System.Windows.Forms.Button();
      this.panel2 = new System.Windows.Forms.Panel();
      this.labelEncryption = new System.Windows.Forms.Label();
      this.pictureBoxEncryption = new System.Windows.Forms.PictureBox();
      this.buttonEncryptCancel = new System.Windows.Forms.Button();
      this.labelPassword = new System.Windows.Forms.Label();
      this.tabPageEncryptConfirm = new System.Windows.Forms.TabPage();
      this.panelEncryptConfirm = new System.Windows.Forms.Panel();
      this.pictureBoxEncryptConfirmBackButton = new System.Windows.Forms.PictureBox();
      this.pictureBoxCheckPasswordValidation = new System.Windows.Forms.PictureBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.labelEncryptionConfirm = new System.Windows.Forms.Label();
      this.pictureBoxEncryptionConfirm = new System.Windows.Forms.PictureBox();
      this.buttonEncryptionConfirmCancel = new System.Windows.Forms.Button();
      this.buttonEncryptStart = new System.Windows.Forms.Button();
      this.pictureBoxInValidIcon = new System.Windows.Forms.PictureBox();
      this.pictureBoxValidIcon = new System.Windows.Forms.PictureBox();
      this.checkBoxReDeleteOriginalFileAfterEncryption = new System.Windows.Forms.CheckBox();
      this.checkBoxReNotMaskEncryptedPassword = new System.Windows.Forms.CheckBox();
      this.labelInputPasswordAgain = new System.Windows.Forms.Label();
      this.textBoxRePassword = new System.Windows.Forms.TextBox();
      this.tabPageDecrypt = new System.Windows.Forms.TabPage();
      this.panelDecrypt = new System.Windows.Forms.Panel();
      this.pictureBoxDecryptBackButton = new System.Windows.Forms.PictureBox();
      this.panel4 = new System.Windows.Forms.Panel();
      this.pictureBoxDecryption = new System.Windows.Forms.PictureBox();
      this.labelDecryption = new System.Windows.Forms.Label();
      this.checkBoxDeleteAtcFileAfterDecryption = new System.Windows.Forms.CheckBox();
      this.checkBoxNotMaskDecryptedPassword = new System.Windows.Forms.CheckBox();
      this.buttonDecryptCancel = new System.Windows.Forms.Button();
      this.buttonDecryptStart = new System.Windows.Forms.Button();
      this.labelDecryptionPassword = new System.Windows.Forms.Label();
      this.textBoxDecryptPassword = new System.Windows.Forms.TextBox();
      this.tabPageProgressState = new System.Windows.Forms.TabPage();
      this.panelProgressState = new System.Windows.Forms.Panel();
      this.pictureBoxProgressStateBackButton = new System.Windows.Forms.PictureBox();
      this.panel5 = new System.Windows.Forms.Panel();
      this.labelProgress = new System.Windows.Forms.Label();
      this.pictureBoxProgress = new System.Windows.Forms.PictureBox();
      this.labelCryptionType = new System.Windows.Forms.Label();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.labelProgressPercentText = new System.Windows.Forms.Label();
      this.labelProgressMessageText = new System.Windows.Forms.Label();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.encryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.selectFoldersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.decryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.optionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.helpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ToolStripMenuItemAtcFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemExeFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemZipPassword = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.selectFilesToEncryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectDirectoryToEncryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectFileToDecryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
      this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
      this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.toolTipZxcvbnWarning = new System.Windows.Forms.ToolTip(this.components);
      this.toolTipZxcvbnSuggestions = new System.Windows.Forms.ToolTip(this.components);
      this.statusStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.panelOuter.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPageStartPage.SuspendLayout();
      this.panelStartPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOff)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOn)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHamburgerMenu)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDec)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZip)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExe)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtc)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeleteOn)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecOff)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecOn)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZipOff)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZipOn)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExeOff)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExeOn)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtcOff)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtcOn)).BeginInit();
      this.tabPageEncrypt.SuspendLayout();
      this.panelEncrypt.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPassStrengthMeter)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrengthEmpty)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength04)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength03)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength02)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength01)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength00)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptBackButton)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryption)).BeginInit();
      this.tabPageEncryptConfirm.SuspendLayout();
      this.panelEncryptConfirm.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptConfirmBackButton)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckPasswordValidation)).BeginInit();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptionConfirm)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInValidIcon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxValidIcon)).BeginInit();
      this.tabPageDecrypt.SuspendLayout();
      this.panelDecrypt.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryptBackButton)).BeginInit();
      this.panel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryption)).BeginInit();
      this.tabPageProgressState.SuspendLayout();
      this.panelProgressState.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressStateBackButton)).BeginInit();
      this.panel5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip3.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDataVersion});
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Name = "statusStrip1";
      // 
      // toolStripStatusLabelDataVersion
      // 
      this.toolStripStatusLabelDataVersion.Name = "toolStripStatusLabelDataVersion";
      resources.ApplyResources(this.toolStripStatusLabelDataVersion, "toolStripStatusLabelDataVersion");
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemOption,
            this.ToolStripMenuItemHelp});
      resources.ApplyResources(this.menuStrip1, "menuStrip1");
      this.menuStrip1.Name = "menuStrip1";
      // 
      // ToolStripMenuItemFile
      // 
      this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEncrypt,
            this.ToolStripMenuItemDecrypt,
            this.toolStripMenuItem2,
            this.ToolStripMenuItemExit});
      this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
      resources.ApplyResources(this.ToolStripMenuItemFile, "ToolStripMenuItemFile");
      this.ToolStripMenuItemFile.DropDownOpened += new System.EventHandler(this.ToolStripMenuItemFile_DropDownOpened);
      // 
      // ToolStripMenuItemEncrypt
      // 
      this.ToolStripMenuItemEncrypt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEncryptSelectFiles,
            this.ToolStripMenuItemEncryptSelectFolder});
      resources.ApplyResources(this.ToolStripMenuItemEncrypt, "ToolStripMenuItemEncrypt");
      this.ToolStripMenuItemEncrypt.Name = "ToolStripMenuItemEncrypt";
      // 
      // ToolStripMenuItemEncryptSelectFiles
      // 
      resources.ApplyResources(this.ToolStripMenuItemEncryptSelectFiles, "ToolStripMenuItemEncryptSelectFiles");
      this.ToolStripMenuItemEncryptSelectFiles.Name = "ToolStripMenuItemEncryptSelectFiles";
      this.ToolStripMenuItemEncryptSelectFiles.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFiles_Click);
      // 
      // ToolStripMenuItemEncryptSelectFolder
      // 
      resources.ApplyResources(this.ToolStripMenuItemEncryptSelectFolder, "ToolStripMenuItemEncryptSelectFolder");
      this.ToolStripMenuItemEncryptSelectFolder.Name = "ToolStripMenuItemEncryptSelectFolder";
      this.ToolStripMenuItemEncryptSelectFolder.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFolder_Click);
      // 
      // ToolStripMenuItemDecrypt
      // 
      resources.ApplyResources(this.ToolStripMenuItemDecrypt, "ToolStripMenuItemDecrypt");
      this.ToolStripMenuItemDecrypt.Name = "ToolStripMenuItemDecrypt";
      this.ToolStripMenuItemDecrypt.Click += new System.EventHandler(this.ToolStripMenuItemDecrypt_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
      // 
      // ToolStripMenuItemExit
      // 
      resources.ApplyResources(this.ToolStripMenuItemExit, "ToolStripMenuItemExit");
      this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
      this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
      // 
      // ToolStripMenuItemOption
      // 
      this.ToolStripMenuItemOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSetting});
      this.ToolStripMenuItemOption.Name = "ToolStripMenuItemOption";
      resources.ApplyResources(this.ToolStripMenuItemOption, "ToolStripMenuItemOption");
      this.ToolStripMenuItemOption.DropDownOpened += new System.EventHandler(this.ToolStripMenuItemOption_DropDownOpened);
      // 
      // ToolStripMenuItemSetting
      // 
      resources.ApplyResources(this.ToolStripMenuItemSetting, "ToolStripMenuItemSetting");
      this.ToolStripMenuItemSetting.Name = "ToolStripMenuItemSetting";
      this.ToolStripMenuItemSetting.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
      // 
      // ToolStripMenuItemHelp
      // 
      this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHelpContents,
            this.toolStripMenuItem1,
            this.ToolStripMenuItemAbout});
      this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
      resources.ApplyResources(this.ToolStripMenuItemHelp, "ToolStripMenuItemHelp");
      // 
      // ToolStripMenuItemHelpContents
      // 
      resources.ApplyResources(this.ToolStripMenuItemHelpContents, "ToolStripMenuItemHelpContents");
      this.ToolStripMenuItemHelpContents.Name = "ToolStripMenuItemHelpContents";
      this.ToolStripMenuItemHelpContents.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      // 
      // ToolStripMenuItemAbout
      // 
      this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
      resources.ApplyResources(this.ToolStripMenuItemAbout, "ToolStripMenuItemAbout");
      this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEncryptSelectFiles,
            this.toolStripButtonEncryptSelectFolder,
            this.toolStripSeparator1,
            this.toolStripButtonDecryptSelectAtcFiles,
            this.toolStripSeparator2,
            this.toolStripButtonOption,
            this.toolStripSeparator3,
            this.toolStripButtonHelp});
      resources.ApplyResources(this.toolStrip1, "toolStrip1");
      this.toolStrip1.Name = "toolStrip1";
      // 
      // toolStripButtonEncryptSelectFiles
      // 
      this.toolStripButtonEncryptSelectFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.toolStripButtonEncryptSelectFiles, "toolStripButtonEncryptSelectFiles");
      this.toolStripButtonEncryptSelectFiles.Name = "toolStripButtonEncryptSelectFiles";
      this.toolStripButtonEncryptSelectFiles.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFiles_Click);
      // 
      // toolStripButtonEncryptSelectFolder
      // 
      this.toolStripButtonEncryptSelectFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.toolStripButtonEncryptSelectFolder, "toolStripButtonEncryptSelectFolder");
      this.toolStripButtonEncryptSelectFolder.Name = "toolStripButtonEncryptSelectFolder";
      this.toolStripButtonEncryptSelectFolder.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFolder_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
      // 
      // toolStripButtonDecryptSelectAtcFiles
      // 
      this.toolStripButtonDecryptSelectAtcFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.toolStripButtonDecryptSelectAtcFiles, "toolStripButtonDecryptSelectAtcFiles");
      this.toolStripButtonDecryptSelectAtcFiles.Name = "toolStripButtonDecryptSelectAtcFiles";
      this.toolStripButtonDecryptSelectAtcFiles.Click += new System.EventHandler(this.ToolStripMenuItemDecrypt_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
      // 
      // toolStripButtonOption
      // 
      this.toolStripButtonOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.toolStripButtonOption, "toolStripButtonOption");
      this.toolStripButtonOption.Name = "toolStripButtonOption";
      this.toolStripButtonOption.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
      // 
      // toolStripButtonHelp
      // 
      this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      resources.ApplyResources(this.toolStripButtonHelp, "toolStripButtonHelp");
      this.toolStripButtonHelp.Name = "toolStripButtonHelp";
      this.toolStripButtonHelp.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // panelOuter
      // 
      this.panelOuter.Controls.Add(this.tabControl1);
      resources.ApplyResources(this.panelOuter, "panelOuter");
      this.panelOuter.Name = "panelOuter";
      // 
      // tabControl1
      // 
      this.tabControl1.AllowDrop = true;
      this.tabControl1.Controls.Add(this.tabPageStartPage);
      this.tabControl1.Controls.Add(this.tabPageEncrypt);
      this.tabControl1.Controls.Add(this.tabPageEncryptConfirm);
      this.tabControl1.Controls.Add(this.tabPageDecrypt);
      this.tabControl1.Controls.Add(this.tabPageProgressState);
      resources.ApplyResources(this.tabControl1, "tabControl1");
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.TabStop = false;
      this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageStartPage
      // 
      this.tabPageStartPage.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageStartPage.Controls.Add(this.panelStartPage);
      resources.ApplyResources(this.tabPageStartPage, "tabPageStartPage");
      this.tabPageStartPage.Name = "tabPageStartPage";
      // 
      // panelStartPage
      // 
      this.panelStartPage.BackColor = System.Drawing.Color.White;
      this.panelStartPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panelStartPage.Controls.Add(this.pictureBoxBackButtonOff);
      this.panelStartPage.Controls.Add(this.pictureBoxBackButtonOn);
      this.panelStartPage.Controls.Add(this.pictureBoxHamburgerMenu);
      this.panelStartPage.Controls.Add(this.buttonExit);
      this.panelStartPage.Controls.Add(this.panel1);
      this.panelStartPage.Controls.Add(this.pictureBoxDeleteOn);
      this.panelStartPage.Controls.Add(this.pictureBoxDecOff);
      this.panelStartPage.Controls.Add(this.pictureBoxDecOn);
      this.panelStartPage.Controls.Add(this.pictureBoxZipOff);
      this.panelStartPage.Controls.Add(this.pictureBoxZipOn);
      this.panelStartPage.Controls.Add(this.pictureBoxExeOff);
      this.panelStartPage.Controls.Add(this.pictureBoxExeOn);
      this.panelStartPage.Controls.Add(this.pictureBoxAtcOff);
      this.panelStartPage.Controls.Add(this.pictureBoxAtcOn);
      this.panelStartPage.Controls.Add(this.labelDragAndDrop);
      resources.ApplyResources(this.panelStartPage, "panelStartPage");
      this.panelStartPage.Name = "panelStartPage";
      this.panelStartPage.VisibleChanged += new System.EventHandler(this.panelStartPage_VisibleChanged);
      this.panelStartPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelStartPage.MouseEnter += new System.EventHandler(this.panelStartPage_MouseEnter);
      this.panelStartPage.MouseLeave += new System.EventHandler(this.panelStartPage_MouseLeave);
      this.panelStartPage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // pictureBoxBackButtonOff
      // 
      resources.ApplyResources(this.pictureBoxBackButtonOff, "pictureBoxBackButtonOff");
      this.pictureBoxBackButtonOff.Name = "pictureBoxBackButtonOff";
      this.pictureBoxBackButtonOff.TabStop = false;
      // 
      // pictureBoxBackButtonOn
      // 
      resources.ApplyResources(this.pictureBoxBackButtonOn, "pictureBoxBackButtonOn");
      this.pictureBoxBackButtonOn.Name = "pictureBoxBackButtonOn";
      this.pictureBoxBackButtonOn.TabStop = false;
      // 
      // pictureBoxHamburgerMenu
      // 
      resources.ApplyResources(this.pictureBoxHamburgerMenu, "pictureBoxHamburgerMenu");
      this.pictureBoxHamburgerMenu.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxHamburgerMenu.Name = "pictureBoxHamburgerMenu";
      this.pictureBoxHamburgerMenu.TabStop = false;
      this.pictureBoxHamburgerMenu.Click += new System.EventHandler(this.pictureBoxHamburgerMenu_Click);
      // 
      // buttonExit
      // 
      resources.ApplyResources(this.buttonExit, "buttonExit");
      this.buttonExit.Name = "buttonExit";
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel1.Controls.Add(this.labelDec);
      this.panel1.Controls.Add(this.labelZip);
      this.panel1.Controls.Add(this.labelExe);
      this.panel1.Controls.Add(this.labelAtc);
      this.panel1.Controls.Add(this.pictureBoxDec);
      this.panel1.Controls.Add(this.pictureBoxZip);
      this.panel1.Controls.Add(this.pictureBoxExe);
      this.panel1.Controls.Add(this.pictureBoxAtc);
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.Name = "panel1";
      // 
      // labelDec
      // 
      resources.ApplyResources(this.labelDec, "labelDec");
      this.labelDec.Name = "labelDec";
      // 
      // labelZip
      // 
      resources.ApplyResources(this.labelZip, "labelZip");
      this.labelZip.Name = "labelZip";
      // 
      // labelExe
      // 
      resources.ApplyResources(this.labelExe, "labelExe");
      this.labelExe.Name = "labelExe";
      // 
      // labelAtc
      // 
      this.labelAtc.BackColor = System.Drawing.Color.Transparent;
      resources.ApplyResources(this.labelAtc, "labelAtc");
      this.labelAtc.Name = "labelAtc";
      // 
      // pictureBoxDec
      // 
      resources.ApplyResources(this.pictureBoxDec, "pictureBoxDec");
      this.pictureBoxDec.Name = "pictureBoxDec";
      this.pictureBoxDec.TabStop = false;
      // 
      // pictureBoxZip
      // 
      resources.ApplyResources(this.pictureBoxZip, "pictureBoxZip");
      this.pictureBoxZip.Name = "pictureBoxZip";
      this.pictureBoxZip.TabStop = false;
      this.pictureBoxZip.Click += new System.EventHandler(this.pictureBoxZip_Click);
      // 
      // pictureBoxExe
      // 
      resources.ApplyResources(this.pictureBoxExe, "pictureBoxExe");
      this.pictureBoxExe.Name = "pictureBoxExe";
      this.pictureBoxExe.TabStop = false;
      this.pictureBoxExe.Click += new System.EventHandler(this.pictureBoxExe_Click);
      // 
      // pictureBoxAtc
      // 
      resources.ApplyResources(this.pictureBoxAtc, "pictureBoxAtc");
      this.pictureBoxAtc.Name = "pictureBoxAtc";
      this.pictureBoxAtc.TabStop = false;
      this.pictureBoxAtc.Click += new System.EventHandler(this.pictureBoxAtc_Click);
      // 
      // pictureBoxDeleteOn
      // 
      resources.ApplyResources(this.pictureBoxDeleteOn, "pictureBoxDeleteOn");
      this.pictureBoxDeleteOn.Name = "pictureBoxDeleteOn";
      this.pictureBoxDeleteOn.TabStop = false;
      // 
      // pictureBoxDecOff
      // 
      resources.ApplyResources(this.pictureBoxDecOff, "pictureBoxDecOff");
      this.pictureBoxDecOff.Name = "pictureBoxDecOff";
      this.pictureBoxDecOff.TabStop = false;
      // 
      // pictureBoxDecOn
      // 
      resources.ApplyResources(this.pictureBoxDecOn, "pictureBoxDecOn");
      this.pictureBoxDecOn.Name = "pictureBoxDecOn";
      this.pictureBoxDecOn.TabStop = false;
      // 
      // pictureBoxZipOff
      // 
      resources.ApplyResources(this.pictureBoxZipOff, "pictureBoxZipOff");
      this.pictureBoxZipOff.Name = "pictureBoxZipOff";
      this.pictureBoxZipOff.TabStop = false;
      // 
      // pictureBoxZipOn
      // 
      resources.ApplyResources(this.pictureBoxZipOn, "pictureBoxZipOn");
      this.pictureBoxZipOn.Name = "pictureBoxZipOn";
      this.pictureBoxZipOn.TabStop = false;
      // 
      // pictureBoxExeOff
      // 
      resources.ApplyResources(this.pictureBoxExeOff, "pictureBoxExeOff");
      this.pictureBoxExeOff.Name = "pictureBoxExeOff";
      this.pictureBoxExeOff.TabStop = false;
      // 
      // pictureBoxExeOn
      // 
      resources.ApplyResources(this.pictureBoxExeOn, "pictureBoxExeOn");
      this.pictureBoxExeOn.Name = "pictureBoxExeOn";
      this.pictureBoxExeOn.TabStop = false;
      // 
      // pictureBoxAtcOff
      // 
      resources.ApplyResources(this.pictureBoxAtcOff, "pictureBoxAtcOff");
      this.pictureBoxAtcOff.Name = "pictureBoxAtcOff";
      this.pictureBoxAtcOff.TabStop = false;
      // 
      // pictureBoxAtcOn
      // 
      resources.ApplyResources(this.pictureBoxAtcOn, "pictureBoxAtcOn");
      this.pictureBoxAtcOn.Name = "pictureBoxAtcOn";
      this.pictureBoxAtcOn.TabStop = false;
      // 
      // labelDragAndDrop
      // 
      resources.ApplyResources(this.labelDragAndDrop, "labelDragAndDrop");
      this.labelDragAndDrop.Name = "labelDragAndDrop";
      this.labelDragAndDrop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelDragAndDrop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageEncrypt
      // 
      this.tabPageEncrypt.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageEncrypt.Controls.Add(this.panelEncrypt);
      resources.ApplyResources(this.tabPageEncrypt, "tabPageEncrypt");
      this.tabPageEncrypt.Name = "tabPageEncrypt";
      // 
      // panelEncrypt
      // 
      this.panelEncrypt.BackColor = System.Drawing.Color.White;
      this.panelEncrypt.Controls.Add(this.textBoxPassword);
      this.panelEncrypt.Controls.Add(this.labelPasswordStrength);
      this.panelEncrypt.Controls.Add(this.checkBoxDeleteOriginalFileAfterEncryption);
      this.panelEncrypt.Controls.Add(this.checkBoxNotMaskEncryptedPassword);
      this.panelEncrypt.Controls.Add(this.pictureBoxPassStrengthMeter);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrengthEmpty);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrength04);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrength03);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrength02);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrength01);
      this.panelEncrypt.Controls.Add(this.pictureBoxPasswordStrength00);
      this.panelEncrypt.Controls.Add(this.pictureBoxEncryptBackButton);
      this.panelEncrypt.Controls.Add(this.buttonEncryptionPasswordOk);
      this.panelEncrypt.Controls.Add(this.panel2);
      this.panelEncrypt.Controls.Add(this.buttonEncryptCancel);
      this.panelEncrypt.Controls.Add(this.labelPassword);
      resources.ApplyResources(this.panelEncrypt, "panelEncrypt");
      this.panelEncrypt.Name = "panelEncrypt";
      this.panelEncrypt.VisibleChanged += new System.EventHandler(this.panelEncrypt_VisibleChanged);
      this.panelEncrypt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelEncrypt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // textBoxPassword
      // 
      resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
      this.textBoxPassword.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxPassword_DragDrop);
      this.textBoxPassword.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxPassword_DragEnter);
      this.textBoxPassword.DragLeave += new System.EventHandler(this.textBoxPassword_DragLeave);
      this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
      // 
      // labelPasswordStrength
      // 
      resources.ApplyResources(this.labelPasswordStrength, "labelPasswordStrength");
      this.labelPasswordStrength.ForeColor = System.Drawing.Color.Gray;
      this.labelPasswordStrength.Name = "labelPasswordStrength";
      // 
      // checkBoxDeleteOriginalFileAfterEncryption
      // 
      resources.ApplyResources(this.checkBoxDeleteOriginalFileAfterEncryption, "checkBoxDeleteOriginalFileAfterEncryption");
      this.checkBoxDeleteOriginalFileAfterEncryption.Name = "checkBoxDeleteOriginalFileAfterEncryption";
      this.checkBoxDeleteOriginalFileAfterEncryption.UseVisualStyleBackColor = true;
      // 
      // checkBoxNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskEncryptedPassword, "checkBoxNotMaskEncryptedPassword");
      this.checkBoxNotMaskEncryptedPassword.Name = "checkBoxNotMaskEncryptedPassword";
      this.checkBoxNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
      // 
      // pictureBoxPassStrengthMeter
      // 
      resources.ApplyResources(this.pictureBoxPassStrengthMeter, "pictureBoxPassStrengthMeter");
      this.pictureBoxPassStrengthMeter.Name = "pictureBoxPassStrengthMeter";
      this.pictureBoxPassStrengthMeter.TabStop = false;
      // 
      // pictureBoxPasswordStrengthEmpty
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrengthEmpty, "pictureBoxPasswordStrengthEmpty");
      this.pictureBoxPasswordStrengthEmpty.Name = "pictureBoxPasswordStrengthEmpty";
      this.pictureBoxPasswordStrengthEmpty.TabStop = false;
      // 
      // pictureBoxPasswordStrength04
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength04, "pictureBoxPasswordStrength04");
      this.pictureBoxPasswordStrength04.Name = "pictureBoxPasswordStrength04";
      this.pictureBoxPasswordStrength04.TabStop = false;
      // 
      // pictureBoxPasswordStrength03
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength03, "pictureBoxPasswordStrength03");
      this.pictureBoxPasswordStrength03.Name = "pictureBoxPasswordStrength03";
      this.pictureBoxPasswordStrength03.TabStop = false;
      // 
      // pictureBoxPasswordStrength02
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength02, "pictureBoxPasswordStrength02");
      this.pictureBoxPasswordStrength02.Name = "pictureBoxPasswordStrength02";
      this.pictureBoxPasswordStrength02.TabStop = false;
      // 
      // pictureBoxPasswordStrength01
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength01, "pictureBoxPasswordStrength01");
      this.pictureBoxPasswordStrength01.Name = "pictureBoxPasswordStrength01";
      this.pictureBoxPasswordStrength01.TabStop = false;
      // 
      // pictureBoxPasswordStrength00
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength00, "pictureBoxPasswordStrength00");
      this.pictureBoxPasswordStrength00.Name = "pictureBoxPasswordStrength00";
      this.pictureBoxPasswordStrength00.TabStop = false;
      // 
      // pictureBoxEncryptBackButton
      // 
      resources.ApplyResources(this.pictureBoxEncryptBackButton, "pictureBoxEncryptBackButton");
      this.pictureBoxEncryptBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryptBackButton.Name = "pictureBoxEncryptBackButton";
      this.pictureBoxEncryptBackButton.TabStop = false;
      this.pictureBoxEncryptBackButton.Click += new System.EventHandler(this.buttonEncryptCancel_Click);
      this.pictureBoxEncryptBackButton.MouseEnter += new System.EventHandler(this.pictureBoxEncryptBackButton_MouseEnter);
      this.pictureBoxEncryptBackButton.MouseLeave += new System.EventHandler(this.pictureBoxEncryptBackButton_MouseLeave);
      // 
      // buttonEncryptionPasswordOk
      // 
      resources.ApplyResources(this.buttonEncryptionPasswordOk, "buttonEncryptionPasswordOk");
      this.buttonEncryptionPasswordOk.Name = "buttonEncryptionPasswordOk";
      this.buttonEncryptionPasswordOk.UseVisualStyleBackColor = true;
      this.buttonEncryptionPasswordOk.Click += new System.EventHandler(this.buttonEncryptionPasswordOk_Click);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.labelEncryption);
      this.panel2.Controls.Add(this.pictureBoxEncryption);
      resources.ApplyResources(this.panel2, "panel2");
      this.panel2.Name = "panel2";
      // 
      // labelEncryption
      // 
      resources.ApplyResources(this.labelEncryption, "labelEncryption");
      this.labelEncryption.Name = "labelEncryption";
      // 
      // pictureBoxEncryption
      // 
      resources.ApplyResources(this.pictureBoxEncryption, "pictureBoxEncryption");
      this.pictureBoxEncryption.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryption.Name = "pictureBoxEncryption";
      this.pictureBoxEncryption.TabStop = false;
      this.pictureBoxEncryption.Click += new System.EventHandler(this.pictureBoxEncryption_Click);
      // 
      // buttonEncryptCancel
      // 
      resources.ApplyResources(this.buttonEncryptCancel, "buttonEncryptCancel");
      this.buttonEncryptCancel.Name = "buttonEncryptCancel";
      this.buttonEncryptCancel.UseVisualStyleBackColor = true;
      this.buttonEncryptCancel.Click += new System.EventHandler(this.buttonEncryptCancel_Click);
      // 
      // labelPassword
      // 
      resources.ApplyResources(this.labelPassword, "labelPassword");
      this.labelPassword.BackColor = System.Drawing.Color.Transparent;
      this.labelPassword.Name = "labelPassword";
      this.labelPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageEncryptConfirm
      // 
      this.tabPageEncryptConfirm.BackColor = System.Drawing.Color.White;
      this.tabPageEncryptConfirm.Controls.Add(this.panelEncryptConfirm);
      resources.ApplyResources(this.tabPageEncryptConfirm, "tabPageEncryptConfirm");
      this.tabPageEncryptConfirm.Name = "tabPageEncryptConfirm";
      // 
      // panelEncryptConfirm
      // 
      this.panelEncryptConfirm.BackColor = System.Drawing.Color.White;
      this.panelEncryptConfirm.Controls.Add(this.pictureBoxEncryptConfirmBackButton);
      this.panelEncryptConfirm.Controls.Add(this.pictureBoxCheckPasswordValidation);
      this.panelEncryptConfirm.Controls.Add(this.panel3);
      this.panelEncryptConfirm.Controls.Add(this.buttonEncryptionConfirmCancel);
      this.panelEncryptConfirm.Controls.Add(this.buttonEncryptStart);
      this.panelEncryptConfirm.Controls.Add(this.pictureBoxInValidIcon);
      this.panelEncryptConfirm.Controls.Add(this.pictureBoxValidIcon);
      this.panelEncryptConfirm.Controls.Add(this.checkBoxReDeleteOriginalFileAfterEncryption);
      this.panelEncryptConfirm.Controls.Add(this.checkBoxReNotMaskEncryptedPassword);
      this.panelEncryptConfirm.Controls.Add(this.labelInputPasswordAgain);
      this.panelEncryptConfirm.Controls.Add(this.textBoxRePassword);
      resources.ApplyResources(this.panelEncryptConfirm, "panelEncryptConfirm");
      this.panelEncryptConfirm.Name = "panelEncryptConfirm";
      this.panelEncryptConfirm.VisibleChanged += new System.EventHandler(this.panelEncryptConfirm_VisibleChanged);
      this.panelEncryptConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelEncryptConfirm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // pictureBoxEncryptConfirmBackButton
      // 
      resources.ApplyResources(this.pictureBoxEncryptConfirmBackButton, "pictureBoxEncryptConfirmBackButton");
      this.pictureBoxEncryptConfirmBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryptConfirmBackButton.Name = "pictureBoxEncryptConfirmBackButton";
      this.pictureBoxEncryptConfirmBackButton.TabStop = false;
      this.pictureBoxEncryptConfirmBackButton.Click += new System.EventHandler(this.buttonEncryptionConfirmCancel_Click);
      this.pictureBoxEncryptConfirmBackButton.MouseEnter += new System.EventHandler(this.pictureBoxEncryptConfirmBackButton_MouseEnter);
      this.pictureBoxEncryptConfirmBackButton.MouseLeave += new System.EventHandler(this.pictureBoxEncryptConfirmBackButton_MouseLeave);
      // 
      // pictureBoxCheckPasswordValidation
      // 
      resources.ApplyResources(this.pictureBoxCheckPasswordValidation, "pictureBoxCheckPasswordValidation");
      this.pictureBoxCheckPasswordValidation.Name = "pictureBoxCheckPasswordValidation";
      this.pictureBoxCheckPasswordValidation.TabStop = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.labelEncryptionConfirm);
      this.panel3.Controls.Add(this.pictureBoxEncryptionConfirm);
      resources.ApplyResources(this.panel3, "panel3");
      this.panel3.Name = "panel3";
      // 
      // labelEncryptionConfirm
      // 
      resources.ApplyResources(this.labelEncryptionConfirm, "labelEncryptionConfirm");
      this.labelEncryptionConfirm.Name = "labelEncryptionConfirm";
      // 
      // pictureBoxEncryptionConfirm
      // 
      resources.ApplyResources(this.pictureBoxEncryptionConfirm, "pictureBoxEncryptionConfirm");
      this.pictureBoxEncryptionConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryptionConfirm.Name = "pictureBoxEncryptionConfirm";
      this.pictureBoxEncryptionConfirm.TabStop = false;
      // 
      // buttonEncryptionConfirmCancel
      // 
      resources.ApplyResources(this.buttonEncryptionConfirmCancel, "buttonEncryptionConfirmCancel");
      this.buttonEncryptionConfirmCancel.Name = "buttonEncryptionConfirmCancel";
      this.buttonEncryptionConfirmCancel.UseVisualStyleBackColor = true;
      this.buttonEncryptionConfirmCancel.Click += new System.EventHandler(this.buttonEncryptionConfirmCancel_Click);
      // 
      // buttonEncryptStart
      // 
      resources.ApplyResources(this.buttonEncryptStart, "buttonEncryptStart");
      this.buttonEncryptStart.Name = "buttonEncryptStart";
      this.buttonEncryptStart.UseVisualStyleBackColor = true;
      this.buttonEncryptStart.Click += new System.EventHandler(this.buttonEncryptStart_Click);
      // 
      // pictureBoxInValidIcon
      // 
      resources.ApplyResources(this.pictureBoxInValidIcon, "pictureBoxInValidIcon");
      this.pictureBoxInValidIcon.Name = "pictureBoxInValidIcon";
      this.pictureBoxInValidIcon.TabStop = false;
      // 
      // pictureBoxValidIcon
      // 
      resources.ApplyResources(this.pictureBoxValidIcon, "pictureBoxValidIcon");
      this.pictureBoxValidIcon.Name = "pictureBoxValidIcon";
      this.pictureBoxValidIcon.TabStop = false;
      // 
      // checkBoxReDeleteOriginalFileAfterEncryption
      // 
      resources.ApplyResources(this.checkBoxReDeleteOriginalFileAfterEncryption, "checkBoxReDeleteOriginalFileAfterEncryption");
      this.checkBoxReDeleteOriginalFileAfterEncryption.Name = "checkBoxReDeleteOriginalFileAfterEncryption";
      this.checkBoxReDeleteOriginalFileAfterEncryption.UseVisualStyleBackColor = true;
      this.checkBoxReDeleteOriginalFileAfterEncryption.CheckedChanged += new System.EventHandler(this.checkBoxReDeleteOriginalFileAfterEncryption_CheckedChanged);
      // 
      // checkBoxReNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxReNotMaskEncryptedPassword, "checkBoxReNotMaskEncryptedPassword");
      this.checkBoxReNotMaskEncryptedPassword.Name = "checkBoxReNotMaskEncryptedPassword";
      this.checkBoxReNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxReNotMaskEncryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxReNotMaskEncryptedPassword_CheckedChanged);
      // 
      // labelInputPasswordAgain
      // 
      resources.ApplyResources(this.labelInputPasswordAgain, "labelInputPasswordAgain");
      this.labelInputPasswordAgain.BackColor = System.Drawing.Color.Transparent;
      this.labelInputPasswordAgain.Name = "labelInputPasswordAgain";
      // 
      // textBoxRePassword
      // 
      resources.ApplyResources(this.textBoxRePassword, "textBoxRePassword");
      this.textBoxRePassword.BackColor = System.Drawing.Color.PapayaWhip;
      this.textBoxRePassword.Name = "textBoxRePassword";
      this.textBoxRePassword.UseSystemPasswordChar = true;
      this.textBoxRePassword.TextChanged += new System.EventHandler(this.textBoxRePassword_TextChanged);
      this.textBoxRePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRePassword_KeyDown);
      // 
      // tabPageDecrypt
      // 
      this.tabPageDecrypt.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageDecrypt.Controls.Add(this.panelDecrypt);
      resources.ApplyResources(this.tabPageDecrypt, "tabPageDecrypt");
      this.tabPageDecrypt.Name = "tabPageDecrypt";
      // 
      // panelDecrypt
      // 
      this.panelDecrypt.BackColor = System.Drawing.Color.White;
      this.panelDecrypt.Controls.Add(this.pictureBoxDecryptBackButton);
      this.panelDecrypt.Controls.Add(this.panel4);
      this.panelDecrypt.Controls.Add(this.checkBoxDeleteAtcFileAfterDecryption);
      this.panelDecrypt.Controls.Add(this.checkBoxNotMaskDecryptedPassword);
      this.panelDecrypt.Controls.Add(this.buttonDecryptCancel);
      this.panelDecrypt.Controls.Add(this.buttonDecryptStart);
      this.panelDecrypt.Controls.Add(this.labelDecryptionPassword);
      this.panelDecrypt.Controls.Add(this.textBoxDecryptPassword);
      resources.ApplyResources(this.panelDecrypt, "panelDecrypt");
      this.panelDecrypt.Name = "panelDecrypt";
      this.panelDecrypt.VisibleChanged += new System.EventHandler(this.panelDecrtpt_VisibleChanged);
      this.panelDecrypt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelDecrypt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // pictureBoxDecryptBackButton
      // 
      resources.ApplyResources(this.pictureBoxDecryptBackButton, "pictureBoxDecryptBackButton");
      this.pictureBoxDecryptBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxDecryptBackButton.Name = "pictureBoxDecryptBackButton";
      this.pictureBoxDecryptBackButton.TabStop = false;
      this.pictureBoxDecryptBackButton.Click += new System.EventHandler(this.buttonDecryptCancel_Click);
      this.pictureBoxDecryptBackButton.MouseEnter += new System.EventHandler(this.pictureBoxDecryptBackButton_MouseEnter);
      this.pictureBoxDecryptBackButton.MouseLeave += new System.EventHandler(this.pictureBoxDecryptBackButton_MouseLeave);
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.pictureBoxDecryption);
      this.panel4.Controls.Add(this.labelDecryption);
      resources.ApplyResources(this.panel4, "panel4");
      this.panel4.Name = "panel4";
      // 
      // pictureBoxDecryption
      // 
      resources.ApplyResources(this.pictureBoxDecryption, "pictureBoxDecryption");
      this.pictureBoxDecryption.Name = "pictureBoxDecryption";
      this.pictureBoxDecryption.TabStop = false;
      // 
      // labelDecryption
      // 
      resources.ApplyResources(this.labelDecryption, "labelDecryption");
      this.labelDecryption.Name = "labelDecryption";
      // 
      // checkBoxDeleteAtcFileAfterDecryption
      // 
      resources.ApplyResources(this.checkBoxDeleteAtcFileAfterDecryption, "checkBoxDeleteAtcFileAfterDecryption");
      this.checkBoxDeleteAtcFileAfterDecryption.Name = "checkBoxDeleteAtcFileAfterDecryption";
      this.checkBoxDeleteAtcFileAfterDecryption.UseVisualStyleBackColor = true;
      this.checkBoxDeleteAtcFileAfterDecryption.CheckedChanged += new System.EventHandler(this.checkBoxDeleteAtcFileAfterDecryption_CheckedChanged);
      // 
      // checkBoxNotMaskDecryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskDecryptedPassword, "checkBoxNotMaskDecryptedPassword");
      this.checkBoxNotMaskDecryptedPassword.Name = "checkBoxNotMaskDecryptedPassword";
      this.checkBoxNotMaskDecryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxNotMaskDecryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxNotMaskDecryptedPassword_CheckedChanged);
      // 
      // buttonDecryptCancel
      // 
      resources.ApplyResources(this.buttonDecryptCancel, "buttonDecryptCancel");
      this.buttonDecryptCancel.Name = "buttonDecryptCancel";
      this.buttonDecryptCancel.UseVisualStyleBackColor = true;
      this.buttonDecryptCancel.Click += new System.EventHandler(this.buttonDecryptCancel_Click);
      // 
      // buttonDecryptStart
      // 
      resources.ApplyResources(this.buttonDecryptStart, "buttonDecryptStart");
      this.buttonDecryptStart.Name = "buttonDecryptStart";
      this.buttonDecryptStart.UseVisualStyleBackColor = true;
      this.buttonDecryptStart.Click += new System.EventHandler(this.buttonDecryptStart_Click);
      // 
      // labelDecryptionPassword
      // 
      resources.ApplyResources(this.labelDecryptionPassword, "labelDecryptionPassword");
      this.labelDecryptionPassword.Name = "labelDecryptionPassword";
      this.labelDecryptionPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelDecryptionPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // textBoxDecryptPassword
      // 
      this.textBoxDecryptPassword.AllowDrop = true;
      resources.ApplyResources(this.textBoxDecryptPassword, "textBoxDecryptPassword");
      this.textBoxDecryptPassword.Name = "textBoxDecryptPassword";
      this.textBoxDecryptPassword.UseSystemPasswordChar = true;
      this.textBoxDecryptPassword.TextChanged += new System.EventHandler(this.textBoxDecryptPassword_TextChanged);
      this.textBoxDecryptPassword.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxDecryptPassword_DragDrop);
      this.textBoxDecryptPassword.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxDecryptPassword_DragEnter);
      this.textBoxDecryptPassword.DragLeave += new System.EventHandler(this.textBoxDecryptPassword_DragLeave);
      this.textBoxDecryptPassword.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBoxDecryptPassword_PreviewKeyDown);
      // 
      // tabPageProgressState
      // 
      this.tabPageProgressState.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageProgressState.Controls.Add(this.panelProgressState);
      resources.ApplyResources(this.tabPageProgressState, "tabPageProgressState");
      this.tabPageProgressState.Name = "tabPageProgressState";
      // 
      // panelProgressState
      // 
      this.panelProgressState.BackColor = System.Drawing.Color.White;
      this.panelProgressState.Controls.Add(this.pictureBoxProgressStateBackButton);
      this.panelProgressState.Controls.Add(this.panel5);
      this.panelProgressState.Controls.Add(this.labelCryptionType);
      this.panelProgressState.Controls.Add(this.buttonCancel);
      this.panelProgressState.Controls.Add(this.labelProgressPercentText);
      this.panelProgressState.Controls.Add(this.labelProgressMessageText);
      this.panelProgressState.Controls.Add(this.progressBar);
      resources.ApplyResources(this.panelProgressState, "panelProgressState");
      this.panelProgressState.Name = "panelProgressState";
      this.panelProgressState.VisibleChanged += new System.EventHandler(this.panelProgressState_VisibleChanged);
      this.panelProgressState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelProgressState.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // pictureBoxProgressStateBackButton
      // 
      resources.ApplyResources(this.pictureBoxProgressStateBackButton, "pictureBoxProgressStateBackButton");
      this.pictureBoxProgressStateBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxProgressStateBackButton.Name = "pictureBoxProgressStateBackButton";
      this.pictureBoxProgressStateBackButton.TabStop = false;
      this.pictureBoxProgressStateBackButton.Click += new System.EventHandler(this.pictureBoxProgressStateBackButton_Click);
      this.pictureBoxProgressStateBackButton.MouseEnter += new System.EventHandler(this.pictureBoxProgressStateBackButton_MouseEnter);
      this.pictureBoxProgressStateBackButton.MouseLeave += new System.EventHandler(this.pictureBoxProgressStateBackButton_MouseLeave);
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.labelProgress);
      this.panel5.Controls.Add(this.pictureBoxProgress);
      resources.ApplyResources(this.panel5, "panel5");
      this.panel5.Name = "panel5";
      // 
      // labelProgress
      // 
      resources.ApplyResources(this.labelProgress, "labelProgress");
      this.labelProgress.Name = "labelProgress";
      // 
      // pictureBoxProgress
      // 
      resources.ApplyResources(this.pictureBoxProgress, "pictureBoxProgress");
      this.pictureBoxProgress.Name = "pictureBoxProgress";
      this.pictureBoxProgress.TabStop = false;
      // 
      // labelCryptionType
      // 
      resources.ApplyResources(this.labelCryptionType, "labelCryptionType");
      this.labelCryptionType.Name = "labelCryptionType";
      this.labelCryptionType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelCryptionType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // buttonCancel
      // 
      resources.ApplyResources(this.buttonCancel, "buttonCancel");
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // labelProgressPercentText
      // 
      resources.ApplyResources(this.labelProgressPercentText, "labelProgressPercentText");
      this.labelProgressPercentText.Name = "labelProgressPercentText";
      this.labelProgressPercentText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelProgressPercentText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // labelProgressMessageText
      // 
      resources.ApplyResources(this.labelProgressMessageText, "labelProgressMessageText");
      this.labelProgressMessageText.Name = "labelProgressMessageText";
      this.labelProgressMessageText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelProgressMessageText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // progressBar
      // 
      resources.ApplyResources(this.progressBar, "progressBar");
      this.progressBar.Maximum = 10000;
      this.progressBar.Name = "progressBar";
      this.progressBar.Step = 1;
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.progressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptToolStripMenuItem,
            this.decryptToolStripMenuItem,
            this.toolStripMenuItem3,
            this.optionToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.helpToolStripMenuItem2,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem1});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
      // 
      // encryptToolStripMenuItem
      // 
      this.encryptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFilesToolStripMenuItem1,
            this.selectFoldersToolStripMenuItem1});
      resources.ApplyResources(this.encryptToolStripMenuItem, "encryptToolStripMenuItem");
      this.encryptToolStripMenuItem.Name = "encryptToolStripMenuItem";
      // 
      // selectFilesToolStripMenuItem1
      // 
      resources.ApplyResources(this.selectFilesToolStripMenuItem1, "selectFilesToolStripMenuItem1");
      this.selectFilesToolStripMenuItem1.Name = "selectFilesToolStripMenuItem1";
      // 
      // selectFoldersToolStripMenuItem1
      // 
      resources.ApplyResources(this.selectFoldersToolStripMenuItem1, "selectFoldersToolStripMenuItem1");
      this.selectFoldersToolStripMenuItem1.Name = "selectFoldersToolStripMenuItem1";
      // 
      // decryptToolStripMenuItem
      // 
      resources.ApplyResources(this.decryptToolStripMenuItem, "decryptToolStripMenuItem");
      this.decryptToolStripMenuItem.Name = "decryptToolStripMenuItem";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
      // 
      // optionToolStripMenuItem1
      // 
      resources.ApplyResources(this.optionToolStripMenuItem1, "optionToolStripMenuItem1");
      this.optionToolStripMenuItem1.Name = "optionToolStripMenuItem1";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
      // 
      // helpToolStripMenuItem2
      // 
      resources.ApplyResources(this.helpToolStripMenuItem2, "helpToolStripMenuItem2");
      this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
      // 
      // exitToolStripMenuItem1
      // 
      resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
      this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAtcFile,
            this.ToolStripMenuItemExeFile,
            this.ToolStripMenuItemZipPassword});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
      // 
      // ToolStripMenuItemAtcFile
      // 
      resources.ApplyResources(this.ToolStripMenuItemAtcFile, "ToolStripMenuItemAtcFile");
      this.ToolStripMenuItemAtcFile.Name = "ToolStripMenuItemAtcFile";
      this.ToolStripMenuItemAtcFile.Click += new System.EventHandler(this.ToolStripMenuItemAtcFile_Click);
      // 
      // ToolStripMenuItemExeFile
      // 
      resources.ApplyResources(this.ToolStripMenuItemExeFile, "ToolStripMenuItemExeFile");
      this.ToolStripMenuItemExeFile.Name = "ToolStripMenuItemExeFile";
      this.ToolStripMenuItemExeFile.Click += new System.EventHandler(this.ToolStripMenuItemExeFile_Click);
      // 
      // ToolStripMenuItemZipPassword
      // 
      resources.ApplyResources(this.ToolStripMenuItemZipPassword, "ToolStripMenuItemZipPassword");
      this.ToolStripMenuItemZipPassword.Name = "ToolStripMenuItemZipPassword";
      this.ToolStripMenuItemZipPassword.Click += new System.EventHandler(this.ToolStripMenuItemZipPassword_Click);
      // 
      // contextMenuStrip3
      // 
      this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFilesToEncryptToolStripMenuItem,
            this.selectDirectoryToEncryptToolStripMenuItem,
            this.selectFileToDecryptToolStripMenuItem,
            this.toolStripMenuItem6,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem7,
            this.onlineHelpToolStripMenuItem});
      this.contextMenuStrip3.Name = "contextMenuStrip3";
      resources.ApplyResources(this.contextMenuStrip3, "contextMenuStrip3");
      // 
      // selectFilesToEncryptToolStripMenuItem
      // 
      resources.ApplyResources(this.selectFilesToEncryptToolStripMenuItem, "selectFilesToEncryptToolStripMenuItem");
      this.selectFilesToEncryptToolStripMenuItem.Name = "selectFilesToEncryptToolStripMenuItem";
      this.selectFilesToEncryptToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFiles_Click);
      // 
      // selectDirectoryToEncryptToolStripMenuItem
      // 
      resources.ApplyResources(this.selectDirectoryToEncryptToolStripMenuItem, "selectDirectoryToEncryptToolStripMenuItem");
      this.selectDirectoryToEncryptToolStripMenuItem.Name = "selectDirectoryToEncryptToolStripMenuItem";
      this.selectDirectoryToEncryptToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFolder_Click);
      // 
      // selectFileToDecryptToolStripMenuItem
      // 
      resources.ApplyResources(this.selectFileToDecryptToolStripMenuItem, "selectFileToDecryptToolStripMenuItem");
      this.selectFileToDecryptToolStripMenuItem.Name = "selectFileToDecryptToolStripMenuItem";
      this.selectFileToDecryptToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemDecrypt_Click);
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
      // 
      // settingsToolStripMenuItem
      // 
      resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
      // 
      // toolStripMenuItem7
      // 
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
      // 
      // onlineHelpToolStripMenuItem
      // 
      resources.ApplyResources(this.onlineHelpToolStripMenuItem, "onlineHelpToolStripMenuItem");
      this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
      this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
      this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "atc";
      resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
      // 
      // toolTipZxcvbnWarning
      // 
      this.toolTipZxcvbnWarning.IsBalloon = true;
      this.toolTipZxcvbnWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
      // 
      // Form1
      // 
      this.AllowDrop = true;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelOuter);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.DoubleBuffered = true;
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
      this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panelOuter.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPageStartPage.ResumeLayout(false);
      this.panelStartPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHamburgerMenu)).EndInit();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDec)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZip)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExe)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtc)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeleteOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZipOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZipOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExeOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExeOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtcOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtcOn)).EndInit();
      this.tabPageEncrypt.ResumeLayout(false);
      this.panelEncrypt.ResumeLayout(false);
      this.panelEncrypt.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPassStrengthMeter)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrengthEmpty)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength04)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength03)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength02)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength01)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPasswordStrength00)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptBackButton)).EndInit();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryption)).EndInit();
      this.tabPageEncryptConfirm.ResumeLayout(false);
      this.panelEncryptConfirm.ResumeLayout(false);
      this.panelEncryptConfirm.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptConfirmBackButton)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckPasswordValidation)).EndInit();
      this.panel3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptionConfirm)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInValidIcon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxValidIcon)).EndInit();
      this.tabPageDecrypt.ResumeLayout(false);
      this.panelDecrypt.ResumeLayout(false);
      this.panelDecrypt.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryptBackButton)).EndInit();
      this.panel4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryption)).EndInit();
      this.tabPageProgressState.ResumeLayout(false);
      this.panelProgressState.ResumeLayout(false);
      this.panelProgressState.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressStateBackButton)).EndInit();
      this.panel5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEncrypt;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOption;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelpContents;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEncryptSelectFiles;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEncryptSelectFolder;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDecrypt;
        private System.Windows.Forms.ToolStripButton toolStripButtonEncryptSelectFiles;
        private System.Windows.Forms.ToolStripButton toolStripButtonEncryptSelectFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDecryptSelectAtcFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonOption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.Panel panelOuter;
        private System.Windows.Forms.TabControl tabControl1;
				private System.Windows.Forms.TabPage tabPageStartPage;
        private System.Windows.Forms.TabPage tabPageEncrypt;
        private System.Windows.Forms.Panel panelEncrypt;
        private System.Windows.Forms.TabPage tabPageDecrypt;
        private System.Windows.Forms.Panel panelDecrypt;
        private System.Windows.Forms.TabPage tabPageProgressState;
        private System.Windows.Forms.Panel panelProgressState;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelProgressPercentText;
        private System.Windows.Forms.Label labelProgressMessageText;
				private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelPassword;
				private System.Windows.Forms.Button buttonEncryptCancel;
				private System.Windows.Forms.Button buttonDecryptCancel;
				private System.Windows.Forms.Button buttonDecryptStart;
				private System.Windows.Forms.Label labelDecryptionPassword;
				private System.Windows.Forms.TextBox textBoxDecryptPassword;
				private System.Windows.Forms.CheckBox checkBoxNotMaskDecryptedPassword;
				private System.Windows.Forms.NotifyIcon notifyIcon1;
				private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
				private System.Windows.Forms.ToolStripMenuItem encryptToolStripMenuItem;
				private System.Windows.Forms.ToolStripMenuItem selectFilesToolStripMenuItem1;
				private System.Windows.Forms.ToolStripMenuItem selectFoldersToolStripMenuItem1;
				private System.Windows.Forms.ToolStripMenuItem decryptToolStripMenuItem;
				private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
				private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem1;
				private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
				private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem2;
				private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
				private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
				private System.Windows.Forms.SaveFileDialog saveFileDialog1;
				private System.Windows.Forms.OpenFileDialog openFileDialog1;
				private System.Windows.Forms.CheckBox checkBoxDeleteAtcFileAfterDecryption;
				private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
				private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAtcFile;
				private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemZipPassword;
				private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExeFile;
				private System.ComponentModel.BackgroundWorker backgroundWorker1;
				private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
				private System.Windows.Forms.Label labelCryptionType;
    private System.Windows.Forms.TabPage tabPageEncryptConfirm;
    private System.Windows.Forms.Panel panel2;
    internal System.Windows.Forms.PictureBox pictureBoxEncryption;
    private System.Windows.Forms.Label labelEncryption;
    private System.Windows.Forms.Button buttonEncryptionPasswordOk;
    private System.Windows.Forms.Panel panelEncryptConfirm;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label labelEncryptionConfirm;
    internal System.Windows.Forms.PictureBox pictureBoxEncryptionConfirm;
    private System.Windows.Forms.Button buttonEncryptionConfirmCancel;
    private System.Windows.Forms.Button buttonEncryptStart;
    private System.Windows.Forms.PictureBox pictureBoxInValidIcon;
    private System.Windows.Forms.PictureBox pictureBoxValidIcon;
    private System.Windows.Forms.CheckBox checkBoxReDeleteOriginalFileAfterEncryption;
    private System.Windows.Forms.CheckBox checkBoxReNotMaskEncryptedPassword;
    private System.Windows.Forms.Label labelInputPasswordAgain;
    private System.Windows.Forms.TextBox textBoxRePassword;
    private System.Windows.Forms.Panel panel4;
    internal System.Windows.Forms.PictureBox pictureBoxDecryption;
    private System.Windows.Forms.Label labelDecryption;
    private System.Windows.Forms.PictureBox pictureBoxCheckPasswordValidation;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label labelProgress;
    internal System.Windows.Forms.PictureBox pictureBoxProgress;
    private System.Windows.Forms.Panel panelStartPage;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label labelDec;
    private System.Windows.Forms.Label labelZip;
    private System.Windows.Forms.Label labelExe;
    private System.Windows.Forms.Label labelAtc;
    protected internal System.Windows.Forms.PictureBox pictureBoxDec;
    internal System.Windows.Forms.PictureBox pictureBoxZip;
    internal System.Windows.Forms.PictureBox pictureBoxExe;
    internal System.Windows.Forms.PictureBox pictureBoxAtc;
    internal System.Windows.Forms.PictureBox pictureBoxDeleteOn;
    internal System.Windows.Forms.PictureBox pictureBoxDecOff;
    internal System.Windows.Forms.PictureBox pictureBoxDecOn;
    internal System.Windows.Forms.PictureBox pictureBoxZipOff;
    internal System.Windows.Forms.PictureBox pictureBoxZipOn;
    internal System.Windows.Forms.PictureBox pictureBoxExeOff;
    internal System.Windows.Forms.PictureBox pictureBoxExeOn;
    internal System.Windows.Forms.PictureBox pictureBoxAtcOff;
    internal System.Windows.Forms.PictureBox pictureBoxAtcOn;
    private System.Windows.Forms.Label labelDragAndDrop;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.PictureBox pictureBoxHamburgerMenu;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
    private System.Windows.Forms.ToolStripMenuItem selectFilesToEncryptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectDirectoryToEncryptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectFileToDecryptToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
    internal System.Windows.Forms.PictureBox pictureBoxBackButtonOn;
    private System.Windows.Forms.PictureBox pictureBoxEncryptBackButton;
    private System.Windows.Forms.PictureBox pictureBoxEncryptConfirmBackButton;
    private System.Windows.Forms.PictureBox pictureBoxDecryptBackButton;
    private System.Windows.Forms.PictureBox pictureBoxProgressStateBackButton;
    internal System.Windows.Forms.PictureBox pictureBoxBackButtonOff;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDataVersion;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrengthEmpty;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrength04;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrength03;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrength02;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrength01;
    private System.Windows.Forms.PictureBox pictureBoxPasswordStrength00;
    private System.Windows.Forms.PictureBox pictureBoxPassStrengthMeter;
    private System.Windows.Forms.CheckBox checkBoxDeleteOriginalFileAfterEncryption;
    private System.Windows.Forms.CheckBox checkBoxNotMaskEncryptedPassword;
    private System.Windows.Forms.Label labelPasswordStrength;
    private DelayTextBox textBoxPassword;
    private System.Windows.Forms.ToolTip toolTipZxcvbnWarning;
    private System.Windows.Forms.ToolTip toolTipZxcvbnSuggestions;
  }
}

