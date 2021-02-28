//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016-2021  Mitsuhiro Hibara
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
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;

namespace AttacheCase
{
  class FileDecrypt3
  {
    private struct FileListData
    {
      public string FilePath;
      public Int64 FileSize;
      public int FileAttribute;
      public DateTime LastWriteDateTime;
      public DateTime CreationDateTime;
      public string Hash;
    }

    // Status code
    private const int ENCRYPT_SUCCEEDED = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED = 2; // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED = 3; // Delete is succeeded.
    private const int READY_FOR_ENCRYPT = 4; // Getting ready for encryption or decryption.
    private const int READY_FOR_DECRYPT = 5; // Getting ready for encryption or decryption.
    private const int ENCRYPTING = 6; // Ecrypting.
    private const int DECRYPTING = 7; // Decrypting.
    private const int DELETING = 8; // Deleting.

    // Error code
    private const int USER_CANCELED = -1;   // User cancel.
    private const int ERROR_UNEXPECTED = -100;
    private const int NOT_ATC_DATA = -101;
    private const int ATC_BROKEN_DATA = -102;
    private const int NO_DISK_SPACE = -103;
    private const int FILE_INDEX_NOT_FOUND = -104;
    private const int PASSWORD_TOKEN_NOT_FOUND = -105;
    private const int NOT_CORRECT_HASH_VALUE = -106;
    private const int INVALID_FILE_PATH = -107;
    private const int OS_DENIES_ACCESS = -108;
    private const int DATA_NOT_FOUND = -109;
    private const int DIRECTORY_NOT_FOUND = -110;
    private const int DRIVE_NOT_FOUND = -111;
    private const int FILE_NOT_LOADED = -112;
    private const int FILE_NOT_FOUND = -113;
    private const int PATH_TOO_LONG = -114;
    private const int IO_EXCEPTION = -115;

    // Overwrite Option
    //private const int USER_CANCELED = -1;
    private const int OVERWRITE = 1;
    private const int OVERWRITE_ALL = 2;
    private const int KEEP_NEWER = 3;
    private const int KEEP_NEWER_ALL = 4;
    // ---
    // Skip Option
    private const int SKIP = 5;
    private const int SKIP_ALL = 6;


    private const int BUFFER_SIZE = 4096;

    // Header data variables
    private const string STRING_TOKEN_NORMAL = "_AttacheCaseData";
    private const string STRING_TOKEN_BROKEN = "_Atc_Broken_Data";
    private const char DATA_FILE_VERSION = (char)130;
    private const string AtC_ENCRYPTED_TOKEN = "atc3";

    // Atc data size of self executable file
    private Int64 _ExeOutSize = 0;
    private Int64 _TotalSize = 0;
    //private Int64 _TotalFileSize = 0;

    //----------------------------------------------------------------------
    // For thie file list after description, open associated with file or folder.
    private readonly List<string> _OutputFileList = new List<string>();
    public List<string> OutputFileList
    {
      get { return _OutputFileList; }
    }

    // Temporary option for overwriting
    // private const int USER_CANCELED  = -1;
    // private const int OVERWRITE      = 1;
    // private const int OVERWRITE_ALL  = 2;
    // private const int KEEP_NEWER     = 3;
    // private const int KEEP_NEWER_ALL = 4;
    // private const int SKIP           = 5;
    // private const int SKIP_ALL       = 6;
    private int _TempOverWriteOption = -1;
    public int TempOverWriteOption
    {
      get { return this._TempOverWriteOption; }
      set { this._TempOverWriteOption = value; }
    }
    // 処理した暗号化ファイルの数
    // The number of ATC files to be decrypted
    private int _NumberOfFiles = 0;
    public int NumberOfFiles
    {
      get { return this._NumberOfFiles; }
      set { this._NumberOfFiles = value; }
    }
    // 処理する暗号化ファイルの合計数
    // Total number of ATC files to be decrypted
    private int _TotalNumberOfFiles = 1;
    public int TotalNumberOfFiles
    {
      get { return this._TotalNumberOfFiles; }
      set { this._TotalNumberOfFiles = value; }
    }
    // 親フォルダーを生成しないか否か
    //Create no parent folder in decryption
    private bool _fNoParentFolder = false;
    public bool fNoParentFolder
    {
      get { return this._fNoParentFolder; }
      set { this._fNoParentFolder = value; }
    }
    // パスワード入力回数制限（読み取り専用）
    //Get limit of times to input password in encrypt files ( readonly ).
    private char _MissTypeLimits = (char)3;
    public char MissTypeLimits
    {
      get { return this._MissTypeLimits; }
    }
    // ファイル、フォルダーのタイムスタンプを復号時に合わせる
    private bool _fSameTimeStamp = false;
    //Set the timestamp to decrypted files or directories
    public bool fSameTimeStamp
    {
      get { return this._fSameTimeStamp; }
      set { this._fSameTimeStamp = value; }
    }
    // 一つずつ親フォルダーを確認、生成しながら復号する（サルベージモード）
    private bool _fSalvageToCreateParentFolderOneByOne = false;
    // Decrypt one by one while creating the parent folder ( Salvage mode ).
    public bool fSalvageToCreateParentFolderOneByOne
    {
      get { return this._fSalvageToCreateParentFolderOneByOne; }
      set { this._fSalvageToCreateParentFolderOneByOne = value; }
    }
    // すべてのファイルを同じ階層のディレクトリーに復号する（サルベージモード）
    private bool _fSalvageIntoSameDirectory = false;
    // Decrypt all files into the directory of the same hierarchy ( Salvage mode ). 
    public bool fSalvageIntoSameDirectory
    {
      get { return this._fSalvageIntoSameDirectory; }
      set { this._fSalvageIntoSameDirectory = value; }
    }
    // 自己実行形式タイプのファイルか
    // Self-executable file
    private bool _fExecutableType = false;
    public bool fExecutableType
    {
      get { return this._fExecutableType; }
    }
    // ファイルのハッシュ値チェックを無視する
    // ignore checking hash data of files
    private bool _fSalvageIgnoreHashCheck = false;
    public bool fSalvageIgnoreHashCheck
    {
      get { return this._fSalvageIgnoreHashCheck; }
      set { this._fSalvageIgnoreHashCheck = value; }
    }

    //----------------------------------------------------------------------
    // The return value of error ( ReadOnly)
    //----------------------------------------------------------------------
    // Input "Error code" for value
    private int _ReturnCode = 0;
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
    // Total file size of files to be decrypted
    private Int64 _TotalFileSize = -1;
    public Int64 TotalFileSize
    {
      get { return this._TotalFileSize; }
    }
    // Free space on the drive to decrypt the file
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

    //----------------------------------------------------------------------
    // The plain text header data of encrypted file ( ReadOnly)
    //----------------------------------------------------------------------
    // Data Sub Version ( ver.2.00~ = "5", ver.2.70~ = "6" )
    private int _DataSebVersion = 0;
    public int DataSebVersion
    {
      get { return this._DataSebVersion; }
    }
    // The broken status of file
    private bool _fBroken = false;
    public bool fBroken
    {
      get { return this._fBroken; }
    }
    // Internal token string ( Whether or not the file is broken )  = "_AttacheCaseData" or "_Atc_Broken_Data"
    private string _TokenStr = "";
    public string TokenStr
    {
      get { return this._TokenStr; }
    }
    // Data File Version ( DATA_FILE_VERSION = 105 )
    private int _DataFileVersion = 0;
    public int DataFileVersion
    {
      get { return this._DataFileVersion; }
    }
    // TYPE_ALGORISM = 1:Rijndael	
    private int _TypeAlgorism = 0;
    public int TypeAlgorism
    {
      get { return this._TypeAlgorism; }
    }
    // The size of encrypted header data
    private int _AtcHeaderSize = 0;
    public int AtcHeaderSize
    {
      get { return this._AtcHeaderSize; }
    }

    // Just this ATC file to decrypt
    private string _AtcFilePath = "";
    public string AtcFilePath
    {
      get { return this._AtcFilePath; }
    }

    // App version
    private short _AppVersion = 0;
    public short AppVersion
    {
      get { return this._AppVersion; }
    }

    // The salt used in Rfc2898DeriveBytes
    private byte[] _salt = new byte[8];
    public byte[] salt
    {
      get { return this._salt; }
    }

    private Rfc2898DeriveBytes _deriveBytes;
    public Rfc2898DeriveBytes deriveBytes
    {
      get { return this._deriveBytes; }
    }

    // List of files to decrypt
    private List<string> _FileList;
    public List<string> FileList
    {
      get { return this._FileList; }
    }

    // Decryption time
    private string _DecryptionTimeString;
    public string DecryptionTimeString
    {
      get { return this._DecryptionTimeString; }
    }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="FilePath"></param>
    public FileDecrypt3(string FilePath)
    {
      _AtcFilePath = FilePath;

      // _AttacheCaseData
      //byte[] AtcTokenByte = { 0x5F, 0x41, 0x74, 0x74, 0x61, 0x63, 0x68, 0x65, 0x43, 0x61, 0x73, 0x65, 0x44, 0x61, 0x74, 0x61};
      int[] AtcTokenByte = { 95, 65, 116, 116, 97, 99, 104, 101, 67, 97, 115, 101, 68, 97, 116, 97 };

      // _Atc_Broken_Data
      //byte[] AtcBrokenTokenByte = { 0x5F, 0x41, 0x74, 0x63, 0x5F, 0x42, 0x72, 0x6F, 0x6B, 0x65, 0x6E, 0x5F, 0x44, 0x61, 0x74, 0x61 };
      int[] AtcBrokenTokenByte = { 95, 65, 116, 99, 95, 66, 114, 111, 104, 101, 110, 95, 68, 97, 116, 97 };

      try
      {
        using (FileStream fs = new FileStream(_AtcFilePath, FileMode.Open, FileAccess.Read))
        {
          bool fToken = false;
          int b;
          while ((b = fs.ReadByte()) > -1)
          {
            //-----------------------------------
            // Check the token "_AttacheCaseData"
            if (b == AtcTokenByte[0])
            {
              fToken = true;
              for (int i = 1; i < AtcTokenByte.Length; i++)
              {
                if (fs.ReadByte() != AtcTokenByte[i])
                {
                  fToken = false;
                  break;
                }
              }
              if (fToken == true)
              {
                if (fs.Position > 20)
                { // Self executabel file
                  _fExecutableType = true;
                  _ExeOutSize = fs.Position - 20;
                }
                break;
              }
            }

            //-----------------------------------
            // Check the token "_AttacheCaseData"
            if (fToken == false && b == AtcBrokenTokenByte[0])
            {
              fToken = true;
              for (int i = 1; i < AtcBrokenTokenByte.Length; i++)
              {
                if (fs.ReadByte() != AtcBrokenTokenByte[i])
                {
                  fToken = false;
                  break;
                }
              }

              if (fToken == true)
              {
                if (fs.Position > 20)
                { // Self executabel file
                  _fExecutableType = true;
                  _fBroken = true;
                  _ExeOutSize = fs.Position - 20;
                }
                break;
              }
            }

            //-----------------------------------
            if (fToken == true)
            {
              break;
            }
            //-----------------------------------

          }// end while();

#if (DEBUG)
          //string msg = fToken == true ? "fTokne: true" : "fTokne: false";
          //System.Windows.Forms.MessageBox.Show(msg);
#endif
        }// end using (FileStream fs = new FileStream(_AtcFilePath, FileMode.Open, FileAccess.Read));

        byte[] byteArray;

        using (FileStream fs = new FileStream(_AtcFilePath, FileMode.Open, FileAccess.Read))
        {
          if (fs.Length < 32)
          {
            _ReturnCode = NOT_ATC_DATA;
            _ErrorFilePath = _AtcFilePath;
            return;
          }

          if (_fExecutableType == true)
          {
            fs.Seek(_ExeOutSize, SeekOrigin.Begin);
          }

          // Plain text header
          byteArray = new byte[2];
          fs.Read(byteArray, 0, 2);
          _AppVersion = BitConverter.ToInt16(byteArray, 0);      // AppVersion

          byteArray = new byte[1];
          fs.Read(byteArray, 0, 1);
          _MissTypeLimits = (char)byteArray[0];                  // MissTypeLimits

          byteArray = new byte[1];
          fs.Read(byteArray, 0, 1);
          _fBroken = BitConverter.ToBoolean(byteArray, 0);       // fBroken

          byteArray = new byte[16];
          fs.Read(byteArray, 0, 16);
          _TokenStr = Encoding.ASCII.GetString(byteArray);       // TokenStr（"_AttacheCaseData" or "_Atc_Broken_Data"）

          byteArray = new byte[4];
          fs.Read(byteArray, 0, 4);
          _DataFileVersion = BitConverter.ToInt32(byteArray, 0); // DATA_FILE_VERSION = 130

          byteArray = new byte[4];
          fs.Read(byteArray, 0, 4);
          _AtcHeaderSize = BitConverter.ToInt32(byteArray, 0);   // AtcHeaderSize ( encrypted header data size )

          // salt
          fs.Read(_salt, 0, 8);

#if (DEBUG)
          //System.Windows.Forms.MessageBox.Show("_TokenStr: " + _TokenStr);
#endif

        } // end using (FileStream fs = new FileStream(_AtcFilePath, FileMode.Open, FileAccess.Read));

        _ReturnCode = DECRYPT_SUCCEEDED;
        return;

      }
      catch (UnauthorizedAccessException)
      {
        //オペレーティング システムが I/O エラーまたは特定の種類のセキュリティエラーのためにアクセスを拒否する場合、スローされる例外
        //The exception that is thrown when the operating system denies access
        //because of an I/O error or a specific type of security error.
        _ReturnCode = OS_DENIES_ACCESS;
        _ErrorFilePath = _AtcFilePath;
        return;
      }
      catch (DirectoryNotFoundException ex)
      {
        //ファイルまたはディレクトリの一部が見つからない場合にスローされる例外
        //The exception that is thrown when part of a file or directory cannot be found
        _ReturnCode = DIRECTORY_NOT_FOUND;
        _ErrorMessage = ex.Message;
        return;
      }
      catch (DriveNotFoundException ex)
      {
        //使用できないドライブまたは共有にアクセスしようとするとスローされる例外
        //The exception that is thrown when trying to access a drive or share that is not available
        _ReturnCode = DRIVE_NOT_FOUND;
        _ErrorMessage = ex.Message;
        return;
      }
      catch (FileLoadException ex)
      {
        //マネージド アセンブリが見つかったが、読み込むことができない場合にスローされる例外
        //The exception that is thrown when a managed assembly is found but cannot be loaded
        _ReturnCode = FILE_NOT_LOADED;
        _ErrorFilePath = ex.FileName;
        return;
      }
      catch (FileNotFoundException ex)
      {
        //ディスク上に存在しないファイルにアクセスしようとして失敗したときにスローされる例外
        //The exception that is thrown when an attempt to access a file that does not exist on disk fails
        _ReturnCode = FILE_NOT_FOUND;
        _ErrorFilePath = ex.FileName;
        return;
      }
      catch (PathTooLongException)
      {
        //パス名または完全修飾ファイル名がシステム定義の最大長を超えている場合にスローされる例外
        //The exception that is thrown when a path or fully qualified file name is longer than the system-defined maximum length
        _ReturnCode = PATH_TOO_LONG;
        return;
      }
      catch (IOException ex)
      {
        //I/Oエラーが発生したときにスローされる例外。現在の例外を説明するメッセージを取得します。
        //The exception that is thrown when an I/O error occurs. Gets a message that describes the current exception.
        _ReturnCode = IO_EXCEPTION;
        _ErrorMessage = ex.Message;
        return;
      }
      catch (Exception ex)
      {
        _ReturnCode = ERROR_UNEXPECTED;
        _ErrorMessage = ex.Message;
        return;
      }

    } // end public FileDecrypt3(string FilePath);

    /// <summary>
    /// The encrypted file by AES (exactly Rijndael) to the original file or folder by user's password.
    /// ユーザーが設定したパスワードによって、AES（正確にはRijndael）によって暗号化されたファイルを
    /// 元のファイル、またはフォルダーに復号して戻す。
    /// </summary>
    /// <param name="FilePath">File path or directory path is encrypted</param>
    /// <param name="OutFileDir">The directory of outputing encryption file.</param>
    /// <param name="Password">Encription password string</param>
    /// <returns>bool true: Success, false: Failed</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:オブジェクトを複数回破棄しない")]
    public bool Decrypt(
      object sender, DoWorkEventArgs e,
      string FilePath, string OutDirPath, string Password, byte[] PasswordBinary, Action<int, string> dialog)
    {

      BackgroundWorker worker = sender as BackgroundWorker;

      //-----------------------------------
      // Header data is starting.
      // Progress event handler
      ArrayList MessageList = new ArrayList();
      MessageList.Add(READY_FOR_DECRYPT);
      MessageList.Add(Path.GetFileName(FilePath));
      worker.ReportProgress(0, MessageList);

      Stopwatch swDecrypt = new Stopwatch();
      Stopwatch swProgress = new Stopwatch();

      swDecrypt.Start();
      swProgress.Start();
      float percent = 0;

      int len = 0;
      byte[] byteArray;

      _FileList = new List<string>();
      Dictionary<int, FileListData> dic = new Dictionary<int, FileListData>();

      if (_TokenStr.Trim() == "_AttacheCaseData")
      {
        // Atc data
      }
      else if (_TokenStr.Trim() == "_Atc_Broken_Data")
      {
        // Atc file is broken
        _ReturnCode = ATC_BROKEN_DATA;
        _ErrorFilePath = FilePath;
        return (false);
      }
      else
      {
        // not AttacheCase data
        _ReturnCode = NOT_ATC_DATA;
        _ErrorFilePath = FilePath;
        return (false);
      }

      //Rfc2898DeriveBytes _deriveBytes;
      if (PasswordBinary == null)
      {
        _deriveBytes = new Rfc2898DeriveBytes(Password, _salt, 1000);
      }
      else
      {
        _deriveBytes = new Rfc2898DeriveBytes(PasswordBinary, _salt, 1000);
      }

      byte[] key = _deriveBytes.GetBytes(32);
      byte[] iv = _deriveBytes.GetBytes(32);

      try
      {
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          if (fs.Length < 32)
          {
            // not AttacheCase data
            _ReturnCode = NOT_ATC_DATA;
            _ErrorFilePath = FilePath;
            return (false);
          }
          else
          {
            if (_ExeOutSize > 0)
            {
              // self-executable file
              fs.Seek(_ExeOutSize + 36, SeekOrigin.Begin);
            }
            else
            {
              fs.Seek(36, SeekOrigin.Begin);
            }
          }

          // The Header of MemoryStream is encrypted
          using (Rijndael aes = new RijndaelManaged())
          {
            aes.BlockSize = 256;             // BlockSize = 32 bytes
            aes.KeySize = 256;               // KeySize = 32 bytes
            aes.Mode = CipherMode.CBC;       // CBC mode
            aes.Padding = PaddingMode.Zeros; // Padding mode is "ZEROS".

            aes.Key = key;
            aes.IV = iv;

            // Decryption interface.
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
            {
              using (MemoryStream ms = new MemoryStream())
              {
                byteArray = new byte[_AtcHeaderSize];
                len = cse.Read(byteArray, 0, _AtcHeaderSize);
                ms.Write(byteArray, 0, _AtcHeaderSize);
#if (DEBUG)
                //string AppDirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                //string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string TempFilePath = Path.Combine(OutDirPath, "decrypt_header.txt");
                using (StreamWriter sw = new StreamWriter(TempFilePath, false, Encoding.UTF8))
                {
                  sw.Write(Encoding.UTF8.GetString(byteArray));
                }
#endif
                // Check Password Token
                if (Encoding.UTF8.GetString(byteArray).IndexOf(AtC_ENCRYPTED_TOKEN) > -1)
                {
                  // Decryption is succeeded.
                }
                else
                {
                  // Token is not match ( Password is not correct )
                  _ReturnCode = PASSWORD_TOKEN_NOT_FOUND;
                  _ErrorFilePath = FilePath;
                  return (false);
                }

                ms.Position = 0;

                var sr = new StreamReader(ms, Encoding.UTF8);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                  if (Regex.IsMatch(line, @"^[0-9]+:"))
                  {
                    _FileList.Add(line);
                  }
                }

              }//end using (MemoryStream ms = new MemoryStream())

            }//end using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read));

          }//end using (Rijndael aes = new RijndaelManaged());

        }//end using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read));

        //----------------------------------------------------------------------
        // Make a list array of the information for each file
        //----------------------------------------------------------------------
        _TotalFileSize = 0;
        string ParentFolder = "";
        bool fDirectoryTraversal = false;
        string InvalidFilePath = "";
        _FileList.ForEach(delegate (string OutputLine)
        {
          int LastWriteDate, CreateDate;
          double LastWriteTime, CreateTime;
          DateTime LastWriteDateTime = DateTime.Parse("0001/01/01");
          DateTime CreationDateTime = DateTime.Parse("0001/01/01");

          FileListData fd = new FileListData();
          string[] OutputFileData = OutputLine.Split('\t');

          //-----------------------------------
          // File number
          // e.g.)
          // 0:sample.txt[\t]49657[\t]32[\t]736194[\t]39585.875[\t]736194[\t]30186.782[\t]5f43aa1fed05350f34c2fabb7ed938457b2497f2b54a50415b51882f333b8ae1
          string[] FilePathSplits = OutputFileData[0].Split(':');
          int FileNum = 0;
          if (Int32.TryParse(FilePathSplits[0], out FileNum) == false)
          {
            FileNum = -1;
          }

          //-----------------------------------
          // Parent folder is not created.
          //
          if (_fNoParentFolder == true)
          {
            // root directory
            if (FileNum == 0)
            {
              if (FilePathSplits.Length > 2)
              {
                // ex. 0:G:\Test.txt
                ParentFolder = FilePathSplits[2];
              }
              else
              {
                ParentFolder = FilePathSplits[1];
              }
            }
            // not root directroy ( files )
            else
            {
              if (FilePathSplits.Length > 2)
              {
                StringBuilder sb = new StringBuilder(FilePathSplits[2]);
                len = ParentFolder.Length;
                FilePathSplits[2] = sb.Replace(ParentFolder, "", 0, len).ToString();
              }
              else
              {
                StringBuilder sb = new StringBuilder(FilePathSplits[1]);
                len = ParentFolder.Length;
                FilePathSplits[1] = sb.Replace(ParentFolder, "", 0, len).ToString();
              }
            }
          }

          //-----------------------------------
          // File path
          //
          string OutFilePath = "";
          if (_fSalvageIntoSameDirectory == true) // Salvage mode?
          {
            OutFilePath = Path.Combine(OutDirPath, Path.GetFileName(FilePathSplits[1]));
          }
          else
          {
            if (FilePathSplits.Length > 2)
            {
              OutFilePath = Path.Combine(OutDirPath, FilePathSplits[2]);
            }
            else
            {
              OutFilePath = Path.Combine(OutDirPath, FilePathSplits[1]);
            }
          }

          //-----------------------------------
          // ディレクトリ・トラバーサル対策
          // Directory traversal countermeasures

          // 余計な ":" が含まれている
          // Extra ":" is included
          if (FilePathSplits.Length > 2)
          {
            fDirectoryTraversal = true;
            InvalidFilePath = OutFilePath;
          }

          try
          {
            // ファイルパスを正規化
            // Canonicalize file path.
            OutFilePath = Path.GetFullPath(OutFilePath);
          }
          catch
          {
            fDirectoryTraversal = true;
            InvalidFilePath = OutFilePath;
          }

          // 正規化したパスが保存先と一致するか
          // Whether the canonicalized path matches the save destination
          if (fDirectoryTraversal == false && OutFilePath.StartsWith(OutDirPath))
          {
            fd.FilePath = OutFilePath;
          }
          else
          {
            fDirectoryTraversal = true;
            InvalidFilePath = OutFilePath;
          }

          //-----------------------------------
          // File size
          if (Int64.TryParse(OutputFileData[1], out fd.FileSize) == false)
          {
            fd.FileSize = -1;
          }
          else
          {
            _TotalFileSize += fd.FileSize;
          }
          //-----------------------------------
          // File attribute
          if (Int32.TryParse(OutputFileData[2], out fd.FileAttribute) == false)
          {
            fd.FileAttribute = -1;
          }

          //-----------------------------------
          /*
           * TTimeStamp = record
           *  Time: Integer;      { Number of seconds since midnight }
           *  Time: Integer;      { Number of milliseconds since midnight }
           *  Date: Integer;      { One plus number of days since 1/1/0001 }
           * end;
          */
          //-----------------------------------

          // Last update timestamp
          try
          {
            if (_fSameTimeStamp == false && Int32.TryParse(OutputFileData[3], out LastWriteDate) == true)
            {
              LastWriteDateTime = LastWriteDateTime.AddDays(LastWriteDate); // Add days
            }
            else
            {
              LastWriteDateTime = DateTime.Now;
            }

            if (_fSameTimeStamp == false && Double.TryParse(OutputFileData[4], out LastWriteTime) == true)
            {
              LastWriteDateTime = LastWriteDateTime.AddSeconds(LastWriteTime);  // Add seconds
            }
            else
            {
              LastWriteDateTime = DateTime.Now;
            }
          }
          catch
          {
            LastWriteDateTime = DateTime.Now;
          }

          fd.LastWriteDateTime = LastWriteDateTime;

          //-----------------------------------
          // Create datetime
          try
          {
            if (_fSameTimeStamp == false && Int32.TryParse(OutputFileData[5], out CreateDate) == true)
            {
              CreationDateTime = CreationDateTime.AddDays(CreateDate);
            }
            else
            {
              CreationDateTime = DateTime.Now;
            }

            if (_fSameTimeStamp == false && Double.TryParse(OutputFileData[6], out CreateTime) == true)
            {
              CreationDateTime = CreationDateTime.AddSeconds(CreateTime);
            }
            else
            {
              CreationDateTime = DateTime.Now;
            }
          }
          catch
          {
            CreationDateTime = DateTime.Now;
          }

          fd.CreationDateTime = CreationDateTime;

          //-----------------------------------
          // SHA-256 hash
          if (OutputFileData.Length > 7)
          {
            fd.Hash = OutputFileData[7];
          }

          //-----------------------------------
          // File datetime ( UTC )
          // ver.3.2.3.0 ~
          if (OutputFileData.Length > 8)
          {
            if (_fSameTimeStamp == false)
            {
              DateTime LastWriteDateTimeUtc;
              DateTime CreationDateTimeUtc;

              // Last update datetime(UTC)
              DateTime.TryParse(OutputFileData[8], out LastWriteDateTimeUtc);
              TimeZoneInfo tzi = TimeZoneInfo.Local;
              fd.LastWriteDateTime = TimeZoneInfo.ConvertTimeFromUtc(LastWriteDateTimeUtc, tzi);
              // Create datetime(UTC)
              DateTime.TryParse(OutputFileData[9], out CreationDateTimeUtc);
              fd.CreationDateTime = TimeZoneInfo.ConvertTimeFromUtc(CreationDateTimeUtc, tzi);
            }
            else
            {
              fd.LastWriteDateTime = DateTime.UtcNow;
              fd.CreationDateTime = DateTime.UtcNow;
            }
          }

          //-----------------------------------
          // Insert to 'Key-Value' type array data.
          dic.Add(FileNum, fd);

        });

        // Directory traversal countermeasures
        if (fDirectoryTraversal == true)
        {
          _ReturnCode = INVALID_FILE_PATH;
          _ErrorFilePath = InvalidFilePath;
          return (false);
        }

        //----------------------------------------------------------------------
        // Check the disk space
        //----------------------------------------------------------------------
        string RootDriveLetter = Path.GetPathRoot(OutDirPath).Substring(0, 1);

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
                _ReturnCode = NO_DISK_SPACE;
                _DriveName = drive.ToString();
                //_TotalFileSize = _TotalFileSize;
                _AvailableFreeSpace = drive.AvailableFreeSpace;
                return (false);
              }
              break;
          }
        }

        //-----------------------------------
        // Decrypt file main data.
        //-----------------------------------
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          //-----------------------------------
          // Adjust the header data in 32 bytes
          int mod = _AtcHeaderSize % 32;
          if (_fExecutableType == true)
          {
            fs.Seek(_ExeOutSize + 36 + _AtcHeaderSize + 32 - mod, SeekOrigin.Begin);
          }
          else
          {
            fs.Seek(36 + _AtcHeaderSize + 32 - mod, SeekOrigin.Begin);
          }

          //-----------------------------------
          // Decyption
          using (Rijndael aes = new RijndaelManaged())
          {
            aes.BlockSize = 256;             // BlockSize = 32bytes
            aes.KeySize = 256;               // KeySize = 32bytes
            aes.Mode = CipherMode.CBC;       // CBC mode
            aes.Padding = PaddingMode.Zeros; // Padding mode
            aes.Key = key;
            aes.IV = iv;
#if DEBUG
            //System.Windows.Forms.MessageBox.Show("dic.Count: " + dic.Count);
#endif
            //Decryption interface.
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
            {
              using (Ionic.Zlib.DeflateStream ds = new Ionic.Zlib.DeflateStream(cse, Ionic.Zlib.CompressionMode.Decompress))
              {
                /*
                public struct FileListData
                {
                  public string FilePath;
                  public Int64 FileSize;
                  public int FileAttribute;
                  public DateTime LastWriteDateTime;
                  public DateTime CreationDateTime;
                  public string Sha256String;
                  // ver.3.2.3.0 ~
                  public DateTime LastWriteDateTimeUtc;
                  public DateTime CreationDateTimeUtc;
                }
                Dictionary<int, FileListData> dic = new Dictionary<int, FileListData>();
                */
                FileStream outfs = null;
                Int64 FileSize = 0;
                int FileIndex = 0;

                bool fSkip = false;

                if (_fNoParentFolder == true)
                {
                  if (dic[0].FilePath.EndsWith("\\") == true)
                  {
                    FileIndex = 1;  // Ignore parent folder.
                  }
                }

                //----------------------------------------------------------------------
                byteArray = new byte[BUFFER_SIZE];

                //while ((len = ds.Read(byteArray, 0, BUFFER_SIZE)) > 0)
                while (true)
                {
                  if (_AppVersion < 3013)
                  {
                    len = cse.Read(byteArray, 0, BUFFER_SIZE);
                  }
                  else
                  {
                    len = ds.Read(byteArray, 0, BUFFER_SIZE);
                  }

                  // 末尾の0バイトファイル、またはフォルダ生成対策
                  if (len == 0) len = 1;

                  int buffer_size = len;

                  while (len > 0)
                  {
                    //----------------------------------------------------------------------
                    // 書き込み中のファイルまたはフォルダが無い場合は作る
                    // Create them if there is no writing file or folder.
                    //----------------------------------------------------------------------
                    if (outfs == null)
                    {
                      //-----------------------------------
                      // Create file or dirctories.
                      if (dic.ContainsKey(FileIndex) == false)
                      {
                        if (FileIndex > dic.Count - 1)
                        {
                          _ReturnCode = DECRYPT_SUCCEEDED;
                          return (true);
                        }
                        else
                        {
                          _ReturnCode = FILE_INDEX_NOT_FOUND;
                          return (false);
                        }
                      }
                      else
                      {
                        //-----------------------------------
                        // Create directory
                        //-----------------------------------
                        if (dic[FileIndex].FilePath.EndsWith("\\") == true)
                        {
                          string path = Path.Combine(OutDirPath, dic[FileIndex].FilePath);
                          DirectoryInfo di = new DirectoryInfo(path);

                          // File already exists.
                          if (Directory.Exists(path) == true)
                          {
                            // Temporary option for overwriting
                            // private const int USER_CANCELED  = -1;
                            // private const int OVERWRITE      = 1;
                            // private const int OVERWRITE_ALL  = 2;
                            // private const int KEEP_NEWER     = 3;
                            // private const int KEEP_NEWER_ALL = 4;
                            // private const int SKIP           = 5;
                            // private const int SKIP_ALL       = 6;
                            if (_TempOverWriteOption == OVERWRITE_ALL)
                            {
                              // Overwrite ( New create )
                            }
                            else if (_TempOverWriteOption == SKIP_ALL)
                            {
                              fSkip = true;
                            }
                            else if (_TempOverWriteOption == KEEP_NEWER_ALL)
                            {
                              if (di.LastWriteTime > dic[FileIndex].LastWriteDateTime)
                              {
                                fSkip = true; // old directory
                              }
                            }
                            else
                            {
                              // Show dialog of comfirming to overwrite. 
                              dialog(0, path);

                              // Cancel
                              if (_TempOverWriteOption == USER_CANCELED)
                              {
                                _ReturnCode = USER_CANCELED;
                                return (false);
                              }
                              else if (_TempOverWriteOption == OVERWRITE || _TempOverWriteOption == OVERWRITE_ALL)
                              {
                                // Overwrite ( New create )
                              }
                              // Skip, or Skip All
                              else if (_TempOverWriteOption == SKIP_ALL)
                              {
                                fSkip = true;
                                _ReturnCode = DECRYPT_SUCCEEDED;
                                return (true);
                              }
                              else if (_TempOverWriteOption == SKIP)
                              {
                                fSkip = true;
                              }
                              else if (_TempOverWriteOption == KEEP_NEWER || _TempOverWriteOption == KEEP_NEWER_ALL)
                              { // New file?
                                if (di.LastWriteTime > dic[FileIndex].LastWriteDateTime)
                                {
                                  fSkip = true;
                                }
                              }
                            }

                            if (fSkip == false)
                            {
                              //隠し属性を削除する
                              di.Attributes &= ~FileAttributes.Hidden;
                              //読み取り専用を削除
                              di.Attributes &= ~FileAttributes.ReadOnly;
                            }

                            //すべての属性を解除
                            //File.SetAttributes(path, FileAttributes.Normal);

                          } // end if ( Directory.Exists )

                          Directory.CreateDirectory(dic[FileIndex].FilePath);
                          _OutputFileList.Add(dic[FileIndex].FilePath);
                          FileSize = 0;
                          FileIndex++;

                          if (FileIndex > dic.Count - 1)
                          {
                            _ReturnCode = DECRYPT_SUCCEEDED;
                            return (true);
                          }

                          continue;

                        }
                        //-----------------------------------
                        // Create file
                        //-----------------------------------
                        else
                        {
                          string path = Path.Combine(OutDirPath, dic[FileIndex].FilePath);
                          FileInfo fi = new FileInfo(path);

                          // File already exists.
                          if (File.Exists(path) == true)
                          {
                            // Salvage Data Mode
                            if (_fSalvageIntoSameDirectory == true)
                            {
                              int SerialNum = 0;
                              while (File.Exists(path) == true)
                              {
                                path = getFileNameWithSerialNumber(path, SerialNum);
                                SerialNum++;
                              }
                            }
                            else
                            {
                              // Temporary option for overwriting
                              // private const int USER_CANCELED  = -1;
                              // private const int OVERWRITE      = 1;
                              // private const int OVERWRITE_ALL  = 2;
                              // private const int KEEP_NEWER     = 3;
                              // private const int KEEP_NEWER_ALL = 4;
                              // private const int SKIP           = 5;
                              // private const int SKIP_ALL       = 6;
                              if (_TempOverWriteOption == OVERWRITE_ALL)
                              {
                                // Overwrite ( New create )
                              }
                              else if (_TempOverWriteOption == SKIP_ALL)
                              {
                                fSkip = true;
                              }
                              else if (_TempOverWriteOption == KEEP_NEWER_ALL)
                              {
                                if (fi.LastWriteTime > dic[FileIndex].LastWriteDateTime)
                                {
                                  fSkip = true;
                                }
                              }
                              else
                              {
                                // Show dialog of comfirming to overwrite. 
                                dialog(0, path);

                                // Cancel
                                if (_TempOverWriteOption == USER_CANCELED)
                                {
                                  _ReturnCode = USER_CANCELED;
                                  return (false);
                                }
                                else if (_TempOverWriteOption == OVERWRITE || _TempOverWriteOption == OVERWRITE_ALL)
                                {
                                  // Overwrite ( New create )
                                }
                                // Skip, or Skip All
                                else if (_TempOverWriteOption == SKIP || _TempOverWriteOption == SKIP_ALL)
                                {
                                  fSkip = true;
                                }
                                else if (_TempOverWriteOption == KEEP_NEWER || _TempOverWriteOption == KEEP_NEWER_ALL)
                                { // New file?
                                  if (fi.LastWriteTime > dic[FileIndex].LastWriteDateTime)
                                  {
                                    fSkip = true; // old directory
                                  }
                                }
                              }

                              if (fSkip == false)
                              {
                                //隠し属性を削除する
                                //fi.Attributes &= ~FileAttributes.Hidden;
                                //読み取り専用を削除
                                //fi.Attributes &= ~FileAttributes.ReadOnly;

                                //すべての属性を解除
                                File.SetAttributes(path, FileAttributes.Normal);
                              }

                            }

                          }// end if ( File.Exists );

                          // Salvage data mode
                          // サルベージ・モード
                          if (_fSalvageToCreateParentFolderOneByOne == true)
                          {
                            // Decrypt one by one while creating the parent folder.
                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                          }

                          if (fSkip == true)
                          {
                            // Not create file
                          }
                          else
                          {
                            outfs = new FileStream(path, FileMode.Create, FileAccess.Write);
                          }

                          _OutputFileList.Add(path);
                          FileSize = 0;

                        }

                      }

                    }// end if (outfs == null);

                    //----------------------------------------------------------------------
                    // Write data
                    //----------------------------------------------------------------------
                    if (FileSize + len < (Int64)dic[FileIndex].FileSize)
                    {
                      if (outfs != null || fSkip == true)
                      {
                        // まだまだ書き込める
                        // can write more
                        if (fSkip == false)
                        {
                          outfs.Write(byteArray, buffer_size - len, len);
                        }
                        FileSize += len;
                        _TotalSize += len;
                        len = 0;
                      }
                    }
                    else
                    {
                      // ファイルの境界を超えて読み込んでいる
                      // Reading beyond file boundaries
                      int rest = (int)(dic[FileIndex].FileSize - FileSize);

                      if (fSkip == false)
                      {
                        // 書き込み完了
                        // Write completed
                        outfs.Write(byteArray, buffer_size - len, rest);
                      }

                      _TotalSize += rest;

                      len -= rest;

                      if (outfs != null)
                      {
                        // 生成したファイルを閉じる
                        // File close
                        outfs.Close();
                        outfs = null;
                      }

                      //----------------------------------------------------------------------
                      // ファイル属性の復元
                      // Restore file attributes

                      if (fSkip == false)
                      {
                        FileInfo fi = new FileInfo(dic[FileIndex].FilePath);

                        // タイムスタンプの復元
                        // Restore the timestamp of a file
                        fi.CreationTime = (DateTime)dic[FileIndex].CreationDateTime;
                        fi.LastWriteTime = (DateTime)dic[FileIndex].LastWriteDateTime;

                        // ファイル属性の復元
                        // Restore file attribute.
                        fi.Attributes = (FileAttributes)dic[FileIndex].FileAttribute;

                        // ハッシュ値のチェック
                        // Check the hash of a file
                        if (_fSalvageIgnoreHashCheck == false)
                        {
                          string hash = GetSha256FromFile(dic[FileIndex].FilePath);
                          if (hash != dic[FileIndex].Hash.ToString())
                          {
                            _ReturnCode = NOT_CORRECT_HASH_VALUE;
                            _ErrorFilePath = dic[FileIndex].FilePath;
                            return (false);
                          }
                        }

                      }

                      FileSize = 0;
                      FileIndex++;

                      fSkip = false;

                      if (FileIndex > dic.Count - 1)
                      {
                        _ReturnCode = DECRYPT_SUCCEEDED;
                        return (true);
                      }

                    }
                    //----------------------------------------------------------------------
                    //進捗の表示
                    string MessageText = "";
                    if (_TotalNumberOfFiles > 1)
                    {
                      MessageText = FilePath + " ( " + _NumberOfFiles.ToString() + "/" + _TotalNumberOfFiles.ToString() + " files" + " )";
                    }
                    else
                    {
                      MessageText = FilePath;
                    }

                    MessageList = new ArrayList();
                    MessageList.Add(DECRYPTING);
                    MessageList.Add(MessageText);

                    // プログレスバーの更新間隔を100msに調整
                    if (swProgress.ElapsedMilliseconds > 100)
                    {
                      percent = ((float)_TotalSize / _TotalFileSize);
                      worker.ReportProgress((int)(percent * 10000), MessageList);
                      swProgress.Restart();
                    }

                    // User cancel
                    if (worker.CancellationPending == true)
                    {
                      if (outfs != null)
                      {
                        outfs.Close();
                        outfs = null;
                      }
                      e.Cancel = true;
                      return (false);
                    }

                  }// end while(len > 0);

                }// end while ((len = ds.Read(byteArray, 0, BUFFER_SIZE)) > 0); 

              }// end using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Decompress));

            }// end using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read));

          }// end using (Rijndael aes = new RijndaelManaged());

        }// end using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read));

      }
      catch (UnauthorizedAccessException)
      {
        //オペレーティング システムが I/O エラーまたは特定の種類のセキュリティエラーのためにアクセスを拒否する場合、スローされる例外
        //The exception that is thrown when the operating system denies access
        //because of an I/O error or a specific type of security error.
        _ReturnCode = OS_DENIES_ACCESS;
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
        _ReturnCode = IO_EXCEPTION;
        _ErrorMessage = ex.Message;
        return (false);
      }
      finally
      {
        swProgress.Stop();
        swDecrypt.Stop();
        // 計測時間
        TimeSpan ts = swDecrypt.Elapsed;
        _DecryptionTimeString = 
          Convert.ToString(ts.Hours) + "h" + Convert.ToString(ts.Minutes) + "m" + 
          Convert.ToString(ts.Seconds) + "s" +Convert.ToString(ts.Milliseconds) + "ms";
      }

    }// end Decrypt();

    /// 計算してチェックサム（SHA-256）を得る
    /// Get a check sum (SHA-256) to calculate
    private static string GetSha256FromFile(string FilePath)
    {
      using (BufferedStream bs = new BufferedStream(File.OpenRead(FilePath), 16 * 1024 * 1024))
      {
        SHA256Managed sha = new SHA256Managed();
        byte[] hash = sha.ComputeHash(bs);

        StringBuilder result = new StringBuilder();
        result.Capacity = 32;
        foreach (byte b in hash)
        {
          result.Append(b.ToString());
        }
        return (result.ToString());
      }
    }
 
    /// ファイル名に連番を振る
    /// Put a serial number to the file name
    private string getFileNameWithSerialNumber(string FilePath, int SerialNum)
    {
      string DirPath = Path.GetDirectoryName(FilePath);
      string FileName = Path.GetFileNameWithoutExtension(FilePath) + SerialNum.ToString("0000") + Path.GetExtension(FilePath);

      return Path.Combine(DirPath, FileName);

    }

  }//end class FileDecrypt2;

}//end namespace AttacheCase;
