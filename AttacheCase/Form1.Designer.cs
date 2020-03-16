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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "cts")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_busy")]
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
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDataVersion});
      this.statusStrip1.Name = "statusStrip1";
      this.toolTipZxcvbnWarning.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip1"));
      // 
      // toolStripStatusLabelDataVersion
      // 
      resources.ApplyResources(this.toolStripStatusLabelDataVersion, "toolStripStatusLabelDataVersion");
      this.toolStripStatusLabelDataVersion.Name = "toolStripStatusLabelDataVersion";
      // 
      // menuStrip1
      // 
      resources.ApplyResources(this.menuStrip1, "menuStrip1");
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemOption,
            this.ToolStripMenuItemHelp});
      this.menuStrip1.Name = "menuStrip1";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.menuStrip1, resources.GetString("menuStrip1.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.menuStrip1, resources.GetString("menuStrip1.ToolTip1"));
      // 
      // ToolStripMenuItemFile
      // 
      resources.ApplyResources(this.ToolStripMenuItemFile, "ToolStripMenuItemFile");
      this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEncrypt,
            this.ToolStripMenuItemDecrypt,
            this.toolStripMenuItem2,
            this.ToolStripMenuItemExit});
      this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
      this.ToolStripMenuItemFile.DropDownOpened += new System.EventHandler(this.ToolStripMenuItemFile_DropDownOpened);
      // 
      // ToolStripMenuItemEncrypt
      // 
      resources.ApplyResources(this.ToolStripMenuItemEncrypt, "ToolStripMenuItemEncrypt");
      this.ToolStripMenuItemEncrypt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEncryptSelectFiles,
            this.ToolStripMenuItemEncryptSelectFolder});
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
      resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      // 
      // ToolStripMenuItemExit
      // 
      resources.ApplyResources(this.ToolStripMenuItemExit, "ToolStripMenuItemExit");
      this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
      this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
      // 
      // ToolStripMenuItemOption
      // 
      resources.ApplyResources(this.ToolStripMenuItemOption, "ToolStripMenuItemOption");
      this.ToolStripMenuItemOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSetting});
      this.ToolStripMenuItemOption.Name = "ToolStripMenuItemOption";
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
      resources.ApplyResources(this.ToolStripMenuItemHelp, "ToolStripMenuItemHelp");
      this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemHelpContents,
            this.toolStripMenuItem1,
            this.ToolStripMenuItemAbout});
      this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
      // 
      // ToolStripMenuItemHelpContents
      // 
      resources.ApplyResources(this.ToolStripMenuItemHelpContents, "ToolStripMenuItemHelpContents");
      this.ToolStripMenuItemHelpContents.Name = "ToolStripMenuItemHelpContents";
      this.ToolStripMenuItemHelpContents.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // toolStripMenuItem1
      // 
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      // 
      // ToolStripMenuItemAbout
      // 
      resources.ApplyResources(this.ToolStripMenuItemAbout, "ToolStripMenuItemAbout");
      this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
      this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
      // 
      // toolStrip1
      // 
      resources.ApplyResources(this.toolStrip1, "toolStrip1");
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEncryptSelectFiles,
            this.toolStripButtonEncryptSelectFolder,
            this.toolStripSeparator1,
            this.toolStripButtonDecryptSelectAtcFiles,
            this.toolStripSeparator2,
            this.toolStripButtonOption,
            this.toolStripSeparator3,
            this.toolStripButtonHelp});
      this.toolStrip1.Name = "toolStrip1";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip1"));
      // 
      // toolStripButtonEncryptSelectFiles
      // 
      resources.ApplyResources(this.toolStripButtonEncryptSelectFiles, "toolStripButtonEncryptSelectFiles");
      this.toolStripButtonEncryptSelectFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonEncryptSelectFiles.Name = "toolStripButtonEncryptSelectFiles";
      this.toolStripButtonEncryptSelectFiles.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFiles_Click);
      // 
      // toolStripButtonEncryptSelectFolder
      // 
      resources.ApplyResources(this.toolStripButtonEncryptSelectFolder, "toolStripButtonEncryptSelectFolder");
      this.toolStripButtonEncryptSelectFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonEncryptSelectFolder.Name = "toolStripButtonEncryptSelectFolder";
      this.toolStripButtonEncryptSelectFolder.Click += new System.EventHandler(this.ToolStripMenuItemEncryptSelectFolder_Click);
      // 
      // toolStripSeparator1
      // 
      resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      // 
      // toolStripButtonDecryptSelectAtcFiles
      // 
      resources.ApplyResources(this.toolStripButtonDecryptSelectAtcFiles, "toolStripButtonDecryptSelectAtcFiles");
      this.toolStripButtonDecryptSelectAtcFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonDecryptSelectAtcFiles.Name = "toolStripButtonDecryptSelectAtcFiles";
      this.toolStripButtonDecryptSelectAtcFiles.Click += new System.EventHandler(this.ToolStripMenuItemDecrypt_Click);
      // 
      // toolStripSeparator2
      // 
      resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      // 
      // toolStripButtonOption
      // 
      resources.ApplyResources(this.toolStripButtonOption, "toolStripButtonOption");
      this.toolStripButtonOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonOption.Name = "toolStripButtonOption";
      this.toolStripButtonOption.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
      // 
      // toolStripSeparator3
      // 
      resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      // 
      // toolStripButtonHelp
      // 
      resources.ApplyResources(this.toolStripButtonHelp, "toolStripButtonHelp");
      this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonHelp.Name = "toolStripButtonHelp";
      this.toolStripButtonHelp.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // panelOuter
      // 
      resources.ApplyResources(this.panelOuter, "panelOuter");
      this.panelOuter.Controls.Add(this.tabControl1);
      this.panelOuter.Name = "panelOuter";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelOuter, resources.GetString("panelOuter.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelOuter, resources.GetString("panelOuter.ToolTip1"));
      // 
      // tabControl1
      // 
      resources.ApplyResources(this.tabControl1, "tabControl1");
      this.tabControl1.AllowDrop = true;
      this.tabControl1.Controls.Add(this.tabPageStartPage);
      this.tabControl1.Controls.Add(this.tabPageEncrypt);
      this.tabControl1.Controls.Add(this.tabPageEncryptConfirm);
      this.tabControl1.Controls.Add(this.tabPageDecrypt);
      this.tabControl1.Controls.Add(this.tabPageProgressState);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip1"));
      this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageStartPage
      // 
      resources.ApplyResources(this.tabPageStartPage, "tabPageStartPage");
      this.tabPageStartPage.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageStartPage.Controls.Add(this.panelStartPage);
      this.tabPageStartPage.Name = "tabPageStartPage";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabPageStartPage, resources.GetString("tabPageStartPage.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.tabPageStartPage, resources.GetString("tabPageStartPage.ToolTip1"));
      // 
      // panelStartPage
      // 
      resources.ApplyResources(this.panelStartPage, "panelStartPage");
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
      this.panelStartPage.Name = "panelStartPage";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelStartPage, resources.GetString("panelStartPage.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelStartPage, resources.GetString("panelStartPage.ToolTip1"));
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
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxBackButtonOff, resources.GetString("pictureBoxBackButtonOff.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxBackButtonOff, resources.GetString("pictureBoxBackButtonOff.ToolTip1"));
      // 
      // pictureBoxBackButtonOn
      // 
      resources.ApplyResources(this.pictureBoxBackButtonOn, "pictureBoxBackButtonOn");
      this.pictureBoxBackButtonOn.Name = "pictureBoxBackButtonOn";
      this.pictureBoxBackButtonOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxBackButtonOn, resources.GetString("pictureBoxBackButtonOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxBackButtonOn, resources.GetString("pictureBoxBackButtonOn.ToolTip1"));
      // 
      // pictureBoxHamburgerMenu
      // 
      resources.ApplyResources(this.pictureBoxHamburgerMenu, "pictureBoxHamburgerMenu");
      this.pictureBoxHamburgerMenu.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxHamburgerMenu.Name = "pictureBoxHamburgerMenu";
      this.pictureBoxHamburgerMenu.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxHamburgerMenu, resources.GetString("pictureBoxHamburgerMenu.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxHamburgerMenu, resources.GetString("pictureBoxHamburgerMenu.ToolTip1"));
      this.pictureBoxHamburgerMenu.Click += new System.EventHandler(this.pictureBoxHamburgerMenu_Click);
      // 
      // buttonExit
      // 
      resources.ApplyResources(this.buttonExit, "buttonExit");
      this.buttonExit.Name = "buttonExit";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonExit, resources.GetString("buttonExit.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonExit, resources.GetString("buttonExit.ToolTip1"));
      this.buttonExit.UseVisualStyleBackColor = true;
      this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
      // 
      // panel1
      // 
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel1.Controls.Add(this.labelDec);
      this.panel1.Controls.Add(this.labelZip);
      this.panel1.Controls.Add(this.labelExe);
      this.panel1.Controls.Add(this.labelAtc);
      this.panel1.Controls.Add(this.pictureBoxDec);
      this.panel1.Controls.Add(this.pictureBoxZip);
      this.panel1.Controls.Add(this.pictureBoxExe);
      this.panel1.Controls.Add(this.pictureBoxAtc);
      this.panel1.Name = "panel1";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip1"));
      // 
      // labelDec
      // 
      resources.ApplyResources(this.labelDec, "labelDec");
      this.labelDec.Name = "labelDec";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelDec, resources.GetString("labelDec.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelDec, resources.GetString("labelDec.ToolTip1"));
      // 
      // labelZip
      // 
      resources.ApplyResources(this.labelZip, "labelZip");
      this.labelZip.Name = "labelZip";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelZip, resources.GetString("labelZip.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelZip, resources.GetString("labelZip.ToolTip1"));
      // 
      // labelExe
      // 
      resources.ApplyResources(this.labelExe, "labelExe");
      this.labelExe.Name = "labelExe";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelExe, resources.GetString("labelExe.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelExe, resources.GetString("labelExe.ToolTip1"));
      // 
      // labelAtc
      // 
      resources.ApplyResources(this.labelAtc, "labelAtc");
      this.labelAtc.BackColor = System.Drawing.Color.Transparent;
      this.labelAtc.Name = "labelAtc";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelAtc, resources.GetString("labelAtc.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelAtc, resources.GetString("labelAtc.ToolTip1"));
      // 
      // pictureBoxDec
      // 
      resources.ApplyResources(this.pictureBoxDec, "pictureBoxDec");
      this.pictureBoxDec.Name = "pictureBoxDec";
      this.pictureBoxDec.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDec, resources.GetString("pictureBoxDec.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDec, resources.GetString("pictureBoxDec.ToolTip1"));
      // 
      // pictureBoxZip
      // 
      resources.ApplyResources(this.pictureBoxZip, "pictureBoxZip");
      this.pictureBoxZip.Name = "pictureBoxZip";
      this.pictureBoxZip.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxZip, resources.GetString("pictureBoxZip.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxZip, resources.GetString("pictureBoxZip.ToolTip1"));
      this.pictureBoxZip.Click += new System.EventHandler(this.pictureBoxZip_Click);
      // 
      // pictureBoxExe
      // 
      resources.ApplyResources(this.pictureBoxExe, "pictureBoxExe");
      this.pictureBoxExe.Name = "pictureBoxExe";
      this.pictureBoxExe.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxExe, resources.GetString("pictureBoxExe.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxExe, resources.GetString("pictureBoxExe.ToolTip1"));
      this.pictureBoxExe.Click += new System.EventHandler(this.pictureBoxExe_Click);
      // 
      // pictureBoxAtc
      // 
      resources.ApplyResources(this.pictureBoxAtc, "pictureBoxAtc");
      this.pictureBoxAtc.Name = "pictureBoxAtc";
      this.pictureBoxAtc.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxAtc, resources.GetString("pictureBoxAtc.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxAtc, resources.GetString("pictureBoxAtc.ToolTip1"));
      this.pictureBoxAtc.Click += new System.EventHandler(this.pictureBoxAtc_Click);
      // 
      // pictureBoxDeleteOn
      // 
      resources.ApplyResources(this.pictureBoxDeleteOn, "pictureBoxDeleteOn");
      this.pictureBoxDeleteOn.Name = "pictureBoxDeleteOn";
      this.pictureBoxDeleteOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDeleteOn, resources.GetString("pictureBoxDeleteOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDeleteOn, resources.GetString("pictureBoxDeleteOn.ToolTip1"));
      // 
      // pictureBoxDecOff
      // 
      resources.ApplyResources(this.pictureBoxDecOff, "pictureBoxDecOff");
      this.pictureBoxDecOff.Name = "pictureBoxDecOff";
      this.pictureBoxDecOff.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDecOff, resources.GetString("pictureBoxDecOff.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDecOff, resources.GetString("pictureBoxDecOff.ToolTip1"));
      // 
      // pictureBoxDecOn
      // 
      resources.ApplyResources(this.pictureBoxDecOn, "pictureBoxDecOn");
      this.pictureBoxDecOn.Name = "pictureBoxDecOn";
      this.pictureBoxDecOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDecOn, resources.GetString("pictureBoxDecOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDecOn, resources.GetString("pictureBoxDecOn.ToolTip1"));
      // 
      // pictureBoxZipOff
      // 
      resources.ApplyResources(this.pictureBoxZipOff, "pictureBoxZipOff");
      this.pictureBoxZipOff.Name = "pictureBoxZipOff";
      this.pictureBoxZipOff.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxZipOff, resources.GetString("pictureBoxZipOff.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxZipOff, resources.GetString("pictureBoxZipOff.ToolTip1"));
      // 
      // pictureBoxZipOn
      // 
      resources.ApplyResources(this.pictureBoxZipOn, "pictureBoxZipOn");
      this.pictureBoxZipOn.Name = "pictureBoxZipOn";
      this.pictureBoxZipOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxZipOn, resources.GetString("pictureBoxZipOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxZipOn, resources.GetString("pictureBoxZipOn.ToolTip1"));
      // 
      // pictureBoxExeOff
      // 
      resources.ApplyResources(this.pictureBoxExeOff, "pictureBoxExeOff");
      this.pictureBoxExeOff.Name = "pictureBoxExeOff";
      this.pictureBoxExeOff.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxExeOff, resources.GetString("pictureBoxExeOff.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxExeOff, resources.GetString("pictureBoxExeOff.ToolTip1"));
      // 
      // pictureBoxExeOn
      // 
      resources.ApplyResources(this.pictureBoxExeOn, "pictureBoxExeOn");
      this.pictureBoxExeOn.Name = "pictureBoxExeOn";
      this.pictureBoxExeOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxExeOn, resources.GetString("pictureBoxExeOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxExeOn, resources.GetString("pictureBoxExeOn.ToolTip1"));
      // 
      // pictureBoxAtcOff
      // 
      resources.ApplyResources(this.pictureBoxAtcOff, "pictureBoxAtcOff");
      this.pictureBoxAtcOff.Name = "pictureBoxAtcOff";
      this.pictureBoxAtcOff.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxAtcOff, resources.GetString("pictureBoxAtcOff.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxAtcOff, resources.GetString("pictureBoxAtcOff.ToolTip1"));
      // 
      // pictureBoxAtcOn
      // 
      resources.ApplyResources(this.pictureBoxAtcOn, "pictureBoxAtcOn");
      this.pictureBoxAtcOn.Name = "pictureBoxAtcOn";
      this.pictureBoxAtcOn.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxAtcOn, resources.GetString("pictureBoxAtcOn.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxAtcOn, resources.GetString("pictureBoxAtcOn.ToolTip1"));
      // 
      // labelDragAndDrop
      // 
      resources.ApplyResources(this.labelDragAndDrop, "labelDragAndDrop");
      this.labelDragAndDrop.Name = "labelDragAndDrop";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelDragAndDrop, resources.GetString("labelDragAndDrop.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelDragAndDrop, resources.GetString("labelDragAndDrop.ToolTip1"));
      this.labelDragAndDrop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelDragAndDrop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageEncrypt
      // 
      resources.ApplyResources(this.tabPageEncrypt, "tabPageEncrypt");
      this.tabPageEncrypt.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageEncrypt.Controls.Add(this.panelEncrypt);
      this.tabPageEncrypt.Name = "tabPageEncrypt";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabPageEncrypt, resources.GetString("tabPageEncrypt.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.tabPageEncrypt, resources.GetString("tabPageEncrypt.ToolTip1"));
      // 
      // panelEncrypt
      // 
      resources.ApplyResources(this.panelEncrypt, "panelEncrypt");
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
      this.panelEncrypt.Name = "panelEncrypt";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelEncrypt, resources.GetString("panelEncrypt.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelEncrypt, resources.GetString("panelEncrypt.ToolTip1"));
      this.panelEncrypt.VisibleChanged += new System.EventHandler(this.panelEncrypt_VisibleChanged);
      this.panelEncrypt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.panelEncrypt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // textBoxPassword
      // 
      resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
      this.textBoxPassword.Name = "textBoxPassword";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.textBoxPassword, resources.GetString("textBoxPassword.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.textBoxPassword, resources.GetString("textBoxPassword.ToolTip1"));
      this.textBoxPassword.UseSystemPasswordChar = true;
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
      this.toolTipZxcvbnWarning.SetToolTip(this.labelPasswordStrength, resources.GetString("labelPasswordStrength.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelPasswordStrength, resources.GetString("labelPasswordStrength.ToolTip1"));
      // 
      // checkBoxDeleteOriginalFileAfterEncryption
      // 
      resources.ApplyResources(this.checkBoxDeleteOriginalFileAfterEncryption, "checkBoxDeleteOriginalFileAfterEncryption");
      this.checkBoxDeleteOriginalFileAfterEncryption.Name = "checkBoxDeleteOriginalFileAfterEncryption";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxDeleteOriginalFileAfterEncryption, resources.GetString("checkBoxDeleteOriginalFileAfterEncryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxDeleteOriginalFileAfterEncryption, resources.GetString("checkBoxDeleteOriginalFileAfterEncryption.ToolTip1"));
      this.checkBoxDeleteOriginalFileAfterEncryption.UseVisualStyleBackColor = true;
      // 
      // checkBoxNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskEncryptedPassword, "checkBoxNotMaskEncryptedPassword");
      this.checkBoxNotMaskEncryptedPassword.Name = "checkBoxNotMaskEncryptedPassword";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxNotMaskEncryptedPassword, resources.GetString("checkBoxNotMaskEncryptedPassword.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxNotMaskEncryptedPassword, resources.GetString("checkBoxNotMaskEncryptedPassword.ToolTip1"));
      this.checkBoxNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxNotMaskEncryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxNotMaskEncryptedPassword_CheckedChanged);
      // 
      // pictureBoxPassStrengthMeter
      // 
      resources.ApplyResources(this.pictureBoxPassStrengthMeter, "pictureBoxPassStrengthMeter");
      this.pictureBoxPassStrengthMeter.Name = "pictureBoxPassStrengthMeter";
      this.pictureBoxPassStrengthMeter.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPassStrengthMeter, resources.GetString("pictureBoxPassStrengthMeter.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPassStrengthMeter, resources.GetString("pictureBoxPassStrengthMeter.ToolTip1"));
      // 
      // pictureBoxPasswordStrengthEmpty
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrengthEmpty, "pictureBoxPasswordStrengthEmpty");
      this.pictureBoxPasswordStrengthEmpty.Name = "pictureBoxPasswordStrengthEmpty";
      this.pictureBoxPasswordStrengthEmpty.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrengthEmpty, resources.GetString("pictureBoxPasswordStrengthEmpty.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrengthEmpty, resources.GetString("pictureBoxPasswordStrengthEmpty.ToolTip1"));
      // 
      // pictureBoxPasswordStrength04
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength04, "pictureBoxPasswordStrength04");
      this.pictureBoxPasswordStrength04.Name = "pictureBoxPasswordStrength04";
      this.pictureBoxPasswordStrength04.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrength04, resources.GetString("pictureBoxPasswordStrength04.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrength04, resources.GetString("pictureBoxPasswordStrength04.ToolTip1"));
      // 
      // pictureBoxPasswordStrength03
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength03, "pictureBoxPasswordStrength03");
      this.pictureBoxPasswordStrength03.Name = "pictureBoxPasswordStrength03";
      this.pictureBoxPasswordStrength03.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrength03, resources.GetString("pictureBoxPasswordStrength03.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrength03, resources.GetString("pictureBoxPasswordStrength03.ToolTip1"));
      // 
      // pictureBoxPasswordStrength02
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength02, "pictureBoxPasswordStrength02");
      this.pictureBoxPasswordStrength02.Name = "pictureBoxPasswordStrength02";
      this.pictureBoxPasswordStrength02.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrength02, resources.GetString("pictureBoxPasswordStrength02.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrength02, resources.GetString("pictureBoxPasswordStrength02.ToolTip1"));
      // 
      // pictureBoxPasswordStrength01
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength01, "pictureBoxPasswordStrength01");
      this.pictureBoxPasswordStrength01.Name = "pictureBoxPasswordStrength01";
      this.pictureBoxPasswordStrength01.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrength01, resources.GetString("pictureBoxPasswordStrength01.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrength01, resources.GetString("pictureBoxPasswordStrength01.ToolTip1"));
      // 
      // pictureBoxPasswordStrength00
      // 
      resources.ApplyResources(this.pictureBoxPasswordStrength00, "pictureBoxPasswordStrength00");
      this.pictureBoxPasswordStrength00.Name = "pictureBoxPasswordStrength00";
      this.pictureBoxPasswordStrength00.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxPasswordStrength00, resources.GetString("pictureBoxPasswordStrength00.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxPasswordStrength00, resources.GetString("pictureBoxPasswordStrength00.ToolTip1"));
      // 
      // pictureBoxEncryptBackButton
      // 
      resources.ApplyResources(this.pictureBoxEncryptBackButton, "pictureBoxEncryptBackButton");
      this.pictureBoxEncryptBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryptBackButton.Name = "pictureBoxEncryptBackButton";
      this.pictureBoxEncryptBackButton.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxEncryptBackButton, resources.GetString("pictureBoxEncryptBackButton.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxEncryptBackButton, resources.GetString("pictureBoxEncryptBackButton.ToolTip1"));
      this.pictureBoxEncryptBackButton.Click += new System.EventHandler(this.buttonEncryptCancel_Click);
      this.pictureBoxEncryptBackButton.MouseEnter += new System.EventHandler(this.pictureBoxEncryptBackButton_MouseEnter);
      this.pictureBoxEncryptBackButton.MouseLeave += new System.EventHandler(this.pictureBoxEncryptBackButton_MouseLeave);
      // 
      // buttonEncryptionPasswordOk
      // 
      resources.ApplyResources(this.buttonEncryptionPasswordOk, "buttonEncryptionPasswordOk");
      this.buttonEncryptionPasswordOk.Name = "buttonEncryptionPasswordOk";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonEncryptionPasswordOk, resources.GetString("buttonEncryptionPasswordOk.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonEncryptionPasswordOk, resources.GetString("buttonEncryptionPasswordOk.ToolTip1"));
      this.buttonEncryptionPasswordOk.UseVisualStyleBackColor = true;
      this.buttonEncryptionPasswordOk.Click += new System.EventHandler(this.buttonEncryptionPasswordOk_Click);
      // 
      // panel2
      // 
      resources.ApplyResources(this.panel2, "panel2");
      this.panel2.Controls.Add(this.labelEncryption);
      this.panel2.Controls.Add(this.pictureBoxEncryption);
      this.panel2.Name = "panel2";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panel2, resources.GetString("panel2.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panel2, resources.GetString("panel2.ToolTip1"));
      // 
      // labelEncryption
      // 
      resources.ApplyResources(this.labelEncryption, "labelEncryption");
      this.labelEncryption.BackColor = System.Drawing.Color.Transparent;
      this.labelEncryption.Name = "labelEncryption";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelEncryption, resources.GetString("labelEncryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelEncryption, resources.GetString("labelEncryption.ToolTip1"));
      // 
      // pictureBoxEncryption
      // 
      resources.ApplyResources(this.pictureBoxEncryption, "pictureBoxEncryption");
      this.pictureBoxEncryption.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryption.Name = "pictureBoxEncryption";
      this.pictureBoxEncryption.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxEncryption, resources.GetString("pictureBoxEncryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxEncryption, resources.GetString("pictureBoxEncryption.ToolTip1"));
      this.pictureBoxEncryption.Click += new System.EventHandler(this.pictureBoxEncryption_Click);
      // 
      // buttonEncryptCancel
      // 
      resources.ApplyResources(this.buttonEncryptCancel, "buttonEncryptCancel");
      this.buttonEncryptCancel.Name = "buttonEncryptCancel";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonEncryptCancel, resources.GetString("buttonEncryptCancel.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonEncryptCancel, resources.GetString("buttonEncryptCancel.ToolTip1"));
      this.buttonEncryptCancel.UseVisualStyleBackColor = true;
      this.buttonEncryptCancel.Click += new System.EventHandler(this.buttonEncryptCancel_Click);
      // 
      // labelPassword
      // 
      resources.ApplyResources(this.labelPassword, "labelPassword");
      this.labelPassword.BackColor = System.Drawing.Color.Transparent;
      this.labelPassword.Name = "labelPassword";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelPassword, resources.GetString("labelPassword.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelPassword, resources.GetString("labelPassword.ToolTip1"));
      this.labelPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // tabPageEncryptConfirm
      // 
      resources.ApplyResources(this.tabPageEncryptConfirm, "tabPageEncryptConfirm");
      this.tabPageEncryptConfirm.BackColor = System.Drawing.Color.White;
      this.tabPageEncryptConfirm.Controls.Add(this.panelEncryptConfirm);
      this.tabPageEncryptConfirm.Name = "tabPageEncryptConfirm";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabPageEncryptConfirm, resources.GetString("tabPageEncryptConfirm.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.tabPageEncryptConfirm, resources.GetString("tabPageEncryptConfirm.ToolTip1"));
      // 
      // panelEncryptConfirm
      // 
      resources.ApplyResources(this.panelEncryptConfirm, "panelEncryptConfirm");
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
      this.panelEncryptConfirm.Name = "panelEncryptConfirm";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelEncryptConfirm, resources.GetString("panelEncryptConfirm.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelEncryptConfirm, resources.GetString("panelEncryptConfirm.ToolTip1"));
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
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxEncryptConfirmBackButton, resources.GetString("pictureBoxEncryptConfirmBackButton.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxEncryptConfirmBackButton, resources.GetString("pictureBoxEncryptConfirmBackButton.ToolTip1"));
      this.pictureBoxEncryptConfirmBackButton.Click += new System.EventHandler(this.buttonEncryptionConfirmCancel_Click);
      this.pictureBoxEncryptConfirmBackButton.MouseEnter += new System.EventHandler(this.pictureBoxEncryptConfirmBackButton_MouseEnter);
      this.pictureBoxEncryptConfirmBackButton.MouseLeave += new System.EventHandler(this.pictureBoxEncryptConfirmBackButton_MouseLeave);
      // 
      // pictureBoxCheckPasswordValidation
      // 
      resources.ApplyResources(this.pictureBoxCheckPasswordValidation, "pictureBoxCheckPasswordValidation");
      this.pictureBoxCheckPasswordValidation.Name = "pictureBoxCheckPasswordValidation";
      this.pictureBoxCheckPasswordValidation.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxCheckPasswordValidation, resources.GetString("pictureBoxCheckPasswordValidation.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxCheckPasswordValidation, resources.GetString("pictureBoxCheckPasswordValidation.ToolTip1"));
      // 
      // panel3
      // 
      resources.ApplyResources(this.panel3, "panel3");
      this.panel3.Controls.Add(this.labelEncryptionConfirm);
      this.panel3.Controls.Add(this.pictureBoxEncryptionConfirm);
      this.panel3.Name = "panel3";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panel3, resources.GetString("panel3.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panel3, resources.GetString("panel3.ToolTip1"));
      // 
      // labelEncryptionConfirm
      // 
      resources.ApplyResources(this.labelEncryptionConfirm, "labelEncryptionConfirm");
      this.labelEncryptionConfirm.Name = "labelEncryptionConfirm";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelEncryptionConfirm, resources.GetString("labelEncryptionConfirm.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelEncryptionConfirm, resources.GetString("labelEncryptionConfirm.ToolTip1"));
      // 
      // pictureBoxEncryptionConfirm
      // 
      resources.ApplyResources(this.pictureBoxEncryptionConfirm, "pictureBoxEncryptionConfirm");
      this.pictureBoxEncryptionConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBoxEncryptionConfirm.Name = "pictureBoxEncryptionConfirm";
      this.pictureBoxEncryptionConfirm.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxEncryptionConfirm, resources.GetString("pictureBoxEncryptionConfirm.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxEncryptionConfirm, resources.GetString("pictureBoxEncryptionConfirm.ToolTip1"));
      // 
      // buttonEncryptionConfirmCancel
      // 
      resources.ApplyResources(this.buttonEncryptionConfirmCancel, "buttonEncryptionConfirmCancel");
      this.buttonEncryptionConfirmCancel.Name = "buttonEncryptionConfirmCancel";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonEncryptionConfirmCancel, resources.GetString("buttonEncryptionConfirmCancel.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonEncryptionConfirmCancel, resources.GetString("buttonEncryptionConfirmCancel.ToolTip1"));
      this.buttonEncryptionConfirmCancel.UseVisualStyleBackColor = true;
      this.buttonEncryptionConfirmCancel.Click += new System.EventHandler(this.buttonEncryptionConfirmCancel_Click);
      // 
      // buttonEncryptStart
      // 
      resources.ApplyResources(this.buttonEncryptStart, "buttonEncryptStart");
      this.buttonEncryptStart.Name = "buttonEncryptStart";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonEncryptStart, resources.GetString("buttonEncryptStart.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonEncryptStart, resources.GetString("buttonEncryptStart.ToolTip1"));
      this.buttonEncryptStart.UseVisualStyleBackColor = true;
      this.buttonEncryptStart.Click += new System.EventHandler(this.buttonEncryptStart_Click);
      // 
      // pictureBoxInValidIcon
      // 
      resources.ApplyResources(this.pictureBoxInValidIcon, "pictureBoxInValidIcon");
      this.pictureBoxInValidIcon.Name = "pictureBoxInValidIcon";
      this.pictureBoxInValidIcon.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxInValidIcon, resources.GetString("pictureBoxInValidIcon.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxInValidIcon, resources.GetString("pictureBoxInValidIcon.ToolTip1"));
      // 
      // pictureBoxValidIcon
      // 
      resources.ApplyResources(this.pictureBoxValidIcon, "pictureBoxValidIcon");
      this.pictureBoxValidIcon.Name = "pictureBoxValidIcon";
      this.pictureBoxValidIcon.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxValidIcon, resources.GetString("pictureBoxValidIcon.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxValidIcon, resources.GetString("pictureBoxValidIcon.ToolTip1"));
      // 
      // checkBoxReDeleteOriginalFileAfterEncryption
      // 
      resources.ApplyResources(this.checkBoxReDeleteOriginalFileAfterEncryption, "checkBoxReDeleteOriginalFileAfterEncryption");
      this.checkBoxReDeleteOriginalFileAfterEncryption.Name = "checkBoxReDeleteOriginalFileAfterEncryption";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxReDeleteOriginalFileAfterEncryption, resources.GetString("checkBoxReDeleteOriginalFileAfterEncryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxReDeleteOriginalFileAfterEncryption, resources.GetString("checkBoxReDeleteOriginalFileAfterEncryption.ToolTip1"));
      this.checkBoxReDeleteOriginalFileAfterEncryption.UseVisualStyleBackColor = true;
      this.checkBoxReDeleteOriginalFileAfterEncryption.CheckedChanged += new System.EventHandler(this.checkBoxReDeleteOriginalFileAfterEncryption_CheckedChanged);
      // 
      // checkBoxReNotMaskEncryptedPassword
      // 
      resources.ApplyResources(this.checkBoxReNotMaskEncryptedPassword, "checkBoxReNotMaskEncryptedPassword");
      this.checkBoxReNotMaskEncryptedPassword.Name = "checkBoxReNotMaskEncryptedPassword";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxReNotMaskEncryptedPassword, resources.GetString("checkBoxReNotMaskEncryptedPassword.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxReNotMaskEncryptedPassword, resources.GetString("checkBoxReNotMaskEncryptedPassword.ToolTip1"));
      this.checkBoxReNotMaskEncryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxReNotMaskEncryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxReNotMaskEncryptedPassword_CheckedChanged);
      // 
      // labelInputPasswordAgain
      // 
      resources.ApplyResources(this.labelInputPasswordAgain, "labelInputPasswordAgain");
      this.labelInputPasswordAgain.BackColor = System.Drawing.Color.Transparent;
      this.labelInputPasswordAgain.Name = "labelInputPasswordAgain";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelInputPasswordAgain, resources.GetString("labelInputPasswordAgain.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelInputPasswordAgain, resources.GetString("labelInputPasswordAgain.ToolTip1"));
      // 
      // textBoxRePassword
      // 
      resources.ApplyResources(this.textBoxRePassword, "textBoxRePassword");
      this.textBoxRePassword.BackColor = System.Drawing.Color.PapayaWhip;
      this.textBoxRePassword.Name = "textBoxRePassword";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.textBoxRePassword, resources.GetString("textBoxRePassword.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.textBoxRePassword, resources.GetString("textBoxRePassword.ToolTip1"));
      this.textBoxRePassword.UseSystemPasswordChar = true;
      this.textBoxRePassword.TextChanged += new System.EventHandler(this.textBoxRePassword_TextChanged);
      this.textBoxRePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRePassword_KeyDown);
      // 
      // tabPageDecrypt
      // 
      resources.ApplyResources(this.tabPageDecrypt, "tabPageDecrypt");
      this.tabPageDecrypt.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageDecrypt.Controls.Add(this.panelDecrypt);
      this.tabPageDecrypt.Name = "tabPageDecrypt";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabPageDecrypt, resources.GetString("tabPageDecrypt.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.tabPageDecrypt, resources.GetString("tabPageDecrypt.ToolTip1"));
      // 
      // panelDecrypt
      // 
      resources.ApplyResources(this.panelDecrypt, "panelDecrypt");
      this.panelDecrypt.BackColor = System.Drawing.Color.White;
      this.panelDecrypt.Controls.Add(this.pictureBoxDecryptBackButton);
      this.panelDecrypt.Controls.Add(this.panel4);
      this.panelDecrypt.Controls.Add(this.checkBoxDeleteAtcFileAfterDecryption);
      this.panelDecrypt.Controls.Add(this.checkBoxNotMaskDecryptedPassword);
      this.panelDecrypt.Controls.Add(this.buttonDecryptCancel);
      this.panelDecrypt.Controls.Add(this.buttonDecryptStart);
      this.panelDecrypt.Controls.Add(this.labelDecryptionPassword);
      this.panelDecrypt.Controls.Add(this.textBoxDecryptPassword);
      this.panelDecrypt.Name = "panelDecrypt";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelDecrypt, resources.GetString("panelDecrypt.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelDecrypt, resources.GetString("panelDecrypt.ToolTip1"));
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
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDecryptBackButton, resources.GetString("pictureBoxDecryptBackButton.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDecryptBackButton, resources.GetString("pictureBoxDecryptBackButton.ToolTip1"));
      this.pictureBoxDecryptBackButton.Click += new System.EventHandler(this.buttonDecryptCancel_Click);
      this.pictureBoxDecryptBackButton.MouseEnter += new System.EventHandler(this.pictureBoxDecryptBackButton_MouseEnter);
      this.pictureBoxDecryptBackButton.MouseLeave += new System.EventHandler(this.pictureBoxDecryptBackButton_MouseLeave);
      // 
      // panel4
      // 
      resources.ApplyResources(this.panel4, "panel4");
      this.panel4.Controls.Add(this.pictureBoxDecryption);
      this.panel4.Controls.Add(this.labelDecryption);
      this.panel4.Name = "panel4";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panel4, resources.GetString("panel4.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panel4, resources.GetString("panel4.ToolTip1"));
      // 
      // pictureBoxDecryption
      // 
      resources.ApplyResources(this.pictureBoxDecryption, "pictureBoxDecryption");
      this.pictureBoxDecryption.Name = "pictureBoxDecryption";
      this.pictureBoxDecryption.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxDecryption, resources.GetString("pictureBoxDecryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxDecryption, resources.GetString("pictureBoxDecryption.ToolTip1"));
      // 
      // labelDecryption
      // 
      resources.ApplyResources(this.labelDecryption, "labelDecryption");
      this.labelDecryption.Name = "labelDecryption";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelDecryption, resources.GetString("labelDecryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelDecryption, resources.GetString("labelDecryption.ToolTip1"));
      // 
      // checkBoxDeleteAtcFileAfterDecryption
      // 
      resources.ApplyResources(this.checkBoxDeleteAtcFileAfterDecryption, "checkBoxDeleteAtcFileAfterDecryption");
      this.checkBoxDeleteAtcFileAfterDecryption.Name = "checkBoxDeleteAtcFileAfterDecryption";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxDeleteAtcFileAfterDecryption, resources.GetString("checkBoxDeleteAtcFileAfterDecryption.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxDeleteAtcFileAfterDecryption, resources.GetString("checkBoxDeleteAtcFileAfterDecryption.ToolTip1"));
      this.checkBoxDeleteAtcFileAfterDecryption.UseVisualStyleBackColor = true;
      this.checkBoxDeleteAtcFileAfterDecryption.CheckedChanged += new System.EventHandler(this.checkBoxDeleteAtcFileAfterDecryption_CheckedChanged);
      // 
      // checkBoxNotMaskDecryptedPassword
      // 
      resources.ApplyResources(this.checkBoxNotMaskDecryptedPassword, "checkBoxNotMaskDecryptedPassword");
      this.checkBoxNotMaskDecryptedPassword.Name = "checkBoxNotMaskDecryptedPassword";
      this.toolTipZxcvbnWarning.SetToolTip(this.checkBoxNotMaskDecryptedPassword, resources.GetString("checkBoxNotMaskDecryptedPassword.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.checkBoxNotMaskDecryptedPassword, resources.GetString("checkBoxNotMaskDecryptedPassword.ToolTip1"));
      this.checkBoxNotMaskDecryptedPassword.UseVisualStyleBackColor = true;
      this.checkBoxNotMaskDecryptedPassword.CheckedChanged += new System.EventHandler(this.checkBoxNotMaskDecryptedPassword_CheckedChanged);
      // 
      // buttonDecryptCancel
      // 
      resources.ApplyResources(this.buttonDecryptCancel, "buttonDecryptCancel");
      this.buttonDecryptCancel.Name = "buttonDecryptCancel";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonDecryptCancel, resources.GetString("buttonDecryptCancel.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonDecryptCancel, resources.GetString("buttonDecryptCancel.ToolTip1"));
      this.buttonDecryptCancel.UseVisualStyleBackColor = true;
      this.buttonDecryptCancel.Click += new System.EventHandler(this.buttonDecryptCancel_Click);
      // 
      // buttonDecryptStart
      // 
      resources.ApplyResources(this.buttonDecryptStart, "buttonDecryptStart");
      this.buttonDecryptStart.Name = "buttonDecryptStart";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonDecryptStart, resources.GetString("buttonDecryptStart.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonDecryptStart, resources.GetString("buttonDecryptStart.ToolTip1"));
      this.buttonDecryptStart.UseVisualStyleBackColor = true;
      this.buttonDecryptStart.Click += new System.EventHandler(this.buttonDecryptStart_Click);
      // 
      // labelDecryptionPassword
      // 
      resources.ApplyResources(this.labelDecryptionPassword, "labelDecryptionPassword");
      this.labelDecryptionPassword.Name = "labelDecryptionPassword";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelDecryptionPassword, resources.GetString("labelDecryptionPassword.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelDecryptionPassword, resources.GetString("labelDecryptionPassword.ToolTip1"));
      this.labelDecryptionPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelDecryptionPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // textBoxDecryptPassword
      // 
      resources.ApplyResources(this.textBoxDecryptPassword, "textBoxDecryptPassword");
      this.textBoxDecryptPassword.AllowDrop = true;
      this.textBoxDecryptPassword.Name = "textBoxDecryptPassword";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.textBoxDecryptPassword, resources.GetString("textBoxDecryptPassword.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.textBoxDecryptPassword, resources.GetString("textBoxDecryptPassword.ToolTip1"));
      this.textBoxDecryptPassword.UseSystemPasswordChar = true;
      this.textBoxDecryptPassword.TextChanged += new System.EventHandler(this.textBoxDecryptPassword_TextChanged);
      this.textBoxDecryptPassword.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxDecryptPassword_DragDrop);
      this.textBoxDecryptPassword.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxDecryptPassword_DragEnter);
      this.textBoxDecryptPassword.DragLeave += new System.EventHandler(this.textBoxDecryptPassword_DragLeave);
      this.textBoxDecryptPassword.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBoxDecryptPassword_PreviewKeyDown);
      // 
      // tabPageProgressState
      // 
      resources.ApplyResources(this.tabPageProgressState, "tabPageProgressState");
      this.tabPageProgressState.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageProgressState.Controls.Add(this.panelProgressState);
      this.tabPageProgressState.Name = "tabPageProgressState";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.tabPageProgressState, resources.GetString("tabPageProgressState.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.tabPageProgressState, resources.GetString("tabPageProgressState.ToolTip1"));
      // 
      // panelProgressState
      // 
      resources.ApplyResources(this.panelProgressState, "panelProgressState");
      this.panelProgressState.BackColor = System.Drawing.Color.White;
      this.panelProgressState.Controls.Add(this.pictureBoxProgressStateBackButton);
      this.panelProgressState.Controls.Add(this.panel5);
      this.panelProgressState.Controls.Add(this.labelCryptionType);
      this.panelProgressState.Controls.Add(this.buttonCancel);
      this.panelProgressState.Controls.Add(this.labelProgressPercentText);
      this.panelProgressState.Controls.Add(this.labelProgressMessageText);
      this.panelProgressState.Controls.Add(this.progressBar);
      this.panelProgressState.Name = "panelProgressState";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panelProgressState, resources.GetString("panelProgressState.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panelProgressState, resources.GetString("panelProgressState.ToolTip1"));
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
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxProgressStateBackButton, resources.GetString("pictureBoxProgressStateBackButton.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxProgressStateBackButton, resources.GetString("pictureBoxProgressStateBackButton.ToolTip1"));
      this.pictureBoxProgressStateBackButton.Click += new System.EventHandler(this.pictureBoxProgressStateBackButton_Click);
      this.pictureBoxProgressStateBackButton.MouseEnter += new System.EventHandler(this.pictureBoxProgressStateBackButton_MouseEnter);
      this.pictureBoxProgressStateBackButton.MouseLeave += new System.EventHandler(this.pictureBoxProgressStateBackButton_MouseLeave);
      // 
      // panel5
      // 
      resources.ApplyResources(this.panel5, "panel5");
      this.panel5.Controls.Add(this.labelProgress);
      this.panel5.Controls.Add(this.pictureBoxProgress);
      this.panel5.Name = "panel5";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.panel5, resources.GetString("panel5.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.panel5, resources.GetString("panel5.ToolTip1"));
      // 
      // labelProgress
      // 
      resources.ApplyResources(this.labelProgress, "labelProgress");
      this.labelProgress.Name = "labelProgress";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelProgress, resources.GetString("labelProgress.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelProgress, resources.GetString("labelProgress.ToolTip1"));
      // 
      // pictureBoxProgress
      // 
      resources.ApplyResources(this.pictureBoxProgress, "pictureBoxProgress");
      this.pictureBoxProgress.Name = "pictureBoxProgress";
      this.pictureBoxProgress.TabStop = false;
      this.toolTipZxcvbnWarning.SetToolTip(this.pictureBoxProgress, resources.GetString("pictureBoxProgress.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.pictureBoxProgress, resources.GetString("pictureBoxProgress.ToolTip1"));
      // 
      // labelCryptionType
      // 
      resources.ApplyResources(this.labelCryptionType, "labelCryptionType");
      this.labelCryptionType.Name = "labelCryptionType";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelCryptionType, resources.GetString("labelCryptionType.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelCryptionType, resources.GetString("labelCryptionType.ToolTip1"));
      this.labelCryptionType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelCryptionType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // buttonCancel
      // 
      resources.ApplyResources(this.buttonCancel, "buttonCancel");
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Name = "buttonCancel";
      this.toolTipZxcvbnWarning.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip1"));
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // labelProgressPercentText
      // 
      resources.ApplyResources(this.labelProgressPercentText, "labelProgressPercentText");
      this.labelProgressPercentText.Name = "labelProgressPercentText";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelProgressPercentText, resources.GetString("labelProgressPercentText.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelProgressPercentText, resources.GetString("labelProgressPercentText.ToolTip1"));
      this.labelProgressPercentText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.labelProgressPercentText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // labelProgressMessageText
      // 
      resources.ApplyResources(this.labelProgressMessageText, "labelProgressMessageText");
      this.labelProgressMessageText.Name = "labelProgressMessageText";
      this.toolTipZxcvbnWarning.SetToolTip(this.labelProgressMessageText, resources.GetString("labelProgressMessageText.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.labelProgressMessageText, resources.GetString("labelProgressMessageText.ToolTip1"));
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
      this.toolTipZxcvbnWarning.SetToolTip(this.progressBar, resources.GetString("progressBar.ToolTip"));
      this.toolTipZxcvbnSuggestions.SetToolTip(this.progressBar, resources.GetString("progressBar.ToolTip1"));
      this.progressBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
      this.progressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
      // 
      // contextMenuStrip1
      // 
      resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
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
      this.toolTipZxcvbnSuggestions.SetToolTip(this.contextMenuStrip1, resources.GetString("contextMenuStrip1.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.contextMenuStrip1, resources.GetString("contextMenuStrip1.ToolTip1"));
      // 
      // encryptToolStripMenuItem
      // 
      resources.ApplyResources(this.encryptToolStripMenuItem, "encryptToolStripMenuItem");
      this.encryptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFilesToolStripMenuItem1,
            this.selectFoldersToolStripMenuItem1});
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
      resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      // 
      // optionToolStripMenuItem1
      // 
      resources.ApplyResources(this.optionToolStripMenuItem1, "optionToolStripMenuItem1");
      this.optionToolStripMenuItem1.Name = "optionToolStripMenuItem1";
      // 
      // toolStripMenuItem4
      // 
      resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      // 
      // helpToolStripMenuItem2
      // 
      resources.ApplyResources(this.helpToolStripMenuItem2, "helpToolStripMenuItem2");
      this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
      // 
      // toolStripMenuItem5
      // 
      resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      // 
      // exitToolStripMenuItem1
      // 
      resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
      this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
      // 
      // contextMenuStrip2
      // 
      resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAtcFile,
            this.ToolStripMenuItemExeFile,
            this.ToolStripMenuItemZipPassword});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.contextMenuStrip2, resources.GetString("contextMenuStrip2.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.contextMenuStrip2, resources.GetString("contextMenuStrip2.ToolTip1"));
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
      resources.ApplyResources(this.contextMenuStrip3, "contextMenuStrip3");
      this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFilesToEncryptToolStripMenuItem,
            this.selectDirectoryToEncryptToolStripMenuItem,
            this.selectFileToDecryptToolStripMenuItem,
            this.toolStripMenuItem6,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem7,
            this.onlineHelpToolStripMenuItem});
      this.contextMenuStrip3.Name = "contextMenuStrip3";
      this.toolTipZxcvbnSuggestions.SetToolTip(this.contextMenuStrip3, resources.GetString("contextMenuStrip3.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this.contextMenuStrip3, resources.GetString("contextMenuStrip3.ToolTip1"));
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
      resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      // 
      // settingsToolStripMenuItem
      // 
      resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
      // 
      // toolStripMenuItem7
      // 
      resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      // 
      // onlineHelpToolStripMenuItem
      // 
      resources.ApplyResources(this.onlineHelpToolStripMenuItem, "onlineHelpToolStripMenuItem");
      this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
      this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemHelpContents_Click);
      // 
      // notifyIcon1
      // 
      resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "atc";
      resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
      // 
      // openFileDialog1
      // 
      resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
      // 
      // folderBrowserDialog1
      // 
      resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
      // 
      // toolTipZxcvbnWarning
      // 
      this.toolTipZxcvbnWarning.IsBalloon = true;
      this.toolTipZxcvbnWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
      // 
      // Form1
      // 
      resources.ApplyResources(this, "$this");
      this.AllowDrop = true;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panelOuter);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.DoubleBuffered = true;
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.toolTipZxcvbnSuggestions.SetToolTip(this, resources.GetString("$this.ToolTip"));
      this.toolTipZxcvbnWarning.SetToolTip(this, resources.GetString("$this.ToolTip1"));
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
      this.panelStartPage.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOff)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackButtonOn)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHamburgerMenu)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
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
      this.panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryption)).EndInit();
      this.tabPageEncryptConfirm.ResumeLayout(false);
      this.panelEncryptConfirm.ResumeLayout(false);
      this.panelEncryptConfirm.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptConfirmBackButton)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheckPasswordValidation)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEncryptionConfirm)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInValidIcon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxValidIcon)).EndInit();
      this.tabPageDecrypt.ResumeLayout(false);
      this.panelDecrypt.ResumeLayout(false);
      this.panelDecrypt.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryptBackButton)).EndInit();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDecryption)).EndInit();
      this.tabPageProgressState.ResumeLayout(false);
      this.panelProgressState.ResumeLayout(false);
      this.panelProgressState.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressStateBackButton)).EndInit();
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
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

