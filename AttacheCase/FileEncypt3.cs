﻿//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016-2019  Mitsuhiro Hibara
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
using System.Collections.ObjectModel;
using Sha2;

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
    private const int INVALID_FILE_PATH        = -107;
    private const int OS_DENIES_ACCESS         = -108;
    private const int DATA_NOT_FOUND           = -109;
    private const int DIRECTORY_NOT_FOUND      = -110;
    private const int DRIVE_NOT_FOUND          = -111;
    private const int FILE_NOT_LOADED          = -112;
    private const int FILE_NOT_FOUND           = -113;
    private const int PATH_TOO_LONG            = -114;
    private const int IO_EXCEPTION             = -115;

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
    //private Int64 _TotalFileSize = 0;
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
    // ATC file ( encrypted file name ) path to output
    private string _AtcFilePath = "";
    public string AtcFilePath
    {
      get { return this._AtcFilePath; }
    }
    // List of files and folders for encryption
    private List<string> _FileList;
    public List<string> FileList
    {
      get { return this._FileList; }
    }

    //----------------------------------------------------------------------
    // The return value of error ( ReadOnly)
    //----------------------------------------------------------------------
    // Input "Error code" for value
    private int _ReturnCode = -1;
    public int ReturnCode
    {
      get { return this._ReturnCode; }
    }
    // File path that caused the error
    private string _ErrorFilePath = "";
    public string ErrorFilePath
    {
      get { return this._ErrorFilePath; }
    }
    // Drive name to decrypt
    private string _DriveName = "";
    public string DriveName
    {
      get { return this._DriveName; }
    }
    // Total file size of files to be encrypted
    private Int64 _TotalFileSize = -1;
    public Int64 TotalFileSize
    {
      get { return this._TotalFileSize; }
    }
    // Free space on the drive to encrypt the file
    private Int64 _AvailableFreeSpace = -1;
    public Int64 AvailableFreeSpace
    {
      get { return this._AvailableFreeSpace; }
    }
    // Error message by the exception
    private string _ErrorMessage = "";
    public string ErrorMessage
    {
      get { return this._ErrorMessage; }
    }

    // Constructor
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
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:オブジェクトを複数回破棄しない")]
    public bool Encrypt(
      object sender, DoWorkEventArgs e,
      string[] FilePaths, string OutFilePath,
      string Password, byte[] PasswordBinary,
      string NewArchiveName)
    {

      _AtcFilePath = OutFilePath;

      BackgroundWorker worker = sender as BackgroundWorker;

      // The timestamp of original file
      DateTime dtCreate = File.GetCreationTime(FilePaths[0]);
      DateTime dtUpdate = File.GetLastWriteTime(FilePaths[0]);
      DateTime dtAccess = File.GetLastAccessTime(FilePaths[0]);

      // Create Header data.
      ArrayList MessageList = new ArrayList
      {
        READY_FOR_ENCRYPT,
        Path.GetFileName(_AtcFilePath)
      };
      worker.ReportProgress(0, MessageList);

      _FileList = new List<string>();
      byte[] byteArray = null;

      // Salt
      Rfc2898DeriveBytes deriveBytes;
      if(PasswordBinary == null)
      { // String Password
        deriveBytes = new Rfc2898DeriveBytes(Password, 8, 1000);
      }
      else
      { // Binary Password
        byte[] random_salt = new byte[8];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(random_salt);
        deriveBytes = new Rfc2898DeriveBytes(PasswordBinary, random_salt, 1000);
      }
      byte[] salt = deriveBytes.Salt;
      byte[] key = deriveBytes.GetBytes(32);
      byte[] iv = deriveBytes.GetBytes(32);

      try
      {
        using (FileStream outfs = new FileStream(_AtcFilePath, FileMode.Create, FileAccess.Write))
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

              NewArchiveName = NewArchiveName + "\\";

              // Now time
              DateTime dtNow = new DateTime();
              FileInfoList.Add("0:" +                              // File number
                               NewArchiveName + "\t" +             // File name
                               "0" + "\t" +                        // File size 
                               "16" + "\t" +                       // File attribute
                               dtNow.Date.Subtract(new DateTime(1, 1, 1)).TotalDays + "\t" +  // Last write date
                               dtNow.TimeOfDay.TotalSeconds + "\t" + // Last write time
                               dtNow.Date.Subtract(new DateTime(1, 1, 1)).TotalDays + "\t" +  // Creation date
                               dtNow.TimeOfDay.TotalSeconds + "\t" + // Creation time
                               "" + "\t" + 
                               // ver.3.2.3.0 ~
                               DateTime.UtcNow.ToString() + "\t" +
                               DateTime.UtcNow.ToString());
              FileNumber++;
            }

            //----------------------------------------------------------------------
            // When encrypt multiple files
            // 複数のファイルを暗号化する場合
            foreach (string FilePath in FilePaths)
            {
              ParentPath = Path.GetDirectoryName(FilePath);

              if (ParentPath.EndsWith("\\") == false)  // In case of 'C:\\' root direcroy.
              {
                ParentPath = ParentPath + "\\";
              }

              if ((worker.CancellationPending == true))
              {
                e.Cancel = true;
                return (false);
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
                                  NewArchiveName +
                                  Item[2] + "\t" +             // Relative file path
                                  Item[3].ToString() + "\t" +  // File size 
                                  Item[4].ToString() + "\t" +  // File attribute
                                  Item[5].ToString() + "\t" +  // Last write date
                                  Item[6].ToString() + "\t" +  // Last write time
                                  Item[7].ToString() + "\t" +  // Creation date
                                  Item[8].ToString() + "\t" +  // Creation time             
                                  Item[9].ToString() + "\t" +  // SHA-256 Hash string
                                  // ver.3.2.3.0 ~      
                                  Item[10].ToString() + "\t" + // Last write date time(UTC)      
                                  Item[11].ToString());        // Creation date time(UTC)       

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
                    return (false);
                  }

                  if (NewArchiveName != "")
                  {
                    Item[2] = NewArchiveName + "\\" + Item[2];
                  }

                  if ((int)Item[0] == 0)
                  { // Directory
                    FileInfoList.Add(FileNumber.ToString() + ":" + // File number
                                      NewArchiveName + Item[2] + "\t" +             // Relative file path
                                      Item[3].ToString() + "\t" +  // File size 
                                      Item[4].ToString() + "\t" +  // File attribute
                                      Item[5].ToString() + "\t" +  // Last write date
                                      Item[6].ToString() + "\t" +  // Last write time
                                      Item[7].ToString() + "\t" +  // Creation date
                                      Item[8].ToString() + "\t" +  // Creation time
                                      "" + "\t" +
                                      // ver.3.2.3.0 ～
                                      Item[10].ToString() + "\t" +  // Last write date time(UTC)
                                      Item[11].ToString());        // Creation date date time(UTC)
                  }
                  else
                  { // File
                    FileInfoList.Add(FileNumber.ToString() + ":" + // File number
                                      NewArchiveName + Item[2] + "\t" +             // Relative file path
                                      Item[3].ToString() + "\t" +  // File size 
                                      Item[4].ToString() + "\t" +  // File attribute
                                      Item[5].ToString() + "\t" +  // Last write date
                                      Item[6].ToString() + "\t" +  // Last write time
                                      Item[7].ToString() + "\t" +  // Creation date
                                      Item[8].ToString() + "\t" +  // Creation time             
                                      Item[9].ToString() + "\t" +  // SHA-256 hash
                                      // ver.3.2.3.0 ～
                                      Item[10].ToString() + "\t" + // Last write date time(UTC)
                                      Item[11].ToString());        // Creation date date time(UTC)
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
                    _FileList.Add(Item[1].ToString());  // Absolute file path
                  }

                  FileNumber++;

                }// end foreach (ArrayList Item in GetFilesList(ParentPath, FilePath));

              }// if (File.Exists(FilePath) == true);

            }// end foreach (string FilePath in FilePaths);

            //----------------------------------------------------------------------
            // Check the disk space
            //----------------------------------------------------------------------
            string RootDriveLetter = Path.GetPathRoot(_AtcFilePath).Substring(0, 1);

            if (RootDriveLetter == "\\")
            {
              // Network
            }
            else
            {
              DriveInfo drive = new DriveInfo(RootDriveLetter);

              DriveType driveType = drive.DriveType;
              switch (driveType)
              {
                case DriveType.CDRom:
                case DriveType.NoRootDirectory:
                case DriveType.Unknown:
                  break;
                case DriveType.Fixed:     // Local Drive
                case DriveType.Network:   // Mapped Drive
                case DriveType.Ram:       // Ram Drive
                case DriveType.Removable: // Usually a USB Drive

                  // The drive is not available, or not enough free space.
                  if (drive.IsReady == false || drive.AvailableFreeSpace < _TotalFileSize)
                  {
                    // not available free space
                    _ReturnCode = NO_DISK_SPACE;
                    _DriveName = drive.ToString();
                    //_TotalFileSize = _TotalFileSize;
                    _AvailableFreeSpace = drive.AvailableFreeSpace;
                    return (false);
                  }
                  break;
              }
            }

            //----------------------------------------------------------------------
            // Create header data

            string[] FileInfoText = (string[])FileInfoList.ToArray(typeof(string));

            byteArray = Encoding.UTF8.GetBytes(string.Join("\n", FileInfoText));
            ms.Write(byteArray, 0, byteArray.Length);

#if (DEBUG)
            ////Output text file of header contents for debug.
            //Int64 NowPosition = ms.Position;
            //ms.Position = 0;
            ////Save to Desktop folder.
            //string OutDirPath = Path.GetDirectoryName(_AtcFilePath);
            //string HeaderTextFilePath = Path.Combine(OutDirPath, "encrypt_header.txt");
            //using (FileStream fsDebug = new FileStream(HeaderTextFilePath, FileMode.Create, FileAccess.Write))
            //{
            //  ms.WriteTo(fsDebug);
            //  ms.Position = NowPosition;
            //}
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
                _AtcHeaderSize = 0;   // exclude IV of header
                buffer = new byte[BUFFER_SIZE];
                while ((len = ms.Read(buffer, 0, BUFFER_SIZE)) > 0)
                {
                  cse.Write(buffer, 0, len);
                  _AtcHeaderSize += len;
                }
              }

            }// end using (Rijndael aes = new RijndaelManaged());

          }// end  using (MemoryStream ms = new MemoryStream());

        }// end using (FileStream outfs = new FileStream(_AtcFilePath, FileMode.Create, FileAccess.Write));


        //----------------------------------------------------------------------
        // 本体データの暗号化
        //----------------------------------------------------------------------
        using (FileStream outfs = new FileStream(_AtcFilePath, FileMode.OpenOrCreate, FileAccess.Write))
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
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                      len = 0;
                      while ((len = fs.Read(buffer, 0, BUFFER_SIZE)) > 0)
                      {
                        ds.Write(buffer, 0, len);
                        _TotalSize += len;

                        string MessageText = "";
                        if (_TotalNumberOfFiles > 1)
                        {
                          MessageText = path + " ( " + _NumberOfFiles.ToString() + " / " + _TotalNumberOfFiles.ToString() + " files )";
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
                          fs.Dispose();
                          e.Cancel = true;
                          return (false);
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

        } // end using (FileStream outfs = new FileStream(_AtcFilePath, FileMode.Create, FileAccess.Write));

        // Set the timestamp of encryption file to original files or directories
        if (_fKeepTimeStamp == true)
        {
          File.SetCreationTime(_AtcFilePath, dtCreate);
          File.SetLastWriteTime(_AtcFilePath, dtUpdate);
          File.SetLastAccessTime(_AtcFilePath, dtAccess);
        }
        else
        {
          dtUpdate = DateTime.Now;
          File.SetLastWriteTime(_AtcFilePath, dtUpdate);
        }

        //Encryption succeed.
        _ReturnCode = ENCRYPT_SUCCEEDED;
        return (true);

      }
      catch (UnauthorizedAccessException)
      {
        //オペレーティング システムが I/O エラーまたは特定の種類のセキュリティエラーのためにアクセスを拒否する場合、スローされる例外
        //The exception that is thrown when the operating system denies access
        //because of an I/O error or a specific type of security error.
        _ReturnCode = OS_DENIES_ACCESS;
        _ErrorFilePath = _AtcFilePath;
        return (false);
      }
      catch (DirectoryNotFoundException ex)
      {
        //ファイルまたはディレクトリの一部が見つからない場合にスローされる例外
        //The exception that is thrown when part of a file or directory cannot be found
        _ReturnCode = DIRECTORY_NOT_FOUND;
        _ErrorMessage = ex.Message;
        return (false);
      }
      catch (DriveNotFoundException ex)
      {
        //使用できないドライブまたは共有にアクセスしようとするとスローされる例外
        //The exception that is thrown when trying to access a drive or share that is not available
        _ReturnCode = DRIVE_NOT_FOUND;
        _ErrorMessage = ex.Message;
        return (false);
      }
      catch (FileLoadException ex)
      {
        //マネージド アセンブリが見つかったが、読み込むことができない場合にスローされる例外
        //The exception that is thrown when a managed assembly is found but cannot be loaded
        _ReturnCode = FILE_NOT_LOADED;
        _ErrorFilePath = ex.FileName;
        return (false);
      }
      catch (FileNotFoundException ex)
      {
        //ディスク上に存在しないファイルにアクセスしようとして失敗したときにスローされる例外
        //The exception that is thrown when an attempt to access a file that does not exist on disk fails
        _ReturnCode = FILE_NOT_FOUND;
        _ErrorFilePath = ex.FileName;
        return (false);
      }
      catch (PathTooLongException)
      {
        //パス名または完全修飾ファイル名がシステム定義の最大長を超えている場合にスローされる例外
        //The exception that is thrown when a path or fully qualified file name is longer than the system-defined maximum length
        _ReturnCode = PATH_TOO_LONG;
        return (false);
      }
      catch (IOException ex)
      {
        //I/Oエラーが発生したときにスローされる例外。現在の例外を説明するメッセージを取得します。
        //The exception that is thrown when an I/O error occurs. Gets a message that describes the current exception.
        _ReturnCode = IO_EXCEPTION;
        _ErrorMessage = ex.Message;
        return (false);
      }
      catch (Exception ex)
      {
        _ReturnCode = ERROR_UNEXPECTED;
        _ErrorMessage = ex.Message;
        return (false);
      }

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
      //DirectoryInfo di = new DirectoryInfo(ParentPath + Path.GetFileName(DirPath));
      DirectoryInfo di = new DirectoryInfo(DirPath);
      List.Add(0);                                      // Directory flag
      List.Add(DirPath);                                // Absolute file path
      List.Add(DirPath.Replace(ParentPath, "") + "\\"); // (string)Remove parent directory path.
      List.Add(0);                                      // File size = 0
      List.Add((int)di.Attributes);                     // (int)File attribute
      /*
        * TTimeStamp = record
        *  Time: Integer;      { Number of seconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      List.Add((Int32)(di.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(di.LastWriteTime.TimeOfDay.TotalSeconds);
      List.Add((Int32)(di.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(di.CreationTime.TimeOfDay.TotalSeconds);
      List.Add("");
      // ver.3.2.3.0～
      List.Add(di.LastWriteTimeUtc);
      List.Add(di.CreationTimeUtc);
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
        *  Time: Integer;      { Number of seconds since midnight }
        *  Date: Integer;      { One plus number of days since 1/1/0001 }
        * end;
      */
      List.Add((Int32)(fi.LastWriteTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(fi.LastWriteTime.TimeOfDay.TotalSeconds);
      List.Add((Int32)(fi.CreationTime.Date.Subtract(new DateTime(1, 1, 1))).TotalDays);
      List.Add(fi.CreationTime.TimeOfDay.TotalSeconds);
      List.Add((string)GetSha256FromFile(AbsoluteFilePath));
      // ver.3.2.3.0～
      List.Add(fi.LastWriteTimeUtc);
      List.Add(fi.CreationTimeUtc);
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
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        ReadOnlyCollection<byte> hash = Sha256.HashFile(fs);

        StringBuilder result = new StringBuilder();
        result.Capacity = 32;
        foreach (byte b in hash)
        {
          result.Append(b.ToString());
        }

        return (result.ToString());
      }
    }


  }// end class FileEncrypt()


}// end namespace

