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
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AttacheCase
{
  public partial class FileEncrypt3
  {
    // Status code
    private const int ENCRYPT_SUCCEEDED  = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED  = 2; // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED   = 3; // Delete is succeeded.
    private const int READY_FOR_ENCRYPT  = 4; // Getting ready for encryption or decryption.
    private const int READY_FOR_DECRYPT  = 5; // Getting ready for encryption or decryption.
    private const int ENCRYPTING         = 6; // Ecrypting.
    private const int DECRYPTING         = 7; // Decrypting.
    private const int DELETING           = 8; // Deleting.

    // Error code
    private const int USER_CANCELED            = -1;   // User cancel.
    private const int ERROR_UNEXPECTED         = -100;
    private const int NOT_ATC_DATA             = -101;
    private const int ATC_BROKEN_DATA          = -102;
    private const int NO_DISK_SPACE            = -103;
    private const int FILE_INDEX_NOT_FOUND     = -104;
    private const int PASSWORD_TOKEN_NOT_FOUND = -105;
    private const int NOT_CORRECT_HASH_VALUE   = -106;

    private byte[] buffer;
    private const int BUFFER_SIZE = 4096;

    // Header data variables
    private static bool fBrocken = false;
    private const string STRING_TOKEN_NORMAL = "_AttacheCaseData";
    private const string STRING_TOKEN_BROKEN = "_Atc_Broken_Data";
    private const int DATA_FILE_VERSION = 130;
    private const string AtC_ENCRYPTED_TOKEN = "atc3";

    //Encrypted header data size
    private int _AtcHeaderSize = 0;
    private Int64 _TotalSize = 0;
    private Int64 _TotalFileSize = 0;
    private Int64 _StartPos = 0;

    // The number of files or folders to be encrypted
    private int _NumberOfFiles = 0;
    public int NumberOfFiles
    {
      get { return this._NumberOfFiles; }
      set { this._NumberOfFiles = value; }
    }

    // Total number of files or folders to be encrypted
    private int _TotalNumberOfFiles = 1;
    public int TotalNumberOfFiles
    {
      get { return this._TotalNumberOfFiles; }
      set { this._TotalNumberOfFiles = value; }
    }

    // Set number of times to input password in encrypt files:
    private char _MissTypeLimits = (char)3;
    public char MissTypeLimits
    {
      get { return this._MissTypeLimits; }
      set { this._MissTypeLimits = value; }
    }

    // Self-executable file
    private bool _fExecutable = false;
    public bool fExecutable
    {
      get { return this._fExecutable; }
      set { this._fExecutable = value; }
    }

    // Set the timestamp of encryption file to original files or directories
    private bool _fKeepTimeStamp = false;
    public bool fKeepTimeStamp
    {
      get { return this._fKeepTimeStamp; }
      set { this._fKeepTimeStamp = value; }
    }

    private string _AtcFilePath = "";
    public string AtcFilePath
    {
      get { return this._AtcFilePath; }
    }

    private List<string> _FileList;
    public List<string> FileList
    {
      get { return this._FileList; }
    }

    public FileEncrypt3()
    {
    }

    /// <summary>
		/// Multiple files or directories is encrypted by AES (exactly Rijndael) to use password string.
    /// 複数のファイル、またはディレクトリをAES（正確にはRijndael）を使って指定のパスワードで暗号化する
    /// </summary>
    /// <param name="FilePath">File path or directory path is encrypted</param>
    /// <param name="OutFilePath">Output encryption file name</param>
    /// <param name="Password">Encription password string</param>
    /// <returns>Encryption success(true) or failed(false)</returns>
		public Tuple<bool, int> Encrypt(
      object sender, DoWorkEventArgs e,
      string[] FilePaths, string OutFilePath,
      string Password, byte[] PasswordBinary,
      string NewArchiveName)
    {

      _AtcFilePath = OutFilePath;

      BackgroundWorker worker = sender as BackgroundWorker;
      // Create Header data.
      ArrayList MessageList = new ArrayList();
      MessageList.Add(READY_FOR_ENCRYPT);
      MessageList.Add(Path.GetFileName(OutFilePath));
      worker.ReportProgress(0, MessageList);

      _FileList = new List<string>();
      byte[] byteArray = null;

      // Salt
      Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, 8, 1000);
      byte[] salt = deriveBytes.Salt;
      byte[] key = deriveBytes.GetBytes(32);
      byte[] iv = deriveBytes.GetBytes(32);

      using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
      {
        // 自己実行形式ファイル（Self-executable file）
        if (_fExecutable == true)
        {
          ExeOutFileSize = rawData.Length;
          outfs.Write(rawData, 0, (int)ExeOutFileSize);
        }

        _StartPos = outfs.Seek(0, SeekOrigin.End);

        // Application version
        Version ver = ApplicationInfo.Version;
        short vernum = Int16.Parse(ver.ToString().Replace(".", ""));
        byteArray = BitConverter.GetBytes(vernum);
        outfs.Write(byteArray, 0, 2);
        // Input password limit
        byteArray = BitConverter.GetBytes(_MissTypeLimits);
        outfs.Write(byteArray, 0, 1);
        // Exceed the password input limit, destroy the file?
        byteArray = BitConverter.GetBytes(fBrocken);
        outfs.Write(byteArray, 0, 1);
        // Token that this is the AttacheCase file
        byteArray = Encoding.ASCII.GetBytes(STRING_TOKEN_NORMAL);
        outfs.Write(byteArray, 0, 16);
        // File sub version
        byteArray = BitConverter.GetBytes(DATA_FILE_VERSION);
        outfs.Write(byteArray, 0, 4);
        // The size of encrypted Atc header size ( reserved ) 
        byteArray = BitConverter.GetBytes((int)0);
        outfs.Write(byteArray, 0, 4);
        // Salt
        outfs.Write(salt, 0, 8);
 
        // Cipher text header.
        using (MemoryStream ms = new MemoryStream())
        {
          byteArray = Encoding.ASCII.GetBytes(AtC_ENCRYPTED_TOKEN + "\n");
          ms.Write(byteArray, 0, byteArray.Length);

          int FileNumber = 0;
          string ParentPath;
          ArrayList FileInfoList = new ArrayList();

          //----------------------------------------------------------------------
          // Put together files in one ( Save as the name ).
          // 複数ファイルを一つにまとめる（ファイルに名前をつけて保存）
          if (NewArchiveName != "")
          {
            // Now time
            DateTime dtNow = new DateTime();
            FileInfoList.Add("0:" +                              // File number
                             NewArchiveName + "\\\t" +           // File name
                             "0" + "\t" +                        // File size 
                             "16" + "\t" +                       // File attribute
                             dtNow.Date.Subtract(new DateTime(1, 1, 1)).TotalDays + "\t" +  // Last write date
                             dtNow.TimeOfDay.TotalSeconds + "\t" + // Last write time
                             dtNow.Date.Subtract(new DateTime(1, 1, 1)).TotalDays + "\t" +  // Creation date
                             dtNow.TimeOfDay.TotalSeconds);       // Creation time             
            FileNumber++;
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
              FileInfoList.Add(FileNumber.ToString() + ":" + // File number
                                //Item[0] + "\t" +           // TypeFlag ( Directory: 0, file: 1 ) 
                                //Item[1] + "\t" +           // Absolute file path
                                Item[2] + "\t" +             // Relative file path
                                Item[3].ToString() + "\t" +  // File size 
                                Item[4].ToString() + "\t" +  // File attribute
                                Item[5].ToString() + "\t" +  // Last write date
                                Item[6].ToString() + "\t" +  // Last write time
                                Item[7].ToString() + "\t" +  // Creation date
                                Item[8].ToString() + "\t" +  // Creation time             
                                Item[9].ToString());         // SHA-256 Hash string      

              // files only
              if (Convert.ToInt32(Item[0]) == 1)
              {
                // Files list for encryption
                _FileList.Add(Item[1].ToString());  // Absolute file path
                                                    // Total file size
                _TotalFileSize += Convert.ToInt64(Item[3]);
              }

              FileNumber++;

            }
            //----------------------------------------------------------------------
            // 暗号化リストを生成（ディレクトリ）
            // Create file to encrypt list ( Directory )
            //----------------------------------------------------------------------
            else
            {
              // Directory
              _FileList.Add(FilePath);  // Absolute file path

              foreach (ArrayList Item in GetFileList(ParentPath, FilePath))
              {
                if ((worker.CancellationPending == true))
                {
                  e.Cancel = true;
                  return Tuple.Create(false, USER_CANCELED);
                }

                if (NewArchiveName != "")
                {
                  Item[2] = NewArchiveName + "\\" + Item[2];
                }

                if ((int)Item[0] == 0)
                {
                  FileInfoList.Add(FileNumber.ToString() + ":" + // File number
                                    Item[2] + "\t" +             // Relative file path
                                    Item[3].ToString() + "\t" +  // File size 
                                    Item[4].ToString() + "\t" +  // File attribute
                                    Item[5].ToString() + "\t" +  // Last write date
                                    Item[6].ToString() + "\t" +  // Last write time
                                    Item[7].ToString() + "\t" +  // Creation date
                                    Item[8].ToString());         // Creation time
                }
                else
                {
                  FileInfoList.Add(FileNumber.ToString() + ":" + // File number
                                    Item[2] + "\t" +             // Relative file path
                                    Item[3].ToString() + "\t" +  // File size 
                                    Item[4].ToString() + "\t" +  // File attribute
                                    Item[5].ToString() + "\t" +  // Last write date
                                    Item[6].ToString() + "\t" +  // Last write time
                                    Item[7].ToString() + "\t" +  // Creation date
                                    Item[8].ToString() + "\t" +  // Creation time             
                                    Item[9].ToString());         // SHA-256 hash             
                }

                if (Convert.ToInt32(Item[0]) == 1)
                { // files only
                  // Files list for encryption
                  _FileList.Add(Item[1].ToString());  // Absolute file path
                                                      // Total file size
                  _TotalFileSize += Convert.ToInt64(Item[3]);
                }
                else
                { // Directory
                  _FileList.Add(Item[1].ToString());	// Absolute file path
                }

                FileNumber++;


              }// end foreach (ArrayList Item in GetFilesList(ParentPath, FilePath));

            }// if (File.Exists(FilePath) == true);

          }// end foreach (string FilePath in FilePaths);

          //----------------------------------------------------------------------
          // Check the disk space
          //----------------------------------------------------------------------
          string RootDriveLetter = Path.GetPathRoot(OutFilePath).Substring(0, 1);
          DriveInfo drive = new DriveInfo(RootDriveLetter);
          // The drive is not available, or not enough free space.
          if (drive.IsReady == false || drive.AvailableFreeSpace < _TotalFileSize)
          {
            e.Result = NO_DISK_SPACE;
            // not available free space
            return Tuple.Create(false, NO_DISK_SPACE);
          }

          //----------------------------------------------------------------------
          // Create header data

          string[] FileInfoText = (string[])FileInfoList.ToArray(typeof(string));

          byteArray = Encoding.UTF8.GetBytes(string.Join("\n", FileInfoText));
          ms.Write(byteArray, 0, byteArray.Length);

#if (DEBUG)
          //Output text file of header contents for debug.
          Int64 NowPosition = ms.Position;
          ms.Position = 0;
          //Save to Desktop folder.
          string AppDirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
          string HeaderTextFilePath = Path.Combine(AppDirPath, "encrypt_header.txt");
          FileStream fsDebug = new FileStream(HeaderTextFilePath, FileMode.Create, FileAccess.Write);
          ms.WriteTo(fsDebug);
          fsDebug.Close();
          ms.Position = NowPosition;
#endif
          // The Header of MemoryStream is encrypted
          using (Rijndael aes = new RijndaelManaged())
          {
            aes.BlockSize = 256;              // BlockSize = 16bytes
            aes.KeySize = 256;                // KeySize = 16bytes
            aes.Mode = CipherMode.CBC;        // CBC mode
            //aes.Padding = PaddingMode.Zeros;  // Padding mode is "None".

            aes.Key = key;
            aes.IV = iv;

            ms.Position = 0;
            //Encryption interface.
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
            {
              //----------------------------------------------------------------------
              // ヘッダーの暗号化
              //----------------------------------------------------------------------
              int len = 0;
              _AtcHeaderSize = 0;		// exclude IV of header
              buffer = new byte[BUFFER_SIZE];
              while ((len = ms.Read(buffer, 0, BUFFER_SIZE)) > 0)
              {
                cse.Write(buffer, 0, len);
                _AtcHeaderSize += len;
              }
            }

          }// end using (Rijndael aes = new RijndaelManaged());

        }// end  using (MemoryStream ms = new MemoryStream());

      }// end using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write));


      //----------------------------------------------------------------------
      // 本体データの暗号化
      //----------------------------------------------------------------------
      using (FileStream outfs = new FileStream(OutFilePath, FileMode.OpenOrCreate, FileAccess.Write))
      {
        byteArray = new byte[4];
        // Back to current positon of 'encrypted file size'
        if (_fExecutable == true)
        {
          outfs.Seek(ExeOutFileSize + 24, SeekOrigin.Begin);  // self executable file
        }
        else
        {
          outfs.Seek(24, SeekOrigin.Begin);
        }

        byteArray = BitConverter.GetBytes(_AtcHeaderSize);
        outfs.Write(byteArray, 0, 4);

        // Out file stream postion move to end
        outfs.Seek(0, SeekOrigin.End);

        // The Header of MemoryStream is encrypted
        using (Rijndael aes = new RijndaelManaged())
        {
          aes.BlockSize = 256;              // BlockSize = 16bytes
          aes.KeySize = 256;                // KeySize = 16bytes
          aes.Mode = CipherMode.CBC;        // CBC mode
          aes.Padding = PaddingMode.Zeros;  // Padding mode

          aes.Key = key;
          aes.IV = iv;

          // Encryption interface.
          ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
          using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
          {
            Ionic.Zlib.CompressionLevel flv = Ionic.Zlib.CompressionLevel.Default;
            switch (AppSettings.Instance.CompressRate)
            {
              case 0:
                flv = Ionic.Zlib.CompressionLevel.Level0;
                break;
              case 1:
                flv = Ionic.Zlib.CompressionLevel.Level1;
                break;
              case 2:
                flv = Ionic.Zlib.CompressionLevel.Level2;
                break;
              case 3:
                flv = Ionic.Zlib.CompressionLevel.Level3;
                break;
              case 4:
                flv = Ionic.Zlib.CompressionLevel.Level4;
                break;
              case 5:
                flv = Ionic.Zlib.CompressionLevel.Level5;
                break;
              case 6:
                flv = Ionic.Zlib.CompressionLevel.Level6;
                break;
              case 7:
                flv = Ionic.Zlib.CompressionLevel.Level7;
                break;
              case 8:
                flv = Ionic.Zlib.CompressionLevel.Level8;
                break;
              case 9:
                flv = Ionic.Zlib.CompressionLevel.Level9;
                break;
            }

            using (Ionic.Zlib.DeflateStream ds = new Ionic.Zlib.DeflateStream(cse, Ionic.Zlib.CompressionMode.Compress, flv))
            {
              int len = 0;
              foreach (string path in _FileList)
              {
                // Only file is encrypted
                if (File.Exists(path) == true)
                {
                  buffer = new byte[BUFFER_SIZE];
                  using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                  {
                    len = 0;
                    while ((len = fs.Read(buffer, 0, BUFFER_SIZE)) > 0)
                    {
                      cse.Write(buffer, 0, len);
                      _TotalSize += len;

                      string MessageText = "";
                      if (_TotalNumberOfFiles > 1)
                      {
                        MessageText = path + " ( " + _NumberOfFiles.ToString() + " files/ " + _TotalNumberOfFiles.ToString() + " folders )";
                      }
                      else
                      {
                        MessageText = path;
                      }
                      float percent = ((float)_TotalSize / _TotalFileSize);
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
                  }

                } // end if (File.Exists(path) == true);

              } // end foreach (string path in _FileList);

              /*
              for (int i = 0; i < 10; i++)
              {
                Random r = new Random();
                byteArray = new byte[BUFFER_SIZE];
                r.NextBytes(byteArray);
                cse.Write(buffer, 0, BUFFER_SIZE);
              }
              */

            } // end using ( Ionic.Zlib.DeflateStream ds);

          } // end using (CryptoStream cse);

        } // end using (Rijndael aes = new RijndaelManaged());

      } // end using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write));

      // Self-executable file
      if (fExecutable == true)
      {
        using (FileStream outfs = new FileStream(OutFilePath, FileMode.Open, FileAccess.Write))
        {
          Int64 DataSize = outfs.Seek(0, SeekOrigin.End);
          DataSize = DataSize - _StartPos;
          byteArray = BitConverter.GetBytes(DataSize);
          outfs.Write(byteArray, 0, sizeof(Int64));
        }
      }

      // Set the timestamp of encryption file to original files or directories
      if (_fKeepTimeStamp == true)
      {
        DateTime dtCreate = File.GetCreationTime((string)AppSettings.Instance.FileList[0]);
        DateTime dtUpdate = File.GetLastWriteTime((string)AppSettings.Instance.FileList[0]);
        DateTime dtAccess = File.GetLastAccessTime((string)AppSettings.Instance.FileList[0]);
        File.SetCreationTime(OutFilePath, dtCreate);
        File.SetLastWriteTime(OutFilePath, dtUpdate);
        File.SetLastAccessTime(OutFilePath, dtAccess);
      }

      //Encryption succeed.
      e.Result = ENCRYPT_SUCCEEDED;
      return Tuple.Create(true, ENCRYPT_SUCCEEDED);


    } // encrypt();


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
      List.Add((int)di.Attributes);                     // (int)File attribute
      /*
        * TTimeStamp = record
        *  Time: Integer;      { Number of milliseconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      List.Add((Int32)(di.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(di.LastWriteTime.TimeOfDay.TotalSeconds);
      List.Add((Int32)(di.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(di.CreationTime.TimeOfDay.TotalSeconds);
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
      List.Add((int)fi.Attributes);               // (int)File attribute
      /*
        * TTimeStamp = record
        *  Time: Integer;      { Number of milliseconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      List.Add((Int32)(fi.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(fi.LastWriteTime.TimeOfDay.TotalSeconds);
      List.Add((Int32)(fi.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(fi.CreationTime.TimeOfDay.TotalSeconds);
      List.Add((string)GetSha256FromFile(AbsoluteFilePath));
      return (List);
    }

    /// <summary>
    /// 計算してチェックサム（SHA-256）を得る
    /// Get a check sum (SHA-256) to calculate
    /// </summary>
    /// <param name="dataToCalculate"></param>
    /// <returns></returns>
    public static string GetSha256FromFile(string FilePath)
    {
      byte[] bytesArray = null;
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        using (SHA256CryptoServiceProvider sha1 = new SHA256CryptoServiceProvider())
        {
          bytesArray = sha1.ComputeHash(fs);
        }
      }

      StringBuilder result = new StringBuilder();
      result.Capacity = 32;
      foreach (byte b in bytesArray)
      {
        result.Append(b.ToString());
      }

      return (result.ToString());

    }



  }// end class FileEncrypt()


}// end namespace

