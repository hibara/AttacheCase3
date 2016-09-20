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
    private const int ENCRYPT_SUCCEEDED  = 1; // Encrypt is succeeded.
    private const int DECRYPT_SUCCEEDED  = 2; // Decrypt is succeeded.
    private const int DELETE_SUCCEEDED   = 3; // Delete is succeeded.
    private const int READY_FOR_PROCESES = 4; // Getting ready for encryption or decryption.
    private const int ENCRYPTING         = 5; // Ecrypting.
    private const int DECRYPTING         = 6; // Decrypting.
    private const int DELETING           = 7; // Deleting.

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
    /// <param name="DelRandNum">Number of Random data</param>
    /// <param name="DelZeroNum">Number of Zeros </param>
    /// <param name="callback"></param>
    public int WipeFile(
      object sender, DoWorkEventArgs e,
      List<string> FilePaths, int DelRandNum, int DelZeroNum)
    {
      try
      {
        Int64 TotalSectors = 0;
        Int64 TotalFileSectors = 0;

        ArrayList MessageList = new ArrayList();

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

        BackgroundWorker worker = sender as BackgroundWorker;
        worker.WorkerSupportsCancellation = true;
        e.Result = DELETING;

        foreach ( string FilePath in FilePaths)
        {
          if (File.Exists(FilePath))
          {
            int TotalTimes = DelRandNum + DelZeroNum;
            int RandNum = DelRandNum;
            int ZeroNum = DelZeroNum;

            while (RandNum > 0 || ZeroNum > 0)
            {
              // Set the files attributes to normal in case it's read-only.
              File.SetAttributes(FilePath, FileAttributes.Normal);

              // Calculate the total number of sectors in the file.
              double sectors = Math.Ceiling(new FileInfo(FilePath).Length / 512.0);

              // Create a dummy-buffer the size of a sector.
              byte[] dummyBuffer = new byte[512];

              // Open a FileStream to the file.
              using (FileStream inputStream = new FileStream(FilePath, FileMode.Open))
              {
                for (int NumOfTimes = 1; NumOfTimes < TotalTimes + 1; NumOfTimes++)
                {
                  // Go to the beginning of the stream
                  inputStream.Position = 0;

                  // Loop all sectors
                  for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++)
                  {
                    TotalSectors++;

                    if (TotalSectors % 10 == 0)
                    {
                      string MessageText =
                        Path.GetFileName(FilePath) + "(" + NumOfTimes + "/" + TotalTimes + ") - " +
                        TotalSectors.ToString() + "/" + TotalFileSectors.ToString() + "sectors";
        
                      float percent = ((float)TotalSectors / TotalFileSectors);
                      MessageList = new ArrayList();
                      MessageList.Add(DELETING);
                      MessageList.Add(MessageText);
                      worker.ReportProgress((int)(percent * 10000), MessageList);
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
                      Array.Clear(dummyBuffer, 0, dummyBuffer.Length);
                    }
                    //-----------------------------------
                    // Write it to the stream
                    inputStream.Write(dummyBuffer, 0, dummyBuffer.Length);

                    // Cancel
                    if (worker.CancellationPending == true)
                    {
                      e.Cancel = true;
                      return(USER_CANCELED);
                    }

                  }// end for (int sectorsWritten = 0; sectorsWritten < sectors; sectorsWritten++);

                } // end for (int NumOfTimes = 1; NumOfTimes < TotalTimes + 1; NumOfTimes++);

                // Truncate the file to 0 bytes.
                // This will hide the original file-length if you try to recover the file.
                inputStream.SetLength(0);

                // Close the stream.
                //inputStream.Close();

              } // end using (FileStream inputStream = new FileStream(FilePath, FileMode.Open))


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


              //WipeDone();

              if (RandNum > 0)
              {
                RandNum--;
              }
              else if (ZeroNum > 0)
              {
                ZeroNum--;
              }

            } // end while (RandNum > 0 || ZeroNum > 0);

          } // end if (File.Exists(filename));

        } // end foreach;
           

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


    # region Events
    public event PassInfoEventHandler PassInfoEvent;
    private void UpdatePassInfo(int currentPass, int totalPasses)
    {
        PassInfoEvent(new PassInfoEventArgs(currentPass, totalPasses));
    }

    public event SectorInfoEventHandler SectorInfoEvent;
    private void UpdateSectorInfo(int currentSector, int totalSectors)
    {
        SectorInfoEvent(new SectorInfoEventArgs(currentSector, totalSectors));
    }

    public event WipeDoneEventHandler WipeDoneEvent;
    private void WipeDone()
    {
        WipeDoneEvent(new WipeDoneEventArgs());
    }

    public event WipeErrorEventHandler WipeErrorEvent;
    private void WipeError(Exception e)
    {
        WipeErrorEvent(new WipeErrorEventArgs(e));
    }
    # endregion
  }

  # region Events
  # region PassInfo
  public delegate void PassInfoEventHandler(PassInfoEventArgs e); 
  public class PassInfoEventArgs : EventArgs
  {
      private readonly int cPass;
      private readonly int tPass;

      public PassInfoEventArgs(int currentPass, int totalPasses)
      {
          cPass = currentPass;
          tPass = totalPasses;
      }

      /// <summary> Get the current pass </summary>
      public int CurrentPass { get { return cPass; } }
      /// <summary> Get the total number of passes to be run </summary> 
      public int TotalPasses { get { return tPass; } }
  }
  # endregion

  # region SectorInfo        
  public delegate void SectorInfoEventHandler(SectorInfoEventArgs e);
  public class SectorInfoEventArgs : EventArgs
  {
      private readonly int cSector;
      private readonly int tSectors;

      public SectorInfoEventArgs(int currentSector, int totalSectors)
      {
          cSector = currentSector;
          tSectors = totalSectors;
      }

      /// <summary> Get the current sector </summary> 
      public int CurrentSector { get { return cSector; } }
      /// <summary> Get the total number of sectors to be run </summary> 
      public int TotalSectors { get { return tSectors; } }
  }
  # endregion

  # region WipeDone        
  public delegate void WipeDoneEventHandler(WipeDoneEventArgs e);
  public class WipeDoneEventArgs : EventArgs
  {
  }
  # endregion

  # region WipeError
  public delegate void WipeErrorEventHandler(WipeErrorEventArgs e);
  public class WipeErrorEventArgs : EventArgs
  {
      private readonly Exception e;

      public WipeErrorEventArgs(Exception error)
      {
          e = error;
      }

      public Exception WipeError{get{ return e;}}
  }
  # endregion
  # endregion
}