//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016  Mitsuhiro Hibara
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
//---------------------------------------------------------------------- 
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AttacheCase.Properties;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace AttacheCase
{
  public partial class Form1 : Form
  {
    // Status Code
    private const int ENCRYPT_SUCCEEDED = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED = 2; // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED  = 3; // Delete is succeeded.
    private const int READY_FOR_ENCRYPT = 4; // Getting ready for encryption or decryption.
    private const int READY_FOR_DECRYPT = 5; // Getting ready for encryption or decryption.
    private const int ENCRYPTING        = 6; // Ecrypting.
    private const int DECRYPTING        = 7; // Decrypting.
    private const int DELETING          = 8; // Deleting.

    // Error Code
    private const int USER_CANCELED            = -1;   // User cancel.
    private const int ERROR_UNEXPECTED         = -100;
    private const int NOT_ATC_DATA             = -101;
    private const int ATC_BROKEN_DATA          = -102;
    private const int NO_DISK_SPACE            = -103;
    private const int FILE_INDEX_NOT_FOUND     = -104;
    private const int PASSWORD_TOKEN_NOT_FOUND = -105;
    private const int NOT_CORRECT_HASH_VALUE   = -106;

    // File Type
    private const int FILE_TYPE_ERROR        = -1;
    private const int FILE_TYPE_NONE         = 0;
    private const int FILE_TYPE_ATC          = 1;
    private const int FILE_TYPE_ATC_EXE      = 2;
    private const int FILE_TYPE_PASSWORD_ZIP = 3;

    // The position of mouse down in main form.
    // マウスボタンがダウンされた位置
    private Point MouseDownPoint;

    // AppSettings.cs
    // 
    // private string[] AppSettings.Instance.FileList = null;
    // List<string> FileList = new List<string>();
    // AppSettings.Instance.FileList

    private int LimitOfInputPassword = -1;

    public static BackgroundWorker bkg;
    public static BackgroundWorker bkgDelete;

    //private FileEncrypt2 encryption2, FileEncrypt3, encryption2;
    private FileDecrypt2 decryption2;
    private FileEncrypt3 encryption3;
    private FileDecrypt3 decryption3;
    private ZipEncrypt compression;
    private Wipe wipe;

    private CancellationTokenSource cts = new CancellationTokenSource();

    /// <summary>
    /// Constructor
    /// </summary>
    public Form1()
    {
      InitializeComponent();

      tabControl1.Visible = false;

      panelStartPage.Parent = panelOuter;
      panelEncrypt.Parent = panelOuter;
      panelEncryptConfirm.Parent = panelOuter;
      panelDecrypt.Parent = panelOuter;
      panelProgressState.Parent = panelOuter;

      // メインウィンドウの終了ボタン
      // Exit button of main window.
      buttonExit.Size = new Size(1, 1);

    }

    /// <summary>
    /// Destructor
    /// </summary>
    ~Form1()
    {
    }

    //======================================================================
    // フォームイベント
    //======================================================================
    #region
    /// <summary>
    /// Form1 Load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_Load(object sender, EventArgs e)
    {
      // View start window
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = false;
      panelStartPage.Visible = true;
      this.AllowDrop = true;

      this.Text = Resources.AttacheCase;

      // Bring AttcheCase window in front of Desktop
      if (AppSettings.Instance.fWindowForeground == true)
      {
        this.TopMost = true;
      }

      // Ajust invalid window form position
      this.Width = AppSettings.Instance.FormWidth;
      this.Height = AppSettings.Instance.FormHeight;

      //初期位置（スクリーン中央）
      //Default window position ( in screen center )
      if (AppSettings.Instance.FormLeft < 0)
      {
        this.Left = Screen.GetBounds(this).Width / 2 - this.Width / 2;
      }
      else
      {
        this.Left = AppSettings.Instance.FormLeft;
      }

      if (AppSettings.Instance.FormTop < 0)
      {
        this.Top = Screen.GetBounds(this).Height / 2 - this.Height / 2;
      }
      else
      {
        this.Top = AppSettings.Instance.FormTop;
      }

      // Not mask password character
      checkBoxNotMaskEncryptedPassword.Checked = AppSettings.Instance.fNotMaskPassword == true ? true : false;
      checkBoxNotMaskDecryptedPassword.Checked = AppSettings.Instance.fNotMaskPassword == true ? true : false;

      //----------------------------------------------------------------------
      // 内容にかかわらず暗号化か復号かを問い合わせる
      // Ask to encrypt or decrypt regardless of contents.
      if (AppSettings.Instance.fAskEncDecode == true && AppSettings.Instance.FileList.Count > 0)
      {
        // Show dialog for confirming to orverwrite
        Form4 frm4 = new Form4("AskEncryptOrDecrypt", "");
        frm4.ShowDialog();
        int ProcessNum = frm4.AskEncryptOrDecrypt;  // 1: Encryption, 2: Decryption, -1: Cancel
        frm4.Dispose();

        //-----------------------------------
        // Encryption
        //-----------------------------------
        if (ProcessNum == 1)
        {
          textBoxPassword.Text = AppSettings.Instance.MyEncryptPasswordString;
          textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;

          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
          panelEncryptConfirm.Visible = true; // Encrypt
        }
        //-----------------------------------
        // Decryption
        //-----------------------------------
        else if (ProcessNum == 2)
        {
          textBoxDecryptPassword.Text = AppSettings.Instance.MyDecryptPasswordString;
          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = false;
          panelProgressState.Visible = false;
          panelDecrypt.Visible = true;        // Decrypt
        }
        //-----------------------------------
        // Cancel
        //-----------------------------------
        else
        {
          //return;
        }

      }

      //----------------------------------------------------------------------
      if (AppSettings.Instance.FileList.Count() > 0)
      {

        int FileType = AppSettings.Instance.DetectFileType();
        if (FileType == FILE_TYPE_NONE)
        {
          if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
          {
            textBoxPassword.Text = AppSettings.Instance.MyEncryptPasswordString;
            textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;
          }

          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;         // Encrypt
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }
        else if (FileType == FILE_TYPE_ATC || FileType == FILE_TYPE_ATC_EXE)
        {
          if (AppSettings.Instance.fMyDecryptPasswordKeep == true)
          {
            textBoxDecryptPassword.Text = AppSettings.Instance.MyDecryptPasswordString;
          }

          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = false;
          panelProgressState.Visible = false;
          panelDecrypt.Visible = true;        // Decrypt
        }
        else if (FileType == FILE_TYPE_PASSWORD_ZIP)
        {
          if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
          {
            textBoxPassword.Text = AppSettings.Instance.MyEncryptPasswordString;
            textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;
          }

          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;       // Encrypt(ZIP)
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }
        else
        {
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
          panelStartPage.Visible = true;     // Main Window

        }

      }

    }
    /// <summary>
    /// Form shown event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_Shown(object sender, EventArgs e)
    {
    }
    
    /// <summary>
    /// Form closed event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      // Application path
      AppSettings.Instance.ApplicationPath = Application.ExecutablePath;

      // Application version
      System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
      System.Version ver = asm.GetName().Version;
      AppSettings.Instance.AppVersion = int.Parse(ver.ToString().Replace(".", ""));

      // Save main form position and size
      AppSettings.Instance.FormLeft = this.Left;
      AppSettings.Instance.FormTop = this.Top;
      AppSettings.Instance.FormWidth = this.Width;
      AppSettings.Instance.FormHeight = this.Height;

      if (File.Exists(AppSettings.Instance.IniFilePath) == true)
      {
        AppSettings.Instance.WriteOptionToIniFile(AppSettings.Instance.IniFilePath);
      }
      else
      {
        string[] cmds = Environment.GetCommandLineArgs();
        if (cmds.Count() <= 1)
        {
          // Save settings to registry
          AppSettings.Instance.SaveOptionsToRegistry();
        }
        else
        {
          //起動時のコマンドライン引数の場合、設定を保存しない。
          //If there is a startup command line arguments, not save the settings.
        }
      }
    }

    /// <summary>
    /// Form1 DragEnter event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_DragEnter(object sender, DragEventArgs e)
    {
      if (panelStartPage.Visible == true || (panelProgressState.Visible == true && progressBar.Value == progressBar.Maximum))
      {
      }
      else if (panelEncrypt.Visible == true && AppSettings.Instance.fAllowPassFile == true)
      {
      }
      else if (panelDecrypt.Visible == true && AppSettings.Instance.fAllowPassFile == true)
      {
      }
      else {
        e.Effect = DragDropEffects.None;
        return;
      }
      if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
      {
        e.Effect = DragDropEffects.Copy;
        panelStartPage.BackColor = Color.Honeydew;
      }

    }

    /// <summary>
    /// Form1 DragLeace event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_DragLeave(object sender, EventArgs e)
    {
      panelStartPage.BackColor = Color.White;
    }

    /// <summary>
    /// Form1 DragDrop event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_DragDrop(object sender, DragEventArgs e)
    {
      panelStartPage.BackColor = Color.White;

      string[] ArrayFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
      AppSettings.Instance.FileList = new List<string>();
      foreach (string FilePath in ArrayFiles)
      {
        AppSettings.Instance.FileList.Add(FilePath);
      }

      // 内容にかかわらず暗号化か復号かを問い合わせる
      // Ask to encrypt or decrypt regardless of contents.
      if (AppSettings.Instance.fAskEncDecode == true)
      {
        // Show dialog for confirming to orverwrite
        Form4 frm4 = new Form4("AskEncryptOrDecrypt", "");
        frm4.ShowDialog();
        int ProcessNum = frm4.AskEncryptOrDecrypt;  // 1: Encryption, 2: Decryption, -1: Cancel
        frm4.Dispose();

        //-----------------------------------
        // Encryption
        //-----------------------------------
        if (ProcessNum == 1)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;        // Encrypt
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
          return;
        }
        //-----------------------------------
        // Decryption
        //-----------------------------------
        else if (ProcessNum == 2)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = true;        // Decrypt
          panelProgressState.Visible = false;
          return;
        }
        //-----------------------------------
        // Cancel
        //-----------------------------------
        else
        {
          this.BackColor = Color.White;
          return;
        }
      }

      //----------------------------------------------------------------------
      // 問い合わせず自動判別する

      // File type
      // private const int FILE_TYPE_ERROR        = -1;
      // private const int FILE_TYPE_NONE         = 0;
      // private const int FILE_TYPE_ATC          = 1;
      // private const int FILE_TYPE_ATC_EXE      = 2;
      // private const int FILE_TYPE_PASSWORD_ZIP = 3;

      // すでに指定されている
      int FileType = 0;
      if (AppSettings.Instance.EncryptionFileType > 0)
      {
        FileType = AppSettings.Instance.EncryptionFileType;
      }
      else
      {
        // 指定がなければ判定する
        FileType = AppSettings.Instance.DetectFileType();
        AppSettings.Instance.EncryptionFileType = FileType;
      }

      if (AppSettings.Instance.FileList.Count() > 0)
      {
        //----------------------------------------------------------------------
        // Decryption
        if (FileType == FILE_TYPE_ATC || FileType == FILE_TYPE_ATC_EXE)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;        
          panelEncryptConfirm.Visible = false; 
          panelDecrypt.Visible = true;         // Decrypt
          panelProgressState.Visible = false;
        }
        //----------------------------------------------------------------------
        // Encryption
        else if (FileType == FILE_TYPE_ERROR || FileType == FILE_TYPE_NONE)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;        // Encrypt
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }
        //----------------------------------------------------------------------
        // Password ZIP
        else if (FileType == FILE_TYPE_PASSWORD_ZIP)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;         // Encrypt
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }
        else
        {
          panelStartPage.Visible = true;     // Main Window
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }

        this.BackColor = Color.White;

      }
    }
    #endregion

    //======================================================================
    // 削除処理
    //======================================================================
    #region
    private bool DeleteData(List<string> FileList)
    {
      pictureBoxProgress.Image = pictureBoxDeleteOn.Image;
      labelProgressMessageText.Text = "-";
      progressBar.Value = 0;
      labelProgressPercentText.Text = "- %";
      this.Update();

      // How to delete a way?
#if (DEBUG)
      string DesktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      string FileListTextPath = Path.Combine(DesktopPath, "file_list_text.txt");
      var FileListText = String.Join("\n", FileList);
      System.IO.File.WriteAllText(FileListTextPath, FileListText, System.Text.Encoding.UTF8);
#endif
      //----------------------------------------------------------------------
      // 通常削除
      // Normal delete
      //----------------------------------------------------------------------
      if (AppSettings.Instance.fCompleteDelFile == 0)
      {
        labelCryptionType.Text = Resources.labelProcessNameDelete;  // Deleting...

        ParallelOptions po = new ParallelOptions();
        po.CancellationToken = cts.Token;

        try
        {
          labelProgress.Text = Resources.labelNormalDelete;

          SynchronizationContext ctx = SynchronizationContext.Current;
          int count = 0;

          Parallel.ForEach(FileList, po, (FilePath, state) =>
          {

            if (File.Exists(FilePath) == true)
            {
              FileSystem.DeleteFile(FilePath);
            }
            else
            {
              // File or direcrory does not exists.
            }

            Interlocked.Increment(ref count);

            ctx.Post(d => {
              progressBar.Value = (int)((float)count / FileList.Count) * 10000;
            }, null);

            po.CancellationToken.ThrowIfCancellationRequested();

          });


          // Delete root directory
          if (Directory.Exists(FileList[0]) == true)
          {
            FileSystem.DeleteDirectory(
              FileList[0],
              UIOption.OnlyErrorDialogs,
              RecycleOption.DeletePermanently,
              UICancelOption.ThrowException
            );
          }

          labelCryptionType.Text = "";
          // 指定のファイル及びフォルダーは削除されました。
          // The specified files or folders has been deleted.
          labelProgressMessageText.Text = Resources.labelNormalDeleteCompleted;
          progressBar.Value = progressBar.Maximum;
          labelProgressPercentText.Text = "100%";
          Application.DoEvents();

        }
        catch (OperationCanceledException e)
        {
          // ユーザーキャンセル
          // User cancel

          labelCryptionType.Text = "";
          // ファイルまたはフォルダーの削除をキャンセルしました。
          // Deleting files or folders has been canceled.
          labelProgressMessageText.Text = Resources.labelNormalDeleteCanceled;
          progressBar.Value = 0;
          labelProgressPercentText.Text = "- %";
          return (false);
        }
        finally
        {
          cts.Dispose();
        }

        return (true);

      }
      //----------------------------------------------------------------------
      // ゴミ箱への移動
      // Send to the trash
      //----------------------------------------------------------------------
      else if (AppSettings.Instance.fCompleteDelFile == 1)
      {
        labelCryptionType.Text = Resources.labelProcessNameMoveToTrash;  // Move to Trash...
        labelProgress.Text = Resources.labelMoveToTrash;

        if (File.Exists(FileList[0]) == true)
        {
          FileSystem.DeleteFile(FileList[0], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
        else if (Directory.Exists(FileList[0]))
        {
          FileSystem.DeleteDirectory(
            FileList[0], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
        }

        labelCryptionType.Text = "";
        // ファイル、またはフォルダーのゴミ箱への移動が完了しました。
        // Move files or folders to the trash was completed.
        labelProgressMessageText.Text = Resources.labelMoveToTrashCompleted;
        progressBar.Value = progressBar.Maximum;
        labelProgressPercentText.Text = "100%";
        Application.DoEvents();

      }
      //----------------------------------------------------------------------
      // 完全削除
      // Complete deleting
      //----------------------------------------------------------------------
      else if (AppSettings.Instance.fCompleteDelFile == 2)
      {
        bkg = new BackgroundWorker();

        labelCryptionType.Text = Resources.labelProcessNameDeleteCompletely;  // Deleting Completely...
        labelProgress.Text = Resources.labellabelCompletelyDelete;
        pictureBoxProgress.Image = pictureBoxDeleteOn.Image;

        wipe = new Wipe();

        bkg.DoWork += (s, d) =>
          wipe.WipeFile(s, d, FileList, AppSettings.Instance.DelRandNum, AppSettings.Instance.DelZeroNum);

        bkg.RunWorkerCompleted += backgroundWorker_Wipe_RunWorkerCompleted;
        bkg.ProgressChanged += backgroundWorker_ProgressChanged;
        bkg.WorkerReportsProgress = true;
        bkg.WorkerSupportsCancellation = true;

        bkg.RunWorkerAsync();
      }

      return (true);

    }

    #endregion

    //----------------------------------------------------------------------
    /// <summary>
    /// 上書きの確認をするダイアログ表示とユーザー応答内容の受け渡し
    /// Show dialog for confirming to overwrite, and passing user command. 
    /// </summary>
    System.Threading.ManualResetEvent _busy = new System.Threading.ManualResetEvent(false);
    private void DialogMessageForOverWrite(int FileType, string FilePath)
    {
      if (decryption2 == null)
      {
        // Not confirm
        if (AppSettings.Instance.fDecryptConfirmOverwrite == false)
        {
          decryption3.TempOverWriteOption = 2;
          return;
        }
        if (decryption3.TempOverWriteOption == 2)
        {  // Overwrite all
          return;
        }
      }
      else
      {
        if (AppSettings.Instance.fDecryptConfirmOverwrite == false)
        {
          decryption2.TempOverWriteOption = 2;
          return;
        }
        if (decryption2.TempOverWriteOption == 2)
        {
          return;
        }
      }

      if (!bkg.IsBusy)
      {
        bkg.RunWorkerAsync();
        // Unblock the worker 
        _busy.Set();
      }

      // Show dialog for confirming to orverwrite
      Form4 frm4;
      if (FileType == 0)
      {
        frm4 = new Form4("ComfirmToOverwriteDir", Resources.labelComfirmToOverwriteDir + Environment.NewLine + FilePath);
      }
      else
      {
        frm4 = new Form4("ComfirmToOverwriteFile", Resources.labelComfirmToOverwriteFile + Environment.NewLine + FilePath);
      }

      frm4.ShowDialog();

      if (decryption2 == null)
      {
        decryption3.TempOverWriteOption = frm4.OverWriteOption;
        decryption3.TempOverWriteForNewDate = frm4.OverWriteForNewDate;
      }
      else
      {
        decryption2.TempOverWriteOption = frm4.OverWriteOption;
        decryption2.TempOverWriteForNewDate = frm4.OverWriteForNewDate;
      }

      frm4.Dispose();

      _busy.Reset();

    }


    /// <summary>
    /// 指定したルートディレクトリのファイルリストを並列処理で取得する
    /// Get a list of files specified root directory in parallel processing
    /// </summary>
    /// <remarks>http://stackoverflow.com/questions/2106877/is-there-a-faster-way-than-this-to-find-all-the-files-in-a-directory-and-all-sub</remarks>
    /// <param name="fileSearchPattern"></param>
    /// <param name="rootFolderPath"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
    {
      Queue<string> pending = new Queue<string>();
      pending.Enqueue(rootFolderPath);
      string[] tmp;
      while (pending.Count > 0)
      {
        rootFolderPath = pending.Dequeue();
        tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
        for (int i = 0; i < tmp.Length; i++)
        {
          yield return tmp[i];
        }
        tmp = Directory.GetDirectories(rootFolderPath);
        for (int i = 0; i < tmp.Length; i++)
        {
          pending.Enqueue(tmp[i]);
        }
      }
    }

    /// <summary>
    /// パスワードファイルとして、ファイルからSHA-1ハッシュを取得してバイト列にする
    /// Get a string of the SHA-1 hash from a file such as the password file
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private byte[] GetPasswordFileHash2(string FilePath)
    {
      byte[] buffer = new byte[255];
      byte[] result = new byte[32];
      //byte[] header = new byte[12];

      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        //SHA1CryptoServiceProviderオブジェクト
        using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
        {
          byte[] array_bytes = sha1.ComputeHash(fs);
          for (int i = 0; i < 20; i++)
          {
            result[i] = array_bytes[i];
          }
        }

        fs.Seek(0, SeekOrigin.Begin);
        while (fs.Read(buffer, 0, 255) > 0)
        {
          // 最後の255バイトを取得しようとしたデータから残り12bytesのパスワードを埋める
          // Fill the rest data with trying to get the last 255 bytes.
        }

        for (int i = 0; i < 12; i++)
        {
          result[20 + i] = buffer[i];
        }

      }
      //string text = System.Text.Encoding.ASCII.GetString(result);
      return (result);

    }

    /// <summary>
    /// パスワードファイルとして、ファイルからSHA-256ハッシュを取得してバイト列にする
    /// Get a string of the SHA-256 hash from a file such as the password file
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private byte[] GetPasswordFileHash3(string FilePath)
    {
      byte[] buffer = new byte[255];
      byte[] result = new byte[32];

      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        //SHA1CryptoServiceProviderオブジェクト
        using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
        {
          byte[] array_bytes = sha256.ComputeHash(fs);
          for (int i = 0; i < 32; i++)
          {
            result[i] = array_bytes[i];
          }
        }
      }
      //string text = System.Text.Encoding.ASCII.GetString(result);
      return (result);

    }

    //======================================================================
    // Backgroundworker
    //======================================================================
    #region

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

      ArrayList MessageList = (ArrayList)e.UserState;

      if (e.ProgressPercentage > 0)
      {
        progressBar.Style = ProgressBarStyle.Continuous;
        progressBar.Value = e.ProgressPercentage;
        labelProgressPercentText.Text = ((float)e.ProgressPercentage / 100).ToString("F") + "%";
      }
      else
      {
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.Value = 0;
      }
      /*
      private const int ENCRYPT_SUCCEEDED = 1; // Encrypt is succeeded.
      private const int DECRYPT_SUCCEEDED = 2; // Decrypt is succeeded.
      private const int DELETE_SUCCEEDED  = 3; // Delete is succeeded.
      private const int READY_FOR_ENCRYPT = 4; // Getting ready for encryption or decryption.
      private const int READY_FOR_DECRYPT = 5; // Getting ready for encryption or decryption.
      private const int ENCRYPTING        = 6; // Ecrypting.
      private const int DECRYPTING        = 7; // Decrypting.
      private const int DELETING          = 8; // Deleting.
      */
      switch ((int)MessageList[0])
      {
        case READY_FOR_ENCRYPT:
          labelCryptionType.Text = Resources.labelGettingReadyForEncryption;
          break;

        case READY_FOR_DECRYPT:
          labelCryptionType.Text = Resources.labelGettingReadyForDecryption;
          break;

        case ENCRYPTING:
          labelCryptionType.Text = Resources.labelProcessNameEncrypt;
          break;

        case DECRYPTING:
          labelCryptionType.Text = Resources.labelProcessNameDecrypt;
          break;

        case DELETING:
          labelCryptionType.Text = Resources.labelProcessNameDelete;
          break;
      }

      notifyIcon1.Text = labelProgressPercentText.Text;
      labelProgressMessageText.Text = (string)MessageList[1];

      this.Update();
     
    }

    private void backgroundWorker_Encryption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

      buttonCancel.Text = Resources.ButtonTextOK;

      if (e.Cancelled)
      {
        // Canceled
        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        labelCryptionType.Text = "";
        notifyIcon1.Text = "- % " + Resources.labelCaptionCanceled;
        AppSettings.Instance.FileList = null;

        // 暗号化の処理はキャンセルされました。
        // Encryption was canceled.
        labelProgressMessageText.Text = Resources.labelEncryptionCanceled;
        return;

      }
      else if (e.Error != null)
      {
        //e.Error.Message;
        labelProgressPercentText.Text = "- %";
        labelProgressMessageText.Text = e.Error.Message;
        progressBar.Value = 0;
        labelProgressMessageText.Text = Resources.labelCaptionError;     // "Error occurred"
        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        return;

      }
      else
      {
        /*
        // Status code
        private const int ENCRYPT_SUCCEEDED   = 1; // Encrypt is succeeded.
        private const int DECRYPT_SUCCEEDED   = 2; // Decrypt is succeeded.
        private const int DELETE_SUCCEEDED    = 3; // Delete is succeeded.
        private const int HEADER_DATA_READING = 4; // Header data is reading.
        private const int ENCRYPTING          = 5; // Ecrypting.
        private const int DECRYPTING          = 6; // Decrypting.
        private const int DELETING            = 7; // Deleting.

        // Error code
        private const int ERROR_UNEXPECTED         = -100;
        private const int NOT_ATC_DATA             = -101;
        private const int ATC_BROKEN_DATA          = -102;
        private const int NO_DISK_SPACE            = -103;
        private const int FILE_INDEX_NOT_FOUND     = -104;
        private const int PASSWORD_TOKEN_NOT_FOUND = -105;
        */

        switch ((int)e.Result)
        {
          case ENCRYPT_SUCCEEDED:

            labelProgressPercentText.Text = "100%";
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = progressBar.Maximum;
            labelCryptionType.Text = "";
            labelProgressMessageText.Text = Resources.labelCaptionCompleted;  // "Completed"
            notifyIcon1.Text = "100% " + Resources.labelCaptionCompleted;

            // Delete file or directories
            if (AppSettings.Instance.fDelOrgFile == true || checkBoxReDeleteOriginalFileAfterEncryption.Checked == true)
            {
              if (AppSettings.Instance.fConfirmToDeleteAfterEncryption == true)
              {
                // 問い合わせ
                // 暗号化ファイルの元となったファイル及びフォルダーを削除しますか？
                //
                // Question
                // Are you sure to delete the files and folders that are the source of the encrypted file?
                DialogResult ret = MessageBox.Show(Resources.DialogMessageDeleteOriginalFilesAndFolders,
                  Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ret == DialogResult.Yes)
                {
                  buttonCancel.Text = Resources.ButtonTextCancel;
                  if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ERROR ||
                      AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE )
                  {
                    DeleteData(encryption3.FileList);
                  }
                }
              }
            }

            AppSettings.Instance.FileList = null;

            if (AppSettings.Instance.fEndToExit == true)
            {
              Application.Exit();
            }

            return;

          //-----------------------------------
          case NO_DISK_SPACE:
            // エラー
            // ドライブに空き容量がありません。処理を中止します。
            //
            // Alert
            // No free space on the disk. The process is aborted.
            MessageBox.Show(Resources.DialogMessageNoDiskSpace,
            Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;


        }// end switch();

        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        labelCryptionType.Text = "";
        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        AppSettings.Instance.FileList = null;

      }

    }

    private void backgroundWorker_Decryption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

      buttonCancel.Text = Resources.ButtonTextOK;

      if (e.Cancelled)
      {
        // Canceled
        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        labelCryptionType.Text = "";
        notifyIcon1.Text = "- % " + Resources.labelCaptionCanceled;
        AppSettings.Instance.FileList = null;

        // 復号処理はキャンセルされました。
        // Decryption was canceled.
        labelProgressMessageText.Text = Resources.labelDecyptionCanceled;
        return;

      }
      else if (e.Error != null)
      {
        //e.Error.Message;
        labelProgressPercentText.Text = "- %";
        labelProgressMessageText.Text = e.Error.Message;
        progressBar.Value = 0;
        labelProgressMessageText.Text = Resources.labelCaptionError;     // "Error occurred"
        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        return;
      }
      else
      {
        /*
        // Status code
        private const int ENCRYPT_SUCCEEDED   = 1; // Encrypt is succeeded.
        private const int DECRYPT_SUCCEEDED   = 2; // Decrypt is succeeded.
        private const int DELETE_SUCCEEDED    = 3; // Delete is succeeded.
        private const int HEADER_DATA_READING = 4; // Header data is reading.
        private const int ENCRYPTING          = 5; // Ecrypting.
        private const int DECRYPTING          = 6; // Decrypting.
        private const int DELETING            = 7; // Deleting.

        // Error code
        private const int ERROR_UNEXPECTED         = -100;
        private const int NOT_ATC_DATA             = -101;
        private const int ATC_BROKEN_DATA          = -102;
        private const int NO_DISK_SPACE            = -103;
        private const int FILE_INDEX_NOT_FOUND     = -104;
        private const int PASSWORD_TOKEN_NOT_FOUND = -105;
        private const int NOT_CORRECT_HASH_VALUE   = -106;
        */

        FileDecryptReturnVal result = (FileDecryptReturnVal)e.Result;
        bool fCancel = false;
        switch (result.ReturnCode)
        {
          case DECRYPT_SUCCEEDED:

            labelProgressPercentText.Text = "100%";
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = progressBar.Maximum;
            labelCryptionType.Text = "";
            labelProgressMessageText.Text = Resources.labelCaptionCompleted;  // "Completed"
            notifyIcon1.Text = "100% " + Resources.labelCaptionCompleted;

            if (AppSettings.Instance.fOpenFile == true)
            {
              List<string> OutputFileList = new List<string>();
              if (decryption2 == null)
              {
                OutputFileList = decryption3.OutputFileList;
              }
              else
              {
                OutputFileList = decryption2.OutputFileList;
              }

              if (OutputFileList.Count() > AppSettings.Instance.ShowDialogWhenMultipleFilesNum)
              {
                // 問い合わせ
                // 復号したファイルが○個以上あります。
                // それでもすべてのファイルを関連付けられたアプリケーションで開きますか？
                //
                // Question
                // There decrypted file is * or more.
                // But, open all of the files associated with application?
                DialogResult ret = MessageBox.Show(string.Format(Resources.DialogMessageOpenMultipleFiles, AppSettings.Instance.ShowDialogWhenMultipleFilesNum),
                Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ret == DialogResult.No)
                {
                  return;
                }
              }

              foreach (string path in OutputFileList)
              {
                if (Path.GetExtension(path).ToLower() == ".exe" || Path.GetExtension(path).ToLower() == ".bat")
                {
                  if (AppSettings.Instance.fShowDialogWhenExeFile == true)
                  {
                    // 問い合わせ
                    // 復号したファイルに実行ファイルが含まれています。以下のファイルを実行しますか？
                    //
                    // Question
                    // It contains the executable files in the decrypted file.
                    // Do you run the following file?
                    DialogResult ret = MessageBox.Show(Resources.DialogMessageExecutableFile + Environment.NewLine + path,
                    Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (ret == DialogResult.No)
                    {
                      continue;
                    }
                    else
                    { // Executable
                      System.Diagnostics.Process p = System.Diagnostics.Process.Start(path);
                    }
                  }
                }
                else if (File.Exists(path) == true)
                {
                  System.Diagnostics.Process p = System.Diagnostics.Process.Start(path);
                }
                else if (Directory.Exists(path) == true)
                {
                  // Open the folder by Explorer
                  System.Diagnostics.Process.Start("EXPLORER.EXE", path);
                }

              }// end foreach;

              // Set the timestamp of files or directories to decryption time.
              if (AppSettings.Instance.fSameTimeStamp == true)
              {
                OutputFileList.ForEach(delegate (String FilePath)
                {
                  DateTime dtNow = DateTime.Now;
                  File.SetCreationTime(FilePath, dtNow);
                  File.SetLastWriteTime(FilePath, dtNow);
                  File.SetLastAccessTime(FilePath, dtNow);
                });
              }

            }// end if (AppSettings.Instance.fOpenFile == true);


            // Delete file or directories
            if (AppSettings.Instance.fDelEncFile == true || checkBoxDeleteAtcFileAfterDecryption.Checked == true)
            {
              if (AppSettings.Instance.fConfirmToDeleteAfterDecryption == true)
              {
                // 問い合わせ
                // 復号の元になった暗号化ファイルを削除しますか？
                //
                // Question
                // Are you sure to delete the encypted file(s) that are the source of the decryption?
                DialogResult ret = MessageBox.Show(Resources.DialogMessageDeleteEncryptedFiles,
                  Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == DialogResult.Yes)
                {
                  buttonCancel.Text = Resources.ButtonTextCancel;
                  List<string> FilePaths = new List<string>();
                  if(decryption2 == null)
                  {
                    FilePaths.Add(decryption3.AtcFilePath);
                  }
                  else
                  {
                    FilePaths.Add(decryption2.AtcFilePath);
                  }
                  DeleteData(FilePaths);
                }
              }
            }

            if (AppSettings.Instance.fEndToExit == true)
            {
              Application.Exit();
            }

            return;

          //-----------------------------------
          case ERROR_UNEXPECTED:
            // エラー
            // 予期せぬエラーが発生しました。処理を中止します。
            //
            // Error
            // An unexpected error has occurred. And stops processing.
            MessageBox.Show(Resources.DialogMessageUnexpectedError,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case NOT_ATC_DATA:
            // エラー
            // 暗号化ファイルではありません。処理を中止します。
            //
            // Error
            // The file is not encrypted file. The process is aborted.
            MessageBox.Show(Resources.DialogMessageNotAtcFile + Environment.NewLine + result.FilePath,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case ATC_BROKEN_DATA:
            // エラー
            // 暗号化ファイル(.atc)は壊れています。処理を中止します。
            //
            // Error
            // Encrypted file ( atc ) is broken. The process is aborted.
            MessageBox.Show(Resources.DialogMessageAtcFileBroken + Environment.NewLine + result.FilePath,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case NO_DISK_SPACE:
            // 警告
            // ドライブに空き容量がありません。処理を中止します。
            //
            // Alert
            // No free space on the disk. The process is aborted.
            MessageBox.Show(Resources.DialogMessageNoDiskSpace,
            Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case FILE_INDEX_NOT_FOUND:
            // エラー
            // 暗号化ファイル内部で、不正なファイルインデックスがありました。
            //
            // Error
            // Internal file index is invalid in encrypted file.
            MessageBox.Show(Resources.DialogMessageFileIndexInvalid,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case NOT_CORRECT_HASH_VALUE:
            // エラー
            // ファイルのハッシュ値が異なります。ファイルが壊れたか、改ざんされた可能性があります。
            // 処理を中止します。
            //
            // Error
            // The file is not the same hash value. Whether the file is corrupted, it may have been made the falsification.
            // The process is aborted.
            MessageBox.Show(Resources.DialogMessageNotSameHash + Environment.NewLine + result.FilePath,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case PASSWORD_TOKEN_NOT_FOUND:
            // エラー
            // パスワードがちがうか、ファイルが破損している可能性があります。
            // 復号できませんでした。
            //
            // Error
            // Password is invalid, or the encrypted file might have been broken.
            // Decryption is aborted.
            MessageBox.Show(Resources.DialogMessageDecryptionError,
            Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (LimitOfInputPassword > 1)
            {
              LimitOfInputPassword--;
              panelStartPage.Visible = false;
              panelEncrypt.Visible = false;
              panelEncryptConfirm.Visible = false;
              panelDecrypt.Visible = true;
              panelProgressState.Visible = false;
              textBoxDecryptPassword.Focus();
              textBoxDecryptPassword.SelectAll();
              return;
            }
            else
            {
              // パスワード回数を超過
              // Exceed times limit of inputting password

              if (AppSettings.Instance.fBroken == true)
              {
                // ファイル破壊を行うか
                // Whether breaking the files
                foreach (string FilePath in AppSettings.Instance.FileList)
                {
                  BreakTheFile(FilePath);
                }
              }
              // スタートページへ戻る
              // Back to Start page.
              panelStartPage.Visible = true;
              panelEncrypt.Visible = false;
              panelEncryptConfirm.Visible = false;
              panelDecrypt.Visible = false;
              panelProgressState.Visible = false;
              textBoxDecryptPassword.Focus();
              textBoxDecryptPassword.SelectAll();
              return;
            }
            break;

          default:
            // ユーザーキャンセル
            fCancel = true;
            break;
        }

        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        progressBar.Style = ProgressBarStyle.Continuous;

        if(fCancel == true)
        {
          labelCryptionType.Text = Resources.labelDecyptionCanceled;
        }
        else
        {
          labelCryptionType.Text = Resources.labelCaptionError;
        }

        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        AppSettings.Instance.FileList = null;

      }
    }

    private void backgroundWorker_Wipe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

      buttonCancel.Text = Resources.ButtonTextOK;

      if (e.Cancelled)
      {
        // Canceled
        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        labelCryptionType.Text = "";
        notifyIcon1.Text = "- % " + Resources.labelCaptionCanceled;
        AppSettings.Instance.FileList = null;

        // ファイル、またはフォルダーの完全削除がキャンセルされました。
        // Complete deleting files or folder has been canceled.
        labelProgressMessageText.Text = Resources.labelCompleteDeleteFileCanceled;
        return;

      }
      else if (e.Error != null)
      {
        //e.Error.Message;
        labelProgressPercentText.Text = "- %";
        labelProgressMessageText.Text = e.Error.Message;
        progressBar.Value = 0;
        labelProgressMessageText.Text = Resources.labelCaptionError;     // "Error occurred"
        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        return;

      }
      else
      {
        switch ((int)e.Result)
        {
          case DELETE_SUCCEEDED:
          
            // The operation completed normally. 
            labelCryptionType.Text = "";
            // ファイル、またはフォルダーの完全削除は正常に完了しました。
            // Files or folders complete deleting is completed normally.
            labelProgressMessageText.Text = Resources.labelCompleteDeletingCompleted;
            labelProgressPercentText.Text = "100%";
            progressBar.Value = progressBar.Maximum;  // 100%
            return;

          //-----------------------------------
          case ERROR_UNEXPECTED:
            // エラー
            // 予期せぬエラーが発生しました。処理を中止します。
            //
            // Alert
            // An unexpected error has occurred. And stops processing.
            MessageBox.Show(Resources.DialogMessageUnexpectedError,
            Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          //-----------------------------------
          case NO_DISK_SPACE:
            // エラー
            // ドライブに空き容量がありません。処理を中止します。
            //
            // Alert
            // No free space on the disk. The process is aborted.
            MessageBox.Show(Resources.DialogMessageNoDiskSpace,
            Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            break;

          default:
            break;

        }

        labelProgressPercentText.Text = "- %";
        progressBar.Value = 0;
        labelCryptionType.Text = "";
        notifyIcon1.Text = "- % " + Resources.labelCaptionError;
        AppSettings.Instance.FileList = null;

      }

    }

#endregion

    //======================================================================
    // メニューアイテム
    //======================================================================
#region

    private void ToolStripMenuItemFile_DropDownOpened(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        ToolStripMenuItemEncrypt.Enabled = true;
        ToolStripMenuItemDecrypt.Enabled = true;
      }
      else
      {
        ToolStripMenuItemEncrypt.Enabled = false;
        ToolStripMenuItemDecrypt.Enabled = false;
      }
    }

    private void ToolStripMenuItemEncryptSelectFiles_Click(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        AppSettings.Instance.FileList = null;
        openFileDialog1.Title = Resources.DialogTitleEncryptSelectFiles;
        openFileDialog1.Filter = Resources.OpenDialogFilterAllFiles;
        openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
        openFileDialog1.Multiselect = true;
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
          foreach (string filename in openFileDialog1.FileNames)
          {
            AppSettings.Instance.FileList.Add(filename);

          }
          // Check memorized password
          if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
          {
            textBoxPassword.Text = textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;
          }

          // Encrypt by memorized password without confirming
          if (AppSettings.Instance.fMemPasswordExe == true)
          {
            buttonEncryptStart.PerformClick();
          }
          else
          {
            panelStartPage.Visible = false;
            panelEncrypt.Visible = true;        // Encrypt
            panelEncryptConfirm.Visible = false;
            panelDecrypt.Visible = false;
            panelProgressState.Visible = false;
          }
        }
      }
    }

    private void ToolStripMenuItemEncryptSelectFolder_Click(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        AppSettings.Instance.FileList = null;
        folderBrowserDialog1.Description = Resources.DialogTitleEncryptSelectFolder;
        folderBrowserDialog1.SelectedPath = AppSettings.Instance.InitDirPath;
        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
          AppSettings.Instance.FileList.Add(folderBrowserDialog1.SelectedPath);
        }
        else{
          return;
        }

        // Check memorized password
        if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
        {
          textBoxPassword.Text = textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;
        }
          
        // Encrypt by memorized password without confirming
        if (AppSettings.Instance.fMemPasswordExe == true)
        {
          buttonEncryptStart.PerformClick();
        }
        else
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = true;        // Encrypt
          panelEncryptConfirm.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
        }
      }
    }

    private void ToolStripMenuItemDecrypt_Click(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        AppSettings.Instance.FileList = null;
        openFileDialog1.Title = Resources.DialogTitleEncryptSelectFiles;
        openFileDialog1.Filter = Resources.SaveDialogFilterAtcFiles;
        openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
        openFileDialog1.Multiselect = true;
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
          foreach (string filname in openFileDialog1.FileNames) 
          {
            if (AppSettings.Instance.FileList == null)
            {
              AppSettings.Instance.FileList = new List<string>();
            }
            AppSettings.Instance.FileList.Add(filname);
          }

          // Check memorized password
          if (AppSettings.Instance.fMyDecryptPasswordKeep == true)
          {
            textBoxDecryptPassword.Text = AppSettings.Instance.MyDecryptPasswordString;
          }
            
          // Encrypt by memorized password without confirming
          if (AppSettings.Instance.fMemPasswordExe)
          {
            buttonDecryptStart.PerformClick();
          }
          else
          {
            panelStartPage.Visible = false;
            panelEncrypt.Visible = false;
            panelEncryptConfirm.Visible = false;
            panelDecrypt.Visible = true;    //Decrypt
            panelProgressState.Visible = false;
          }
        }

      }
    }

    private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void ToolStripMenuItemOption_DropDownOpened(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        ToolStripMenuItemSetting.Enabled = true;
      }
      else
      {
        ToolStripMenuItemSetting.Enabled = false;
      }
    }

    private void ToolStripMenuItemSetting_Click(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        // Show Option form
        Form3 frm3 = new Form3();
        frm3.ShowDialog();
        frm3.Dispose();

        pictureBoxAtc.Image = pictureBoxAtcOff.Image;
        pictureBoxExe.Image = pictureBoxExeOff.Image;
        pictureBoxZip.Image = pictureBoxZipOff.Image;
        pictureBoxDec.Image = pictureBoxDecOff.Image;

        switch (AppSettings.Instance.EncryptionFileType)
        {
          case FILE_TYPE_ATC:
            pictureBoxAtc.Image = pictureBoxAtcOn.Image;
            break;
          case FILE_TYPE_ATC_EXE:
            pictureBoxExe.Image = pictureBoxExeOn.Image;
            break;
          case FILE_TYPE_PASSWORD_ZIP:
            pictureBoxZip.Image = pictureBoxZipOn.Image;
            break;
          default:
            break;
        }
        
      }
    }

    private void ToolStripMenuItemHelpContents_Click(object sender, EventArgs e)
    {
      // Open 'Online Help' in web browser.
      System.Diagnostics.Process.Start("https://hibara.org/software/attachecase/help/");
    }

    private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
    {
      // Show AttacheCase's information
      Form2 frm2 = new Form2();
      frm2.ShowDialog();
      frm2.Dispose();
    }

#endregion

    //======================================================================
    // 各ウィンドウページが表示されたときに発生するイベント
    //======================================================================
#region
    /// <summary>
    /// panelStartPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void panelStartPage_VisibleChanged(object sender, EventArgs e)
    {
      if (panelStartPage.Visible == true)
      {
        AppSettings.Instance.FileList = null; // Clear file list
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_NONE;
        this.AllowDrop = true;

        toolStripButtonEncryptSelectFiles.Enabled = true;
        toolStripButtonEncryptSelectFolder.Enabled = true;
        toolStripButtonDecryptSelectAtcFiles.Enabled = true;
        toolStripButtonOption.Enabled = true;

        this.AcceptButton = null;
        this.CancelButton = buttonExit;

        // File type for encryption. 
        int FileType = 0;
        if (AppSettings.Instance.SameEncryptionFileTypeAlways > 0)
        {
          FileType = AppSettings.Instance.SameEncryptionFileTypeAlways;
        }
        else if(AppSettings.Instance.SameEncryptionFileTypeBefore > 0)
        {
          FileType = AppSettings.Instance.SameEncryptionFileTypeBefore;
        }
        else
        {
          FileType = AppSettings.Instance.EncryptionFileType;
        }

        if (FileType == 1)
        {
          pictureBoxAtc_Click(sender, e);
        }
        else if (FileType == 2)
        {
          pictureBoxExe_Click(sender, e);
        }
        else if (FileType == 3)
        {
          pictureBoxZip_Click(sender, e);
        }
        else
        { //FileType == 0
          pictureBoxAtc.Image = pictureBoxAtcOff.Image;
          pictureBoxExe.Image = pictureBoxExeOff.Image;
          pictureBoxZip.Image = pictureBoxZipOff.Image;
          pictureBoxDec.Image = pictureBoxDecOff.Image;
        }

        // TextBoxes
        textBoxPassword.Text = "";
        textBoxPassword.Enabled = true;
        textBoxPassword.BackColor = Color.White;

        textBoxRePassword.Text = "";
        textBoxRePassword.Enabled = true;
        textBoxRePassword.BackColor = Color.White;

        textBoxDecryptPassword.Text = "";
        textBoxDecryptPassword.Enabled = true;
        textBoxDecryptPassword.BackColor = Color.White;

        // Password files
        AppSettings.Instance.TempEncryptionPassFilePath = "";
        AppSettings.Instance.TempDecryptionPassFilePath = "";

      }
    }
     

    /// <summary>
    /// panelEncrypt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void panelEncrypt_VisibleChanged(object sender, EventArgs e)
    {
      if (panelEncrypt.Visible == true)
      {
        toolStripButtonEncryptSelectFiles.Enabled = false;
        toolStripButtonEncryptSelectFolder.Enabled = false;
        toolStripButtonDecryptSelectAtcFiles.Enabled = false;
        toolStripButtonOption.Enabled = false;

        // Not mask password character
        // 「*」で隠さずパスワードを確認しながら入力する
        if (AppSettings.Instance.fNoHidePassword == true)
        {
          checkBoxNotMaskEncryptedPassword.Checked = true;
        }
        else
        {
          checkBoxNotMaskDecryptedPassword.Checked = false;
        }
          
        // Encryption will be the same file type always.
        // 常に同じ暗号化ファイルの種類にする
        if (AppSettings.Instance.EncryptionFileType == 0 && AppSettings.Instance.SameEncryptionFileTypeAlways > 0)
        {
          AppSettings.Instance.EncryptionFileType = AppSettings.Instance.SameEncryptionFileTypeAlways;
        }
        // Save same encryption type that was used to before.
        // 前に使った暗号化ファイルの種類にする
        else if (AppSettings.Instance.EncryptionFileType == 0 && AppSettings.Instance.SameEncryptionFileTypeBefore > 0)
        {
          AppSettings.Instance.EncryptionFileType = AppSettings.Instance.EncryptionFileType;
        }

        // Select file type
        // labelPasswordValidation.Text = "";
        if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC)
        {
          pictureBoxEncryption.Image = pictureBoxAtcOn.Image;
          labelEncryption.Text = labelAtc.Text;
        }
        else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
        {
          pictureBoxEncryption.Image = pictureBoxExeOn.Image;
          labelEncryption.Text = labelExe.Text;
        }
        else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
        {
          pictureBoxEncryption.Image = pictureBoxZipOn.Image;
          labelEncryption.Text = labelZip.Text;
        }
        else
        {
          pictureBoxEncryption.Image = pictureBoxAtcOn.Image;
          labelEncryption.Text = labelAtc.Text;
        }

        textBoxPassword.Focus();

        //Show the check box in main form window
        if (AppSettings.Instance.fEncryptShowDelChkBox == true)
        {
          checkBoxDeleteOriginalFileAfterEncryption.Visible = true;
          checkBoxReDeleteOriginalFileAfterEncryption.Visible = true;

          if (AppSettings.Instance.fDelOrgFile == true)
          {
            checkBoxDeleteOriginalFileAfterEncryption.Checked = true;
          }
          else
          {
            checkBoxDeleteOriginalFileAfterEncryption.Checked = false;
          }
        }
        else
        {
          checkBoxDeleteOriginalFileAfterEncryption.Visible = false;
          checkBoxReDeleteOriginalFileAfterEncryption.Visible = false;
        }

        // Allow Drag and Drop 'password file' instead of password
        // パスワードの代わりに「パスワードファイル」のドラッグ＆ドロップを許可する
        if (AppSettings.Instance.fAllowPassFile == true)
        {
          this.AllowDrop = true;
        }
        else
        {
          this.AllowDrop = false;
        }

        // Password file
        if (AppSettings.Instance.fAllowPassFile == true)
        {
          textBoxPassword.AllowDrop = true;
        }
        else
        {
          textBoxPassword.AllowDrop = false;
        }

        if (AppSettings.Instance.fCheckPassFile == true)
        {
          if (File.Exists(AppSettings.Instance.PassFilePath) == true)
          {
            textBoxPassword.Text = AppSettings.Instance.GetSha256HashFromFile(AppSettings.Instance.PassFilePath);
            textBoxRePassword.Text = textBoxPassword.Text;
            panelStartPage.Visible = false;
            panelEncrypt.Visible = false;
            panelEncryptConfirm.Visible = true;
            panelDecrypt.Visible = false;
            panelProgressState.Visible = false;
          }
          else
          {
            if (AppSettings.Instance.fNoErrMsgOnPassFile == false)
            {
              // 注意
              // 動作設定で指定されたパスワードファイルが見つかりません。
              // [FilePath]
              //
              // Alert
              // Password is not found that specified in setting panel.
              // [FilePath]
              DialogResult ret = MessageBox.Show(Resources.DialogMessagePasswordFileNotFound + Environment.NewLine + AppSettings.Instance.PassFilePath,
              Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return;
          }
        }

        // 記憶パスワード（保存されたパスワードファイルより優先される）
        // Memorized password is priority than the saved password file
        if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
        {
          textBoxPassword.Text = AppSettings.Instance.MyEncryptPasswordString;
          textBoxRePassword.Text = AppSettings.Instance.MyEncryptPasswordString;
        }

        // さらにコマンドラインオプションが優先される
        // 
        if (AppSettings.Instance.EncryptPasswordStringFromCommandLine != "")
        {
          textBoxPassword.Text = AppSettings.Instance.EncryptPasswordStringFromCommandLine;
          textBoxRePassword.Text = AppSettings.Instance.EncryptPasswordStringFromCommandLine;
        }

        // 確認せず即座に実行
        // Run immediately without confirming
        if (AppSettings.Instance.fMemPasswordExe == true)
        {
          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
          panelEncryptConfirm.Visible = true;
          buttonEncryptStart.PerformClick();  // Execute to encrypt
        }
        
      }
    }

    /// <summary>
    /// panelEncryptConfirm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void panelEncryptConfirm_VisibleChanged(object sender, EventArgs e)
    {
      if (panelEncryptConfirm.Visible == true)
      {
        pictureBoxEncryptionConfirm.Image = pictureBoxEncryption.Image;
        labelEncryptionConfirm.Text = labelEncryption.Text;
        
        if (textBoxPassword.Text == textBoxRePassword.Text)
        {
          pictureBoxCheckPasswordValidation.Image = pictureBoxValidIcon.Image;
          textBoxRePassword.BackColor = Color.Honeydew;

          if (AppSettings.Instance.fMemPasswordExe == true)
          {
            buttonEncryptStart.PerformClick();
          }
        }
        else
        {
          pictureBoxCheckPasswordValidation.Image = null;
          textBoxRePassword.BackColor = Color.PapayaWhip;
        }

        this.AcceptButton = buttonEncryptStart;
        this.CancelButton = buttonEncryptionConfirmCancel;
        textBoxRePassword.Focus();
        
      }
    }

    /// <summary>
    /// 
    /// 確認パスワード入力テキストボックスのテキスト編集イベント
    /// </summary>
    private void textBoxRePassword_TextChanged(object sender, EventArgs e)
    {
      if (textBoxPassword.Text == textBoxRePassword.Text)
      {
        pictureBoxCheckPasswordValidation.Image = pictureBoxValidIcon.Image;
        textBoxRePassword.BackColor = Color.Honeydew;
      }
      else
      {
        pictureBoxCheckPasswordValidation.Image = pictureBoxInValidIcon.Image;
        textBoxRePassword.BackColor = Color.PapayaWhip;
      }
    }

    /// <summary>
    /// panelDecrtpt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void panelDecrtpt_VisibleChanged(object sender, EventArgs e)
    {
      if (panelDecrypt.Visible == true)
      {
        toolStripButtonEncryptSelectFiles.Enabled = false;
        toolStripButtonEncryptSelectFolder.Enabled = false;
        toolStripButtonDecryptSelectAtcFiles.Enabled = false;
        toolStripButtonOption.Enabled = false;

        this.AcceptButton = buttonDecryptStart;
        this.CancelButton = buttonDecryptCancel;

        switch (AppSettings.Instance.EncryptionFileType)
        {
          case FILE_TYPE_ATC:
          case FILE_TYPE_ATC_EXE:
          case FILE_TYPE_PASSWORD_ZIP:
            break;

          default:  // Unexpected
            //注意
            //想定外のファイルです。復号することができません。
            //
            //Alert
            //Unexpected decrypted files.It stopped the process.
            DialogResult ret = MessageBox.Show(Resources.DialogMessageUnexpectedDecryptedFiles,
            Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        //Show the check box in main form window
        if (AppSettings.Instance.fDecryptShowDelChkBox == true)
        {
          checkBoxDeleteAtcFileAfterDecryption.Visible = true;
        }
        else
        {
          checkBoxDeleteAtcFileAfterDecryption.Visible = false;
        }

        textBoxDecryptPassword.Focus();

        //Allow Drag and Drop file instead of password
        if (AppSettings.Instance.fAllowPassFile == true)
        {
          textBoxDecryptPassword.AllowDrop = true;
        }
        else
        {
          textBoxDecryptPassword.AllowDrop = false;
        }

        if (AppSettings.Instance.fCheckPassFileDecrypt == true)
        {
          if (File.Exists(AppSettings.Instance.PassFilePathDecrypt) == true)
          {
            textBoxDecryptPassword.Text = AppSettings.Instance.GetSha256HashFromFile(AppSettings.Instance.PassFilePath);
          }
          else
          {
            if (AppSettings.Instance.fNoErrMsgOnPassFile == false)
            {
              // 注意
              // 動作設定で指定されたパスワードファイルが見つかりません。
              // [FilePath]
              //
              // Alert
              // Password is not found that specified in setting panel.
              // [FilePath]
              DialogResult ret = MessageBox.Show(Resources.DialogMessagePasswordFileNotFound + Environment.NewLine + AppSettings.Instance.PassFilePathDecrypt,
              Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return;
          }
        }

        // 記憶パスワード（保存されたパスワードファイルより優先される）
        // Memorized password is priority than the saved password file
        if (AppSettings.Instance.fMyDecryptPasswordKeep == true)
        {
          textBoxDecryptPassword.Text = AppSettings.Instance.MyDecryptPasswordString;
        }

        // さらにコマンドラインからのパスワードが優先される
        if (AppSettings.Instance.DecryptPasswordStringFromCommandLine != "")
        {
          textBoxDecryptPassword.Text = AppSettings.Instance.DecryptPasswordStringFromCommandLine;
        }
                                                                                                  
        // 確認せず即座に実行
        // Run immediately without confirming
        if (AppSettings.Instance.fMemPasswordExe == true)
        {
          buttonDecryptStart.PerformClick();  // Execute to decrypt
        }

      }
    }

    private void panelProgressState_VisibleChanged(object sender, EventArgs e)
    {
      if (panelProgressState.Visible == true)
      {
        toolStripButtonEncryptSelectFiles.Enabled = false;
        toolStripButtonEncryptSelectFolder.Enabled = false;
        toolStripButtonDecryptSelectAtcFiles.Enabled = false;
        toolStripButtonOption.Enabled = false;

        this.CancelButton = buttonCancel;

        //labelCryptionType.Text = "";
        labelProgressMessageText.Text = "-";
        labelProgressPercentText.Text = "- %";
        buttonCancel.Text = Resources.ButtonTextCancel;  // Cancel button

      }
    }
#endregion

    //======================================================================
    // Encrypt window ( panelEncrypt )
    //======================================================================
#region Encrypt Window
    /// <summary>
    /// 
    /// 暗号化ウィンドウでのポップアップメニューから各ファイルタイプの選択
    /// </summary>
    private void pictureBoxEncryption_Click(object sender, EventArgs e)
    {
      Point p = pictureBoxEncryption.PointToScreen(pictureBoxEncryption.ClientRectangle.Location);
      this.contextMenuStrip2.Show(p);
    }

    private void ToolStripMenuItemAtcFile_Click(object sender, EventArgs e)
    {
      // Encrypt to ATC file
      AppSettings.Instance.EncryptionFileType = FILE_TYPE_ATC;
      pictureBoxEncryption.Image = pictureBoxAtcOn.Image;
      labelEncryption.Text = labelAtc.Text;
      textBoxPassword.Focus();
    }

    private void ToolStripMenuItemExeFile_Click(object sender, EventArgs e)
    {
      // Encrypt to EXE(ATC) file
      AppSettings.Instance.EncryptionFileType = FILE_TYPE_ATC_EXE;
      pictureBoxEncryption.Image = pictureBoxExeOn.Image;
      labelEncryption.Text = labelExe.Text;
      textBoxPassword.Focus();
    }

    private void ToolStripMenuItemZipPassword_Click(object sender, EventArgs e)
    {
      // Encrypt to Zip file
      AppSettings.Instance.EncryptionFileType = FILE_TYPE_PASSWORD_ZIP;
      pictureBoxEncryption.Image = pictureBoxZipOn.Image;
      labelEncryption.Text = labelZip.Text;
      textBoxPassword.Focus();
    }

    /// <summary>
    /// テキストボックスの入力イベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void textBoxPassword_TextChanged(object sender, EventArgs e)
    {
      if(textBoxPassword.Text == textBoxRePassword.Text)
      {
        pictureBoxCheckPasswordValidation.Image = pictureBoxValidIcon.Image;
        textBoxRePassword.BackColor = Color.Honeydew;
      }
      else
      {
        textBoxRePassword.BackColor = Color.PapayaWhip;
      }

    }

    private void textBoxPassword_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
        textBoxPassword.BackColor = Color.Honeydew;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void textBoxPassword_DragDrop(object sender, DragEventArgs e)
    {
      string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if ( File.Exists(FilePaths[0]) == true)
      {
        AppSettings.Instance.TempEncryptionPassFilePath = FilePaths[0];
        textBoxPassword.Text = AppSettings.BytesToHexString(GetPasswordFileHash3(AppSettings.Instance.TempEncryptionPassFilePath));
        textBoxRePassword.Text = textBoxPassword.Text;
        panelStartPage.Visible = false;
        panelEncrypt.Visible = false;
        panelEncryptConfirm.Visible = true;      // EncryptConfirm
        panelDecrypt.Visible = false;
        panelProgressState.Visible = false;
      }
      else
      {
        // 注意
        // パスワードファイルにフォルダーを使うことはできません。
        //
        // Alert
        // Not use the folder to the password file.
        DialogResult ret = MessageBox.Show(Resources.DialogMessageNotDirectoryInPasswordFile,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      
    }

    private void textBoxPassword_DragLeave(object sender, EventArgs e)
    {
      textBoxPassword.BackColor = SystemColors.Window;
    }

    private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.Handled = true;
        buttonEncryptionPasswordOk_Click(sender, e);
      }
    }

    private void buttonEncryptionPasswordOk_Click(object sender, EventArgs e)
    {
      //-----------------------------------
      // Display enryption confirm window
      //-----------------------------------
      panelStartPage.Visible = false;
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = true;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = false;

    }

    private void buttonEncryptionConfirmCancel_Click(object sender, EventArgs e)
    {
      //-----------------------------------
      // Back and display enryption confirm window
      //-----------------------------------
      panelStartPage.Visible = false;
      panelEncrypt.Visible = true;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = false;
      textBoxRePassword.Text = "";

    }

    /// <summary>
    ///  "Not &mask password character" checkbox click event
    /// 「パスワードをマスクしない」チェックボックスのクリックイベント
    /// </summary>
    private void checkBoxNotMaskEncryptedPassword_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxNotMaskEncryptedPassword.Checked == true)
      {
        checkBoxReNotMaskEncryptedPassword.Checked = true;
        textBoxPassword.PasswordChar = (char)0;
        textBoxPassword.UseSystemPasswordChar = false;
        textBoxRePassword.PasswordChar = (char)0;
        textBoxRePassword.UseSystemPasswordChar = false;
      }
      else
      {
        checkBoxReNotMaskEncryptedPassword.Checked = false;
        textBoxPassword.UseSystemPasswordChar = true;
        textBoxRePassword.UseSystemPasswordChar = true;
      }
    }

    private void checkBoxReNotMaskEncryptedPassword_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxNotMaskEncryptedPassword.Checked == true)
      {
        checkBoxNotMaskEncryptedPassword.Checked = true;
        textBoxPassword.PasswordChar = (char)0;
        textBoxPassword.UseSystemPasswordChar = false;
        textBoxRePassword.PasswordChar = (char)0;
        textBoxRePassword.UseSystemPasswordChar = false;
      }
      else
      {
        checkBoxNotMaskEncryptedPassword.Checked = false;
        textBoxPassword.UseSystemPasswordChar = true;
        textBoxRePassword.UseSystemPasswordChar = true;
      }

    }

    /// <summary>
    /// "&Delete original files or directories after encryption" checkbox click event
    /// 「暗号化完了後に元ファイルを削除する(&D)」クリックイベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void checkBoxDeleteOriginalFileAfterEncryption_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxDeleteOriginalFileAfterEncryption.Checked == true)
      {
        checkBoxReDeleteOriginalFileAfterEncryption.Checked = true;
      }
      else
      {
        checkBoxReDeleteOriginalFileAfterEncryption.Checked = false;
      }

    }

    private void checkBoxReDeleteOriginalFileAfterEncryption_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxReDeleteOriginalFileAfterEncryption.Checked == true)
      {
        checkBoxDeleteOriginalFileAfterEncryption.Checked = true;
      }
      else
      {
        checkBoxDeleteOriginalFileAfterEncryption.Checked = false;
      }

    }

    private void pictureBoxEncryptConfirmBackButton_MouseEnter(object sender, EventArgs e)
    {
      pictureBoxEncryptConfirmBackButton.Image = pictureBoxBackButtonOn.Image;
    }

    private void pictureBoxEncryptConfirmBackButton_MouseLeave(object sender, EventArgs e)
    {
      pictureBoxEncryptConfirmBackButton.Image = pictureBoxBackButtonOff.Image;
    }

    //======================================================================
    /// <summary>
    /// 
    /// 「panelEncrypt」- 暗号化「実行」のボタンが押されたイベント
    /// </summary>
    //======================================================================
    private void buttonEncryptStart_Click(object sender, EventArgs e)
    {
      //Not mask password character
      AppSettings.Instance.fNotMaskPassword = checkBoxNotMaskEncryptedPassword.Checked ? true : false;

      //-----------------------------------
      // Password in TextBox
      //-----------------------------------
      if (textBoxPassword.Text != textBoxRePassword.Text)
      {
        // Invalid mkark
        // pictureBoxCheckPasswordValidation.Image = pictureBoxInValidIcon.Image;
        // labelPasswordValidation.Text = Resources.labelCaptionPasswordInvalid;
        // 注意
        // ２つのパスワードが一致しません。入力し直してください。
        //
        // Alert
        // Two Passwords do not match, it is invalid.
        // Input them again.
        DialogResult ret = MessageBox.Show(Resources.DialogMessagePasswordsNotMatch,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        if (ret == DialogResult.OK)
        {
          textBoxPassword.Focus();
          textBoxPassword.SelectAll();
        }

        return;
      }

      //===================================
      // OK → 暗号化の処理へ
      //===================================

      // Valid mark
      pictureBoxCheckPasswordValidation.Image = pictureBoxValidIcon.Image;
      //labelPasswordValidation.Text = Resources.labelCaptionPasswordValid;

      //-----------------------------------
      // Create one encrypted file from files
      //-----------------------------------
      saveFileDialog1.FileName = "";
      if (AppSettings.Instance.fAllFilePack == true)
      {
        saveFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
          AppSettings.Instance.InitDirPath = Path.GetDirectoryName(saveFileDialog1.FileName);
        }
        else
        {
          return;
        }
      }

      //-----------------------------------
      // Directory to oputput encrypted files
      //-----------------------------------
      //string OutDirPath = Path.GetDirectoryName(AppSettings.Instance.FileList[0]);  // default
      string OutDirPath = "";
      foreach (string path in AppSettings.Instance.FileList)
      {
        if (Directory.Exists(Path.GetDirectoryName(path)) == true)
        {
          OutDirPath = Path.GetDirectoryName(path);
        }
      }

      if (AppSettings.Instance.fSaveToSameFldr == true)
      {
        OutDirPath = AppSettings.Instance.SaveToSameFldrPath;
      }

      if (Directory.Exists(OutDirPath) == false)
      {
        // 注意
        // 保存先のフォルダーが見つかりません！　処理を中止します。
        //
        // Alert
        // The folder to save is not found! Process is aborted.
        DialogResult ret = MessageBox.Show(Resources.DialogMessageDirectoryNotFount + Environment.NewLine + OutDirPath,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
 
      //-----------------------------------
      // Encrypted files camouflage with extension
      //-----------------------------------
      string Extension = "";
      if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC || AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE)
      {
        Extension = AppSettings.Instance.fAddCamoExt == true ? AppSettings.Instance.CamoExt : ".atc";
      }
      else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
      {
        Extension = AppSettings.Instance.fAddCamoExt == true ? AppSettings.Instance.CamoExt : ".exe";
      }
      else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
      {
        Extension = AppSettings.Instance.fAddCamoExt == true ? AppSettings.Instance.CamoExt : ".zip";
      }

      //-----------------------------------
      // Encryption password
      //-----------------------------------
      string EncryptionPassword = "";

      if (AppSettings.Instance.fMyEncryptPasswordKeep == true)
      {
        EncryptionPassword = AppSettings.Instance.MyEncryptPasswordString;
      }
      else
      {
        EncryptionPassword = textBoxRePassword.Text;
      }

      //-----------------------------------
      // Password file
      //-----------------------------------
      // ※パスワードファイルは、記憶パスワードや通常の入力されたパスワードよりも優先される。
      // * This password files is priority than memorized encryption password and inputting normal password string.
      byte[] EncryptionPasswordBinary = null;
      if (AppSettings.Instance.fAllowPassFile == true && AppSettings.Instance.EncryptionFileType != FILE_TYPE_PASSWORD_ZIP)  // ATC(EXE) only
      {
        // Check specified password file for Decryption
        if (AppSettings.Instance.fCheckPassFile == true)
        {
          if (File.Exists(AppSettings.Instance.PassFilePath) == true)
          {
            EncryptionPasswordBinary = GetPasswordFileHash3(AppSettings.Instance.PassFilePath);
          }
          else
          {
            if (AppSettings.Instance.fNoErrMsgOnPassFile == false)
            {
              // エラー
              // 暗号化時に指定されたパスワードファイルが見つかりません。
              //
              // Error
              // The specified password file is not found in encryption.
              DialogResult ret = MessageBox.Show(
                Resources.DialogMessageEncryptionPasswordFileNotFound + Environment.NewLine + AppSettings.Instance.PassFilePath,
                Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return;
          }
        }

        // Drag & Drop Password file
        if (File.Exists(AppSettings.Instance.TempEncryptionPassFilePath) == true)
        {
          EncryptionPasswordBinary = GetPasswordFileHash3(AppSettings.Instance.TempEncryptionPassFilePath);
        }
      }

      //-----------------------------------
      // Always minimize when running
      //-----------------------------------
      if (AppSettings.Instance.fMainWindowMinimize == true)
      {
        this.WindowState = FormWindowState.Minimized;
      }

      //-----------------------------------
      // Minimizing a window without displaying in the taskbar
      //-----------------------------------
      if (AppSettings.Instance.fTaskBarHide == true)
      {
        this.Hide();
      }

      //-----------------------------------
      // Display in the task tray
      //-----------------------------------
      if (AppSettings.Instance.fTaskTrayIcon == true)
      {
        notifyIcon1.Visible = true;
      }
      else
      {
        notifyIcon1.Visible = false;
      }

      //-----------------------------------
      // Save same encryption type that was used to before.
      //-----------------------------------
      AppSettings.Instance.SameEncryptionFileTypeBefore = AppSettings.Instance.EncryptionFileType;

      //-----------------------------------
      // Display progress window
      //-----------------------------------
      panelStartPage.Visible = false;
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = true;

      labelCryptionType.Text = Resources.labelProcessNameEncrypt;

      string OutputDirPath = "";

      if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE || AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC)
      {
        pictureBoxProgress.Image = pictureBoxAtcOn.Image;
        labelProgress.Text = labelAtc.Text;

        // Save to the same directory?
        if (AppSettings.Instance.fSaveToSameFldr == true)
        {
          if (Directory.Exists(AppSettings.Instance.SaveToSameFldrPath) == true)
          {
            OutputDirPath = AppSettings.Instance.SaveToSameFldrPath;
          }
        }

      }
      else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
      {
        pictureBoxProgress.Image = pictureBoxExeOn.Image;
        labelProgress.Text = labelExe.Text;

        // Save to the same directory?
        if (AppSettings.Instance.fSaveToSameFldr == true)
        {
          if (Directory.Exists(AppSettings.Instance.SaveToSameFldrPath) == true)
          {
            OutputDirPath = AppSettings.Instance.SaveToSameFldrPath;
          }
        }
      }
      else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
      {
        pictureBoxProgress.Image = pictureBoxZipOn.Image;
        labelProgress.Text = labelZip.Text;

        // Save the zip file(s) to the same directory?
        if (AppSettings.Instance.fZipToSameFldr == true)
        {
          if (Directory.Exists(AppSettings.Instance.ZipToSameFldrPath) == true)
          {
            OutputDirPath = AppSettings.Instance.ZipToSameFldrPath;
          }
        }
      }

      string AtcFilePath = "";
        
      //----------------------------------------------------------------------
      // Create one encrypted file from files
      //----------------------------------------------------------------------
      if (AppSettings.Instance.fAllFilePack == true)
      {
        // 複数ファイルは一つの暗号化ファイルにまとめる

        // 複数のファイルやフォルダをアタッシェケースにドラッグ＆ドロップなどすると、
        // １つの暗号化ファイルとしてまとめます。 このオプションを選択している場合、
        // 暗号化の際には、新しくつけるファイル名を指定します。

        saveFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
        // Input encrypted file name for putting together
        // 一つにまとめる暗号化ファイル名入力
        saveFileDialog1.Title = Resources.DialogTitleAllPackFiles;
        saveFileDialog1.Filter = Resources.SaveDialogFilterAtcFiles;
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
          AtcFilePath = saveFileDialog1.FileName;
          AppSettings.Instance.InitDirPath = Path.GetDirectoryName(saveFileDialog1.FileName);
        }
        else
        { //キャンセル(Cancel)
          panelStartPage.Visible = false;
          panelEncrypt.Visible = false;
          panelEncryptConfirm.Visible = true;
          panelDecrypt.Visible = false;
          panelProgressState.Visible = false;
          return;
        }
          
        int NumberOfFiles = 0;

        if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
        {
          encryption3 = new FileEncrypt3();
          encryption3.NumberOfFiles = 0;
          encryption3.TotalNumberOfFiles = 0;
        }
        else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
        {
          compression = new ZipEncrypt();
          compression.NumberOfFiles = 0;
          compression.TotalNumberOfFiles = 0;
        }

        //-----------------------------------
        //Set number of times to input password in encrypt files
        if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
        {
          char input_limit;
          if (0 < AppSettings.Instance.MissTypeLimitsNum && AppSettings.Instance.MissTypeLimitsNum < 11)
          {
            input_limit = (char)AppSettings.Instance.MissTypeLimitsNum;
          }
          else
          {
            input_limit = (char)3;
          }
          encryption3.MissTypeLimits = input_limit;
        }

        //-----------------------------------
        // Save encryption files to same folder.
        if (AppSettings.Instance.fSaveToSameFldr == false)
        {
          OutputDirPath = Path.GetDirectoryName(AtcFilePath);
        }

        //-----------------------------------
        // Specify the format of the encryption file name
        string FileName = "";
        if (AppSettings.Instance.fAutoName == true)
        {
          //Create encrypted file including extension
          if (AppSettings.Instance.fExtInAtcFileName == true)
          {
            FileName = AppSettings.Instance.getSpecifyFileNameFormat(
              AppSettings.Instance.AutoNameFormatText, AtcFilePath, NumberOfFiles
            );
            FileName = FileName + Extension;
          }
          else
          {
            FileName = Path.GetFileNameWithoutExtension(FileName) + Extension;
          }
        }
        else
        {
          //Create encrypted file including extension
          if (AppSettings.Instance.fExtInAtcFileName == true)
          {
            FileName = Path.GetFileName(AtcFilePath) + Extension;
          }
          else
          {
            FileName = Path.GetFileNameWithoutExtension(AtcFilePath) + Extension;
          }
        }

        AtcFilePath = Path.Combine(OutputDirPath, FileName);

        //-----------------------------------
        //Confirm &overwriting when same file name exists.
        if (AppSettings.Instance.fEncryptConfirmOverwrite == true)
        {
          if (File.Exists(AtcFilePath) == true)
          {
            // 問い合わせ
            // 以下のファイルはすでに存在しています。上書きして保存しますか？
            //
            // Question
            // The following file already exists. Do you overwrite the files to save?
            DialogResult ret = MessageBox.Show(Resources.labelComfirmToOverwriteFile + Environment.NewLine + AtcFilePath,
            Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ret == DialogResult.No)
            {
              panelStartPage.Visible = false;
              panelEncrypt.Visible = true;
              panelEncryptConfirm.Visible = false;
              panelDecrypt.Visible = false;
              panelProgressState.Visible = false;
              return;
            }
          }
        }

        //-----------------------------------
        // Self executable file
        //-----------------------------------
        if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
        {
          encryption3.fExecutable = true;
        }

        //-----------------------------------
        //　Set the timestamp of encryption file to original files or directories
        //-----------------------------------
        encryption3.fKeepTimeStamp = AppSettings.Instance.fKeepTimeStamp;

        //-----------------------------------
        // Encryption start
        //-----------------------------------

        // BackgroundWorker event handler
        bkg = new BackgroundWorker();

        if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
            AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
        {
          bkg.DoWork += (s, d) =>
          encryption3.Encrypt(
            s, d,
            AppSettings.Instance.FileList.ToArray(),
            AtcFilePath,
            EncryptionPassword, EncryptionPasswordBinary,
            Path.GetFileNameWithoutExtension(AtcFilePath));
        }
        else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
        {
          bkg.DoWork += (s, d) =>
          compression.Encrypt(
            s, d,
            AppSettings.Instance.FileList.ToArray(),
            AtcFilePath,
            EncryptionPassword, EncryptionPasswordBinary,
            Path.GetFileNameWithoutExtension(AtcFilePath));
        }
        bkg.RunWorkerCompleted += backgroundWorker_Encryption_RunWorkerCompleted;
        bkg.ProgressChanged += backgroundWorker_ProgressChanged;
        bkg.WorkerReportsProgress = true;
        bkg.WorkerSupportsCancellation = true;

        bkg.RunWorkerAsync();
        

      }// end if (AppSettings.Instance.EncryptionFileType == TYPE_ATC_ENCRYPT && AllPackFilePath != "");
      //----------------------------------------------------------------------
      // Encrypt or decrypt files in directory one by one
      //----------------------------------------------------------------------
      else if (AppSettings.Instance.fFilesOneByOne == true)
      {
        // フォルダ内のファイルは個別に暗号化/復号する

        // フォルダがアタッシェケースへドラッグ＆ドロップなどされた場合、
        // サブフォルダ以下にあるファイルすべてを個別に暗号化します。
        // ただし、その中に既に暗号化ファイル（.atcまたは、.exe）か、
        // .zipファイルが含まれる場合は、それを無視します。

        foreach (string TheFileList in AppSettings.Instance.FileList)
        {
          IEnumerable<string> FileLists = GetFileList("*", TheFileList);
          foreach (string FilePath in FileLists)
          {
            AppSettings.Instance.FileList.Add(FilePath);
          }
        }

        int NumberOfFiles = 0;
        int TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
        foreach (string FilePath in AppSettings.Instance.FileList)
        {
          if (File.Exists(FilePath) == false)
          {
            continue;
          }

          NumberOfFiles++;

          if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
          {
            encryption3 = new FileEncrypt3();
            encryption3.NumberOfFiles = NumberOfFiles;
            encryption3.TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
          }
          else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
          {
            compression = new ZipEncrypt();
            compression.NumberOfFiles = NumberOfFiles;
            compression.TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
          }

          if (AppSettings.Instance.DetectFileType() == FILE_TYPE_NONE)
          {
            //-----------------------------------
            // Save encryption files to same folder.
            if (AppSettings.Instance.fSaveToSameFldr == false)
            {
              OutputDirPath = Path.GetDirectoryName(FilePath);
            }

            //-----------------------------------
            // Specify the format of the encryption file name
            string FileName = "";
            if (AppSettings.Instance.fAutoName == true)
            {
              //Create encrypted file including extension
              if (AppSettings.Instance.fExtInAtcFileName == true)
              {
                FileName = AppSettings.Instance.
                  getSpecifyFileNameFormat(AppSettings.Instance.AutoNameFormatText, FilePath, NumberOfFiles);
                FileName = FileName + Extension;
              }
              else
              {
                FileName = Path.GetFileNameWithoutExtension(FileName) + Extension;
              }
            }
            else
            {
              //Create encrypted file including extension
              if (AppSettings.Instance.fExtInAtcFileName == true)
              {
                FileName = Path.GetFileName(FilePath) + Extension;
              }
              else
              {
                FileName = Path.GetFileNameWithoutExtension(FilePath) + Extension;
              }
            }

            AtcFilePath = Path.Combine(OutputDirPath, FileName);

            //Confirm &overwriting when same file name exists.
            if (AppSettings.Instance.fEncryptConfirmOverwrite == true)
            {
              if (File.Exists(AtcFilePath) == true)
              {
                // 問い合わせ
                // 以下のファイルはすでに存在しています。上書きして保存しますか？
                //
                // Question
                // The following file already exists. Do you overwrite the files to save?
                DialogResult ret = MessageBox.Show(Resources.labelComfirmToOverwriteFile + Environment.NewLine + AtcFilePath,
                Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ret == DialogResult.No)
                {
                  panelStartPage.Visible = false;
                  panelEncrypt.Visible = true;
                  panelEncryptConfirm.Visible = false;
                  panelDecrypt.Visible = false;
                  panelProgressState.Visible = false;
                  return;
                }
              }
            }

            //-----------------------------------
            // Self executable file
            //-----------------------------------
            if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
            {
              encryption3.fExecutable = true;
            }

            //-----------------------------------
            //　Set the timestamp of encryption file to original files or directories
            //-----------------------------------
            encryption3.fKeepTimeStamp = AppSettings.Instance.fKeepTimeStamp;

            //-----------------------------------
            // Encryption start
            //-----------------------------------

            // BackgroundWorker event handler
            bkg = new BackgroundWorker();

            if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
                AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
                AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
            {
              bkg.DoWork += (s, d) =>
              encryption3.Encrypt(
                s, d,
                new string[] { FilePath },
                AtcFilePath,
                EncryptionPassword, EncryptionPasswordBinary,
                Path.GetFileNameWithoutExtension(AtcFilePath));
            }
            else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
            {
              bkg.DoWork += (s, d) =>
              compression.Encrypt(
                s, d,
                new string[] { FilePath },
                AtcFilePath,
                EncryptionPassword, EncryptionPasswordBinary,
                Path.GetFileNameWithoutExtension(AtcFilePath));
            }

            bkg.RunWorkerCompleted += backgroundWorker_Encryption_RunWorkerCompleted;
            bkg.ProgressChanged += backgroundWorker_ProgressChanged;
            bkg.WorkerReportsProgress = true;
            bkg.WorkerSupportsCancellation = true;

            bkg.RunWorkerAsync();
              
          }// end if (CheckFileType(FilePath) == 0);

        }// end foreach (string FilePath in FileList);

      }// end else if (AppSettings.Instance.fFilesOneByOne == true);
      //----------------------------------------------------------------------
      // Normal
      //----------------------------------------------------------------------
      else
      {  //AppSettings.Instance.fNormal;

        // なにもしない
        //
        // 複数のファイルやフォルダをアタッシェケースにドラッグ＆ドロップなどすると、
        // それぞれを暗号化しファイルが生成されます。 フォルダの場合は、サブフォルダも含め、
        // フォルダ単位でパックされます

        int NumberOfFiles = 0;
        int TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
        foreach (string FilePath in AppSettings.Instance.FileList)
        {
          if (FilePath == null)
          {
            continue;
          }

          NumberOfFiles++;

          if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
          {
            encryption3 = new FileEncrypt3();
            encryption3.NumberOfFiles = NumberOfFiles;
            encryption3.TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
          }
          else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
          {
            compression = new ZipEncrypt();
            compression.NumberOfFiles = NumberOfFiles;
            compression.TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
          }

          //-----------------------------------
          // Save encryption files to same folder.
          if (AppSettings.Instance.fSaveToSameFldr == false)
          {
            OutputDirPath = Path.GetDirectoryName(FilePath);
          }

          //-----------------------------------
          // Specify the format of the encryption file name
          string FileName = "";
          if (AppSettings.Instance.fAutoName == true)
          {
            //Create encrypted file including extension
            if (AppSettings.Instance.fExtInAtcFileName == true)
            {
              FileName = AppSettings.Instance.
                getSpecifyFileNameFormat(AppSettings.Instance.AutoNameFormatText, FilePath, NumberOfFiles);
              FileName = FileName + Extension;
            }
            else
            {
              FileName = Path.GetFileNameWithoutExtension(FileName) + Extension;
            }
          }
          else
          {
            //Create encrypted file including extension
            if (AppSettings.Instance.fExtInAtcFileName == true)
            {
              FileName = Path.GetFileName(FilePath) + Extension;
            }
            else
            {
              FileName = Path.GetFileNameWithoutExtension(FilePath) + Extension;
            }
          }

          AtcFilePath = Path.Combine(OutputDirPath, FileName);

          //-----------------------------------
          //Confirm &overwriting when same file name exists.
          if (AppSettings.Instance.fEncryptConfirmOverwrite == true)
          {
            if (File.Exists(AtcFilePath) == true)
            {
              // 問い合わせ
              // 以下のファイルはすでに存在しています。上書きして保存しますか？
              //
              // Question
              // The following file already exists. Do you overwrite the files to save?
              DialogResult ret = MessageBox.Show(Resources.labelComfirmToOverwriteFile + Environment.NewLine + AtcFilePath,
              Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

              if (ret == DialogResult.No)
              {
                panelStartPage.Visible = false;
                panelEncrypt.Visible = true;
                panelEncryptConfirm.Visible = false;
                panelDecrypt.Visible = false;
                panelProgressState.Visible = false;
                return;
              }
            }
          }

          //----------------------------------------------------------------------
          // Self executable file
          //----------------------------------------------------------------------
          if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
          {
            encryption3.fExecutable = true;
          }

          //-----------------------------------
          //　Set the timestamp of encryption file to original files or directories
          //-----------------------------------
          if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
          {
            encryption3.fKeepTimeStamp = AppSettings.Instance.fKeepTimeStamp;
          }

          //----------------------------------------------------------------------
          // Encrypt
          //----------------------------------------------------------------------

          // BackgroundWorker event handler
          bkg = new BackgroundWorker();

          if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_NONE ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC ||
              AppSettings.Instance.EncryptionFileType == FILE_TYPE_ATC_EXE)
          {
            bkg.DoWork += (s, d) =>
            encryption3.Encrypt(
              s, d,
              AppSettings.Instance.FileList.ToArray(),
              AtcFilePath,
              EncryptionPassword, EncryptionPasswordBinary,
              "");
          }
          else if (AppSettings.Instance.EncryptionFileType == FILE_TYPE_PASSWORD_ZIP)
          {
            bkg.DoWork += (s, d) =>
            compression.Encrypt(
              s, d,
              AppSettings.Instance.FileList.ToArray(),
              AtcFilePath,
              EncryptionPassword, EncryptionPasswordBinary,
              "");
          }

          bkg.RunWorkerCompleted += backgroundWorker_Encryption_RunWorkerCompleted;
          bkg.ProgressChanged += backgroundWorker_ProgressChanged;
          bkg.WorkerReportsProgress = true;
          bkg.WorkerSupportsCancellation = true;

          bkg.RunWorkerAsync();
        }

      }
        
    }//private void buttonEncryptStart_Click(object sender, EventArgs e)


    /// <summary>
    /// 
    /// panelEncrypt「キャンセル」ボタンのクリックイベント
    /// </summary>
    private void buttonEncryptCancel_Click(object sender, EventArgs e)
    {
      textBoxPassword.Text = "";
      textBoxRePassword.Text = "";
      checkBoxNotMaskEncryptedPassword.Checked = false;
      AppSettings.Instance.TempEncryptionPassFilePath = "";
      //
      //スタートウィンドウへ戻る
      panelStartPage.Visible = true;
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = false;

      panelStartPage_VisibleChanged(sender, e);

    }

    private void pictureBoxEncryptBackButton_MouseEnter(object sender, EventArgs e)
    {
      pictureBoxEncryptBackButton.Image = pictureBoxBackButtonOn.Image; 
    }

    private void pictureBoxEncryptBackButton_MouseLeave(object sender, EventArgs e)
    {
      pictureBoxEncryptBackButton.Image = pictureBoxBackButtonOff.Image;
    }


    #endregion

    //======================================================================
    // Decrypt window ( panelDecrypt )
    //======================================================================
    #region Decrypt window

    /// <summary>
    /// 「パスワードマスクをしない(&M)」クリックイベント
    /// 'Not mask password character' checkbox click event.
    /// </summary>
    private void checkBoxNotMaskDecryptedPassword_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxNotMaskDecryptedPassword.Checked == true)
      {
        textBoxDecryptPassword.PasswordChar = (char)0;
        textBoxDecryptPassword.UseSystemPasswordChar = false;
      }
      else
      {
        textBoxDecryptPassword.UseSystemPasswordChar = true;
      }
    }

    /// <summary>
    /// 「復号後に暗号化ファイルを削除する(&D)」クリックイベント
    /// 'Delete &encrypted file after decryption' checkbox click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void checkBoxDeleteAtcFileAfterDecryption_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void textBoxDecryptPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        buttonDecryptStart.PerformClick();
      }
    }

    private void textBoxDecryptPassword_DragDrop(object sender, DragEventArgs e)
    {
      string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if (File.Exists(FilePaths[0]) == true)
      {
        AppSettings.Instance.TempDecryptionPassFilePath = FilePaths[0];
        AppSettings.Instance.MyDecryptPasswordBinary = GetPasswordFileHash3(AppSettings.Instance.TempDecryptionPassFilePath);
        textBoxDecryptPassword.Text = AppSettings.BytesToHexString(AppSettings.Instance.MyDecryptPasswordBinary);
        textBoxDecryptPassword.BackColor = SystemColors.ButtonFace;
        textBoxDecryptPassword.Enabled = false;
      }
      else
      {
        // 注意
        // パスワードファイルにフォルダーを使うことはできません。
        //
        // Alert
        // Not use the folder to the password file.
        DialogResult ret = MessageBox.Show(Resources.DialogMessageNotDirectoryInPasswordFile,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void textBoxDecryptPassword_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
        textBoxDecryptPassword.BackColor = Color.Honeydew;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void textBoxDecryptPassword_DragLeave(object sender, EventArgs e)
    {
      textBoxDecryptPassword.BackColor = SystemColors.Window;
    }

    private void textBoxDecryptPassword_DragOver(object sender, DragEventArgs e)
    {

    }

    //======================================================================
    /// <summary>
    ///  Decryption button 'Click' event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //======================================================================
    private void buttonDecryptStart_Click(object sender, EventArgs e)
    {

      // Not mask password character
      AppSettings.Instance.fNotMaskPassword = checkBoxNotMaskDecryptedPassword.Checked ? true : false;
        
      //-----------------------------------
      // Directory to oputput decrypted files
      //-----------------------------------
      string OutDirPath = "";
      if (AppSettings.Instance.fDecodeToSameFldr == true)
      {
        OutDirPath = AppSettings.Instance.DecodeToSameFldrPath;
      }

      if (Directory.Exists(OutDirPath) == false)
      {
        OutDirPath = Path.GetDirectoryName((string)AppSettings.Instance.FileList[0]);
      }

      //-----------------------------------
      // Decryption password
      //-----------------------------------
      string DecryptionPassword = "";
      if (AppSettings.Instance.fMyDecryptPasswordKeep == true)
      {
        DecryptionPassword = AppSettings.Instance.MyDecryptPasswordString;
      }
      else
      {
        DecryptionPassword = textBoxDecryptPassword.Text;
      }
        
      //-----------------------------------
      // Always minimize when running
      //-----------------------------------
      if (AppSettings.Instance.fMainWindowMinimize == true)
      {
        this.WindowState = FormWindowState.Minimized;
      }

      //-----------------------------------
      // Minimizing a window without displaying in the taskbar
      //-----------------------------------
      if (AppSettings.Instance.fTaskBarHide == true)
      {
        this.Hide();
      }

      //-----------------------------------
      // Display in the task tray
      //-----------------------------------
      if (AppSettings.Instance.fTaskTrayIcon == true)
      {
        notifyIcon1.Visible = true;
      }
      else
      {
        notifyIcon1.Visible = false;
      }
        
      //-----------------------------------
      // Display progress window
      //-----------------------------------
      panelStartPage.Visible = false;
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = true;

      labelProgress.Text = labelDecryption.Text;
      pictureBoxProgress.Image = pictureBoxDecOn.Image;

      labelCryptionType.Text = Resources.labelProcessNameDecrypt;

      this.Update();

      //-----------------------------------
      // Preparing for devrypting
      // 
      //-----------------------------------
      int NumberOfFiles = 0;
      int TotalNumberOfFiles = AppSettings.Instance.FileList.Count();
      foreach (string AtcFilePath in AppSettings.Instance.FileList)
      {
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.MarqueeAnimationSpeed = 50;
        // 復号するための準備をしています...
        // Getting ready for decryption...
        labelProgressMessageText.Text = Resources.labelGettingReadyForDecryption;

        NumberOfFiles++;

        decryption3 = new FileDecrypt3(AtcFilePath);

        if (decryption3.TokenStr == "_AttacheCaseData")
        {
          // Encryption data ( O.K. )
        }
        else if (decryption3.TokenStr == "_Atc_Broken_Data")
        {
          // エラー
          // この暗号化ファイルは破壊されています。処理を中止します。
          //
          // Alert
          // This encrypted file is broken. The process is aborted.
          MessageBox.Show(Resources.DialogMessageAtcFileBroken + Environment.NewLine + AtcFilePath,
          Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;
        }
        else
        {
          // エラー
          // 暗号化ファイルではありません。処理を中止します。
          //
          // Alert
          // The file is not encrypted file. The process is aborted.
          MessageBox.Show(Resources.DialogMessageNotAtcFile + Environment.NewLine + AtcFilePath,
          Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;
        }

        //-----------------------------------
        // Password file
        //-----------------------------------

        // ※パスワードファイルは、記憶パスワードよりも優先される。
        // * This password files is priority than memorized encryption password.

        byte[] DecryptionPasswordBinary = null;
        if (AppSettings.Instance.fAllowPassFile == true)
        {
          // Check specified password file for Decryption
          if (AppSettings.Instance.fCheckPassFileDecrypt == true)
          {
            if (File.Exists(AppSettings.Instance.PassFilePathDecrypt) == true)
            {
              if (decryption3.DataFileVersion < 130)
              {
                DecryptionPasswordBinary = GetPasswordFileHash2(AppSettings.Instance.PassFilePathDecrypt);
              }
              else
              {
                DecryptionPasswordBinary = GetPasswordFileHash3(AppSettings.Instance.PassFilePathDecrypt);
              }
            }
            else
            {
              if (AppSettings.Instance.fNoErrMsgOnPassFile == false)
              {
                // エラー
                // 復号時の指定されたパスワードファイルが見つかりません。
                //
                // Error
                // The specified password file is not found in decryption.
                DialogResult ret = MessageBox.Show(
                  Resources.DialogMessageDecryptionPasswordFileNotFound + Environment.NewLine + AppSettings.Instance.PassFilePathDecrypt,
                  Resources.DialogTitleError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              }
              return;
            }
          }

          // コマンドラインからのパスワード
          if (AppSettings.Instance.EncryptPasswordStringFromCommandLine != "")
          {
            DecryptionPassword = AppSettings.Instance.EncryptPasswordStringFromCommandLine;
            DecryptionPasswordBinary = null;
          } 

          // Drag & Drop Password file
          if (File.Exists(AppSettings.Instance.TempDecryptionPassFilePath) == true)
          {
            if (decryption3.DataFileVersion < 130)
            {
              DecryptionPasswordBinary = GetPasswordFileHash2(AppSettings.Instance.TempDecryptionPassFilePath);
              DecryptionPassword = "";
            }
            else
            {
              DecryptionPasswordBinary = GetPasswordFileHash3(AppSettings.Instance.TempDecryptionPassFilePath);
              DecryptionPassword = "";
            }
          }
        }
        
        // BackgroundWorker event handler
        bkg = new BackgroundWorker();
        bkg.RunWorkerCompleted += backgroundWorker_Decryption_RunWorkerCompleted;
        bkg.ProgressChanged += backgroundWorker_ProgressChanged;
        bkg.WorkerReportsProgress = true;
        bkg.WorkerSupportsCancellation = true;

        //-----------------------------------
        // Old version 
        if (decryption3.DataFileVersion < 130)
        {
          decryption3 = null; // ver.3 is null
          decryption2 = new FileDecrypt2(AtcFilePath);
          decryption2.fNoParentFolder = AppSettings.Instance.fNoParentFldr;
          decryption2.NumberOfFiles = NumberOfFiles;
          decryption2.TotalNumberOfFiles = TotalNumberOfFiles;
          decryption2.TempOverWriteOption = (AppSettings.Instance.fDecryptConfirmOverwrite == false ? 2 : 0);
          if (LimitOfInputPassword == -1)
          {
            LimitOfInputPassword = decryption2.MissTypeLimits;
          }
          toolStripStatusLabelDataVersion.Text = "Data ver.2";
          this.Update();

          //======================================================================
          // Decryption start
          // 復号開始
          // http://stackoverflow.com/questions/4807152/sending-arguments-to-background-worker
          //======================================================================
          bkg.DoWork += (s, d) =>
          {
            decryption2.Decrypt(
              s, d,
              AtcFilePath, OutDirPath, DecryptionPassword, DecryptionPasswordBinary,
              DialogMessageForOverWrite);
          };

          bkg.RunWorkerAsync();

        }
        //-----------------------------------
        // Current version
        else if (decryption3.DataFileVersion < 140)
        {
          decryption2 = null;
          decryption3.fNoParentFolder = AppSettings.Instance.fNoParentFldr;
          decryption3.NumberOfFiles = NumberOfFiles;
          decryption3.TotalNumberOfFiles = TotalNumberOfFiles;
          decryption3.TempOverWriteOption = (AppSettings.Instance.fDecryptConfirmOverwrite == false ? 2 : 0);
          if (LimitOfInputPassword == -1)
          {
            LimitOfInputPassword = decryption3.MissTypeLimits;
          }
          toolStripStatusLabelDataVersion.Text = "Data ver.3";
          this.Update();

          //======================================================================
          // Decryption start
          // 復号開始
          // http://stackoverflow.com/questions/4807152/sending-arguments-to-background-worker
          //======================================================================
          bkg.DoWork += (s, d) =>
          {
            decryption3.Decrypt(
              s, d,
              AtcFilePath, OutDirPath, DecryptionPassword, DecryptionPasswordBinary,
              DialogMessageForOverWrite);
          };

          bkg.RunWorkerAsync();

        }
        //-----------------------------------
        // Higher version 
        else
        {
          // 警告
          // このファイルはアタッシェケースの上位バージョンで暗号化されています。
          // 復号できません。処理を中止します。
          //
          // Alert
          // This file has been encrypted with a higher version.
          // It can not be decrypted. The process is aborted.
          MessageBox.Show(Resources.DialogMessageHigherVersion + Environment.NewLine + AtcFilePath,
          Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;

        }

      }// end foreach (string AtcFilePath in AtcFilePaths);

    }

    // Cancel button click event.                
    private void buttonDecryptCancel_Click(object sender, EventArgs e)
    {
      // Cancel, If decryption is not being processed 
      panelEncrypt.Visible = false;
      panelEncryptConfirm.Visible = false;
      panelDecrypt.Visible = false;
      panelProgressState.Visible = false;
      panelStartPage.Visible = true;
      textBoxDecryptPassword.Text = "";
      AppSettings.Instance.TempDecryptionPassFilePath = "";
    }

    private void pictureBoxDecryptBackButton_MouseEnter(object sender, EventArgs e)
    {
      pictureBoxDecryptBackButton.Image = pictureBoxBackButtonOn.Image;
    }

    private void pictureBoxDecryptBackButton_MouseLeave(object sender, EventArgs e)
    {
      pictureBoxDecryptBackButton.Image = pictureBoxBackButtonOff.Image;
    }


    #endregion

    //======================================================================
    // Progress window ( panelProgressState )
    //======================================================================
    #region

    /// <summary>
    ///  "Back" button
    ///  「戻る」ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      if (buttonCancel.Text == Resources.ButtonTextOK)
      {
        //スタートウィンドウ表示
        panelStartPage.Visible = true;
        panelEncrypt.Visible = false;
        panelEncryptConfirm.Visible = false;
        panelDecrypt.Visible = false;
        panelProgressState.Visible = false;

        panelStartPage_VisibleChanged(sender, e);

        return;
      }
      else
      {
        //-----------------------------------
        // Canceling
        // キャンセル処理
        //-----------------------------------
        if (bkg != null && bkg.IsBusy == true)
        {
          bkg.CancelAsync();
          buttonCancel.Text = Resources.ButtonTextOK;
        }

        if (cts != null)
        {
          cts.Cancel();
          buttonCancel.Text = Resources.ButtonTextOK;
        }

      }

    }

    private void pictureBoxProgressStateBackButton_MouseEnter(object sender, EventArgs e)
    {
      pictureBoxProgressStateBackButton.Image = pictureBoxBackButtonOn.Image;
    }

    private void pictureBoxProgressStateBackButton_MouseLeave(object sender, EventArgs e)
    {
      pictureBoxProgressStateBackButton.Image = pictureBoxBackButtonOff.Image;
    }

    private void pictureBoxProgressStateBackButton_Click(object sender, EventArgs e)
    {
      if (buttonCancel.Text == Resources.ButtonTextOK)
      {
        //スタートウィンドウ表示
        panelStartPage.Visible = true;
        panelEncrypt.Visible = false;
        panelEncryptConfirm.Visible = false;
        panelDecrypt.Visible = false;
        panelProgressState.Visible = false;

        panelStartPage_VisibleChanged(sender, e);

        return;
      }
      else
      {
        return;
      }
    }

    #endregion

    /// <summary>
    /// Notify Icon Mouse Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.WindowState = FormWindowState.Normal;
      }
      if (this.Visible == false)
      {
        this.Show();
      }
      this.Activate();
    }
      
    //======================================================================
    /// <summary>
    /// ファイルを破壊して、当該内部トークンを「破壊」ステータスに書き換える
    /// Break a specified file, and rewrite the token of broken status
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    //======================================================================
    public bool BreakTheFile(string FilePath)
    {
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
      {
        byte[] byteArray = new byte[16];
        if (fs.Read(byteArray, 4, 16) == 16)
        {
          string TokenStr = System.Text.Encoding.ASCII.GetString(byteArray);
          if (TokenStr == "_AttacheCaseData")
          {
            // Rewriting Token
            fs.Seek(4, SeekOrigin.Begin);
            byteArray = System.Text.Encoding.ASCII.GetBytes("_Atc_Broken_Data");
            fs.Write(byteArray, 0, 16);

            // Break IV of the file
            byteArray = new byte[32];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(byteArray);

            fs.Seek(32, SeekOrigin.Begin);
            fs.Write(byteArray, 0, 32);
          }
          else if (TokenStr == "_Atc_Broken_Data")
          {
            // broken already
            return (true);
          }
          else
          {  // Token is not found.
            return (false);
          }
        }
        else
        {
          return (false);
        }

      }// end using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read));

      return (true);
    }

    //======================================================================
    /// <summary>
    /// 文字コードを判別する
    /// </summary>
    /// <remarks>
    /// 参考：http://dobon.net/vb/dotnet/string/detectcode.html
    /// Jcode.pmのgetcodeメソッドを移植したものです。
    /// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
    /// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
    /// </remarks>
    /// <param name="bytes">文字コードを調べるデータ</param>
    /// <returns>適当と思われるEncodingオブジェクト。
    /// 判断できなかった時はnull。</returns>
    //======================================================================
    public static System.Text.Encoding GetCode(byte[] bytes)
    {
      const byte bEscape = 0x1B;
      const byte bAt = 0x40;
      const byte bDollar = 0x24;
      const byte bAnd = 0x26;
      const byte bOpen = 0x28;    //'('
      const byte bB = 0x42;
      const byte bD = 0x44;
      const byte bJ = 0x4A;
      const byte bI = 0x49;

      int len = bytes.Length;
      byte b1, b2, b3, b4;

      //Encode::is_utf8 は無視

      bool isBinary = false;
      for (int i = 0; i < len; i++)
      {
        b1 = bytes[i];
        if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
        {
          //'binary'
          isBinary = true;
          if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
          {
            //smells like raw unicode
            return System.Text.Encoding.Unicode;
          }
        }
      }
      if (isBinary)
      {
        return null;
      }

      //not Japanese
      bool notJapanese = true;
      for (int i = 0; i < len; i++)
      {
        b1 = bytes[i];
        if (b1 == bEscape || 0x80 <= b1)
        {
          notJapanese = false;
          break;
        }
      }
      if (notJapanese)
      {
        return System.Text.Encoding.ASCII;
      }

      for (int i = 0; i < len - 2; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        b3 = bytes[i + 2];

        if (b1 == bEscape)
        {
          if (b2 == bDollar && b3 == bAt)
          {
            //JIS_0208 1978
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bDollar && b3 == bB)
          {
            //JIS_0208 1983
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bOpen && (b3 == bB || b3 == bJ))
          {
            //JIS_ASC
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bOpen && b3 == bI)
          {
            //JIS_KANA
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          if (i < len - 3)
          {
            b4 = bytes[i + 3];
            if (b2 == bDollar && b3 == bOpen && b4 == bD)
            {
              //JIS_0212
              //JIS
              return System.Text.Encoding.GetEncoding(50220);
            }
            if (i < len - 5 &&
                b2 == bAnd && b3 == bAt && b4 == bEscape &&
                bytes[i + 4] == bDollar && bytes[i + 5] == bB)
            {
              //JIS_0208 1990
              //JIS
              return System.Text.Encoding.GetEncoding(50220);
            }
          }
        }
      }

      //should be euc|sjis|utf8
      //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
      int sjis = 0;
      int euc = 0;
      int utf8 = 0;
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
            ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
        {
          //SJIS_C
          sjis += 2;
          i++;
        }
      }
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
            (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
        {
          //EUC_C
          //EUC_KANA
          euc += 2;
          i++;
        }
        else if (i < len - 2)
        {
          b3 = bytes[i + 2];
          if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
              (0xA1 <= b3 && b3 <= 0xFE))
          {
            //EUC_0212
            euc += 3;
            i += 2;
          }
        }
      }
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
        {
          //UTF8
          utf8 += 2;
          i++;
        }
        else if (i < len - 2)
        {
          b3 = bytes[i + 2];
          if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
              (0x80 <= b3 && b3 <= 0xBF))
          {
            //UTF8
            utf8 += 3;
            i += 2;
          }
        }
      }
      //M. Takahashi's suggestion
      //utf8 += utf8 / 2;

      System.Diagnostics.Debug.WriteLine(
          string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
      if (euc > sjis && euc > utf8)
      {
        //EUC
        return System.Text.Encoding.GetEncoding(51932);
      }
      else if (sjis > euc && sjis > utf8)
      {
        //SJIS
        return System.Text.Encoding.GetEncoding(932);
      }
      else if (utf8 > euc && utf8 > sjis)
      {
        //UTF8
        return System.Text.Encoding.UTF8;
      }

      return null;
    }

    private void panelStartPage_MouseEnter(object sender, EventArgs e)
    {
      panelStartPage.BackColor = Color.WhiteSmoke;
    }

    private void panelStartPage_MouseLeave(object sender, EventArgs e)
    {
      panelStartPage.BackColor = Color.White;
    }
    
    private void pictureBoxDec_Click(object sender, EventArgs e)
    {

    }

    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
      {
        //マウスダウンした位置を記憶する
        MouseDownPoint = new Point(e.X, e.Y);
      }
    }

    private void Form1_MouseMove(object sender, MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
      {
        this.Left += e.X - MouseDownPoint.X;
        this.Top += e.Y - MouseDownPoint.Y;
      }
    }

    private void buttonExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        this.Close();
      }
    }

    private void panelStartPage_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        this.Close();
      }
    }

    private void pictureBoxAtc_Click(object sender, EventArgs e)
    {
      if (pictureBoxAtc.Image == pictureBoxAtcOn.Image)
      {
        pictureBoxAtc.Image = pictureBoxAtcOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_NONE;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_NONE;
      }
      else
      {
        pictureBoxAtc.Image = pictureBoxAtcOn.Image;
        pictureBoxExe.Image = pictureBoxExeOff.Image;
        pictureBoxZip.Image = pictureBoxZipOff.Image;
        pictureBoxDec.Image = pictureBoxDecOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_ATC;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_ATC;

        pictureBoxEncryption.Image = pictureBoxAtcOn.Image;

      }
    }

    private void pictureBoxExe_Click(object sender, EventArgs e)
    {
      if(pictureBoxExe.Image == pictureBoxExeOn.Image)
      {
        pictureBoxExe.Image = pictureBoxExeOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_NONE;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_NONE;
      }
      else
      {
        pictureBoxExe.Image = pictureBoxExeOn.Image;
        pictureBoxAtc.Image = pictureBoxAtcOff.Image;
        pictureBoxZip.Image = pictureBoxZipOff.Image;
        pictureBoxDec.Image = pictureBoxDecOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_ATC_EXE;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_ATC_EXE;

        pictureBoxExe.Image = pictureBoxExeOn.Image;
      }
    }

    private void pictureBoxZip_Click(object sender, EventArgs e)
    {
      if (pictureBoxZip.Image == pictureBoxZipOn.Image)
      {
        pictureBoxZip.Image = pictureBoxZipOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_NONE;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_NONE;
      }
      else
      {
        pictureBoxZip.Image = pictureBoxZipOn.Image;
        pictureBoxAtc.Image = pictureBoxAtcOff.Image;
        pictureBoxExe.Image = pictureBoxExeOff.Image;
        pictureBoxDec.Image = pictureBoxDecOff.Image;
        AppSettings.Instance.EncryptionFileType = FILE_TYPE_PASSWORD_ZIP;
        AppSettings.Instance.SameEncryptionFileTypeBefore = FILE_TYPE_PASSWORD_ZIP;

        pictureBoxEncryption.Image = pictureBoxZipOn.Image;

      }

    }

    private void pictureBoxHamburgerMenu_Click(object sender, EventArgs e)
    {
      Point p = pictureBoxHamburgerMenu.PointToScreen(pictureBoxHamburgerMenu.ClientRectangle.Location);
      p.X += pictureBoxHamburgerMenu.Width;
      this.contextMenuStrip3.Show(p);

    }

  }// end public partial class Form1 : Form;

}// end namespace AttacheCase;
