using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AttacheCase
{
  public class Logger
  {
    [System.Flags]
    private enum LogLevel
    {
        TRACE,
        INFO,
        DEBUG,
        WARNING,
        ERROR,
        FATAL
    }

    private const string FILE_EXT = ".log";
    private const string datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
    private System.Diagnostics.Stopwatch StopWatch = new System.Diagnostics.Stopwatch();
    
    private string _LogFilePath = "";
     public string LogFilePath
    {
      get { return this._LogFilePath; }
    }
    private System.Text.Encoding _TextEncoding;
    public System.Text.Encoding TextEncoding
    {
      get { return this._TextEncoding; }
    }

    // Constructor
    public Logger(string FilePath = "", Encoding enc = null, bool fClear = true)
    {
      if ( enc == null){
        _TextEncoding = System.Text.Encoding.UTF8;
      }
      else{
        _TextEncoding = enc;
      }
      // Log file path
      if (File.Exists(FilePath) == true){
        _LogFilePath = FilePath;
      }
      else{
        try
        {
          // Create the file, or overwrite if the file exists.
          using (FileStream fs = File.Create(FilePath))
          {
            _LogFilePath = FilePath;
          }
        }
        catch
        {
          _LogFilePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + FILE_EXT;
        }
      }

      if ( fClear == true) {
        using (FileStream fs = File.Create(_LogFilePath))
        {
          fs.SetLength(0);
        }
      }

    }

    /// <summary>
    /// Log a DEBUG message
    /// </summary>
    /// <param name="text">Message</param>
    public void Debug(string text)
    {
      WriteFormattedLog(LogLevel.DEBUG, text);
    }
    /// <summary>
    /// Log an ERROR message
    /// </summary>
    /// <param name="text">Message</param>
    public void Error(string text)
    {
      WriteFormattedLog(LogLevel.ERROR, text);
    }
    /// <summary>
    /// Log a FATAL ERROR message
    /// </summary>
    /// <param name="text">Message</param>
    public void Fatal(string text)
    {
      WriteFormattedLog(LogLevel.FATAL, text);
    }
    /// <summary>
    /// Log an INFO message
    /// </summary>
    /// <param name="text">Message</param>
    public void Info(string text)
    {
      WriteFormattedLog(LogLevel.INFO, text);
    }
    /// <summary>
    /// Log a TRACE message
    /// </summary>
    /// <param name="text">Message</param>
    public void Trace(string text)
    {
      WriteFormattedLog(LogLevel.TRACE, text);
    }

    /// <summary>
    /// Log a WARNING message
    /// </summary>
    /// <param name="text">Message</param>
    public void Warning(string text)
    {
      WriteFormattedLog(LogLevel.WARNING, text);
    }

    private void WriteFormattedLog(LogLevel level, string text)
    {
      string pretext;
      switch (level)
      {
        case LogLevel.TRACE:
          pretext = "[TRACE]   " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        case LogLevel.INFO:
          pretext = "[INFO]    " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        case LogLevel.DEBUG:
          pretext = "[DEBUG]   " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        case LogLevel.WARNING:
          pretext = "[WARNING] " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        case LogLevel.ERROR:
          pretext = "[ERROR]   " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        case LogLevel.FATAL:
          pretext = "[FATAL]   " + System.DateTime.Now.ToString(datetimeFormat) + "  ";
          break;
        default:
          pretext = "";
          break;
      }
      WriteLine(pretext + text);
    }

    private void WriteLine(string text, bool fAppend = true)
    {
      try
      {
        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_LogFilePath, fAppend, _TextEncoding))
        {
          if (!string.IsNullOrEmpty(text))
          {
            sw.WriteLine(text);
          }
        }
      }
      catch
      {
        throw;
      }
    }

    public void StopWatchStart(){
      StopWatch.Reset();
      Info("StopWatch Start...");
      StopWatch.Start();
    }

    public void StopWatchStop(){
      StopWatch.Stop();
      Info("StopWatch Stop.");
      // Measurement time
      TimeSpan ts = StopWatch.Elapsed;
      Info("Time: " + ts.ToString());
    }



  }
}
