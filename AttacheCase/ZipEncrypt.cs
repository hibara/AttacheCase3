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
using System.Collections.Generic;
using Ionic.Zip;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Text;

namespace AttacheCase
{
  class ZipEncrypt
  {
    // Status code
    private const int ENCRYPT_SUCCEEDED = 1;  // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED = 2;  // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED  = 3;  // Delete is succeeded.
    private const int READY_FOR_ENCRYPT = 4;  // Getting ready for encryption or decryption.
    private const int READY_FOR_DECRYPT = 5;  // Getting ready for encryption or decryption.
    private const int ENCRYPTING        = 6;  // Ecrypting.
    private const int DECRYPTING        = 7;  // Decrypting.
    private const int DELETING          = 8;  // Deleting.

    // Error code
    private const int USER_CANCELED            = -1;     // User cancel.
    private const int ERROR_UNEXPECTED         = -100;
    private const int NOT_ATC_DATA             = -101;
    private const int ATC_BROKEN_DATA          = -102;
    private const int NO_DISK_SPACE            = -103;
    private const int FILE_INDEX_NOT_FOUND     = -104;
    private const int PASSWORD_TOKEN_NOT_FOUND = -105;
    private const int NOT_CORRECT_HASH_VALUE   = -106;

    // ZIP password encryption algorithm
    private const int ENCRYPTION_ALGORITHM_PKZIPWEAK = 0;
    private const int ENCRYPTION_ALGORITHM_WINZIPAES128 = 1;
    private const int ENCRYPTION_ALGORITHM_WINZIPAES256 = 2;

    private const int BUFFER_SIZE = 4096;

    private Int64 _TotalSize = 0;
    private Int64 _TotalFileSize = 0;

    // The number of files or folders to be compressed
    private int _NumberOfFiles = 0;
    public int NumberOfFiles
    {
      get { return this._NumberOfFiles; }
      set { this._NumberOfFiles = value; }
    }

    // Total number of files or folders to be compressed
    private int _TotalNumberOfFiles = 1;
    public int TotalNumberOfFiles
    {
      get { return this._TotalNumberOfFiles; }
      set { this._TotalNumberOfFiles = value; }
    }

    // Password
    private string _Password = "";
    public string Password
    {
      get { return this._Password; }
      set { this._Password = value; }
    }

    private List<string> _FileList;
    public List<string> FileList
    {
      get { return this._FileList; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="FilePaths"></param>
    /// <param name="OutFilePath"></param>
    /// <param name="Password"></param>
    /// <param name="PasswordBinary"></param>
    /// <param name="NewArchiveName"></param>
    /// <returns></returns>
    public Tuple<bool, int> Encrypt( object sender, DoWorkEventArgs e,
      string[] FilePaths, string OutFilePath, string Password, byte[] PasswordBinary, string NewArchiveName)
    {

      BackgroundWorker worker = sender as BackgroundWorker;

      byte[] bufferKey = new byte[32];

      e.Result = ENCRYPTING;

      _FileList = new List<string>();

      try
      {
        using (FileStream outfs = File.Open(OutFilePath, FileMode.Create, FileAccess.ReadWrite))
        {
          using (var zip = new ZipOutputStream(outfs))
          {
            zip.AlternateEncoding = System.Text.Encoding.GetEncoding("shift_jis");
            //zip.AlternateEncoding = System.Text.Encoding.UTF8;
            zip.AlternateEncodingUsage = Ionic.Zip.ZipOption.Always;

            // Password
            zip.Password = Password;

            // Encryption Algorithm
            switch (AppSettings.Instance.ZipEncryptionAlgorithm)
            {
              case ENCRYPTION_ALGORITHM_PKZIPWEAK:
                zip.Encryption = EncryptionAlgorithm.PkzipWeak;
                break;

              case ENCRYPTION_ALGORITHM_WINZIPAES128:
                zip.Encryption = EncryptionAlgorithm.WinZipAes128;
                break;

              case ENCRYPTION_ALGORITHM_WINZIPAES256:
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                break;                                     
            }

            // Compression level
            switch (AppSettings.Instance.CompressRate)
            {
              case 0:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                break;
              case 1:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                break;
              case 2:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level2;
                break;
              case 3:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level3;
                break;
              case 4:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level4;
                break;
              case 5:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level5;
                break;
              case 6:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                break;
              case 7:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level7;
                break;
              case 8:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level8;
                break;
              case 9:
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                break;
              default:
                break;
            }

            string ParentPath;
            ArrayList FileInfoList = new ArrayList();

            //----------------------------------------------------------------------
            // Put together files in one ( Save as the name ).
            // 複数ファイルを一つにまとめる（ファイルに名前をつけて保存）
            if (NewArchiveName != "")
            {
              ParentPath = NewArchiveName + "\\";
              zip.PutNextEntry(ParentPath);

            }

            //----------------------------------------------------------------------
            // When encrypt multiple files
            // 複数のファイルを暗号化する場合
            foreach (string FilePath in FilePaths)
            {
              ParentPath = Path.GetDirectoryName(FilePath) + "\\";

              if ((worker.CancellationPending == true))
              {
                e.Cancel = true;
                return Tuple.Create(false, USER_CANCELED);
              }

              //----------------------------------------------------------------------
              // 暗号化リストを生成（ファイル）
              // Create file to encrypt list ( File )
              //----------------------------------------------------------------------
              if (File.Exists(FilePath) == true)
              {
                ArrayList Item = GetFileInfo(ParentPath, FilePath);
                FileInfoList.Add(Item);
                //Item[0]       // TypeFlag ( Directory: 0, file: 1 ) 
                //Item[1]       // Absolute file path
                //Item[2]       // Relative file path
                //Item[3]       // File size 

                // files only
                if ((int)Item[0] == 1)
                {
                  _TotalFileSize += Convert.ToInt64(Item[3]);
                  _FileList.Add((string)Item[1]);
                }

              }
              //----------------------------------------------------------------------
              // 暗号化リストを生成（ディレクトリ）
              // Create file to encrypt list ( Directory )
              //----------------------------------------------------------------------
              else
              {
                // Directory
                foreach (ArrayList Item in GetFileList(ParentPath, FilePath))
                {
                  if ((worker.CancellationPending == true))
                  {
                    e.Cancel = true;
                    return Tuple.Create(false, USER_CANCELED);
                  }

                  if (NewArchiveName != "")
                  {
                    Item[2] = NewArchiveName + "\\" + Item[2] + "\\";
                  }

                  FileInfoList.Add(Item);

                  if (Convert.ToInt32(Item[0]) == 1)
                  { // files only
                    _TotalFileSize += Convert.ToInt64(Item[3]); // File size
                  }

                  _FileList.Add((string)Item[1]);

                }// end foreach (ArrayList Item in GetFilesList(ParentPath, FilePath));

              }// if (File.Exists(FilePath) == true);

            }// end foreach (string FilePath in FilePaths);

#if (DEBUG)
            string DeskTopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string TempFilePath = Path.Combine(DeskTopPath, "zip_encrypt.txt");
            using (StreamWriter sw = new StreamWriter(TempFilePath, false, System.Text.Encoding.UTF8))
            {
              foreach (ArrayList Item in FileInfoList)
              {
                string OneLine = Item[0] + "\t" + Item[1] + "\t" + Item[2] + "\t" + Item[3] + "\n";
                sw.Write(OneLine);
              }
            }
#endif
            _NumberOfFiles = 0;
            _TotalNumberOfFiles = FileInfoList.Count;
            string MessageText = "";
            ArrayList MessageList = new ArrayList();
            float percent;

            foreach (ArrayList Item in FileInfoList)
            {
              //Item[0]       // TypeFlag ( Directory: 0, file: 1 ) 
              //Item[1]       // Absolute file path
              //Item[2]       // Relative file path
              //Item[3]       // File size 
              zip.PutNextEntry((string)Item[2]);
              _NumberOfFiles++;

              //-----------------------------------
              // Directory
              if ((int)Item[0] == 0)
              {
                if (_TotalNumberOfFiles > 1)
                {
                  MessageText = (string)Item[1] + " ( " + _NumberOfFiles.ToString() + " / " + _TotalNumberOfFiles.ToString() + " )";
                }
                else
                {
                  MessageText = (string)Item[1];
                }

                percent = ((float)_TotalSize / _TotalFileSize);
                MessageList = new ArrayList();
                MessageList.Add(ENCRYPTING);
                MessageList.Add(MessageText);
                worker.ReportProgress((int)(percent * 10000), MessageList);

                if (worker.CancellationPending == true)
                {
                  e.Cancel = true;
                  return Tuple.Create(false, USER_CANCELED);
                }

              }
              else
              {
                //-----------------------------------
                // File
                using (FileStream fs = File.Open((string)Item[1], FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write))
                {
                  byte[] buffer = new byte[BUFFER_SIZE];
                  int len;

                  while ((len = fs.Read(buffer, 0, buffer.Length)) > 0)
                  {

                    zip.Write(buffer, 0, len);

                    if (_TotalNumberOfFiles > 1)
                    {
                      MessageText = (string)Item[1] + " ( " + _NumberOfFiles.ToString() + " / " + _TotalNumberOfFiles.ToString() + " )";
                    }
                    else
                    {
                      MessageText = (string)Item[1];
                    }

                    _TotalSize += len;
                    percent = ((float)_TotalSize / _TotalFileSize);
                    MessageList = new ArrayList();
                    MessageList.Add(ENCRYPTING);
                    MessageList.Add(MessageText);
                    worker.ReportProgress((int)(percent * 10000), MessageList);

                    if (worker.CancellationPending == true)
                    {
                      e.Cancel = true;
                      return Tuple.Create(false, USER_CANCELED);
                    }

                  }// end while ((len = fs.Read(buffer, 0, buffer.Length)) > 0);

                }// end using (FileStream fs = File.Open((string)Item[1], FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write));

              }// end if ((int)Item[0] == 0);

            }// end foreach (ArrayList Item in FileInfoList);

          }// using (var zip = new ZipOutputStream(outfs));

        }// end using (FileStream outfs = File.Open(OutFilePath, FileMode.Create, FileAccess.ReadWrite));


      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message);
        e.Result = ERROR_UNEXPECTED;
        return Tuple.Create(true, ERROR_UNEXPECTED);
      }

      //Encryption succeed.
      e.Result = ENCRYPT_SUCCEEDED;
      return Tuple.Create(true, ENCRYPT_SUCCEEDED);

    }

    /// <summary>
    /// 指定したルートディレクトリのファイルリストを並列処理で取得する
    /// Get a list of files from the specified root directory in parallel processing
    /// </summary>
    /// <remarks>http://stackoverflow.com/questions/2106877/is-there-a-faster-way-than-this-to-find-all-the-files-in-a-directory-and-all-sub</remarks>
    /// <param name="fileSearchPattern"></param>
    /// <param name="rootFolderPath"></param>
    /// <returns></returns>
    private static IEnumerable<ArrayList> GetFileList(string ParentPath, string rootFolderPath)
    {
      Queue<string> pending = new Queue<string>();
      pending.Enqueue(rootFolderPath);
      string[] tmp;
      while (pending.Count > 0)
      {
        rootFolderPath = pending.Dequeue();
        //-----------------------------------
        // Directory
        ArrayList list = new ArrayList();
        list = GetDirectoryInfo(ParentPath, rootFolderPath);
        yield return list;
        tmp = Directory.GetFiles(rootFolderPath);
        for (int i = 0; i < tmp.Length; i++)
        {
          //-----------------------------------
          // File
          list = GetFileInfo(ParentPath, (string)tmp[i]);
          yield return list;
        }
        tmp = Directory.GetDirectories(rootFolderPath);
        for (int i = 0; i < tmp.Length; i++)
        {
          pending.Enqueue(tmp[i]);
        }
      }
    }

    /// <summary>
    /// 指定のディレクトリ情報をリストで取得する
    /// Get the information of specific DIRECTORY in the ArrayList
    /// </summary>
    /// <param name="ParentPath"></param>
    /// <param name="DirPath"></param>
    /// <returns></returns>
    private static ArrayList GetDirectoryInfo(string ParentPath, string DirPath)
    {
      ArrayList List = new ArrayList();
      DirectoryInfo di = new DirectoryInfo(ParentPath + Path.GetFileName(DirPath));
      List.Add(0);                                      // Directory flag
      List.Add(DirPath);                                // Absolute file path
      List.Add(DirPath.Replace(ParentPath, "") + "\\"); // (string)Remove parent directory path.
      List.Add(0);                                      // File size = 0
      //List.Add((int)di.Attributes);                     // (int)File attribute
      /*
        * TTimeStamp = record
        *  Time: Integer;      { Number of milliseconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      //List.Add((Int32)(di.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      //List.Add(di.LastWriteTime.TimeOfDay.TotalSeconds);
      //List.Add((Int32)(di.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      //List.Add(di.CreationTime.TimeOfDay.TotalSeconds);
      return (List);
    }

    /// <summary>
    /// 指定のファイル情報をリストで取得する
    /// Get the information of specific FILE in the ArrayList
    /// </summary>
    /// <param name="ParentPath"></param>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private static ArrayList GetFileInfo(string ParentPath, string FilePath)
    {
      ArrayList List = new ArrayList();
      string AbsoluteFilePath = FilePath;
      FileInfo fi = new FileInfo(FilePath);
      List.Add(1);                                // File flag
      List.Add(FilePath);                         // Absolute file path
      List.Add(FilePath.Replace(ParentPath, "")); // (string)Remove parent directory path.
      List.Add(fi.Length);                        // (int64)File size
      //List.Add((int)fi.Attributes);               // (int)File attribute
      /*
        * TTimeStamp = record
        *  Time: Integer;      { Number of milliseconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      //List.Add((Int32)(fi.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      //List.Add(fi.LastWriteTime.TimeOfDay.TotalSeconds);
      //List.Add((Int32)(fi.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      //List.Add(fi.CreationTime.TimeOfDay.TotalSeconds);
      //List.Add((string)GetSha256FromFile(AbsoluteFilePath));
      return (List);
    }



  }
}
