using System;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using AttacheCase.Properties;
using System.Text;
using System.Security.Cryptography;

namespace AttacheCase
{
  /// <summary>
  /// Save AttacheCase setting to registry class by Singleton pattern
  /// </summary>
  public class AppSettings
  {
    // File type
    private const int FILE_TYPE_ERROR        = -1;
    private const int FILE_TYPE_NONE         = 0;
    private const int FILE_TYPE_ATC          = 1;
    private const int FILE_TYPE_ATC_EXE      = 2;
    private const int FILE_TYPE_PASSWORD_ZIP = 3;

    // Process Type
    private const int PROCESS_TYPE_ERROR        = -1;
    private const int PROCESS_TYPE_NONE         = 0;
    private const int PROCESS_TYPE_ATC          = 1;
    private const int PROCESS_TYPE_ATC_EXE      = 2;
    private const int PROCESS_TYPE_PASSWORD_ZIP = 3;
    private const int PROCESS_TYPE_DECRYPTION   = 4;
    

    //
    // An INI file handling class using C#
    // http://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C
    //
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    //-----------------------------------
    // メンバ変数(Member Variable)
    //-----------------------------------

    // Self instance
    private static AppSettings _Instance;

    private string RegistryPathAppInfo = String.Format(@"Software\Hibara\{0}\AppInfo", "AttacheCase3");
    private string RegistryPathWindowPos = String.Format(@"Software\Hibara\{0}\WindowPos", "AttacheCase3");
    private string RegistryPathMyKey = String.Format(@"Software\Hibara\{0}\MyKey", "AttacheCase3");
    private string RegistryPathOption = String.Format(@"Software\Hibara\{0}\Option", "AttacheCase3");
    
    // Static instance ( Singleton pattern )
    public static AppSettings Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new AppSettings();
        }
        return _Instance;
      }
      set 
      { 
        _Instance = value; 
      }
    }

    //----------------------------------------------------------------------
    // File List
    private List<string> _FileList = new List<string>();
    public List<string> FileList
    {
      get { return this._FileList; }
      set { this._FileList = value; }
    }
                           
    //----------------------------------------------------------------------
    // 一時的な設定ファイルパス（INIファイル）
    // Temporary setting file path ( INI file )
    #region
    private string _IniFilePath;
    public string IniFilePath
    {
      get { return this._IniFilePath; }
      set { this._IniFilePath = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Window Pos
    #region
    //----------------------------------------------------------------------
    private int _FormTop;
    public int FormTop
    {
      get { return this._FormTop; }
      set { this._FormTop = value; }
    }
    private int _FormLeft;
    public int FormLeft
    {
      get { return this._FormLeft; }
      set { this._FormLeft = value; }
    }
    private int _FormWidth;
    public int FormWidth
    {
      get { return this._FormWidth; }
      set { this._FormWidth = value; }
    }
    private int _FormHeight;
    public int FormHeight
    {
      get { return this._FormHeight; }
      set { this._FormHeight = value; }
    }
    private int _FormStyle;
    public int FormStyle
    {
      get { return this._FormStyle; }
      set { this._FormStyle = value; }
    }
    private int _TabSelectedIndex;
    public int TabSelectedIndex
    {
      get { return this._TabSelectedIndex; }
      set { this._TabSelectedIndex = value; }
    }
    private string _InitDirPath;
    public string InitDirPath
    {
      get { return this._InitDirPath; }
      set { this._InitDirPath = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // General
    #region
    //----------------------------------------------------------------------

    private bool _fEndToExit;             // 処理後に終了するか
    //Exit AttacheCase after process.
    public bool fEndToExit
    {
      get { return this._fEndToExit; }
      set { this._fEndToExit = value; }
    }

    private bool _fOpenFile;              // 復号したファイルを関連付けされたソフトで開く
    //Open decrypted files by associated application
    public bool fOpenFile
    {
      get { return this._fOpenFile; }
      set { this._fOpenFile = value; }
    }

    private bool _fShowDialogWhenExeFile; // 復号したファイルに実行ファイルが含まれるとき警告ダイアログを出す
    //Show dialog when containing the executable file.
    public bool fShowDialogWhenExeFile
    {
      get { return this._fShowDialogWhenExeFile; }
      set { this._fShowDialogWhenExeFile = value; }
    }

    private int _ShowDialogWhenMultipleFilesNum; // 復号したファイルが複数個あるとき警告ダイアログを出す
    //Show dialog when more than multiple files
    public int ShowDialogWhenMultipleFilesNum
    {
      get { return this._ShowDialogWhenMultipleFilesNum; }
      set { this._ShowDialogWhenMultipleFilesNum = value; }
    }

    private bool _fAskEncDecode;                 // 暗号/復号処理かを問い合わせる
    //Ask to encrypt or decrypt regardless of file content
    public bool fAskEncDecode
    {
      get { return this._fAskEncDecode; }
      set { this._fAskEncDecode = value; }
    }

    private bool _fNoHidePassword;               //「*」で隠さずパスワードを確認しながら入力する
    //Confirm inputting password without masking
    public bool fNoHidePassword
    {
      get { return this._fNoHidePassword; }
      set { this._fNoHidePassword = value; }
    }

    //Always output to Executable file
    private bool _fSaveToExeout;                // 常に自己実行形式で出力する
    public bool fSaveToExeout
    {
      get { return this._fSaveToExeout; }
      set { this._fSaveToExeout = value; }
    }

    private bool _fShowExeoutChkBox;            // メインフォームにチェックボックスを表示する
    //Always display chekbox of this option
    public bool fShowExeoutChkBox
    {
      get { return this._fShowExeoutChkBox; }
      set { this._fShowExeoutChkBox = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Passwords
    #region
    //----------------------------------------------------------------------
    private bool _fMyEncryptPasswordKeep;            // 暗号化パスワードを記憶するか
    //Remember password for encryption
    public bool fMyEncryptPasswordKeep
    {
      get { return this._fMyEncryptPasswordKeep; }
      set { this._fMyEncryptPasswordKeep = value; }
    }

    private string _MyEncryptPasswordString;          // 暗号化パスワード（文字列）
    public string MyEncryptPasswordString
    {
      get { return this._MyEncryptPasswordString; }
      set { this._MyEncryptPasswordString = value; }
    }

    private byte[] _MyEncryptPasswordBinary;          // 暗号化パスワード（バイナリ）
    public byte[] MyEncryptPasswordBinary
    {
      get { return this._MyEncryptPasswordBinary; }
      set { this._MyEncryptPasswordBinary = value; }
    }

    private string _EncryptPasswordStringFromCommandLine; // コマンドラインからの暗号化パスワード（文字列）
    public string EncryptPasswordStringFromCommandLine
    {
      get { return this._EncryptPasswordStringFromCommandLine; }
      set { this._EncryptPasswordStringFromCommandLine = value; }
    }
                  
    private bool _fMyDecryptPasswordKeep;             // 復号パスワードを記憶するか
    //Remember &Decryption password
    public bool fMyDecryptPasswordKeep
    {
      get { return this._fMyDecryptPasswordKeep; }
      set { this._fMyDecryptPasswordKeep = value; }
    }

    private string _MyDecryptPasswordString;           // 復号パスワード（文字列）
    public string MyDecryptPasswordString
    {
      get { return this._MyDecryptPasswordString; }
      set { this._MyDecryptPasswordString = value; }
    }

    private byte[] _MyDecryptPasswordBinary;           // 復号パスワード（バイナリ）
    public byte[] MyDecryptPasswordBinary
    {
      get { return this._MyDecryptPasswordBinary; }
      set { this._MyDecryptPasswordBinary = value; }
    }

    private string _DecryptPasswordStringFromCommandLine; // コマンドラインからの復号パスワード（文字列）
    public string DecryptPasswordStringFromCommandLine
    {
      get { return this._DecryptPasswordStringFromCommandLine; }
      set { this._EncryptPasswordStringFromCommandLine = value; }
    }

    private bool _fMemPasswordExe;                 //記憶パスワードで確認なく実行する
    //Encrypt/Decrypt by &memorized password without confirming
    public bool fMemPasswordExe
    {
      get { return this._fMemPasswordExe; }
      set { this._fMemPasswordExe = value; }
    }

    // Not mask password character
    private bool _fNotMaskPassword;
    public bool fNotMaskPassword
    {
      get { return this._fNotMaskPassword; }
      set { this._fNotMaskPassword = value; }
    }
    

    #endregion

    //----------------------------------------------------------------------
    // Window
    #region
    //----------------------------------------------------------------------
    private bool _fMainWindowMinimize;     // 常にウィンドウを最小化して処理する
    //Always execute by minimize the window
    public bool fMainWindowMinimize
    {
      get { return this._fMainWindowMinimize; }
      set { this._fMainWindowMinimize = value; }
    }

    private bool _fTaskBarHide;           // タスクバーに表示しない
    //Minimizing a window without appearing in the taskbar
    public bool fTaskBarHide
    {
      get { return this._fTaskBarHide; }
      set { this._fTaskBarHide = value; }
    }

    private bool _fTaskTrayIcon;          // タスクトレイにアイコンを表示する
    //Display in the task tray
    public bool fTaskTrayIcon
    {
      get { return this._fTaskTrayIcon; }
      set { this._fTaskTrayIcon = value; }
    }

    private bool _fOpenFolder;              // フォルダの場合に復号後に開くか
    //In the case of the folder, open it in Explorer after decrypting
    public bool fOpenFolder
    {
      get { return this._fOpenFolder; }
      set { this._fOpenFolder = value; }
    }

    private bool _fWindowForeground;       // デスクトップで最前面にウィンドウを表示する
    //Bring AttcheCase window in front of Desktop
    public bool fWindowForeground
    {
      get { return this._fWindowForeground; }
      set { this._fWindowForeground = value; }
    }

    private bool _fNoMultipleInstance;    // 複数起動しない
    //Not Allow multiple in&stance of AttcheCase
    public bool fNoMultipleInstance
    {
      get { return this._fNoMultipleInstance; }
      set { this._fNoMultipleInstance = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Save 
    #region
    //----------------------------------------------------------------------
    private int _TempOverWriteOption;
    // Temporary option for overwriting
    public int TempOverWriteOption
    {
      get { return this._TempOverWriteOption; }
      set { this._TempOverWriteOption = value; }
    }
    // Temporay option for overwriting for new date only.
    private bool _OverWriteForNewDate;
    public bool OverWriteForNewDate
    {
      get { return this._OverWriteForNewDate; }
      set { this._OverWriteForNewDate = value; }
    }

    private int _EncryptionFileType = 0;              // 暗号化ファイルの種類
                                                      // Encryption type
    public int EncryptionFileType
    {
      get { return this._EncryptionFileType; }
      set { this._EncryptionFileType = value; }
    }

    private bool _fEncryptionSameFileTypeAlways;      // 常に同じ暗号化ファイルの種類にする
                                                      // Save same encryption type always.
    public bool fEncryptionSameFileTypeAlways
    {
      get { return this._fEncryptionSameFileTypeAlways; }
      set { this._fEncryptionSameFileTypeAlways = value; }
    }

    private int _EncryptionSameFileTypeAlways;        // 常に同じ暗号化ファイルの種類
                                                      // Same encryption type always.
    public int EncryptionSameFileTypeAlways
    {
      get { return this._EncryptionSameFileTypeAlways; }
      set { this._EncryptionSameFileTypeAlways = value; }
    }

    private bool _fEncryptionSameFileTypeBefore;      // 前に使った暗号化ファイルの種類にする
    // Save same encryption type that was used to before.
    public bool fEncryptionSameFileTypeBefore
    {
      get { return this._fEncryptionSameFileTypeBefore; }
      set { this._fEncryptionSameFileTypeBefore = value; }
    }

    private int _EncryptionSameFileTypeBefore;       // 前に使った暗号化ファイルの種類
                                                      // Same encryption type that was used to before.
    public int EncryptionSameFileTypeBefore
    {
      get { return this._EncryptionSameFileTypeBefore; }
      set { this._EncryptionSameFileTypeBefore = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Save Encrypt
    #region
    //----------------------------------------------------------------------

    private bool _fSaveToSameFldr;                 // 暗号化ファイルを常に同じ場所に保存するか
    //Save to same folder in &encryption
    public bool fSaveToSameFldr
    {
      get { return this._fSaveToSameFldr; }
      set { this._fSaveToSameFldr = value; }
    }

    private string _SaveToSameFldrPath;           // その保存場所
    // The folder path
    public string SaveToSameFldrPath
    {
      get { return this._SaveToSameFldrPath; }
      set { this._SaveToSameFldrPath = value; }
    }

    private bool _fEncryptConfirmOverwrite;      // 同名ファイルの上書きを確認するか
    //Confirm overwriting when same filename exists
    public bool fEncryptConfirmOverwrite
    {
      get { return this._fEncryptConfirmOverwrite; }
      set { this._fEncryptConfirmOverwrite = value; }
    }

    private bool _fNormal;                // 何もしない
    //Normal
    public bool fNormal
    {
      get { return this._fNormal; }
      set
      {
        if ((this._fNormal = value) == true)
        {
          _fAllFilePack = false;
          _fFilesOneByOne = false;
        }
      }
    }

    private bool _fAllFilePack;           // 複数のファイルを暗号化する際は一つにまとめる
    //Create one encrypted file from files
    public bool fAllFilePack
    {
      get { return this._fAllFilePack; }
      set
      {
        if ((this._fAllFilePack = value) == true)
        {
          _fNormal = false;
          _fFilesOneByOne = false;
        }
      }
    }

    private bool _fFilesOneByOne;         // フォルダ内のファイルは個別に暗号化/復号する
    //Encrypt or decrypt files in directory one by one
    public bool fFilesOneByOne
    {
      get { return this._fFilesOneByOne; }
      set
      {
        if ((this._fFilesOneByOne = value) == true)
        {
          _fNormal = false;
          _fAllFilePack = false;
        }
      }
    }

    private bool _fKeepTimeStamp;         // 暗号化ファイルのタイムスタンプを元ファイルに合わせる
    //Set the timestamp of encryption file to original files or directories
    public bool fKeepTimeStamp
    {
      get { return this._fKeepTimeStamp; }
      set { this._fKeepTimeStamp = value; }
    }

    private bool _fExtInAtcFileName;      // 暗号化ファイル名に拡張子を含める
    //Create encrypted file &including extension
    public bool fExtInAtcFileName
    {
      get { return this._fExtInAtcFileName; }
      set { this._fExtInAtcFileName = value; }
    }

    private bool _fAutoName;              // 自動で暗号化ファイル名を付加する
    //Specify the format of the encryption file name
    public bool fAutoName
    {
      get { return this._fAutoName; }
      set { this._fAutoName = value; }
    }

    private string _AutoNameFormatText;    // 自動で付加するファイル名書式
    public string AutoNameFormatText
    {
      get { return this._AutoNameFormatText; }
      set { this._AutoNameFormatText = value; }
    }

    private bool _fAutoNameAlphabets;      // アルファベットを使う
    // Use alphabets
    public bool fAutoNameAlphabets
    {
      get { return this._fAutoNameAlphabets; }
      set { this._fAutoNameAlphabets = value; }
    }

    private bool _fAutoNameLowerCase;      // アルファベットの小文字を使う
    // Use alphabets for lower case
    public bool fAutoNameLowerCase
    {
      get { return this._fAutoNameLowerCase; }
      set { this._fAutoNameLowerCase = value; }
    }

    private bool _fAutoNameUpperCase;      // アルファベットの大文字を使う
    // Use alphabets for upper case
    public bool fAutoNameUpperCase
    {
      get { return this._fAutoNameUpperCase; }
      set { this._fAutoNameUpperCase = value; }
    }

    private bool _fAutoNameNumbers;       // 数字を使う
    // Use string of numbers
    public bool fAutoNameNumbers
    {
      get { return this._fAutoNameNumbers; }
      set { this._fAutoNameNumbers = value; }
    }

    private bool _fAutoNameSymbols;       // 記号を使う
    // Use symbols
    public bool fAutoNameSymbols
    {
      get { return this._fAutoNameSymbols; }
      set { this._fAutoNameSymbols = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Save Decrypt
    #region
    //----------------------------------------------------------------------

    //Save to same folder in decryption
    private bool _fDecodeToSameFldr;                  // 常に同じ場所へファイルを復号するか
    public bool fDecodeToSameFldr
    {
      get { return this._fDecodeToSameFldr; }
      set { this._fDecodeToSameFldr = value; }
    }

    private string _DecodeToSameFldrPath;             // その保存場所
    // The folder path
    public string DecodeToSameFldrPath
    {
      get { return this._DecodeToSameFldrPath; }
      set { this._DecodeToSameFldrPath = value; }
    }

    private bool _fDecryptConfirmOverwrite;          // 同名ファイルの上書きを確認するか
    //Confirm overwriting when same filename exists
    public bool fDecryptConfirmOverwrite
    {
      get { return this._fDecryptConfirmOverwrite; }
      set { this._fDecryptConfirmOverwrite = value; }
    }

    private bool _fNoParentFldr;                     // 復号するときに親フォルダを生成しない
    //Create no parent folder in decryption
    public bool fNoParentFldr
    {
      get { return this._fNoParentFldr; }
      set { this._fNoParentFldr = value; }
    }

    private bool _fSameTimeStamp;                   // ファイル、フォルダーのタイムスタンプを復号時に合わせる
    //Set the timestamp to decrypted files or directories
    public bool fSameTimeStamp
    {
      get { return this._fSameTimeStamp; }
      set { this._fSameTimeStamp = value; }
    }

    private bool _fCompareFile;                    // 暗号処理後にファイルコンペアを行うか
    //Com&pare data files after encryption
    public bool fCompareFile
    {
      get { return this._fCompareFile; }
      set { this._fCompareFile = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Password ZIP 
    #region
    //----------------------------------------------------------------------
    //Save to the same folder in decryption
    private bool _fZipToSameFldr;                // 常に同じ場所へファイルを復号するか
    public bool fZipToSameFldr
    {
      get { return this._fZipToSameFldr; }
      set { this._fZipToSameFldr = value; }
    }

    private string _ZipToSameFldrPath;           // その保存場所
    public string ZipToSameFldrPath              // The folder path
    {
      get { return this._ZipToSameFldrPath; }
      set { this._ZipToSameFldrPath = value; }
    }

    private bool _fZipConfirmOverwrite;          // 同名ファイルの上書きを確認するか
    public bool fZipConfirmOverwrite             // Confirm overwriting when the same filename exists
    {
      get { return this._fZipConfirmOverwrite; }
      set { this._fZipConfirmOverwrite = value; }
    }

    private int _ZipEncryptionAlgorithm;           // 暗号アルゴリズム
    public int ZipEncryptionAlgorithm              // 0: PkzipWeak, 1: WinZipAes128, 2: WinZipAes256 
    {
      get { return this._ZipEncryptionAlgorithm; }
      set { this._ZipEncryptionAlgorithm = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Delete
    #region
    //----------------------------------------------------------------------

    private bool _fDelOrgFile;                  // 暗号化した後、元ファイルを削除する
    //Delete original files or directories after encryption
    public bool fDelOrgFile
    {
      get { return this._fDelOrgFile; }
      set { this._fDelOrgFile = value; }
    }

    private bool _fEncryptShowDelChkBox;            // メインフォームにチェックボックスを表示する
    //Show the check box in main form window
    public bool fEncryptShowDelChkBox
    {
      get { return this._fEncryptShowDelChkBox; }
      set { this._fEncryptShowDelChkBox = value; }
    }

    private bool _fConfirmToDeleteAfterEncryption;  // 暗号化後に元ファイルまたはフォルダの削除確認メッセージを表示するか
    //Show confirmation dialog to delete file or directories
    public bool fConfirmToDeleteAfterEncryption
    {
      get { return this._fConfirmToDeleteAfterEncryption; }
      set { this._fConfirmToDeleteAfterEncryption = value; }
    }

    private bool _fDelEncFile;                     // 復号した後、暗号化ファイルを削除する
    //Delete encrypted file after decryption
    public bool fDelEncFile
    {
      get { return this._fDelEncFile; }
      set { this._fDelEncFile = value; }
    }

    private bool _fDecryptShowDelChkBox;            // メインフォームにチェックボックスを表示する
    //Show the check box in main form window
    public bool fDecryptShowDelChkBox
    {
      get { return this._fDecryptShowDelChkBox; }
      set { this._fDecryptShowDelChkBox = value; }
    }

    private bool _fConfirmToDeleteAfterDecryption;  // 復号後に元の暗号化ファイルを削除確認メッセージを表示するか
    //Show confirmation dialog to delete file or directories
    public bool fConfirmToDeleteAfterDecryption
    {
      get { return this._fConfirmToDeleteAfterDecryption; }
      set { this._fConfirmToDeleteAfterDecryption = value; }
    }
      
    private int _fCompleteDelFile;                 // 完全削除を行うか(0:通常，1:ごみ箱, 2:完全削除）
    //Advanced Delete Option [0: Normal Delete, 1: Complete erase, 2: Send to Trash ]
    public int fCompleteDelFile
    {
      get { return this._fCompleteDelFile; }
      set
      {
        if ((this._fCompleteDelFile = value) < 0 || (this._fCompleteDelFile = value) > 3)
        {
          this._fCompleteDelFile = 2;
        }
      }
    }

    //Set the number of time to overwrite and completely delete
    private int _DelRandNum;                 // 乱数を何回書き込み消去するか
    //Number of Random data
    public int DelRandNum
    {
      get { return this._DelRandNum; }
      set { this._DelRandNum = value; }
    }

    private int _DelZeroNum;                 // NULLを何回書き込み消去するか
    //Number of Zeros 
    public int DelZeroNum
    {
      get { return this._DelZeroNum; }
      set { this._DelZeroNum = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Compression
    #region
    //----------------------------------------------------------------------

    private int _CompressRate;               // 圧縮率
    // Enable compression
    public int CompressRate
    {
      get { return this._CompressRate; }
      set
      {
        if ((this._CompressRate = value) < 0 || (this._CompressRate = value) > 9)
        {
          this._CompressRate = 6;
        }
      }
    }

    #endregion

    //----------------------------------------------------------------------
    // System
    #region
    //----------------------------------------------------------------------

    private int _fAssociationFile;                //関連付け設定
    //Association with AttacheCase files ( *.atc )
    public int fAssociationFile
    {
      get { return this._fAssociationFile; }
      set { this._fAssociationFile = value; }
    }

    private int _AtcsFileIconIndex;               // ファイルアイコン番号
    // Number of preset icon
    public int AtcsFileIconIndex
    {
      get { return this._AtcsFileIconIndex; }
      set
      {
        if ((this._AtcsFileIconIndex = value) < 0 || (this._AtcsFileIconIndex = value) > 4)
        {
          this._AtcsFileIconIndex = 1;
        }
      }
    }

    private string _UserRegIconFilePath;       // ユーザー指定のファイルアイコンパス
                                               // Specify the my file icon path
    public string UserRegIconFilePath
    {
      get { return this._UserRegIconFilePath; }
      set { this._UserRegIconFilePath = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Password file
    #region
    //----------------------------------------------------------------------

    private bool _fAllowPassFile;                  // パスワードファイルのドラッグ＆ドロップを許す
    //Allow a password file to drag and drop
    public bool fAllowPassFile
    {
      get { return this._fAllowPassFile; }
      set { this._fAllowPassFile = value; }
    }

    private bool _fCheckPassFile;                  // 暗号化時にパスワードファイルを自動チェックする
    //Check password file for Encryption
    public bool fCheckPassFile
    {
      get { return this._fCheckPassFile; }
      set { this._fCheckPassFile = value; }
    }

    private string _PassFilePath;                 // 暗号時のパスワードファイルパス
                                                  //Password file path for encryption
    public string PassFilePath
    {
      get { return this._PassFilePath; }
      set { this._PassFilePath = value; }
    }

    private string _TempEncryptionPassFilePath;    // 暗号時の一時パスワードファイルパス（保存されない）
    //The path of the password file that is dragged and dropped by user
    public string TempEncryptionPassFilePath
    {
      get { return this._TempEncryptionPassFilePath; }
      set { this._TempEncryptionPassFilePath = value; }
    }


    private bool _fCheckPassFileDecrypt;          // 復号時にパスワードファイルを自動チェックする
    //Check password file for Decryption
    public bool fCheckPassFileDecrypt
    {
      get { return this._fCheckPassFileDecrypt; }
      set { this._fCheckPassFileDecrypt = value; }
    }

    private string _PassFilePathDecrypt;          // 復号時のパスワードファイルパス
    //Password file path for decryption
    public string PassFilePathDecrypt
    {
      get { return this._PassFilePathDecrypt; }
      set { this._PassFilePathDecrypt = value; }
    }

    private string _TempDecryptionPassFilePath;    // 暗号時の一時パスワードファイルパス（保存されない）
                                                   //The path of the password file that is dragged and dropped by user
    public string TempDecryptionPassFilePath
    {
      get { return this._TempDecryptionPassFilePath; }
      set { this._TempDecryptionPassFilePath = value; }
    }

    private bool _fNoErrMsgOnPassFile;            // パスワードファイルがない場合エラーを出さない
    public bool fNoErrMsgOnPassFile               // //It's not issued an error message when password file doesn't exists
    {
      get { return this._fNoErrMsgOnPassFile; }
      set { this._fNoErrMsgOnPassFile = value; }
    }

    private bool _fPasswordFileExe;               //パスワードファイルで確認なく実行する
    public bool fPasswordFileExe                  //Encrypt/Decrypt by the password of Password file without confirming.
    {
      get { return this._fPasswordFileExe; }
      set { this._fPasswordFileExe = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Camouflage Extension
    #region
    //----------------------------------------------------------------------
    private bool _fAddCamoExt;            // 暗号化ファイルの拡張子を偽装する
                                          // Encrypted files camouflage with extension
    public bool fAddCamoExt
    {
      get { return this._fAddCamoExt; }
      set { this._fAddCamoExt = value; }
    }

    private string _CamoExt;              // その拡張子
                                          // It's extension string
    public string CamoExt
    {
      get { return this._CamoExt; }
      set { this._CamoExt = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Input Password limit
    #region
    //----------------------------------------------------------------------

    private int _MissTypeLimitsNum;          // パスワードのタイプミス制限回数（ver.2.70～）
    //Set number of times to input password in encrypt files:
    public int MissTypeLimitsNum
    {
      get { return this._MissTypeLimitsNum; }
      set
      {
        if ((this._MissTypeLimitsNum = value) < 0 || (this._MissTypeLimitsNum = value) > 10)
        {
          this._MissTypeLimitsNum = 3; // default value
        }
      }
    }

    private bool _fBroken;                // タイプミス回数を超えたときにファイルを破壊するか否か（ver.2.70～）
    //If input wrong password to the number of times, destroy it
    public bool fBroken
    {
      get { return this._fBroken; }
      set { this._fBroken = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Salvage 
    #region
    private bool _fSalvageToCreateParentFolderOneByOne;
    // Decrypt one by one while creating the parent folder.
    public bool fSalvageToCreateParentFolderOneByOne
    {
      get { return this._fSalvageToCreateParentFolderOneByOne; }
      set { this._fSalvageToCreateParentFolderOneByOne = value; }
    }
    
    private bool _fSalvageIntoSameDirectory;
    // Decrypt all files into the directory of the same hierarchy.
    public bool fSalvageIntoSameDirectory
    {
      get { return this._fSalvageIntoSameDirectory; }
      set { this._fSalvageIntoSameDirectory = value; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Command line Only
    #region
    //----------------------------------------------------------------------

    // The sum of the command line arguments
    private int _CommandLineArgsNum = 0;          // コマンドライン引数の合計
    public int CommandLineArgsNum
    {
      get { return this._CommandLineArgsNum; }
    }
    
    private bool _fOver4GBok = false;             // 4GB超えを容認
    //Allow more than 4GB of file size to be created   
    public bool fOver4GBok
    {
      get { return this._fOver4GBok; }
      set { this._fOver4GBok = value; }
    }

    private bool _fHideMainForm = false;          // メインフォームを非表示
    //When running on the command line, do not display the main form
    public bool fHideMainForm
    {
      get { return this._fHideMainForm; }
      set { this._fHideMainForm = value; }
    }

    private bool _fNoErrorMsg = false;            // エラーメッセージ表示の抑制
    //When running on the command line, do not display error message
    public bool fNoErrorMsg
    {
      get { return this._fNoErrorMsg; }
      set { this._fNoErrorMsg = value; }
    }

    private int _ProcTypeWithoutAsk = 0;         // 暗号/復号処理か（動作設定にはない。コマンドラインのみ）
    //On the command line, specify encryption or decryption ( 1: Encrypt, 2: Decrypt )
    public int ProcTypeWithoutAsk
    {
      get { return this._ProcTypeWithoutAsk; }
      set { this._ProcTypeWithoutAsk = value; }
    }

    // 1: ATC, 2: EXE(ATC), 3: ZIP, 0: Others(Encrypt file?);
    private int[] _FileType = new int[4] { 0, 0, 0, 0 }; 
    public int[] FileType
    {
      get { return this._FileType; }
    }

    #endregion

    //----------------------------------------------------------------------
    // Others
    #region
    //----------------------------------------------------------------------

    private string _Language;                    //使用言語
    //Language
    public string Language
    {
      get { return this._Language; }
      set { this._Language = value; }
    }

    private string _CurrentConfiguration;         //現在の設定
    //Language
    public string CurrentConfiguration
    {
      get { return this._CurrentConfiguration; }
      set { this._CurrentConfiguration = value; }
    }

    private string _ApplicationPath;              //アタッシェケース本体（EXE）の場所
    public string ApplicationPath
    {
      get { return this._ApplicationPath;  }
      set { this._ApplicationPath = value; }
    }

    private int _AppVersion;                      //アタッシェケースのバージョン
    // Get this application version
    public int AppVersion
    {
      get { return this._AppVersion; }
      set { this._AppVersion = value; }
    }

    private int _ActiveTreeNode;                  // 開いていたツリービュー項目
    // Active option panel 
    public int ActiveTreeNode
    {
      get { return this._ActiveTreeNode; }
      set { this._ActiveTreeNode = value; }
    }


    #endregion

    //----------------------------------------------------------------------
    /// <summary>
    /// Cunstractor（コンストラクタ）
    /// </summary>
    //----------------------------------------------------------------------
    private AppSettings()
    {
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// アタッシェケースの設定を読み込まれたソースから取得する
    /// Get the AttacheCase configuration from the settings of reading sources on the beginning
    /// </summary>
    //----------------------------------------------------------------------
    public void ReadOptions()
    {
      //----------------------------------------------------------------------
      // アタッシェケースのすべての設定をレジストリから読み込む
      // Load ALL settings of AttacheCase from registry
      this.ReadOptionsFromRegistry();

      //----------------------------------------------------------------------
      // アタッシェケース本体のある場所に設定用INIファイルがあるか？
      // Is there INI file in the location where AttacheCase Application exists?
      string FilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "_AtcCase.ini");
      if (File.Exists(FilePath) == true)
      {
        _IniFilePath = FilePath;
        ReadOptionFromIniFile(_IniFilePath);
      }

      //----------------------------------------------------------------------
      // 起動時のコマンドライン引数に渡されたファイルパスに、設定用INIファイルがあるか？ 
      // Is there INI file in the file path of startup command line arguments?
      string[] cmds = Environment.GetCommandLineArgs();
      for(int i = 0; i < cmds.Length; i++)
      {
        // File list processed
        if (i > 0 && cmds[i].IndexOf("/") != 0)
        {
          if (File.Exists(cmds[i]) == true || Directory.Exists(cmds[i]) == true)
          {
            FilePath = Path.Combine(Path.GetDirectoryName(cmds[i]), "_AtcCase.ini");
            if (File.Exists(FilePath) == true)
            {
              _IniFilePath = FilePath;
              ReadOptionFromIniFile(_IniFilePath);
              break;
            }
          }
        }
      }

      //----------------------------------------------------------------------
      // 起動時のコマンドラインオプションから設定を読み込む（上書き）
      // Load the settings from startup command line option ( Overwrite )
      _CommandLineArgsNum = this.ParserArguments();

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// アタッシェケースの設定を読み込まれたソースへ保存する（または保存しない）
    /// Save the setting of AttacheCase to source of reading on the beginning (or not to save)
    /// </summary>
    //----------------------------------------------------------------------
    public void SaveOptions(bool fTemporarySettings)
    {
      //コマンドラインからの設定が読み込まれているときは保存しない
      if (_CommandLineArgsNum > 0 && fTemporarySettings == true)
      {
        // Do not save when the setting from the command line argument is loaded
        return;
      }

      if (File.Exists(_IniFilePath) == true && fTemporarySettings == true)  
      {
        // INIファイルへの保存
        // If there is to read INI file, save to it.
        WriteOptionToIniFile(_IniFilePath);
        return;
      }

      //レジストリへの保存
      SaveOptionsToRegistry();

    }
        
    //----------------------------------------------------------------------
    /// <summary>
    /// Read all options of AttacheCase from sysytem registry
    /// アタッシェケースの設定をレジストリから読み込む
    /// </summary>
    //----------------------------------------------------------------------
    public void ReadOptionsFromRegistry()
    {
      using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathAppInfo, false))
      {
        if (reg == null)
        {
          Registry.CurrentUser.CreateSubKey(RegistryPathAppInfo);
          Registry.CurrentUser.CreateSubKey(RegistryPathWindowPos);
          Registry.CurrentUser.CreateSubKey(RegistryPathMyKey);
          Registry.CurrentUser.CreateSubKey(RegistryPathOption);
        }
      }

      //----------------------------------------------------------------------
      // Options has been stored in the registry for 'string type' in old version of AttacheCase,
      // so when you take it out, you need to cast values to the type as necessary.
      // 旧バージョンのアタッシェケースでは、レジストリへ文字列型で格納していたため、
      // レジストリから取り出すときには、必要に応じてその型へキャストする必要があります。
      //
      //-----------------------------------
      // Open the key (HKEY_CURRENT_USER\Software\Hibara\AttacheCase）by ReadOnly
      using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathAppInfo, true))
      {
        //Application infomation
        //_ApplicationPath = (string)reg.GetValue("AppPath", "");
        _ApplicationPath = Application.ExecutablePath;
        _AppVersion = (int)reg.GetValue("AppVersion", 0);
      }

      //----------------------------------------------------------------------
      // Windows Positions and size
      using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathWindowPos, true))
      {
        _FormTop = int.Parse((string)reg.GetValue("WindowTop", "-1"));
        _FormLeft = int.Parse((string)reg.GetValue("WindowLeft", "-1"));
        _FormWidth = int.Parse((string)reg.GetValue("WindowWidth", "420"));
        _FormHeight = int.Parse((string)reg.GetValue("WindowHeight", "380"));
        // Main form state
        _FormStyle = int.Parse((string)reg.GetValue("FormStyle", "0"));     // WindowState = Normal;
        // Selected node index
        _ActiveTreeNode = int.Parse((string)reg.GetValue("ActiveTreeNode", "0"));
        // Initial directory path in dialog
        _InitDirPath = (string)reg.GetValue("InitDirPath", "");
        
      }

      //----------------------------------------------------------------------
      // Store Passwords
      using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathMyKey, true))
      {
        _fMyEncryptPasswordKeep = ((string)reg.GetValue("fMyEncryptPasswordKeep", "0") == "1") ? true : false;
        _fMyDecryptPasswordKeep = ((string)reg.GetValue("fMyDecryptPasswordKeep", "0") == "1") ? true : false;

        _MyEncryptPasswordBinary = (byte[])reg.GetValue("MyEncryptPasswordString", null);
        if (_MyEncryptPasswordBinary == null)
        {
          _MyEncryptPasswordBinary = null;
        }
        else
        {
          _MyEncryptPasswordString = DecryptMyPassword(_MyEncryptPasswordBinary);
          _MyEncryptPasswordBinary = null;
        }

        _MyDecryptPasswordBinary = (byte[])reg.GetValue("MyDecryptPasswordString", null);
        if (_MyDecryptPasswordBinary == null)
        {
          _MyDecryptPasswordBinary = null;
        }
        else
        {
          _MyDecryptPasswordString = DecryptMyPassword(_MyDecryptPasswordBinary);
          _MyDecryptPasswordBinary = null;

        }

        _fMemPasswordExe = ((string)reg.GetValue("fMemPasswordExe", "0") == "1") ? true : false;
        _fNotMaskPassword = ((string)reg.GetValue("fNotMaskPassword", "0") == "1") ? true : false; 
        
      }

      //----------------------------------------------------------------------
      // Options
      using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathOption, true))
      {
        //-----------------------------------
        // General
        _fEndToExit = ((string)reg.GetValue("fEndToExit", "0") == "1") ? true : false;
        _fOpenFile = ((string)reg.GetValue("fOpenFile", "0") == "1") ? true : false;
        _fShowDialogWhenExeFile = ((string)reg.GetValue("fShowDialogWhenExeFile", "1") == "1") ? true : false;
        _ShowDialogWhenMultipleFilesNum = int.Parse((string)reg.GetValue("ShowDialogWhenMultipleFilesNum", "5"));
        _fAskEncDecode = ((string)reg.GetValue("fAskEncDecode", "0") == "1") ? true : false;
        _fNoHidePassword = ((string)reg.GetValue("fNoHidePassword", "0") == "1") ? true : false;
        _fSaveToExeout = ((string)reg.GetValue("fSaveToExeout", "0") == "1") ? true : false;
        _fShowExeoutChkBox = ((string)reg.GetValue("fShowExeoutChkBox", "1") == "1") ? true : false;

        //-----------------------------------
        // Window
        _fMainWindowMinimize = ((string)reg.GetValue("fMainWindowMinimize", "0") == "1") ? true : false;
        _fTaskBarHide = ((string)reg.GetValue("fTaskBarHide", "0") == "1") ? true : false;
        _fTaskTrayIcon = ((string)reg.GetValue("fTaskTrayIcon", "0") == "1") ? true : false;
        _fOpenFolder = ((string)reg.GetValue("fOpenFolder", "0") == "1") ? true : false;
        _fWindowForeground = ((string)reg.GetValue("fWindowForeground", "1") == "1") ? true : false;
        _fNoMultipleInstance = ((string)reg.GetValue("fNoMultipleInstance", "0") == "1") ? true : false;

        //-----------------------------------
        // Save Encrypt
        //Integer = 1: ATC, 2: EXE(ATC), 3: ZIP, 0: Others(Encrypt file?)
        _EncryptionFileType = int.Parse((string)reg.GetValue("EncryptionFileType", "0"));
        _fEncryptionSameFileTypeAlways = ((string)reg.GetValue("fEncryptionSameFileTypeAlways", "1") == "1") ? true : false;
        _EncryptionSameFileTypeAlways = int.Parse((string)reg.GetValue("EncryptionSameFileTypeAlways", "0"));

        _fEncryptionSameFileTypeBefore = ((string)reg.GetValue("fEncryptionSameFileTypeBefore", "1") == "1") ? true : false;
        _EncryptionSameFileTypeBefore = int.Parse((string)reg.GetValue("EncryptionSameFileTypeBefore", "0"));
        _fSaveToSameFldr = ((string)reg.GetValue("fSaveToSameFldr", "0") == "1") ? true : false;
        _SaveToSameFldrPath = (string)reg.GetValue("SaveToSameFldrPath", "");

        _fEncryptConfirmOverwrite = ((string)reg.GetValue("fEncryptConfirmOverwrite", "1") == "1") ? true : false;
        _fAllFilePack = ((string)reg.GetValue("fAllFilePack", "0") == "1") ? true : false;
        _fFilesOneByOne = ((string)reg.GetValue("fFilesOneByOne", "0") == "1") ? true : false;
        _fNormal = (_fAllFilePack == false && _fFilesOneByOne == false) ? true : false;
        _fKeepTimeStamp = ((string)reg.GetValue("fKeepTimeStamp", "0") == "1") ? true : false;
        _fExtInAtcFileName = ((string)reg.GetValue("fExtInAtcFileName", "0") == "1") ? true : false;
        _fAutoName = ((string)reg.GetValue("fAutoName", "0") == "1") ? true : false;
        _AutoNameFormatText = (string)reg.GetValue("AutoNameFormatText", "<filename>_<date:yyyy_mm_dd><ext>");

        _fAutoNameAlphabets = ((string)reg.GetValue("fAutoNameAlphabets", "1") == "1") ? true : false;
        _fAutoNameLowerCase = ((string)reg.GetValue("fAutoNameLowerCase", "1") == "1") ? true : false;
        _fAutoNameUpperCase = ((string)reg.GetValue("fAutoNameUpperCase", "1") == "1") ? true : false;
        _fAutoNameNumbers = ((string)reg.GetValue("fAutoNameNumbers", "1") == "1") ? true : false;
        _fAutoNameSymbols = ((string)reg.GetValue("fAutoNameSymbols", "0") == "1") ? true : false;

        //-----------------------------------
        // Save Decrypt
        _fDecodeToSameFldr = ((string)reg.GetValue("fDecodeToSameFldr", "0") == "1") ? true : false;
        _DecodeToSameFldrPath = (string)reg.GetValue("DecodeToSameFldrPath", "");
        _fDecryptConfirmOverwrite = ((string)reg.GetValue("fDecryptConfirmOverwrite", "1") == "1") ? true : false;
        _fNoParentFldr = ((string)reg.GetValue("fNoParentFldr", "0") == "1") ? true : false;
        _fSameTimeStamp = ((string)reg.GetValue("fSameTimeStamp", "0") == "1") ? true : false;
        _fCompareFile = ((string)reg.GetValue("fCompareFile", "0") == "1") ? true : false;

        //-----------------------------------
        // Password ZIP
        _fZipToSameFldr = ((string)reg.GetValue("fZipToSameFldr", "0") == "1") ? true : false;
        _ZipToSameFldrPath = (string)reg.GetValue("ZipToSameFldrPath", "");
        _fZipConfirmOverwrite = ((string)reg.GetValue("fZipConfirmOverwrite", "1") == "1") ? true : false;
        _ZipEncryptionAlgorithm = int.Parse((string)reg.GetValue("ZipEncryptionAlgorithm", "0"));

        //-----------------------------------
        // Delete
        _fDelOrgFile = ((string)reg.GetValue("fDelOrgFile", "0") == "1") ? true : false;
        _fEncryptShowDelChkBox = ((string)reg.GetValue("fEncryptShowDelChkBox", "0") == "1") ? true : false;
        _fConfirmToDeleteAfterEncryption = ((string)reg.GetValue("fConfirmToDeleteAfterEncryption", "1") == "1") ? true : false;

        _fDelEncFile = ((string)reg.GetValue("fDelEncFile", "0") == "1") ? true : false;
        _fDecryptShowDelChkBox = ((string)reg.GetValue("fDecryptShowDelChkBox", "0") == "1") ? true : false;
        _fConfirmToDeleteAfterDecryption = ((string)reg.GetValue("fConfirmToDeleteAfterDecryption", "1") == "1") ? true : false;
        
        _fCompleteDelFile = int.Parse((string)reg.GetValue("fCompleteDelFile", "0"));
        _DelRandNum = int.Parse((string)reg.GetValue("DelRandNum", "0"));
        _DelZeroNum = int.Parse((string)reg.GetValue("DelZeroNum", "1"));
        
        //-----------------------------------
        //Compression
        _CompressRate = int.Parse((string)reg.GetValue("CompressRate", "6"));
        
        //-----------------------------------
        // System
        _fAssociationFile = int.Parse((string)reg.GetValue("fAssociationFile", "1"));
        _AtcsFileIconIndex = int.Parse((string)reg.GetValue("AtcsFileIconIndex", "1"));
        _UserRegIconFilePath = (string)reg.GetValue("UserRegIconFilePath", "");

        //-----------------------------------
        //Password file 
        _fAllowPassFile = ((string)reg.GetValue("fAllowPassFile", "0") == "1") ? true : false;
        _fCheckPassFile = ((string)reg.GetValue("fCheckPassFile", "0") == "1") ? true : false;
        _fCheckPassFileDecrypt = ((string)reg.GetValue("fCheckPassFileDecrypt", "0") == "1") ? true : false;
        _PassFilePath = (string)reg.GetValue("PassFilePath", "");
        _PassFilePathDecrypt = (string)reg.GetValue("PassFilePathDecrypt", "");
        _fNoErrMsgOnPassFile = ((string)reg.GetValue("fNoErrMsgOnPassFile", "0") == "1") ? true : false;
        _fPasswordFileExe = ((string)reg.GetValue("fPasswordFileExe", "0") == "1") ? true : false;

        //-----------------------------------
        //Camouflage Extension 
        _fAddCamoExt = ((string)reg.GetValue("fAddCamoExt", "0") == "1") ? true : false;
        _CamoExt = (string)reg.GetValue("CamoExt", ".jpg");

        //-----------------------------------
        // Input Password limit
        _MissTypeLimitsNum = int.Parse((string)reg.GetValue("MissTypeLimitsNum", "3"));
        _fBroken = ((string)reg.GetValue("fBroken", "0") == "1") ? true : false;
        
        //-----------------------------------
        // Salvage
        _fSalvageToCreateParentFolderOneByOne = ((string)reg.GetValue("fSalvageToCreateParentFolderOneByOne", "0") == "1") ? true : false;
        _fSalvageIntoSameDirectory = ((string)reg.GetValue("fSalvageIntoSameDirectory", "0") == "1") ? true : false;
        
        //-----------------------------------
        // Others
        _Language = (string)reg.GetValue("Language", "");
      }

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// Save options of AtacheCase to system registry
    /// アタッシェケースの設定をレジストリに書き込む
    /// </summary>
    //----------------------------------------------------------------------
    public void SaveOptionsToRegistry()
    {
      //-----------------------------------
      // Open the registry key (AppInfo).
      using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(RegistryPathAppInfo))
      {
        reg.SetValue("AppPath", Application.ExecutablePath);
        reg.SetValue("AppVersion", _AppVersion);
      }

      //-----------------------------------
      // Window
      using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(RegistryPathWindowPos))
      {
        //-----------------------------------
        // Windows Positions and size
        reg.SetValue("WindowTop", _FormTop.ToString());
        reg.SetValue("WindowLeft", _FormLeft.ToString());
        reg.SetValue("WindowWidth", _FormWidth.ToString());
        reg.SetValue("WindowHeight", _FormHeight.ToString());
        reg.SetValue("FormStyle", _FormStyle.ToString());
        reg.SetValue("ActiveTreeNode", _ActiveTreeNode.ToString());
        reg.SetValue("InitDirPath", _InitDirPath);
      }

      //----------------------------------------------------------------------
      // These registry is stored in a 'string' type 
      // for compatibility with old versions of AttacheCase.
      //
      // 旧バージョンとの互換性を保つため、極力、設定のレジストリへの保存は
      //「文字列型」で格納します（※記憶パスワードだけ例外）。
      //     
      //----------------------------------------------------------------------
      // Store Passwords
      using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(RegistryPathMyKey))
      {
        reg.SetValue("fMyEncryptPasswordKeep", _fMyEncryptPasswordKeep == true ? "1" : "0");
        reg.SetValue("fMyDecryptPasswordKeep", _fMyDecryptPasswordKeep == true ? "1" : "0");
        reg.SetValue("MyEncryptPasswordString", EncryptMyPassword(_MyEncryptPasswordString), RegistryValueKind.Binary);
        reg.SetValue("MyDecryptPasswordString", EncryptMyPassword(_MyDecryptPasswordString), RegistryValueKind.Binary);
        reg.SetValue("fMemPasswordExe", _fMemPasswordExe == true ? "1" : "0");
        reg.SetValue("fNotMaskPassword", _fNotMaskPassword == true ? "1" : "0");
      }

      //----------------------------------------------------------------------
      // Options
      using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(RegistryPathOption))
      {
        //-----------------------------------
        // General
        reg.SetValue("fEndToExit", _fEndToExit == true ? "1" : "0");
        reg.SetValue("fOpenFile", _fOpenFile == true ? "1" : "0");
        reg.SetValue("fShowDialogWhenExeFile", _fShowDialogWhenExeFile == true ? "1" : "0");
        reg.SetValue("ShowDialogWhenMultipleFilesNum", _ShowDialogWhenMultipleFilesNum.ToString());  // int

        reg.SetValue("fAskEncDecode", _fAskEncDecode == true ? "1" : "0");
        reg.SetValue("fNoHidePassword", _fNoHidePassword == true ? "1" : "0");
        reg.SetValue("fSaveToExeout", _fSaveToExeout == true ? "1" : "0");
        reg.SetValue("fShowExeoutChkBox", _fShowExeoutChkBox == true ? "1" : "0");

        //-----------------------------------
        // Window
        reg.SetValue("fMainWindowMinimize", _fMainWindowMinimize == true ? "1" : "0");
        reg.SetValue("fTaskBarHide", _fTaskBarHide == true ? "1" : "0");
        reg.SetValue("fTaskTrayIcon", _fTaskTrayIcon == true ? "1" : "0");
        reg.SetValue("fOpenFolder", _fOpenFolder == true ? "1" : "0");
        reg.SetValue("fWindowForeground", _fWindowForeground == true ? "1" : "0");
        reg.SetValue("fNoMultipleInstance", _fNoMultipleInstance == true ? "1" : "0");

        //-----------------------------------
        // Save Encrypt
        reg.SetValue("EncryptionFileType", _EncryptionFileType.ToString());
        reg.SetValue("fEncryptionSameFileTypeAlways", _fEncryptionSameFileTypeAlways == true ? "1" : "0");
        reg.SetValue("EncryptionSameFileTypeAlways", _EncryptionSameFileTypeAlways.ToString());
        reg.SetValue("fEncryptionSameFileTypeBefore", _fEncryptionSameFileTypeBefore == true ? "1" : "0");
        reg.SetValue("EncryptionSameFileTypeBefore", _EncryptionSameFileTypeBefore.ToString());
        reg.SetValue("fSaveToSameFldr", _fSaveToSameFldr == true ? "1" : "0");
        reg.SetValue("SaveToSameFldrPath", _SaveToSameFldrPath);
        reg.SetValue("fEncryptConfirmOverwrite", _fEncryptConfirmOverwrite == true ? "1" : "0");
        reg.SetValue("fAllFilePack", _fAllFilePack == true ? "1" : "0");
        reg.SetValue("fFilesOneByOne", _fFilesOneByOne == true ? "1" : "0");
        reg.SetValue("fNormal", _fNormal == true ? "1" : "0");
        reg.SetValue("fKeepTimeStamp", _fKeepTimeStamp == true ? "1" : "0");
        reg.SetValue("fExtInAtcFileName", _fExtInAtcFileName == true ? "1" : "0");
        reg.SetValue("fAutoName", _fAutoName == true ? "1" : "0");
        reg.SetValue("AutoNameFormatText", _AutoNameFormatText);
        reg.SetValue("fAutoNameAlphabets", _fAutoNameAlphabets == true ? "1" : "0");
        reg.SetValue("fAutoNameLowerCase", _fAutoNameLowerCase == true ? "1" : "0");
        reg.SetValue("fAutoNameUpperCase", _fAutoNameUpperCase == true ? "1" : "0");
        reg.SetValue("fAutoNameNumbers", _fAutoNameNumbers == true ? "1" : "0");
        reg.SetValue("fAutoNameSymbols", _fAutoNameSymbols == true ? "1" : "0");

        //-----------------------------------
        // Save Decrypt
        reg.SetValue("fDecodeToSameFldr", _fDecodeToSameFldr == true ? "1" : "0");
        reg.SetValue("DecodeToSameFldrPath", _DecodeToSameFldrPath);
        reg.SetValue("fDecryptConfirmOverwrite", _fDecryptConfirmOverwrite == true ? "1" : "0");
        reg.SetValue("fNoParentFldr", _fNoParentFldr == true ? "1" : "0");
        reg.SetValue("fSameTimeStamp", _fSameTimeStamp == true ? "1" : "0");
        reg.SetValue("fCompareFile", _fCompareFile == true ? "1" : "0");

        //-----------------------------------
        // Password ZIP
        reg.SetValue("fZipToSameFldr", _fZipToSameFldr == true ? "1" : "0");
        reg.SetValue("ZipToSameFldrPath", _ZipToSameFldrPath);
        reg.SetValue("fZipConfirmOverwrite", _fZipConfirmOverwrite == true ? "1" : "0");
        reg.SetValue("ZipEncryptionAlgorithm", _ZipEncryptionAlgorithm.ToString()); // int

        //-----------------------------------
        // Delete
        reg.SetValue("fDelOrgFile", _fDelOrgFile == true ? "1" : "0");
        reg.SetValue("fEncryptShowDelChkBox", _fEncryptShowDelChkBox == true ? "1" : "0");
        reg.SetValue("fDelEncFile", _fDelEncFile == true ? "1" : "0");
        reg.SetValue("fDecryptShowDelChkBox", _fDecryptShowDelChkBox == true ? "1" : "0");
        reg.SetValue("fConfirmToDeleteAfterEncryption", _fConfirmToDeleteAfterEncryption == true ? "1" : "0");
        reg.SetValue("fDecryptShowDelChkBox", _fDecryptShowDelChkBox == true ? "1" : "0");
        reg.SetValue("fConfirmToDeleteAfterDecryption", _fConfirmToDeleteAfterDecryption == true ? "1" : "0");
        reg.SetValue("fCompleteDelFile", _fCompleteDelFile.ToString());  //int 
        reg.SetValue("DelRandNum", _DelRandNum.ToString());
        reg.SetValue("DelZeroNum", _DelZeroNum.ToString());
        
        //-----------------------------------
        //Compression
        reg.SetValue("CompressRate", _CompressRate.ToString());

        //-----------------------------------
        // System
        reg.SetValue("fAssociationFile", _fAssociationFile.ToString());  //int
        reg.SetValue("AtcsFileIconIndex", _AtcsFileIconIndex.ToString());  //int
        reg.SetValue("UserRegIconFilePath", _UserRegIconFilePath);

        //-----------------------------------
        //Password file
        reg.SetValue("fAllowPassFile", _fAllowPassFile == true ? "1" : "0");
        reg.SetValue("fCheckPassFile", _fCheckPassFile == true ? "1" : "0");
        reg.SetValue("PassFilePath", _PassFilePath);

        reg.SetValue("fCheckPassFileDecrypt", _fCheckPassFileDecrypt == true ? "1" : "0");
        reg.SetValue("PassFilePathDecrypt", _PassFilePathDecrypt);
        reg.SetValue("fNoErrMsgOnPassFile", _fNoErrMsgOnPassFile == true ? "1" : "0");
        reg.SetValue("fPasswordFileExe", _fPasswordFileExe == true ? "1" : "0");

        //-----------------------------------
        //Camoufage Extension
        reg.SetValue("fAddCamoExt", _fAddCamoExt == true ? "1" : "0");
        reg.SetValue("CamoExt", _CamoExt);

        //-----------------------------------
        // Input Password limit
        reg.SetValue("MissTypeLimitsNum", _MissTypeLimitsNum.ToString());
        reg.SetValue("fBroken", _fBroken == true ? "1" : "0");

        //-----------------------------------
        // Salvage
        reg.SetValue("fSalvageToCreateParentFolderOneByOne", _fSalvageToCreateParentFolderOneByOne == true ? "1" : "0");
        reg.SetValue("fSalvageIntoSameDirectory", _fSalvageIntoSameDirectory == true ? "1" : "0");
        
        //-----------------------------------
        // Others
        reg.SetValue("Language", _Language);
      }
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// 指定のINIファイルから設定を読み込む
    /// Read options from specified INI file
    /// </summary>
    /// <param name="IniFilePath">Specified INI file</param>
    //----------------------------------------------------------------------
    public void ReadOptionFromIniFile(string IniFilePath)
    {
      string ReturnValue = "";

      //-----------------------------------
      // Application infomation
      ReadIniFile(IniFilePath, ref _ApplicationPath, "AppInfo", "AppPath", "");
      ReadIniFile(IniFilePath, ref _AppVersion, "AppInfo", "AppVersion", "0");
          
      //-----------------------------------
      // Window positions
      //-----------------------------------
      ReadIniFile(IniFilePath, ref _FormStyle, "WindowPos", "FormStyle", "0");
      ReadIniFile(IniFilePath, ref _FormTop, "WindowPos", "WindowTop", "-1");
      ReadIniFile(IniFilePath, ref _FormLeft, "WindowPos", "WindowLeft", "-1");
      ReadIniFile(IniFilePath, ref _FormWidth, "WindowPos", "WindowWidth", "420");
      ReadIniFile(IniFilePath, ref _FormHeight, "WindowPos", "WindowHeight", "380");
      ReadIniFile(IniFilePath, ref _InitDirPath, "WindowPos", "InitDirPath", "");

      ReadIniFile(IniFilePath, ref _ActiveTreeNode, "WindowPos", "ActiveTreeNode", "0");
      ReadIniFile(IniFilePath, ref _InitDirPath, "WindowPos", "InitDirPath", "");

      //-----------------------------------
      // Stored Passwords
      //-----------------------------------
      ReadIniFile(IniFilePath, ref _fMyEncryptPasswordKeep, "MyKey", "fMyEncryptPasswordKeep", "");
      ReadIniFile(IniFilePath, ref ReturnValue, "MyKey", "MyEncryptPasswordString", "");
      if (ReturnValue != "")
      {
        _MyEncryptPasswordBinary = HexStringToByteArray(ReturnValue.ToString());
        _MyEncryptPasswordString = DecryptMyPassword(_MyEncryptPasswordBinary);
      }

      ReadIniFile(IniFilePath, ref _fMyDecryptPasswordKeep, "MyKey", "fMyDecryptPasswordKeep", "");
      ReadIniFile(IniFilePath, ref ReturnValue, "MyKey", "MyDecryptPasswordString", "");
      if(ReturnValue != "")
      {
        _MyDecryptPasswordBinary = HexStringToByteArray(ReturnValue.ToString());
        _MyDecryptPasswordString = DecryptMyPassword(_MyDecryptPasswordBinary);
      }
      ReadIniFile(IniFilePath, ref _fMemPasswordExe, "MyKey", "fMemPasswordExe", "0");
      ReadIniFile(IniFilePath, ref _fNotMaskPassword, "MyKey", "fNotMaskPassword", "0");

      //-----------------------------------
      // Options
      //-----------------------------------

      // General
      ReadIniFile(IniFilePath, ref _fEndToExit, "Option", "fEndToExit", "0");
      ReadIniFile(IniFilePath, ref _fOpenFile, "Option", "fOpenFile", "0");
      ReadIniFile(IniFilePath, ref _fShowDialogWhenExeFile, "Option", "fShowDialogWhenExeFile", "1");
      ReadIniFile(IniFilePath, ref _ShowDialogWhenMultipleFilesNum, "Option", "ShowDialogWhenMultipleFilesNum", "5");
      ReadIniFile(IniFilePath, ref _fAskEncDecode, "Option", "fAskEncDecode", "0");
      ReadIniFile(IniFilePath, ref _fNoHidePassword, "Option", "fNoHidePassword", "0");
      ReadIniFile(IniFilePath, ref _fSaveToExeout, "Option", "fSaveToExeout", "0");
      ReadIniFile(IniFilePath, ref _fShowExeoutChkBox, "Option", "fShowExeoutChkBox", "1");

      // Window
      ReadIniFile(IniFilePath, ref _fMainWindowMinimize, "Option", "fMainWindowMinimize", "0");
      ReadIniFile(IniFilePath, ref _fTaskBarHide, "Option", "fTaskBarHide", "0");
      ReadIniFile(IniFilePath, ref _fTaskTrayIcon, "Option", "fTaskTrayIcon", "0");
      ReadIniFile(IniFilePath, ref _fOpenFolder, "Option", "fOpenFolder", "0");
      ReadIniFile(IniFilePath, ref _fWindowForeground, "Option", "fWindowForeground", "1");
      ReadIniFile(IniFilePath, ref _fNoMultipleInstance, "Option", "fNoMultipleInstance", "1");

      // Save Encrypt

      //Integer = 1: ATC, 2: EXE(ATC), 3: ZIP, 0: Others(Encrypt file?)
      ReadIniFile(IniFilePath, ref _EncryptionFileType, "Option", "EncryptionFileType", "0");
      ReadIniFile(IniFilePath, ref _fEncryptionSameFileTypeAlways, "Option", "fEncryptionSameFileTypeAlways", "0");
      ReadIniFile(IniFilePath, ref _EncryptionSameFileTypeAlways, "Option", "EncryptionSameFileTypeAlways", "-1");
      ReadIniFile(IniFilePath, ref _fEncryptionSameFileTypeBefore, "Option", "fEncryptionSameFileTypeBefore", "0");
      ReadIniFile(IniFilePath, ref _EncryptionSameFileTypeBefore, "Option", "EncryptionSameFileTypeBefore", "-1");

      ReadIniFile(IniFilePath, ref _fSaveToSameFldr, "Option", "fSaveToSameFldr", "0");
      ReadIniFile(IniFilePath, ref _SaveToSameFldrPath, "Option", "SaveToSameFldrPath", "");
      ReadIniFile(IniFilePath, ref _fEncryptConfirmOverwrite, "Option", "fEncryptConfirmOverwrite", "1");
      ReadIniFile(IniFilePath, ref _fAllFilePack, "Option", "fAllFilePack", "0");
      ReadIniFile(IniFilePath, ref _fFilesOneByOne, "Option", "fFilesOneByOne", "0");
      _fNormal = (_fAllFilePack == false && _fFilesOneByOne == false) ? true : false;
      ReadIniFile(IniFilePath, ref _fKeepTimeStamp, "Option", "fKeepTimeStamp", "0");
      ReadIniFile(IniFilePath, ref _fExtInAtcFileName, "Option", "fExtInAtcFileName", "0");
      ReadIniFile(IniFilePath, ref _fAutoName, "Option", "fAutoName", "0");
      ReadIniFile(IniFilePath, ref _AutoNameFormatText, "Option", "AutoNameFormatText", "<filename>_<date:yyyy_mm_dd><ext>");
      ReadIniFile(IniFilePath, ref _fAutoNameAlphabets, "Option", "fAutoNameAlphabets", "1");
      ReadIniFile(IniFilePath, ref _fAutoNameLowerCase, "Option", "fAutoNameLowerCase", "1");
      ReadIniFile(IniFilePath, ref _fAutoNameUpperCase, "Option", "fAutoNameUpperCase", "1");
      ReadIniFile(IniFilePath, ref _fAutoNameNumbers, "Option", "fAutoNameNumbers", "1");
      ReadIniFile(IniFilePath, ref _fAutoNameSymbols, "Option", "fAutoNameSymbols", "0");

      // Save Decrypt
      ReadIniFile(IniFilePath, ref _fDecodeToSameFldr, "Option", "fDecodeToSameFldr", "0");
      ReadIniFile(IniFilePath, ref _DecodeToSameFldrPath, "Option", "DecodeToSameFldrPath", "");
      ReadIniFile(IniFilePath, ref _fDecryptConfirmOverwrite, "Option", "fDecryptConfirmOverwrite", "1");
      ReadIniFile(IniFilePath, ref _fNoParentFldr, "Option", "fNoParentFldr", "0");
      ReadIniFile(IniFilePath, ref _fSameTimeStamp, "Option", "fSameTimeStamp", "0");
      ReadIniFile(IniFilePath, ref _fCompareFile, "Option", "fCompareFile", "0");

      // Password ZIP
      ReadIniFile(IniFilePath, ref _fZipToSameFldr, "Option", "fZipToSameFldr", "0");
      ReadIniFile(IniFilePath, ref _ZipToSameFldrPath, "Option", "ZipToSameFldrPath", "");
      ReadIniFile(IniFilePath, ref _fZipConfirmOverwrite, "Option", "fZipConfirmOverwrite", "1");
      ReadIniFile(IniFilePath, ref _ZipEncryptionAlgorithm, "Option", "ZipEncryptionAlgorithm", "0"); // int

      // Delete
      ReadIniFile(IniFilePath, ref _fDelOrgFile, "Option", "fDelOrgFile", "0");
      ReadIniFile(IniFilePath, ref _fEncryptShowDelChkBox, "Option", "fEncryptShowDelChkBox", "0");
      ReadIniFile(IniFilePath, ref _fConfirmToDeleteAfterEncryption, "Option", "fConfirmToDeleteAfterEncryption", "1");
      ReadIniFile(IniFilePath, ref _fDelEncFile, "Option", "fDelEncFile", "0");
      ReadIniFile(IniFilePath, ref _fDecryptShowDelChkBox, "Option", "fDecryptShowDelChkBox", "0");
      ReadIniFile(IniFilePath, ref _fConfirmToDeleteAfterDecryption, "Option", "fConfirmToDeleteAfterDecryption", "1");
      ReadIniFile(IniFilePath, ref _fCompleteDelFile, "Option", "fCompleteDelFile", "1");
      ReadIniFile(IniFilePath, ref _DelRandNum, "Option", "DelRandNum", "0");
      ReadIniFile(IniFilePath, ref _DelZeroNum, "Option", "DelZeroNum", "1");

      //Compression
      ReadIniFile(IniFilePath, ref _CompressRate, "Option", "CompressRate", "6");
          
      // System
      ReadIniFile(IniFilePath, ref _fAssociationFile, "Option", "fAssociationFile", "1");
      ReadIniFile(IniFilePath, ref _AtcsFileIconIndex, "Option", "AtcsFileIconIndex", "1");
      ReadIniFile(IniFilePath, ref _UserRegIconFilePath, "Option", "UserRegIconFilePath", "");

      //Password file 
      ReadIniFile(IniFilePath, ref _fAllowPassFile, "Option", "fAllowPassFile", "0");
      ReadIniFile(IniFilePath, ref _fCheckPassFile, "Option", "fCheckPassFile", "0");
      ReadIniFile(IniFilePath, ref _PassFilePath, "Option", "PassFilePath", "");

      ReadIniFile(IniFilePath, ref _fCheckPassFileDecrypt, "Option", "fCheckPassFileDecrypt", "0");
      ReadIniFile(IniFilePath, ref _PassFilePathDecrypt, "Option", "PassFilePathDecrypt", "");
      ReadIniFile(IniFilePath, ref _fNoErrMsgOnPassFile, "Option", "fNoErrMsgOnPassFile", "0");
      ReadIniFile(IniFilePath, ref _fPasswordFileExe, "Option", "fPasswordFileExe", "0");

      //Camouflage Extension
      ReadIniFile(IniFilePath, ref _fAddCamoExt, "Option", "fAddCamoExt", "0");
      ReadIniFile(IniFilePath, ref _CamoExt, "Option", "CamoExt", ".jpg");

      // Input Password limit
      ReadIniFile(IniFilePath, ref _MissTypeLimitsNum, "Option", "MissTypeLimitsNum", "3");
      ReadIniFile(IniFilePath, ref _fBroken, "Option", "fBroken", "0");
      
      // Salvage
      ReadIniFile(IniFilePath, ref _fSalvageToCreateParentFolderOneByOne, "Option", "fSalvageToCreateParentFolderOneByOne", "0");
      ReadIniFile(IniFilePath, ref _fSalvageIntoSameDirectory, "Option", "fSalvageIntoSameDirectory", "0");

      // Others
      ReadIniFile(IniFilePath, ref _Language, "Option", "Language", "");
      
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// 指定のINIファイルへ設定を書き込む
    /// Write options to specified INI file
    /// </summary>
    /// <param name="IniFilePath">Specified INI file</param>
    //----------------------------------------------------------------------
    public void WriteOptionToIniFile(string IniFilePath)
    {

      //-----------------------------------
      // Open the registry key (AppInfo).
      WriteIniFile(IniFilePath, _ApplicationPath, "AppInfo", "AppPath");
      WriteIniFile(IniFilePath, _AppVersion, "AppInfo", "AppVersion");
      
      //-----------------------------------
      // Window
      WriteIniFile(IniFilePath, _FormTop, "WindowPos", "WindowTop");
      WriteIniFile(IniFilePath, _FormLeft, "WindowPos", "WindowLeft");
      WriteIniFile(IniFilePath, _FormWidth, "WindowPos", "WindowWidth");
      WriteIniFile(IniFilePath, _FormHeight, "WindowPos", "WindowHeight");
      WriteIniFile(IniFilePath, _FormStyle, "WindowPos", "FormStyle");

      WriteIniFile(IniFilePath, _ActiveTreeNode, "WindowPos", "ActiveTreeNode");
      WriteIniFile(IniFilePath, _InitDirPath, "WindowPos", "InitDirPath");
      
      //----------------------------------------------------------------------
      // Store Passwords
      WriteIniFile(IniFilePath, _fMyEncryptPasswordKeep, "MyKey", "fMyEncryptPasswordKeep");
      WriteIniFile(IniFilePath, _fMyDecryptPasswordKeep, "MyKey", "fMyDecryptPasswordKeep");

      byte[] bytes = new byte[32];
      bytes = EncryptMyPassword(_MyEncryptPasswordString);
      string p = ByteArrayToHexString(bytes);
      Console.WriteLine(p);
      WriteIniFile(IniFilePath, p, "MyKey", "MyEncryptPasswordString");

      bytes = new byte[32];
      bytes = EncryptMyPassword(_MyDecryptPasswordString);
      p = ByteArrayToHexString(bytes);
      Console.WriteLine(p);
      WriteIniFile(IniFilePath, p, "MyKey", "MyDecryptPasswordString");

      WriteIniFile(IniFilePath, _fMemPasswordExe, "MyKey", "fMemPasswordExe");
      WriteIniFile(IniFilePath, _fNotMaskPassword, "MyKey", "fNotMaskPassword");

      //----------------------------------------------------------------------
      // Options
            
      // General
      WriteIniFile(IniFilePath, _fEndToExit, "Option", "fEndToExit");
      WriteIniFile(IniFilePath, _fOpenFile, "Option", "fOpenFile");
      WriteIniFile(IniFilePath, _fShowDialogWhenExeFile, "Option", "fShowDialogWhenExeFile");
      WriteIniFile(IniFilePath, _ShowDialogWhenMultipleFilesNum, "Option", "ShowDialogWhenMultipleFilesNum");
      WriteIniFile(IniFilePath, _fAskEncDecode, "Option", "fAskEncDecode");
      WriteIniFile(IniFilePath, _fNoHidePassword, "Option", "fNoHidePassword");
      WriteIniFile(IniFilePath, _fSaveToExeout, "Option", "fSaveToExeout");
      WriteIniFile(IniFilePath, _fShowExeoutChkBox, "Option", "fShowExeoutChkBox");

      //-----------------------------------
      // Window
      WriteIniFile(IniFilePath, _fMainWindowMinimize, "Option", "fMainWindowMinimize");
      WriteIniFile(IniFilePath, _fTaskBarHide, "Option", "fTaskBarHide");
      WriteIniFile(IniFilePath, _fTaskTrayIcon, "Option", "fTaskTrayIcon");
      WriteIniFile(IniFilePath, _fOpenFolder, "Option", "fOpenFolder");
      WriteIniFile(IniFilePath, _fWindowForeground, "Option", "fWindowForeground");
      WriteIniFile(IniFilePath, _fNoMultipleInstance, "Option", "fNoMultipleInstance");

      //-----------------------------------
      // Save Encrypt
      WriteIniFile(IniFilePath, _EncryptionFileType, "Option", "EncryptionFileType");
      WriteIniFile(IniFilePath, _fEncryptionSameFileTypeAlways, "Option", "fEncryptionSameFileTypeAlways");
      WriteIniFile(IniFilePath, _EncryptionSameFileTypeAlways, "Option", "EncryptionSameFileTypeAlways");
      WriteIniFile(IniFilePath, _fEncryptionSameFileTypeBefore, "Option", "fEncryptionSameFileTypeBefore");
      WriteIniFile(IniFilePath, _EncryptionSameFileTypeBefore, "Option", "EncryptionSameFileTypeBefore");
      WriteIniFile(IniFilePath, _fSaveToSameFldr, "Option", "fSaveToSameFldr");
      WriteIniFile(IniFilePath, _SaveToSameFldrPath, "Option", "SaveToSameFldrPath");
      WriteIniFile(IniFilePath, _fEncryptConfirmOverwrite, "Option", "fEncryptConfirmOverwrite");

      WriteIniFile(IniFilePath, _fAllFilePack, "Option", "fAllFilePack");
      WriteIniFile(IniFilePath, _fFilesOneByOne, "Option", "fFilesOneByOne");
      WriteIniFile(IniFilePath, _fNormal, "Option", "fNormal");
      WriteIniFile(IniFilePath, _fKeepTimeStamp, "Option", "fKeepTimeStamp");
      WriteIniFile(IniFilePath, _fExtInAtcFileName, "Option", "fExtInAtcFileName");

      WriteIniFile(IniFilePath, _fAutoName, "Option", "fAutoName");
      WriteIniFile(IniFilePath, _AutoNameFormatText, "Option", "AutoNameFormatText");
      WriteIniFile(IniFilePath, _fAutoNameAlphabets, "Option", "fAutoNameAlphabets");
      WriteIniFile(IniFilePath, _fAutoNameLowerCase, "Option", "fAutoNameLowerCase");
      WriteIniFile(IniFilePath, _fAutoNameUpperCase, "Option", "fAutoNameUpperCase");
      WriteIniFile(IniFilePath, _fAutoNameNumbers, "Option", "fAutoNameNumbers");
      WriteIniFile(IniFilePath, _fAutoNameSymbols, "Option", "fAutoNameSymbols");

      //-----------------------------------
      // Save Decrypt
      WriteIniFile(IniFilePath, _fDecodeToSameFldr, "Option", "fDecodeToSameFldr");
      WriteIniFile(IniFilePath, _DecodeToSameFldrPath, "Option", "DecodeToSameFldrPath");
      WriteIniFile(IniFilePath, _fDecryptConfirmOverwrite, "Option", "fDecryptConfirmOverwrite");
      WriteIniFile(IniFilePath, _fNoParentFldr, "Option", "fNoParentFldr");
      WriteIniFile(IniFilePath, _fSameTimeStamp, "Option", "fSameTimeStamp");
      WriteIniFile(IniFilePath, _fCompareFile, "Option", "fCompareFile");

      //-----------------------------------
      // Password ZIP
      WriteIniFile(IniFilePath, _fZipToSameFldr, "Option", "fZipToSameFldr");
      WriteIniFile(IniFilePath, _ZipToSameFldrPath, "Option", "ZipToSameFldrPath");
      WriteIniFile(IniFilePath, _fZipConfirmOverwrite, "Option", "fZipConfirmOverwrite");
      WriteIniFile(IniFilePath, _ZipEncryptionAlgorithm, "Option", "ZipEncryptionAlgorithm");

      //-----------------------------------
      // Delete
      WriteIniFile(IniFilePath, _fDelOrgFile, "Option", "fDelOrgFile");
      WriteIniFile(IniFilePath, _fEncryptShowDelChkBox, "Option", "fEncryptShowDelChkBox");
      WriteIniFile(IniFilePath, _fConfirmToDeleteAfterEncryption, "Option", "fConfirmToDeleteAfterEncryption");

      WriteIniFile(IniFilePath, _fDelEncFile, "Option", "fDelEncFile");
      WriteIniFile(IniFilePath, _fDecryptShowDelChkBox, "Option", "fDecryptShowDelChkBox");
      WriteIniFile(IniFilePath, _fConfirmToDeleteAfterDecryption, "Option", "fConfirmToDeleteAfterDecryption");

      WriteIniFile(IniFilePath, _fCompleteDelFile, "Option", "fCompleteDelFile");
      WriteIniFile(IniFilePath, _DelRandNum, "Option", "DelRandNum");
      WriteIniFile(IniFilePath, _DelZeroNum, "Option", "DelZeroNum");

      //-----------------------------------
      //Compression
      WriteIniFile(IniFilePath, _CompressRate, "Option", "CompressRate");
    
      //-----------------------------------
      // System
      WriteIniFile(IniFilePath, _fAssociationFile, "Option", "fAssociationFile");  // int
      WriteIniFile(IniFilePath, _UserRegIconFilePath, "Option", "UserRegIconFilePath");

      //-----------------------------------
      //Password file
      WriteIniFile(IniFilePath, _fAllowPassFile, "Option", "fAllowPassFile");
      WriteIniFile(IniFilePath, _fCheckPassFile, "Option", "fCheckPassFile");
      WriteIniFile(IniFilePath, _PassFilePath, "Option", "PassFilePath");

      WriteIniFile(IniFilePath, _fCheckPassFileDecrypt, "Option", "fCheckPassFileDecrypt");
      WriteIniFile(IniFilePath, _PassFilePathDecrypt, "Option", "PassFilePathDecrypt");
      WriteIniFile(IniFilePath, _fNoErrMsgOnPassFile, "Option", "fNoErrMsgOnPassFile");
      WriteIniFile(IniFilePath, _fPasswordFileExe, "Option", "fPasswordFileExe");

      //-----------------------------------
      //Camouflage Extension
      WriteIniFile(IniFilePath, _fAddCamoExt, "Option", "fAddCamoExt");
      WriteIniFile(IniFilePath, _CamoExt, "Option", "CamoExt");

      //-----------------------------------
      // Input Password limit
      WriteIniFile(IniFilePath, _MissTypeLimitsNum, "Option", "MissTypeLimitsNum");
      WriteIniFile(IniFilePath, _fBroken, "Option", "fBroken");

      //-----------------------------------
      // Salvage
      WriteIniFile(IniFilePath, _fSalvageToCreateParentFolderOneByOne, "Option", "fSalvageToCreateParentFolderOneByOne");
      WriteIniFile(IniFilePath, _fSalvageIntoSameDirectory, "Option", "fSalvageIntoSameDirectory");
  
      //-----------------------------------
      // Others
      WriteIniFile(IniFilePath, _Language, "Option", "Language");

    }
        
    //----------------------------------------------------------------------
    /// <summary>
    /// INIファイルからの読み込み（オーバーロード）
    /// Read options from INI file ( Overload )
    /// </summary>
    /// <param name="FilePath">INI file path</param>
    /// <param name="o">Option variable</param>
    /// <param name="section">INI file 'section' item</param>
    /// <param name="key">INI file 'key' item</param>
    /// <param name="defval">INI file default value</param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    public void ReadIniFile(string IniFilePath, ref int o, string section, string key, string defval)  // Integer
    {
      StringBuilder ResultValue = new StringBuilder(255);
      if (GetPrivateProfileString(section, key, defval, ResultValue, 255, IniFilePath) > 0)
      {
        o = int.Parse(ResultValue.ToString());
      }
    }

    public void ReadIniFile(string IniFilePath, ref string o, string section, string key, string defval)  // string
    {
      StringBuilder ResultValue = new StringBuilder(255);
      if (GetPrivateProfileString(section, key, defval, ResultValue, 255, IniFilePath) > 0)
      {
        o = ResultValue.ToString();
      }
    }

    public void ReadIniFile(string IniFilePath, ref bool o, string section, string key, string defval)  // bool
    {
      StringBuilder ResultValue = new StringBuilder(255);
      if (GetPrivateProfileString(section, key, defval, ResultValue, 255, IniFilePath) > 0)
      {
        o = (ResultValue.ToString() == "1" ? true : false);
      }
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// INIファイルへの書き込み
    /// Write options to INI file
    /// </summary>
    /// <param name="IniFilePath">INI file path</param>
    /// <param name="section">INI file 'section' item</param>
    /// <param name="key">INI file 'key' item</param>
    /// <param name="o">Object(int, string, bool)</param>
    //----------------------------------------------------------------------
    public void WriteIniFile(string IniFilePath, object o, string section, string key)
    {
      string value = "";
      if(o == null)
      {
        value = "";
      }
      else if (o.GetType() == typeof(bool))
      {
        value = (bool)o == true ? "1" : "0";
      }
      else if (o.GetType() == typeof(string) || o.GetType() == typeof(int))
      {
        value = o.ToString();
      }

      WritePrivateProfileString(section, key, value, IniFilePath);

    }
    
    //----------------------------------------------------------------------
    /// <summary>
    /// 設定を指定のXMLファイルパスから読み込み、インスタンスへ反映する
    /// Read the configuration from the specified XML file path, reflect to an instance.
    /// </summary>
    /// <param name="FilePath"></param>
    //----------------------------------------------------------------------
    public void ReadOptionsFromXML(string FilePath)
    {
      XmlSerializer szr =  new XmlSerializer(typeof(AppSettings));
      using (StreamReader sr = new StreamReader(FilePath, new System.Text.UTF8Encoding(false)))
      {
        //XMLファイルから読み込み、逆シリアル化する
        Instance = (AppSettings)szr.Deserialize(sr);
      }

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// 設定を指定のファイルパスにXMLファイルとして出力する
    /// Save the configuration as an XML file to the specified file path.
    /// </summary>
    /// <param name="FilePath"></param>
    //----------------------------------------------------------------------
    public void SaveOptionsToXML(string FilePath)
    {
      XmlSerializer szr = new XmlSerializer(typeof(AppSettings));
      using (StreamWriter sw = new StreamWriter(FilePath, false, new System.Text.UTF8Encoding(false)))
      {
        //シリアル化し、XMLファイルに保存する
        szr.Serialize(sw, AppSettings.Instance);
      }
    
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// 起動時のコマンドライン引数から設定を読み込む
    /// Read the configuration from the startup command line arguments
    /// </summary>
    /// <param name="cmds"></param>
    //----------------------------------------------------------------------
    public int ParserArguments()
    {
      int ResultNum;

      int i = -1;
      string[] cmds = Environment.GetCommandLineArgs();
      foreach (string cmd in cmds)       
      {
        if (i == -1)
        {
          i++;
          continue;
        }

        char[] charsToTrim = { '\t', ',', ' '};
        string cmdOpt = cmd.Trim(charsToTrim);

        // File list processed
        if (cmdOpt.IndexOf("/") == -1){
          if ( File.Exists(cmdOpt) == true || Directory.Exists(cmdOpt)){

            if (Path.IsPathRooted(cmdOpt) == false)
            {
              cmdOpt = Path.GetFullPath(cmdOpt);
            }

            //何の種類のファイルか
            _FileType[CheckFileType(cmdOpt)]++;
            _FileList.Add(cmdOpt);
          }
          continue;
        }

        string[] values = cmdOpt.Split('=');
        if (values.Length < 2)
        { // Mistaken?
          continue;
        }
        string key = values[0];
        string value = values[1];

        switch(key)
        {
          //-----------------------------------
          // 一般設定
          //-----------------------------------    
          #region
          // Password
          case "/p": // パスワード
                     // 暗号化、復号の両方にパスワードを入れる
            _EncryptPasswordStringFromCommandLine = value;
            _DecryptPasswordStringFromCommandLine = value;
            break;

          // Exit AttacheCase after process.
          case "/exit": // 処理後に終了するか
            if (value == "1")
            {
              _fEndToExit = true;
            }
            else if (value == "0")
            {
              _fEndToExit = false;
            }
            break;

          // Open decrypted files by associated application
          case "/opf": // 復号したファイルを関連付けされたソフトで開く
            if (value == "1")
            {
              _fOpenFile = true;
            }
            else if (value == "0")
            {
              _fOpenFile = false;
            }
            break;
            
          // Show dialog when containing the executable file.
          case "/exe": // 復号したファイルに実行ファイルが含まれるとき警告ダイアログを出す
            if (value == "1")
            {
              _fShowDialogWhenExeFile = true;
            }
            else if (value == "0")
            {
              _fShowDialogWhenExeFile = false;
            }
            break;

          // Show dialog when more than multiple files.
          case "/decnum":  // 復号したファイルが複数個あるとき警告ダイアログを出す
            if (int.TryParse(value, out ResultNum) == true)
            {
              _ShowDialogWhenMultipleFilesNum = ResultNum;
            }
            break;

          // Ask to encrypt or decrypt regardless of file content
          case "/askende": // 暗号/復号処理かを問い合わせる
            if (value == "1")
            {
              _fAskEncDecode = true;
            }
            else if (value == "0")
            {
              _fAskEncDecode = false;
            }
            break;

          // Confirm inputting password without masking
          case "/nohide": //「*」で隠さずパスワードを確認しながら入力する
            if (value == "1")
            {
              _fNoHidePassword = true;
            }
            else if (value == "0")
            {
              _fNoHidePassword = false;
            }
            break;

          // Always output to Executable file
          case "/exeout": // 常に自己実行形式で出力する
            if (value == "1")
            {
              _fSaveToExeout = true;
            }
            else if (value == "0")
            {
              _fSaveToExeout = false;
            }
            break;

          // Always display chekbox of this option
          case "/chkexeout": // メインフォームにチェックボックスを表示する
            if (value == "1")
            {
              _fShowExeoutChkBox = true;
            }
            else if (value == "0")
            {
              _fShowExeoutChkBox = false;
            }
            break;

          #endregion

          //-----------------------------------
          // パスワード
          //-----------------------------------          
          #region
          //Encrypt/Decrypt by &memorized password without confirming
          case "/mempexe": // 記憶パスワードで確認なく実行する
            if (value == "1")
            {
              _fMemPasswordExe = true;
            }
            else if (value == "0")
            {
              _fMemPasswordExe = false;
            }
            break;

          #endregion

          //-----------------------------------
          // ウィンドウ
          //-----------------------------------          
          #region
          // Always execute by minimize the window
          case "/wmin": // 常にウィンドウを最小化して処理する
            if (value == "1")
            {
              _fMainWindowMinimize = true;
              _fWindowForeground = false;  // not coexist
            }
            else if (value == "0")
            {
              _fMainWindowMinimize = false;
            }
            break;

          // Minimizing a window without appearing in the taskbar
          case "/tskb": // タスクバーに表示しない
            if (value == "1")
            {
              _fTaskBarHide = true;
            }
            else if (value == "0")
            {
              _fTaskBarHide = false;
            }
            break;

          // Display in the task tray
          case "/tsktr": // タスクトレイにアイコンを表示する
            if (value == "1")
            {
              _fTaskTrayIcon = true;
            }
            else if (value == "0")
            {
              _fTaskTrayIcon = false;
            }
            break;

          // Bring AttcheCase window in front of Desktop
          case "/front": // デスクトップで最前面にウィンドウを表示する
            if (value == "1")
            {
              _fWindowForeground = true;
              _fMainWindowMinimize = false; // not coexist
            }
            else if (value == "0")
            {
              _fWindowForeground = false;
            }
            break;

          // Not Allow multiple in&stance of AttcheCase
          case "/nomulti": // 複数起動しない 
            if (value == "1")
            {
              _fNoMultipleInstance = true;
            }
            else if (value == "0")
            {
              _fNoMultipleInstance = false;
            }
            break;

          #endregion

          //-----------------------------------
          // 保存設定
          //-----------------------------------          
          #region
          // Encryption type ( Integer = 1: ATC, 2: EXE(ATC), 3: ZIP, 0: false )
          case "/entype": // 暗号化ファイルの種類
            if (value == "1")
            {
              _EncryptionFileType = 1;
            }
            else if (value == "2")
            {
              _EncryptionFileType = 2;
            }
            else if (value == "3")
            {
              _EncryptionFileType = 3;
            } 
            else
            {
              _EncryptionFileType = 0;
            }
            break;

          // Save same encryption type always. ( Integer = 1: ATC, 2: EXE(ATC), 3: ZIP, 0: false )
          case "/sametype":  // 常に同じ暗号化ファイルの種類にする
            if (value == "1")
            {
              _EncryptionSameFileTypeAlways = 1;
              _fEncryptionSameFileTypeAlways = true;
            }
            else if (value == "2")
            {
              _EncryptionSameFileTypeAlways = 2;
              _fEncryptionSameFileTypeAlways = true;
            }
            else if (value == "3")
            {
              _EncryptionSameFileTypeAlways = 3;
              _fEncryptionSameFileTypeAlways = true;
            } 
            else
            {
              _EncryptionSameFileTypeAlways = 0;
              _fEncryptionSameFileTypeAlways = false;
            }
            break;

          // Save same encryption type that was used to before. ( Integer = 1: ATC, 2: EXE(ATC), 3: ZIP, 0: false )
          case "/beforetype":  // 前に使った暗号化ファイルの種類にする
            if (value == "1")
            {
              _EncryptionSameFileTypeBefore = 1;
              _fEncryptionSameFileTypeBefore = true;
            }
            else if (value == "2")
            {
              _EncryptionSameFileTypeBefore = 2;
              _fEncryptionSameFileTypeBefore = true;
            }
            else if (value == "3")
            {
              _EncryptionSameFileTypeBefore = 3;
              _fEncryptionSameFileTypeBefore = true;
            } 
            else
            {
              _EncryptionSameFileTypeBefore = 0;
              _fEncryptionSameFileTypeBefore = false;
            }
            break;

          #endregion

          //-----------------------------------
          // 保存設定（暗号化）
          //-----------------------------------          
          #region
          // Save to same folder in &encryption
          case "/saveto": //常に同じ場所へ暗号化ファイルを保存する
            if (Directory.Exists(value) == true)
            {
              if (Path.IsPathRooted(value) == false)
              {
                value = Path.GetFullPath(value);
              }
              _SaveToSameFldrPath = value;
              _fSaveToSameFldr = true;
            }
            break;

          // Confirm overwriting when same filename exists 
          case "/ow": // 同名ファイルの上書きを確認するか（確認無で上書きするか？）
            if ( value == "1")
            {
              _fEncryptConfirmOverwrite = true;
              _fDecryptConfirmOverwrite = true;
            }
            else if (value == "0")
            {
              _fEncryptConfirmOverwrite = false;
              _fDecryptConfirmOverwrite = false;
            }
            break;
            
          // Create one encrypted file from files
          case "/allpack": // 複数のファイルを暗号化する際は一つにまとめる
            if (value == "1")
            {
              _fAllFilePack = true;
            }
            else if (value == "0")
            {
              _fAllFilePack = false;
            }
            break;

          // Encrypt or decrypt files in directory one by one
          case "/oneby": // フォルダ内のファイルは個別に暗号化する
            if (value == "1")
            {
              _fFilesOneByOne = true;
            }
            else if (value == "0")
            {
              _fFilesOneByOne = false;
            }
            break;

          // Set the timestamp of encryption file to original files or directories
          case "/orgdt": // 暗号化ファイルのタイムスタンプを元ファイルに合わせる
            if (value == "1")
            {
              _fKeepTimeStamp = true;
            }
            else if (value == "0")
            {
              _fKeepTimeStamp = false;
            }
            break;
            
          // Create encrypted file &including extension
          case "/withext": // 暗号化ファイル名に拡張子を含める
            if(value == "1"){
              _fExtInAtcFileName = true;
            }
            else if (value == "0")
            {
              _fExtInAtcFileName = false;
            }
            break;

          // Specify the format of the encryption file name
          case "/autoname": // 自動で暗号化ファイル名を付加する
            _fAutoName = true;
            _AutoNameFormatText = value;
            break;

          // Encrypted files camouflage with extension
          case "/camoext": // 暗号化ファイルの拡張子を偽装する

            if ( IsValidFileName(value) == false)
            {
              // 注意
              // 
              // Windowsのファイル名には以下の文字が使えません！
              // 
              // \\ / : , * ? \" < > |
              //
              // Alert
              // The following characters cannot be used for the file name of Windows.
              // 
              // \\ / : , * ? \" < > |
              MessageBox.Show(Resources.DialogMessageNotUseWindowsFileName,
              Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              return(-1);
            }
            else
            {
              _fAddCamoExt = true;
              _CamoExt = value;
            }

            break;

          #endregion
            
          //-----------------------------------
          // 保存設定（復号）
          //-----------------------------------    
          #region
          // Save to the same folder in decryption
          case "/dsaveto": // 常に同じ場所へファイルを復号化する
            if (Directory.Exists(value) == true)
            {
              if (Path.IsPathRooted(value) == false)
              {
                value = Path.GetFullPath(value);
              }
              _DecodeToSameFldrPath = value;
              _fDecodeToSameFldr = true;
            }
            break;

          // Confirm overwriting when same filename exists
          //case "ow": // 同名ファイルの上書きを確認するか（確認無で上書きするか？）＝暗号化時と共通オプション
          //  break;

          // Create no parent folder in decryption
          case "/nopfldr": // 復号するときに親フォルダを生成しない
            if ( value == "1")
            {
              _fNoParentFldr = true;
            }
            else if (value == "0")
            {
              _fNoParentFldr = false;
            }
            break;

          // Set the timestamp to decrypted files or directories
          case "/now": // 復号したファイルのタイムスタンプを生成日時にする
            if ( value == "1")
            {
              _fSameTimeStamp = true;
            }
            else if (value == "0")
            {
              _fSameTimeStamp = false;
            }
            break;

          #endregion

          //-----------------------------------
          // 保存設定（ZIP）
          //-----------------------------------    
          #region
          // Save to the same folder in ZIP
          case "/zipsaveto": // 常に同じ場所へファイルを暗号化する
            if (Directory.Exists(value) == true)
            {
              if (Path.IsPathRooted(value) == false)
              {
                value = Path.GetFullPath(value);
              }
              _ZipToSameFldrPath = value;
              _fZipToSameFldr = true;
            }
            break;

          // Confirm overwriting when same filename exists
          //case "ow": // 同名ファイルの上書きを確認するか（確認無で上書きするか？）＝暗号化時と共通オプション
          //  break;

          // EncryptionAlgorithm ( 0: PkzipWeak, 1: WinZipAes128, 2: WinZipAes256 ) 
          case "/zipalgo":
            if (value == "1")
            {
              _ZipEncryptionAlgorithm = 1;
            }
            else if (value == "2")
            {
              _ZipEncryptionAlgorithm = 2;
            }
            else
            {
              _ZipEncryptionAlgorithm = 0;
            }
            break;

          #endregion

          //-----------------------------------
          // 削除設定
          //-----------------------------------          
          #region
          // Delete original files or directories after encryption
          case "/del": // 元ファイルの完全削除を行うか
            if (int.TryParse(value, out ResultNum))
            {
              if (ResultNum > 0 && ResultNum < 4)
              {
                _fDelOrgFile = true;
                _fCompleteDelFile = ResultNum;  // 0: 削除しない, 1: 通常削除, 2: ごみ箱, 3: 完全削除
              }
              else
              {
                _fDelOrgFile = false;
              }
            }
            break;

          // Delete encrypted file after decryption
          case "/delenc": // 暗号化ファイルの完全削除を行うか
            if (int.TryParse(value, out ResultNum))
            {
              if ( ResultNum > 0 && ResultNum < 4)
              {
                _fDelEncFile = true;
                _fCompleteDelFile = ResultNum;  // 0: Not de;ete 1: Normal delete, 2: Go to trash, 3: Completely delete
              }
              else
              {
                _fDelEncFile = false;
              }
            }
            break;

          // Show the check box in main form window
          case "/chkdel": // メインフォームにチェックボックスを表示する
            if ( value == "1" )
            {
              _fEncryptShowDelChkBox = true;
              _fDecryptShowDelChkBox = true;
            }
            else if (value == "0")
            {
              _fEncryptShowDelChkBox = false;
              _fDecryptShowDelChkBox = false;
            }
            break;

          // Show confirmation dialog to delete file or directories
          case "/comfdel":  //削除確認メッセージを表示するか
            if ( value == "1" )
            {
              _fConfirmToDeleteAfterEncryption = true;
              _fConfirmToDeleteAfterDecryption = true;
            }
            else if (value == "0")
            {
              _fConfirmToDeleteAfterEncryption = false;
              _fConfirmToDeleteAfterDecryption = false;
            }
            break;

          //Advanced Delete Option [0: Normal Delete, 1: Complete erase, 2: Send to Trash ]
          case "/delrand": // 乱数を何回書き込み消去するか
            if ( int.TryParse(value, out ResultNum) == true)
            {
              if ( 0 < ResultNum && ResultNum < 100)
              {
                _DelRandNum = ResultNum;
              }
            }
            break;

          case "/delnull": // NULLを何回書き込み消去するか
            if ( int.TryParse(value, out ResultNum) == true)
            {
              if ( 0 < ResultNum && ResultNum < 100)
              {
                _DelZeroNum = ResultNum;
              }
            }
            break;

          #endregion

          //-----------------------------------
          // 圧縮
          //-----------------------------------          
          #region
          // Enable compression
          case "/comprate": // 圧縮率
            if ( int.TryParse(value, out ResultNum) == true)
            {
              if ( -1 < ResultNum || ResultNum < 10)
              {
                _CompressRate = ResultNum;
              }

            }
            break;

          #endregion

          //-----------------------------------
          // パスワードファイル
          //-----------------------------------          
          #region
          // Allow a password file to drag and drop
          case "/pf": // パスワードにファイルの指定を許可する
            if ( value == "1" )
            {
              _fAllowPassFile = true;
            }
            else if (value == "0")
            {
              _fAllowPassFile = false;
            }
            break;

          // Password file path for encryption
          case "/pfile": // 暗号化時のパスワードファイルパス
            if (Directory.Exists(value) == true)
            {
              if (Path.IsPathRooted(value) == false)
              {
                value = Path.GetFullPath(value);
              }
              _PassFilePath = value;
              _fAllowPassFile = true;
            }
            break;

          // Password file path for decryption
          case "/dpfile": // 復号時のパスワードファイルパス
            if (Directory.Exists(value) == true)
            {
              if (Path.IsPathRooted(value) == false)
              {
                value = Path.GetFullPath(value);
              }
              _PassFilePathDecrypt = value;
              _fAllowPassFile = true;
            }
            break;

          // It's not issued an error message when password file doesn't exists
          case "/nomsgp": // パスワードファイルがない場合エラーを出さない
            if (value == "1")
            {
              _fNoErrMsgOnPassFile = true;
            }
            else if (value == "0")
            {
              _fNoErrMsgOnPassFile = false;
            }
            break;

          #endregion

          //-----------------------------------
          // パスワード入力制限
          //-----------------------------------          
          #region
          // Set number of times to input password in encrypt files
          case "/typelimit": // パスワードのタイプミス制限回数
            if (int.TryParse(value, out ResultNum) == true)
            {
              if ( 0 <= ResultNum || ResultNum <= 10)
              {
                _MissTypeLimitsNum = ResultNum;
              }
            }
            break;

          // If input wrong password to the number of times, destroy it
          case "/breakfile":  // // タイプミス回数を超えたときにファイルを破壊するか否か
            if ( value == "1" )
            {
              _fBroken = true;
            }
            else if (value == "0")
            {
              _fBroken = false;
            }
            break;

          #endregion

          //-----------------------------------
          // サルベージ
          //-----------------------------------
          #region

          // Decrypt one by one while creating the parent folder.
          case "/slvgfolder": // 一つずつ親フォルダーを確認、生成しながら復号する
            if (value == "1")
            {
              _fSalvageToCreateParentFolderOneByOne = true;
            }
            else if (value == "0")
            {
              _fSalvageToCreateParentFolderOneByOne = false;
            }
            break;

          // Decrypt all files into the directory of the same hierarchy.
          case "/slvgsame": // すべてのファイルを同じ階層のディレクトリーに復号する
            if (value == "")
            {
              _fSalvageIntoSameDirectory = true;
            }
            else if (value == "0")
            {
              _fSalvageIntoSameDirectory = false;
            }
            break;

          #endregion

          //-----------------------------------
          //その他（コマンドラインからのみ）
          //-----------------------------------
          #region
          case "/en": // 明示的な暗号処理
            if (value == "1")
            {
              _ProcTypeWithoutAsk = 1;                                 
            }
            break;

          case "/de": // 明示的な復号処理
            if (value == "2")
            {
              _ProcTypeWithoutAsk = 2;
            }
            break;

          case "/4gbok": // 4GB超えを容認
            if (value == "1")
            {
              _fOver4GBok = true;
            }
            else if (value == "0")
            {
              _fOver4GBok = false;
            }
            break;

          case "/list": // リストファイルからのパスの読み込み
            string dir = Path.GetDirectoryName(value);
            if (File.Exists(value) == true)
            {
              int c = 0;
              string line;
              // Read the List file and Add to FileList.
              using (StreamReader sr = new StreamReader(value))
              {
                while ((line = sr.ReadLine()) != null)
                {
                  if (line == "") continue;
                  string filename = Path.Combine(dir, line);
                  string fullpath = Path.GetFullPath(filename);
                  if ( File.Exists(fullpath))
                  {
                    _FileList.Add(fullpath);
                  }
                  c++;
                }
              }
            }
            break;

          #endregion

        }// end switch;

        i++;

      }// end foreach (string cmd in cmds);

      return (i);

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// Add more files or directories.
    /// パラメータ以外（ドラッグ＆ドロップなど）からファイルを追加する
    /// </summary>
    /// <param name="FilePath"></param>
    //----------------------------------------------------------------------
    public void AddArgsFile(string FilePath)
    {
      if (File.Exists(FilePath) == true)
      {
        //何の種類のファイルか
        _FileType[CheckFileType(FilePath)]++;
        _FileList.Add(FilePath);
      }

    }

    //----------------------------------------------------------------------
    /// <summary>
    /// Encrypt stored passwords
    /// 記憶パスワードを暗号化する
    /// </summary>
    /// <param name="MyPasswordString"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    private byte[] EncryptMyPassword(string MyPasswordString)
    {
      if (MyPasswordString == null)
      {
        MyPasswordString = "";
      }
      // Get the drive name where the application is installed
      //アプリケーションがインストールされているドライブ名を取得
      string RootDriveName = Path.GetPathRoot(Application.ExecutablePath);
      // Get the drive serial number.
      string volNumString = GetDriveSerialNumber();

      // "The serial number of the drive volume + MachineName" is set by encryption for stored passwords
      // ex).  818980454_HIBARA
      string Password = volNumString + "_" + Environment.MachineName;

      byte[] salt = new byte[8];
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      rng.GetBytes(salt);

      Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, salt, 1000);

      byte[] key = deriveBytes.GetBytes(256 / 8);
      byte[] iv = deriveBytes.GetBytes(256 / 8);

      RijndaelManaged aes = new RijndaelManaged
      {
        BlockSize = 256,              // BlockSize = 32bytes
        KeySize = 256,                // KeySize = 32byte
        Mode = CipherMode.CBC,
        Padding = PaddingMode.Zeros
      };
      
      aes.Key = key;
      aes.IV = iv;

      MemoryStream ms = new MemoryStream();
      ms.Write(salt, 0, 8);

      ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
      using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
      {
        using (StreamWriter sw = new StreamWriter(cs))
        {
          //Write all data to the stream.
          sw.Write(MyPasswordString);
        }
      }

      return ms.ToArray();

    }

    //----------------------------------------------------------------------
    /// <summary>
    ///  Decrypt stored passwords
    ///  記憶パスワードを復号して元に戻す
    /// </summary>
    /// <param name="bytesPassword"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    private string DecryptMyPassword(byte[] MyPasswordBinary)
    {
      // Get the drive name where the application is installed
      //アプリケーションがインストールされているドライブ名を取得
      string RootDriveName = Path.GetPathRoot(Application.ExecutablePath);
      // Get the drive serial number.
      string volNumString = GetDriveSerialNumber();
      // "The serial number of the drive volume + MachineName" is set by encryption for stored passwords
      // ex).  818980454_HIBARA
      string Password = volNumString + "_" + Environment.MachineName;

      RijndaelManaged aes = new RijndaelManaged
      {
        BlockSize = 256,
        KeySize = 256,
        Mode = CipherMode.CBC,
        Padding = PaddingMode.Zeros
      };

      using (MemoryStream ms = new MemoryStream(MyPasswordBinary))
      {
        byte[] salt = new byte[8];
        ms.Read(salt, 0, 8);
        Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, salt, 1000);

        aes.Key = deriveBytes.GetBytes(256 / 8);
        aes.IV = deriveBytes.GetBytes(256 / 8);

        try
        {
          ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
          using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader sr = new StreamReader(cs))
            {
              // Read the decrypted bytes from the decrypting stream
              // and place them in a string.
              return (sr.ReadToEnd());
            }
          }
        }
        catch(Exception e)
        {
          return ("");
        }

      }

    }
    
    //----------------------------------------------------------------------
    /// <summary>
    /// Specify the format of the encryption file name
    /// 指定した書式でファイル名を生成する
    /// </summary>
    /// <param name="FormatString"></param  >
    /// <param name="FilePath"></param>
    /// <param name="fInit"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    public string getSpecifyFileNameFormat(string FormatString, string FilePath)
    {
      // ファイル名            : <filename> 
      // 拡張子                : <ext> 
      // 日付                  : <date:[指定書式]> 
      // 連番                  : <num:[桁数]> 
      // ランダムな文字列      : <random:[文字数]> 
      #region

      int SerialNum = 1;
      string ReturnString = "";
      while (true)
      {
        ReturnString = FormatString;

        //-----------------------------------
        // File Name
        ReturnString = Regex.Replace(ReturnString, @"<filename>", Path.GetFileNameWithoutExtension(FilePath));

        //-----------------------------------
        // Extension
        ReturnString = Regex.Replace(ReturnString, @"<ext>", Path.GetExtension(FilePath));

        //-----------------------------------
        // Date time
        Regex r = new Regex(@"<date:(.*?)>", RegexOptions.IgnoreCase);
        Match m = r.Match(ReturnString);
        while (m.Success)
        {
          DateTime dt = DateTime.Now;
          string DateTimeString = dt.ToString(m.Groups[1].Value);
          ReturnString = Regex.Replace(ReturnString, m.Value, DateTimeString);
          m = m.NextMatch();
        }

        //-----------------------------------
        // Serial number
        r = new Regex(@"<num:([0-9]*?)>", RegexOptions.IgnoreCase);
        m = r.Match(ReturnString);
        while (m.Success)
        {
          int FigNum = 0;
          if (int.TryParse(m.Groups[1].Value, out FigNum) == true)
          {
            ReturnString = Regex.Replace(ReturnString, m.Value, SerialNum.ToString(new string('0', FigNum)));
          }
          m = m.NextMatch();
        }

        //-----------------------------------
        // Random string 
        r = new Regex(@"<random:([0-9]*?)>", RegexOptions.IgnoreCase);
        m = r.Match(ReturnString);
        while (m.Success)
        {
          int FigNum = 0;
          if (int.TryParse(m.Groups[1].Value, out FigNum) == false)
          {
            FigNum = 8;
          }

          string CharAlphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
          string CharAlphabetLower = "abcdefghijklmnopqrstuvwxyz";
          string CharNumbers = "0123456789";
          string CharSymbols = "=-+!_#$%&()[]{}~^`'@";
          string Chars = "";

          if (_fAutoNameAlphabets == true)
          {
            if (_fAutoNameUpperCase == true)
            {
              Chars += CharAlphabetUpper;
            }
            if (_fAutoNameLowerCase == true)
            {
              Chars += CharAlphabetLower;
            }
          }

          if (_fAutoNameNumbers == true)
          {
            Chars += CharNumbers;
          }

          if (_fAutoNameSymbols == true)
          {
            Chars += CharSymbols;
          }

          if (Chars == "")
          {
            Chars = CharAlphabetUpper;
          }

          char[] stringChars = new char[FigNum];
          Random random = new Random();

          for (int i = 0; i < stringChars.Length; i++)
          {
            stringChars[i] = Chars[random.Next(Chars.Length)];
          }

          ReturnString = Regex.Replace(ReturnString, m.Value, new string(stringChars));

          m = m.NextMatch();

        }

        //-----------------------------------
        // Windowsでの禁止文字列を使っていないか
        //-----------------------------------
        if (IsValidFileName(ReturnString) == false)
        {
          // 注意
          // 
          // Windowsのファイル名には以下の文字が使えません！
          // 
          // \\ / : , * ? \" < > |
          //
          // Alert
          // The following characters cannot be used for the file name of Windows.
          // 
          // \\ / : , * ? \" < > |
          MessageBox.Show(Resources.DialogMessageNotUseWindowsFileName,
          Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        if(File.Exists(Path.Combine(Path.GetDirectoryName(FilePath), ReturnString)) == false)
        {
          break;
        }
        else
        {
          SerialNum++;
        }

      }
      
      return(ReturnString);

      #endregion
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// ドライブボリュームのシリアルナンバーを取得する
    /// Get serial number of the drive volume 
    /// </summary>
    /// <returns></returns>
    //----------------------------------------------------------------------
    #region
    [DllImport("kernel32.dll")]
    private static extern long GetVolumeInformation(
                          string PathName,
                          System.Text.StringBuilder VolumeNameBuffer,
                          UInt32 VolumeNameSize,
                          ref UInt32 VolumeSerialNumber,
                          ref UInt32 MaximumComponentLength,
                          ref UInt32 FileSystemFlags,
                          System.Text.StringBuilder FileSystemNameBuffer,
                          UInt32 FileSystemNameSize
    );
    private string GetDriveSerialNumber()
    {
      //アプリケーションがインストールされているドライブ名を取得
      string RootDriveName = Path.GetPathRoot(Application.ExecutablePath);

      uint serial_number = 0;
      uint max_component_length = 0;
      System.Text.StringBuilder sb_volume_name = new System.Text.StringBuilder(256);
      UInt32 file_system_flags = new UInt32();
      System.Text.StringBuilder sb_file_system_name = new System.Text.StringBuilder(256);

      if (GetVolumeInformation(RootDriveName, sb_volume_name,
          (UInt32)sb_volume_name.Capacity, ref serial_number, ref max_component_length,
          ref file_system_flags, sb_file_system_name, (UInt32)sb_file_system_name.Capacity) == 0)
      {
        return ("0");
      }
      else
      {
        return (serial_number.ToString());
      }

    }
    #endregion

    //----------------------------------------------------------------------
    /// <summary>
    /// Windowsファイルシステムで禁止文字列があるかチェックする
    /// </summary>
    /// <param name="testName"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    bool IsValidFileName(string FileName)
    {
      Regex containsABadCharacter = new Regex("["  + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]");
      if (containsABadCharacter.IsMatch(FileName))
      {
        return false;
      }
      else
      {
        return true;
      }
    }


    //----------------------------------------------------------------------
    /// <summary>
    /// 16進数(Hex)文字列からバイナリデータに変換する
    /// 参考：http://stackoverflow.com/questions/623104/byte-to-hex-string/5919521#5919521
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    //----------------------------------------------------------------------
    public static byte[] HexStringToByteArray(string Hex)
    {
      byte[] Bytes = new byte[Hex.Length / 2];
      int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
       0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
       0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

      for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
      {
        Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                          HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
      }

      return Bytes;
    }

    /// <summary>
    /// バイナリーデータから16進(Hex)文字列に変換する
    /// 
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string BytesToHexString(byte[] bytes)
    {
      return BitConverter.ToString(bytes).Replace("-", string.Empty);
    }

    public static string ByteArrayToHexString(byte[] Bytes)
    {
      StringBuilder Result = new StringBuilder(Bytes.Length * 2);
      string HexAlphabet = "0123456789ABCDEF";

      foreach (byte B in Bytes)
      {
        Result.Append(HexAlphabet[(int)(B >> 4)]);
        Result.Append(HexAlphabet[(int)(B & 0xF)]);
      }

      return Result.ToString();
    }

    /// <summary>
    /// Detect file type to drag and drop ( Directory, ATC, EXE[by ATC], ZIP ).
    /// 投げ込まれたファイルの種類を特定する（ディレクトリ, ATC, EXE[by ATC], ZIP）
    /// </summary>
    /// <remarks>http://stackoverflow.com/a/929418</remarks>
    /// <returns> 1: ATC, 2: EXE(ATC), 2: EXE(ATC), 3: ZIP, 0: Others(Encrypt file?)</returns>
    private int CheckFileType(string FilePath)
    {
      const string SignatureZip = "50-4B-03-04";
      const string SignatureAtc = "_AttacheCaseData";
      const string SignatureAtcBroken = "_Atc_Broken_Data";
            
      //-----------------------------------
      // ディレクトリー
      // Directory
      //-----------------------------------
      if (Directory.Exists(FilePath) == true)
      {
        return (0);
      }
      // ファイルが存在しない
      // File does not exists.
      else if (File.Exists(FilePath) == false)
      {
        return (0);
      }

      //-----------------------------------
      // ZIPファイルの判別
      // Detect Zip file
      //-----------------------------------
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      {
        if (fs.Length < 4)
        {
          return (0);
        }
        else
        {
          byte[] signature = new byte[4];
          int bytesRequired = 4;
          int index = 0;
          while (bytesRequired > 0)
          {
            int bytesRead = fs.Read(signature, index, bytesRequired);
            bytesRequired -= bytesRead;
            index += bytesRead;
          }
          string actualSignature = BitConverter.ToString(signature);
          if (actualSignature == SignatureZip && Path.GetExtension(FilePath).ToLower() == ".zip")
          {
            return (3);  // Zip file
          }
        }
        //-----------------------------------
        // ATCファイルの判別
        // Detect atc file
        //-----------------------------------
        fs.Seek(4, SeekOrigin.Begin);
        byte[] bufferSignature = new byte[16];
        fs.Read(bufferSignature, 0, 16);
        string SignatureText = Encoding.ASCII.GetString(bufferSignature);
        if (SignatureText == SignatureAtc || SignatureText == SignatureAtcBroken)
        {
          return (1);
        }
        //-----------------------------------
        // EXE(ATC)ファイルの判別
        // Detect Exe(atc) file
        // http://stackoverflow.com/questions/2863683/how-to-find-if-a-file-is-an-exe
        //-----------------------------------
        byte[] twoBytes = new byte[2];
        fs.Seek(0, SeekOrigin.Begin);
        fs.Read(twoBytes, 0, 2);
        if (Encoding.UTF8.GetString(twoBytes) == "MZ")
        {
          // _AttacheCaseData
          int[] AtcTokenByte = { 95, 65, 116, 116, 97, 99, 104, 101, 67, 97, 115, 101, 68, 97, 116, 97 };
          // _Atc_Broken_Data
          int[] AtcBrokenTokenByte = { 95, 65, 116, 99, 95, 66, 114, 111, 104, 101, 110, 95, 68, 97, 116, 97 };

          bool fToken = false;
          int b, pos = 0;
          while ((b = fs.ReadByte()) > -1 || pos < 50000)
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
                pos++;
              }
              if (fToken == true)
              {
                if (pos > 20)
                { // Self executabel file
                  return (2);
                }
              }
            }

            //-----------------------------------
            // Check the token "_Atc_Broken_Data"
            if (b == AtcBrokenTokenByte[0])
            {
              fToken = true;
              for (int i = 1; i < AtcBrokenTokenByte.Length; i++)
              {
                if (fs.ReadByte() != AtcBrokenTokenByte[i])
                {
                  fToken = false;
                  break;
                }
                pos++;
              }

              if (fToken == true)
              {
                _fBroken = true;
              }
            }

            pos++;

            if (fToken == true)
            {
              break;
            }
            //-----------------------------------

          }// end while();

        }
        else
        {
          return (0);
        }
         
      }// end using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

      //-----------------------------------
      // いずれのファイルでもない
      // Not correspond to any file
      //-----------------------------------
      return (0);

    }

    /// <summary>
    /// 投げ込まれたファイルタイプから処理内容を決定する
    /// </summary>
    public int DetectFileType()
    {
      // Process Type
      // private const int PROCESS_TYPE_ERROR        = -1;
      // private const int PROCESS_TYPE_NONE         = 0;
      // private const int PROCESS_TYPE_ATC          = 1;
      // private const int PROCESS_TYPE_ATC_EXE      = 2;
      // private const int PROCESS_TYPE_PASSWORD_ZIP = 3;
      // private const int PROCESS_TYPE_DECRYPTION   = 4;

      _FileType = new int[4] { 0, 0, 0, 0 };

      foreach (string f in _FileList)
      {
        // 1: ATC, 2: EXE(ATC), 2: EXE(ATC), 3: ZIP, 0: Others(Encrypt file?)
        _FileType[CheckFileType(f)]++;
      }

      // Process Type
      // private const int PROCESS_TYPE_ERROR        = -1;
      // private const int PROCESS_TYPE_NONE         = 0;
      // private const int PROCESS_TYPE_ATC          = 1;
      // private const int PROCESS_TYPE_ATC_EXE      = 2;
      // private const int PROCESS_TYPE_PASSWORD_ZIP = 3;
      // private const int PROCESS_TYPE_DECRYPTION   = 4;
      if ((_FileType[1] > 0 || _FileType[2] > 0) && _FileType[3] == 0 && _FileType[0] == 0)
      {
        return PROCESS_TYPE_DECRYPTION;
      }
      else if (_FileType[1] == 0 && _FileType[2] > 0 && _FileType[3] == 0 && _FileType[0] == 0)
      {
        return PROCESS_TYPE_DECRYPTION;
      }
      else if (_FileType[1] == 0 && _FileType[2] == 0 && _FileType[3] > 0 && _FileType[0] == 0)
      {
        //AppSettings.Instance.EncryptionFileType = FILE_TYPE_PASSWORD_ZIP;
        //return 3;
        return PROCESS_TYPE_PASSWORD_ZIP;
      }
      else if (_FileType[1] == 0 && _FileType[2] == 0 && _FileType[3] == 0 && _FileType[0] > 0)
      {
        return PROCESS_TYPE_ATC;
      }
      else
      {
        return PROCESS_TYPE_ERROR;
      }
    }

    //----------------------------------------------------------------------
    /// <summary>
    /// ファイルからSHA-256(32 bytes)ハッシュ値を求める
    /// Get SHA-256(32bytes) hash data value from a file.
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns>string[]</returns>
    //----------------------------------------------------------------------
    public string GetSha256HashFromFile(string FilePath)
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

    //----------------------------------------------------------------------
    /// <summary>
    /// パスワードファイルからキーとなるSHA-1と混ぜ合わせたハッシュ値を求める
    /// Get the mixed SHA-1 data value from 'Password file' instead of keys.
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns>string[] (shift_jis)</returns>
    //----------------------------------------------------------------------
    public byte[] GetSha1HashFromFile(string FilePath)
    {
      byte[] buffer = new byte[255];
      byte[] result = new byte[32];
      //byte[] header = new byte[12];

      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
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

      //string text = System.Text.Encoding.UTF8.GetString(result);
      string text = Encoding.GetEncoding("shift_jis").GetString(result);

      return (result);

    }

    
  }



}


