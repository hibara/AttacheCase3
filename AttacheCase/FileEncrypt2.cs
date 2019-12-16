//---------------------------------------------------------------------- 
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

namespace AttacheCase
{
  public partial class FileEncrypt2
  {
    // Status code
    private const int ENCRYPT_SUCCEEDED = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED = 2; // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED  = 3; // Delete is succeeded.
    private const int READY_FOR_ENCRYPT = 4; // Getting ready for encryption or decryption.
    private const int READY_FOR_DECRYPT = 5; // Getting ready for encryption or decryption.
    private const int ENCRYPTING        = 6; // Ecrypting.
    private const int DECRYPTING        = 7; // Decrypting.
    private const int DELETING          = 8; // Deleting.

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
    private const int    BUFFER_SIZE = 4096;
		// Header data variables
		private const  char   DATA_SUB_VERSION    = (char)6;  //ver.2.00~ = "5", ver.2.70~ = "6"
    private const  char   RESERVED_DATA       = (char)0;
    private char          charMissTypeLimits  = (char)3;
    private static bool   fBrocken            = false;
    private const  string STRING_TOKEN_NORMAL = "_AttacheCaseData";
    private const  string STRING_TOKEN_BROKEN = "_Atc_Broken_Data";
    private const  int    DATA_FILE_VERSION   = 105;
    private const  int    TYPE_ALGORISM       = 1;            // 1: Rijndael
    private const  string AtC_ENCRYPTED_TOKEN = "Passcode:AttacheCase";
    
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
		private char _MissTypeLimits;
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

    public FileEncrypt2()
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
    public Tuple<bool, int> Encrypt(
			object sender, DoWorkEventArgs e, 
			string[] FilePaths, string OutFilePath, 
			string Password, byte[] PasswordBinary,
			string NewArchiveName)
    {
			byte[] bufferPassword;
			byte[] bufferKey = new byte[32];

      _AtcFilePath = OutFilePath;

      BackgroundWorker worker = sender as BackgroundWorker;

      //-----------------------------------
      // Header data is starting.
      // Progress event handler
      ArrayList MessageList = new ArrayList();
      MessageList.Add(READY_FOR_ENCRYPT);
      MessageList.Add(Path.GetFileName(OutFilePath));
      worker.ReportProgress(0, MessageList);
     
      _FileList = new List<string>();
			byte[] byteArray = null;
      
			using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
      {
        // 自己実行形式ファイル（Self-executable file）
        if (_fExecutable == true)
        {
          ExeOutFileSize = rawData.Length;
          outfs.Write(rawData, 0, (int)ExeOutFileSize);
        }

        _StartPos = outfs.Seek(0, SeekOrigin.End);

        byteArray = new byte[16];
        // Plain text header
        byteArray = BitConverter.GetBytes(DATA_SUB_VERSION);
        outfs.Write(byteArray, 0, 1);
				byteArray = null;
        byteArray = BitConverter.GetBytes(RESERVED_DATA);
        outfs.Write(byteArray, 0, 1);
				byteArray = null;
        byteArray = BitConverter.GetBytes(charMissTypeLimits);
        outfs.Write(byteArray, 0, 1);
				byteArray = null;
				byteArray = BitConverter.GetBytes(fBrocken);
        outfs.Write(byteArray, 0, 1);
				byteArray = null;
				byteArray =Encoding.ASCII.GetBytes(STRING_TOKEN_NORMAL);
        outfs.Write(byteArray, 0, 16);

				byteArray = null;
				byteArray = BitConverter.GetBytes(DATA_FILE_VERSION);
        outfs.Write(byteArray, 0, 4);
				byteArray = null;
				byteArray = BitConverter.GetBytes(TYPE_ALGORISM);
        outfs.Write(byteArray, 0, 4);

        // Reserve cipher text header size after encrypting.
				byteArray = null;
        byteArray = BitConverter.GetBytes(_AtcHeaderSize);
        outfs.Write(byteArray, 0, 4);

        // Cipher text header.
        using (MemoryStream ms = new MemoryStream())
        {
					byteArray =Encoding.ASCII.GetBytes("Passcode:AttacheCase\n");
					ms.Write(byteArray, 0, byteArray.Length);

					DateTime dt = DateTime.Now;
					byteArray = Encoding.ASCII.GetBytes("LastDateTime:" + dt.ToString("yyyy/MM/dd HH:mm:ss\n"));
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
						FileInfoList.Add("Fn_0:" +                            // File number
							               NewArchiveName + "\\\t" +            // File name
														 "0"  + "\t" +                        // File size 
														 "16" + "\t" +                        // File attribute
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
							FileInfoList.Add("Fn_" + FileNumber.ToString() + ":" + // File number
                                //Item[0] + "\t" +                      // TypeFlag ( Directory: 0, file: 1 ) 
                                //Item[1] + "\t" +                      // Absolute file path
                                Item[2] + "\t" +                      // Relative file path
                                Item[3].ToString() + "\t" +           // File size 
																Item[4].ToString() + "\t" +           // File attribute
																Item[5].ToString() + "\t" +           // Last write date
																Item[6].ToString() + "\t" +           // Last write time
																Item[7].ToString() + "\t" +           // Creation date
																Item[8].ToString());                  // Creation time             

							// files only
							if (Convert.ToInt32(Item[0]) == 1)
							{
								// Files list for encryption
								_FileList.Add(Item[1].ToString());	// Absolute file path
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

								FileInfoList.Add("Fn_" + FileNumber.ToString() + ":" + // File number
									                //Item[0] + "\t" +                      // TypeFlag ( Directory: 0, file: 1 ) 
									                //Item[1] + "\t" +                      // Absolute file path
																	Item[2] + "\t" +                    // Relative file path
																	Item[3].ToString() + "\t" +           // File size 
																	Item[4].ToString() + "\t" +           // File attribute
																	Item[5].ToString() + "\t" +           // Last write date
																	Item[6].ToString() + "\t" +           // Last write time
																	Item[7].ToString() + "\t" +           // Creation date
																	Item[8].ToString());                  // Creation time             

								if (Convert.ToInt32(Item[0]) == 1)
                { // files only
                  // Files list for encryption
                  _FileList.Add(Item[1].ToString());	// Absolute file path
									// Total file size
									_TotalFileSize += Convert.ToInt64(Item[3]);
								}
                else
                { // Directory
                  _FileList.Add(Item[1].ToString());	// Absolute file path
                }

                FileNumber++;

							}// end foreach (ArrayList Item in GetFilesList(ParentPath, FilePath));
						
						}

					}// end foreach (string FilePath in FilePaths);

          //----------------------------------------------------------------------
          // Check the disk space
          //----------------------------------------------------------------------
          string RootDriveLetter = Path.GetPathRoot(OutFilePath).Substring(0, 1);

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
                  e.Result = NO_DISK_SPACE;
                  // not available free space
                  return Tuple.Create(false, NO_DISK_SPACE);
                }
                break;
            }
          }

          //----------------------------------------------------------------------
          // Create header data

          string[] FileInfoText = (string[])FileInfoList.ToArray(typeof(string));
			
					// Shift-JIS ( Japanese )
          byteArray = Encoding.GetEncoding(932).GetBytes(string.Join("\n", FileInfoText));
          ms.Write(byteArray, 0, byteArray.Length);
					Console.WriteLine(FileInfoText);
          // UTF-8
          byteArray = Encoding.UTF8.GetBytes("\n"+string.Join("\n", FileInfoText).Replace("Fn_", "U_"));
          ms.Write(byteArray, 0, byteArray.Length);
					Console.WriteLine(FileInfoText);

#if DEBUG
          //Output text file of header contents for debug.
          Int64 NowPosition = ms.Position;
          ms.Position = 0;
					//Save to Desktop folder.
					string DesktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
					string HeaderTextFilePath = Path.Combine(DesktopPath, "encrypt_header.txt");
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
            
						// Password
						if (PasswordBinary != null)
						{	// Binary
							bufferPassword = PasswordBinary;
						}
						else
						{	// Text
							bufferPassword = Encoding.UTF8.GetBytes(Password);
							//byte[] bufferPassword = Encoding.GetEncoding(932).GetBytes(Password);  // Shift-JIS
						}

						// Password is 256 bit, so truncated up to 32 bytes or fill up the data size. 
						// パスワードは256 bitなので、32バイトまで切り詰めるか、あるいはそのサイズまで埋める処理
						for (int i = 0; i < bufferKey.Length; i++)
						{
							if (i < bufferPassword.Length)
							{
								// Cut down to 32 bytes characters.
								bufferKey[i] = bufferPassword[i];
							}
							else
							{
								bufferKey[i] = 0;	// Zero filled
							}
						}
            aes.Key = bufferKey;

						// Initilization Vector
						aes.GenerateIV();
						outfs.Write(aes.IV, 0, 32);

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
					outfs.Seek(ExeOutFileSize + 28, SeekOrigin.Begin);	// self executable file
				}
				else
				{
					outfs.Seek(28, SeekOrigin.Begin);
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
          //aes.Padding = PaddingMode.PKCS7;  // Padding mode is "PKCS7".
                                            
          // Password is 256 bit, so truncated up to 32 bytes or fill up the data size. 
          for (int i = 0; i < bufferKey.Length; i++)
					{
						if (i < bufferPassword.Length)
						{
							//Cut down to 32bytes characters.
							bufferKey[i] = bufferPassword[i];
						}
						else
						{
							bufferKey[i] = 0;
						}
					}
					aes.Key = bufferKey;
					// Initilization Vector
					byte[] iv = new byte[32];
					RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
					rng.GetNonZeroBytes(iv);
					aes.IV = iv;

					outfs.Write(iv, 0, 32);

					// Encryption interface.
					ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
					using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
					{
						// zlib 2bytes header
						cse.WriteByte(0x78);

            // cse.WriteByte(0x01);	//No Compression/low
            cse.WriteByte(0x9C);  //Default Compression
            //cse.WriteByte(0xDA);	　//Best Compression

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

            using ( Ionic.Zlib.DeflateStream ds = new Ionic.Zlib.DeflateStream(cse, Ionic.Zlib.CompressionMode.Compress, flv))
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
											ds.Write(buffer, 0, len);
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

                      MessageList = new ArrayList();
                      MessageList.Add(DECRYPTING);
                      MessageList.Add(MessageText);
                      float percent = ((float)_TotalSize / _TotalFileSize);
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
              Random r = new Random();
              byteArray = new byte[BUFFER_SIZE];
              r.NextBytes(byteArray);
              ds.Write(buffer, 0, BUFFER_SIZE);
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
    /// Get a list of files specified root directory in parallel processing
    /// </summary>
    /// <remarks>http://stackoverflow.com/questions/2106877/is-there-a-faster-way-than-this-to-find-all-the-files-in-a-directory-and-all-sub</remarks>
    /// <param name="fileSearchPattern"></param>
    /// <param name="rootFolderPath"></param>
    /// <returns></returns>
    public static IEnumerable<ArrayList> GetFileList(string ParentPath, string rootFolderPath)
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
		/// 
		/// </summary>
		/// <param name="ParentPath"></param>
		/// <param name="FilePath"></param>
		/// <returns></returns>
		private static ArrayList GetFileInfo(string ParentPath, string FilePath)
		{
			ArrayList List = new ArrayList();
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
			return (List);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ParentPath"></param>
		/// <param name="DirPath"></param>
		/// <returns></returns>
		private static ArrayList GetDirectoryInfo(string ParentPath, string DirPath)
		{
			ArrayList List = new ArrayList();
			DirectoryInfo di = new DirectoryInfo(ParentPath + Path.GetFileName(DirPath));
			List.Add(0);                                           // Directory flag
			List.Add(DirPath);                              // Absolute file path
			List.Add(DirPath.Replace(ParentPath, "") + "\\"); // (string)Remove parent directory path.
			List.Add(0);                                           // File size = 0
			List.Add((int)di.Attributes);                          // (int)File attribute
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


  }// end class FileEncrypt()


}// end namespace

