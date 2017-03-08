using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace AttacheCase
{
  public class Wipe
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

    // 削除中のファイルパス
    // A file path being deleted.
    private string _DeletingFilePath = "";
    public string DeletingFilePath
    {
      get { return this._DeletingFilePath; }
    }

    // 処理中の合計セクタ数
    // Total sectors progress
    private Int64 _TotalSectors = 0;
    public Int64 TotalSectors
    {
      get { return this._TotalSectors; }
    }

    // 処理する最大の合計セクタ数
    // The maximum value to be processed
    private Int64 _TotalFileSectors = 0;
    public Int64 TotalFileSectors
    {
      get { return this._TotalFileSectors; }
    }

    // ゼロ埋め、またはランダム埋めの処理した合計数
    // Total progress of Zero fill and Random fill
    private int _NumOfTimes = 0;
    public int NumOfTimes
    {
      get { return this._NumOfTimes; }
    }

    // 処理するゼロ埋め、またはランダム埋めの合計数
    // Total times of Zero fill and Random fill
    private int _TotalTimes = 0;
    public int TotalTimes
    {
      get { return this._TotalTimes; }
    }

    // ユーザーキャンセル
    // User cancel
    private bool _fUserCancel = false;
    public bool fUserCancel
    {
      get { return this._fUserCancel; }
      set { this._fUserCancel = value; }
    }


    /// <summary>
    /// Deletes a file in a secure way by overwriting it with
    /// random garbage data n times.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="FilePaths">Array of file paths</param>
    /// <param name="DelRand/*Num">Number of Random data</param>
    /// <param name="DelZeroNum">Number of Zeros </param>
    public int WipeFile(
      object sender, DoWorkEventArgs e,
      List<string> FilePaths, int DelRandNum, int DelZeroNum)
    {
      try
      {
        Int64 TotalSectors = 0;
        Int64 TotalFileSectors = 0;

        List<string> FileList = new List<string>();

        ArrayList MessageList = new ArrayList();

        foreach(string FilePath in FilePaths)
        {
          if (File.Exists(FilePath) == true)
          {
            FileList.Add(FilePath);
          }
          else if (Directory.Exists(FilePath) == true)
          {
            foreach (string f in GetFileList("*", FilePath))
            {
              FileList.Add(f);
            }
          }
        }

        FilePaths = FileList;


        ParallelOptions options = new ParallelOptions();
        options.MaxDegreeOfParallelism = Environment.ProcessorCount;
        Parallel.ForEach(FilePaths, options, FilePath =>
        {
          if (File.Exists(FilePath))
          {
            // Calculate the total number of sectors in the file.
            lock (FilePaths) TotalFileSectors += (Int64)Math.Ceiling(new FileInfo(FilePath).Length / 512.0);
          }
        });

        /*
        foreach (string path in FilePaths)
        {
          {
            if (File.Exists(path))
            {
              // Calculate the total number of sectors in the file.
              TotalFileSectors += (Int64)Math.Ceiling(new FileInfo(path).Length / 512.0);
            }
          }
        }
        */

        TotalFileSectors = TotalFileSectors * (DelRandNum + DelZeroNum);

        BackgroundWorker worker = sender as BackgroundWorker;
        worker.WorkerSupportsCancellation = true;
        e.Result = DELETING;

        foreach (string FilePath in FilePaths)
        {
          if (File.Exists(FilePath) == false)
          {
            continue;
          }
          int _TotalTimes = DelRandNum + DelZeroNum;
          int RandNum = DelRandNum;
          int ZeroNum = DelZeroNum;

          // Set the files attributes to normal in case it's read-only.
          File.SetAttributes(FilePath, FileAttributes.Normal);

          // Calculate the total number of sectors in the file.
          double sectors = Math.Ceiling(new FileInfo(FilePath).Length / 512.0);

          // Create a dummy-buffer the size of a sector.
          byte[] dummyBuffer = new byte[512];

          // Open a FileStream to the file.
          using (FileStream fs = new FileStream(FilePath, FileMode.Open))
          {
            while (RandNum > 0 || ZeroNum > 0)
            {

              _NumOfTimes = _TotalTimes - (RandNum + ZeroNum) + 1;

              // Go to the beginning of the stream
              fs.Position = 0;

              // Loop all sectors
              for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
              {
                TotalSectors++;

                if (TotalSectors % 10 == 0)
                {
                  string _DeleteType = "";
                  if(RandNum > 0)
                  {
                    _DeleteType = "Random";
                  }
                  else if (ZeroNum > 0 )
                  {
                    _DeleteType = "Zeros";
                  }

                  string MessageText =
                  String.Format("{0} ({1}: {2}/{3}) - {4}/{5} sectors",
                    Path.GetFileName(FilePath), // {0}
                    _DeleteType,                // {1}
                    _NumOfTimes,                // {2}
                    _TotalTimes,                // {3}
                    TotalSectors.ToString(),    // {4}
                    TotalFileSectors.ToString() // {5}
                    );
                  
                  float percent = ((float)TotalSectors / TotalFileSectors);
                  MessageList = new ArrayList();
                  MessageList.Add(DELETING);
                  MessageList.Add(MessageText);
                  System.Random r = new System.Random();
                  if (r.Next(0, 20) == 4)
                  {
                    worker.ReportProgress((int)(percent * 10000), MessageList);
                  }
                }

                //-----------------------------------
                // Random number fills
                if (RandNum > 0)
                {
                  // Create a cryptographic Random Number Generator.
                  // This is what I use to create the garbage data.
                  // Fill the dummy-buffer with random data
                  RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                  rng.GetBytes(dummyBuffer);
                }
                //-----------------------------------
                // Zeros fills
                else
                {
                  // Zeros fills
                  Array.Clear(dummyBuffer, 0, 512);
                }
                //-----------------------------------
                // Write it to the stream
                fs.Write(dummyBuffer, 0, 512);

                // Cancel
                if (worker.CancellationPending == true)
                {
                  e.Cancel = true;
                  return (USER_CANCELED);
                }

              }// end for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++);
             
              if (RandNum > 0)
              {
                RandNum--;
              }
              else if (ZeroNum > 0)
              {
                ZeroNum--;
              }

              // Truncate the file to 0 bytes.
              // This will hide the original file-length if you try to recover the file.
              if (RandNum == 0 && ZeroNum == 0)
              {
                fs.SetLength(0);
                break;
              }

              // Close the stream.
              //inputStream.Close();

            } // end while (RandNum > 0 || ZeroNum > 0);

          } // end using (FileStream inputStream = new FileStream(FilePath, FileMode.Open))

          //WipeDone();

          // As an extra precaution I change the dates of the file so the
          // original dates are hidden if you try to recover the file.
          DateTime dt = new DateTime(2037, 1, 1, 0, 0, 0);
          File.SetCreationTime(FilePath, dt);
          File.SetLastAccessTime(FilePath, dt);
          File.SetLastWriteTime(FilePath, dt);

          File.SetCreationTimeUtc(FilePath, dt);
          File.SetLastAccessTimeUtc(FilePath, dt);
          File.SetLastWriteTimeUtc(FilePath, dt);

          // Finally, delete the file
          File.Delete(FilePath);

        } // end foreach (string FilePath in FilePaths);


      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        e.Result = ERROR_UNEXPECTED;
        return (ERROR_UNEXPECTED);
      }

      // Delete root directory
      if (Directory.Exists(FilePaths[0]) == true)
      {
        FileSystem.DeleteDirectory(
          FilePaths[0],
          UIOption.OnlyErrorDialogs,
          RecycleOption.DeletePermanently,
          UICancelOption.ThrowException
        );
      }

      e.Result = DELETE_SUCCEEDED;
      return (DELETE_SUCCEEDED);

    }

    /// <summary>
    /// 
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
        yield return rootFolderPath;
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

  }

}
 