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
using System.ComponentModel;
using System.Windows.Forms;
using AttacheCase;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Collections.ObjectModel;
using Sha2;

namespace Exeout
{
  public partial class Form1 : Form
	{
    // Status code
    //private const int ENCRYPT_SUCCEEDED   = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED     = 2; // Decrypt is succeeded.
    //private const int DELETE_SUCCEEDED    = 3; // Delete is succeeded.
    private const int HEADER_DATA_READING   = 4; // Header data is reading.
    //private const int ENCRYPTING          = 5; // Ecrypting.
    private const int DECRYPTING            = 6; // Decrypting.
    //private const int DELETING            = 7; // Deleting.

    // Error code
    private const int USER_CANCELED            = -1;   // User cancel.
    private const int ERROR_UNEXPECTED         = -100;
    private const int NOT_ATC_DATA             = -101;
    private const int ATC_BROKEN_DATA          = -102;
    private const int NO_DISK_SPACE            = -103;
    private const int FILE_INDEX_NOT_FOUND     = -104;
    private const int PASSWORD_TOKEN_NOT_FOUND = -105;

    private string CurrentCultureName = "";

		public static BackgroundWorker bkg;
		public int LimitOfInputPassword = -1;
		private FileDecrypt3 decryption = null;
		string TempDecryptionPassFilePath = "";

		public Form1()
		{
			InitializeComponent();

			this.Text = Path.GetFileName(Application.ExecutablePath);

      //MessageBox.Show(CultureInfo.CurrentCulture.Name);
      
      if ( CultureInfo.CurrentCulture.Name.IndexOf("ja") > -1)
      {
        CurrentCultureName = "ja";

        labelMessage.Text = "パスワードを入力してください：";
        checkBoxNotMaskPassword.Text = "パスワードを表示";
        buttonDecrypt.Text = "復号/元に戻す(&D)";
        buttonExit.Text = "終了(&X)";

      }
      else
      {
        CurrentCultureName = "en";
      }
      
    }

		private void Form1_Load(object sender, EventArgs e)
		{
			// stab
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			textBox1.Focus();
			textBox1.SelectAll();
		}

		private void checkBoxNotMaskPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxNotMaskPassword.Checked == true)
			{
				textBox1.UseSystemPasswordChar = true;
			}
			else
			{
				textBox1.PasswordChar = (char)0;
        textBox1.UseSystemPasswordChar = false;
      }
    }

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				buttonDecrypt_Click(sender, e);
			}
		}

    private void buttonExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void toolStripStatusLabel1_Click(object sender, EventArgs e)
    {
      // Show dialog for confirming to orverwrite
      Form2 frm2 = new Form2();
      frm2.ShowDialog();
      frm2.Dispose();
    }

    private void textBox1_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if (File.Exists(files[0]) == true)
      {
        TempDecryptionPassFilePath = files[0];
        buttonDecrypt.PerformClick(); // Decryption start.
      }
    }

    private void textBox1_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
        textBox1.BackColor = Color.Honeydew;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void textBox1_DragLeave(object sender, EventArgs e)
    {
      textBox1.BackColor = SystemColors.Window;
    }

    /// <summary>
    /// Decryption start.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonDecrypt_Click(object sender, EventArgs e)
		{
      buttonDecrypt.Enabled = false;
			checkBoxNotMaskPassword.Visible = false;
			progressBar1.Location = textBox1.Location;
			progressBar1.Width = textBox1.Width;
			progressBar1.Visible = true;
			labelPercent.Text = "- %";

			//-----------------------------------
			// Directory to oputput decrypted files
			//-----------------------------------
			string OutDirPath = Path.GetDirectoryName(Application.ExecutablePath);

			//-----------------------------------
			// Decryption password
			//-----------------------------------
			string DecryptionPassword = textBox1.Text;
		
			//-----------------------------------
			// Password file
			//-----------------------------------

			// Drag & Drop Password file
			byte[] DecryptionPasswordBinary = null;
			if (File.Exists(TempDecryptionPassFilePath) == true)
			{
				DecryptionPasswordBinary = GetPasswordFileHash3(TempDecryptionPassFilePath);
			}
		
			//-----------------------------------
			// Preparing for decrypting
			// 
			//-----------------------------------
			decryption = new FileDecrypt3(Application.ExecutablePath);

			if (decryption.TokenStr == "_AttacheCaseData")
			{
				// Encryption data ( O.K. )
			}
			else if (decryption.TokenStr == "_Atc_Broken_Data")
			{
        //
        // エラー
        // この暗号化ファイルは壊れています。処理を中止します。
        //
        // Alert
        // This encrypted file is broken. The process is aborted.
        //
        string DialogTitleAlert = "Alert";
        string DialogMessageAtcFileBroken = "This encrypted file is broken. The process is aborted.";

        if (CurrentCultureName == "ja")
        {
          DialogTitleAlert = "エラー";
          DialogMessageAtcFileBroken = "この暗号化ファイルは壊れています。処理を中止します。";
        }
        MessageBox.Show(DialogMessageAtcFileBroken,	DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        if (CurrentCultureName == "ja")
        {
          labelMessage.Text = "復号処理を中止しました。";
        }
        else
        {
          labelMessage.Text = "Process of decryption has been aborted.";
        }
        labelPercent.Text = "- %";

        return;
			}
			else
			{
        // 
        // エラー
        // 暗号化ファイルではありません。処理を中止します。
        //
        // Alert
        // The file is not encrypted file. The process is aborted.
        // 
        string DialogTitleAlert = "Alert";
        string DialogMessageNotAtcFile = "The file is not encrypted file. The process is aborted.";
        if (CurrentCultureName == "ja")
        {
          DialogTitleAlert = "エラー";
          DialogMessageNotAtcFile = "暗号化ファイルではありません。処理を中止します。";
        }
        MessageBox.Show(DialogMessageNotAtcFile, DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        if (CurrentCultureName == "ja")
        {
          labelMessage.Text = "復号処理を中止しました。";
        }
        else
        {
          labelMessage.Text = "Process of decryption has been aborted.";
        }
        labelPercent.Text = "- %";

        return;
			}

			if (LimitOfInputPassword == -1)
			{
				LimitOfInputPassword = decryption.MissTypeLimits;
			}

#if (DEBUG)
      System.Windows.Forms.MessageBox.Show("BackgroundWorker event handler.");
#endif
      //======================================================================
      // BackgroundWorker event handler
      bkg = new BackgroundWorker();
			bkg.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			bkg.ProgressChanged += backgroundWorker_ProgressChanged;
			bkg.WorkerReportsProgress = true;

#if (DEBUG)
      System.Windows.Forms.MessageBox.Show("Decryption start.");
#endif
      //======================================================================
      // Decryption start
      // 復号開始
      // Refer：http://stackoverflow.com/questions/4807152/sending-arguments-to-background-worker
      //======================================================================
      bkg.DoWork += (s, d) =>
			{
				decryption.Decrypt(
					s, d,
					Application.ExecutablePath, OutDirPath, DecryptionPassword, DecryptionPasswordBinary,
					DialogMessageForOverWrite);
			};

			bkg.RunWorkerAsync();
			
		}

    //======================================================================
    // Backgroundworker
    //======================================================================
#region

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
      if (e.ProgressPercentage > 0)
      {
        ArrayList MessageList = (ArrayList)e.UserState;

        progressBar1.Style = ProgressBarStyle.Continuous;
        progressBar1.Value = e.ProgressPercentage / 100;
        labelPercent.Text = ((float)e.ProgressPercentage / 100).ToString("F") + "%";

        // ((int)MessageList[0] == DECRYPTING )
        labelMessage.Text = (string)MessageList[1];
        this.Update();
      }
      else
      {
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.Value = 0;
        if (CurrentCultureName == "ja")
        {
          labelMessage.Text = "復号するための準備をしています...";
        }
        else
        {
          labelMessage.Text = "Getting ready for decryption...";
        }
        labelPercent.Text = "- %";
      }

    }

    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

#if (DEBUG)
      System.Windows.Forms.MessageBox.Show("backgroundWorker_RunWorkerCompleted");
#endif
                                
      if (e.Cancelled)
      {
        // Canceled
        labelPercent.Text = "- %";
        progressBar1.Value = 0;

        if (CurrentCultureName == "ja")
        {
          labelMessage.Text = "キャンセルされました。";
        }
        else
        {
          labelMessage.Text = "Canceled.";
        }
        return;

      }
      else if (e.Error != null)
      {
        //e.Error.Message;
        if (CurrentCultureName == "ja")
        {
          labelMessage.Text = "エラー：" + e.Error.Message;
        }
        else
        {
          labelMessage.Text = "Error occurred: " + e.Error.Message;
        }
        return;
      }
      else
      {
        string DialogTitleAlert;
        if (CurrentCultureName == "ja")
        {
          DialogTitleAlert = "エラー";
        }
        else
        {
          DialogTitleAlert = "Error";
        }

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

        switch (result.ReturnCode)
        {
          //-----------------------------------
          case DECRYPT_SUCCEEDED:

            labelPercent.Text = "100%";
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = progressBar1.Maximum;

            if (CurrentCultureName == "ja")
            {
              labelMessage.Text = "完了";
            }
            else
            {
              labelMessage.Text = "Completed";
            }
            return;

          //-----------------------------------
          case NOT_ATC_DATA:
            // エラー
            // 暗号化ファイルではありません。処理を中止します。
            //
            // Alert
            // The file is not encrypted file. The process is aborted.
            string DialogMessageNotAtcFile = "The file is not encrypted file. The process is aborted.";
            if (CurrentCultureName == "ja")
            {
              DialogMessageNotAtcFile = "暗号化ファイルではありません。処理を中止します。";
            }
            MessageBox.Show(DialogMessageNotAtcFile, DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (CurrentCultureName == "ja")
            {
              labelMessage.Text = "復号処理を中止しました。";
            }
            else
            {
              labelMessage.Text = "Process of decryption has been aborted.";
            }
            labelPercent.Text = "- %";

            return;

          //-----------------------------------
          case ATC_BROKEN_DATA:
            // エラー
            // 暗号化ファイル(.atc)は壊れています。処理を中止します。
            //
            // Alert
            // Encrypted file ( atc ) is broken. The process is aborted.
            string DialogMessageAtcFileBroken = "Encrypted file ( atc ) is broken. The process is aborted.";
            if (CurrentCultureName == "ja")
            {
              DialogMessageNotAtcFile = "暗号化ファイル(.atc)は壊れています。処理を中止します。";
            }
            MessageBox.Show(DialogMessageAtcFileBroken, DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (CurrentCultureName == "ja")
            {
              labelMessage.Text = "復号処理を中止しました。";
            }
            else
            {
              labelMessage.Text = "Process of decryption has been aborted.";
            }
            labelPercent.Text = "- %";

            return;

          //-----------------------------------
          case NO_DISK_SPACE:
            // エラー
            // ドライブに空き容量がありません。処理を中止します。
            //
            // Alert
            // No free space on the disk. The process is aborted.
            string DialogMessageNoDiskSpace = "No free space on the disk. The process is aborted.";
            if (CurrentCultureName == "ja")
            {
              DialogMessageNoDiskSpace = "ドライブに空き容量がありません。処理を中止します。";
            }
            MessageBox.Show(DialogMessageNoDiskSpace, DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (CurrentCultureName == "ja")
            {
              labelMessage.Text = "復号処理を中止しました。";
            }
            else
            {
              labelMessage.Text = "Process of decryption has been aborted.";
            }
            labelPercent.Text = "- %";

            return;

          //-----------------------------------
          case FILE_INDEX_NOT_FOUND:
            // エラー
            // 暗号化ファイル内部で、不正なファイルインデックスがありました。
            //
            // Alert
            // Internal file index is invalid in encrypted file.
            string DialogMessageFileIndexInvalid = "Internal file index is invalid in encrypted file.";
            if (CurrentCultureName == "ja")
            {
              DialogMessageFileIndexInvalid = "暗号化ファイル内部で、不正なファイルインデックスがありました。";
            }
            MessageBox.Show(DialogMessageFileIndexInvalid,
            DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (CurrentCultureName == "ja")
            {
              labelMessage.Text = "復号処理を中止しました。";
            }
            else
            {
              labelMessage.Text = "Process of decryption has been aborted.";
            }
            labelPercent.Text = "- %";

            return;

          //-----------------------------------
          case PASSWORD_TOKEN_NOT_FOUND:

          default:  // ERROR_UNEXPECTED
                    // エラー
                    // パスワードがちがうか、ファイルが破損している可能性があります。
                    // 復号できませんでした。
                    //
                    // Alert
                    // Password is invalid, or the encrypted file might have been broken.
                    // Decryption is aborted.
            string DialogMessageDecryptionError = "Password is invalid, or the encrypted file might have been broken.\nDecryption is aborted.";
            if (CurrentCultureName == "ja")
            {
              DialogMessageDecryptionError = "パスワードがちがうか、ファイルが破損している可能性があります。\n復号できませんでした。";
            }
            MessageBox.Show(DialogMessageDecryptionError, DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (LimitOfInputPassword > 1)
            {
              LimitOfInputPassword--;
              progressBar1.Visible = false;
              textBox1.Focus();
              textBox1.SelectAll();
              buttonDecrypt.Enabled = true;

              if (CurrentCultureName == "ja")
              {
                labelMessage.Text = "パスワードを入力してください：";
              }
              else
              {
                labelMessage.Text = "Input Password:";
              }

            }
            else
            {
              // パスワード回数を超過
              // Exceed times limit of inputting password
              if (decryption.fBroken == true)
              {
                BreakTheFile(Application.ExecutablePath);
              }
              else
              {
                Application.Exit();
              }

            }
            return;

        }// end switch();

      }

		}

#endregion

		//----------------------------------------------------------------------
		/// <summary>
		/// 上書きの確認をするダイアログ表示とユーザー応答内容の受け渡し
		/// Show dialog for confirming to overwrite, and passing user command. 
		/// </summary>
		//----------------------------------------------------------------------
		System.Threading.ManualResetEvent _busy = new System.Threading.ManualResetEvent(false);
		private void DialogMessageForOverWrite(int FileType, string FilePath)
		{
			if (decryption.TempOverWriteOption == 2)
			{	// Overwrite all
				return;
			}

			if (!bkg.IsBusy)
			{
				bkg.RunWorkerAsync();
				// Unblock the worker 
				_busy.Set();
			}

      // 問い合わせ
      // 以下のファイルはすでに存在しています。上書きして保存しますか？
      // 
      // Question
      // The following file already exists. Do you overwrite the files to save?
      //
      string DialogTitleQuestion = "Question";
      string labelComfirmToOverwriteFile = "The following file already exists. Do you overwrite the files to save?";
      if (CurrentCultureName == "ja")
      {
        DialogTitleQuestion = "問い合わせ";
        labelComfirmToOverwriteFile = "以下のファイルはすでに存在しています。上書きして保存しますか？";
      }
      if (MessageBox.Show(labelComfirmToOverwriteFile + "\n" + FilePath, 
        DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				decryption.TempOverWriteOption = 2;	//Overwrite all
			}			
			
			_busy.Reset();

		}

		/// <summary>
		/// Update to decrypt progress display.
		/// 復号処理の進捗状況を更新する
		/// </summary>
		/// <param name="size"></param>
		/// <param name="TotalSize"></param>
		private void UpdateDecryptProgress(Int64 TotalSize, Int64 TotalFileSize, int StatusCode, string MessageText)
		{
			float percent = (float)TotalSize / TotalFileSize;
			bkg.ReportProgress((int)(percent * 10000), MessageText);
		}

    /// <summary>
    /// パスワードファイルとして、ファイルからSHA-256ハッシュを取得してバイト列にする
    /// Get a string of the SHA-256 hash from a file such as the password file
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private byte[] GetPasswordFileHash3(string FilePath)
    {

      byte[] result = new byte[32];
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        ReadOnlyCollection<byte> hash = Sha256.HashFile(fs);

        for (int i = 0; i < 32; i++)
        {
          result[i] = hash[i];
        }

        return (result);
      }

      /*
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
      */

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// ファイルを破壊して、当該内部トークンを「破壊」ステータスに書き換える
    /// Break a specified file, and rewrite the token of broken status
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
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
					{	// Token is not found.
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
		


  }

}
