using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GetHash
{
  class Program
  {
    static void Main(string[] args)
    {
      string exeFileName = "";
      string zipFileName = "";
      string dirpath = "";
      string filename = "";
      string basefilename = "";
      string ext = "";
      string exeMD5Hash = "";
      string zipMD5Hash = "";
      string exeSHA1Hash = "";
      string zipSHA1Hash = "";

      if (args.Length == 0)
      {
        Console.WriteLine("コマンドライン引数はありません。");
        return;
      }

      List<string> FileList = new List<string>();

      string[] cmds = Environment.GetCommandLineArgs();
      foreach (string cmd in cmds)
      {
        if (File.Exists(Path.GetFullPath(cmd)) == true)
        {
          FileList.Add(Path.GetFullPath(cmd));
        }
      }

      foreach (string file in FileList)
      {
        dirpath = Path.GetDirectoryName(file);
        filename = Path.GetFileName(file);
        basefilename = Path.GetFileNameWithoutExtension(file);
        ext = Path.GetExtension(file);
        //-----------------------------------
        //　File type
        if (ext == ".exe")
        {
          exeFileName = filename;
        }
        else
        {
          zipFileName = filename;
        }
        //-----------------------------------
        // MD5
        string md5 = getMd5Hash(file);
        string HashFilePath = Path.Combine(dirpath, filename) + ".md5";
        File.WriteAllText(HashFilePath, md5, Encoding.UTF8);
        if (ext == ".exe")
        {
          exeMD5Hash = md5;
        }
        else
        {
          zipMD5Hash = md5;
        }

        //-----------------------------------
        // SHA-1
        string sha1 = getSha1Hash(file);
        HashFilePath = Path.GetDirectoryName(file) + "\\" + Path.GetFileName(file) + ".sha1";
        File.WriteAllText(HashFilePath, sha1, Encoding.UTF8);
        if (ext == ".exe")
        {
          exeSHA1Hash = sha1;
        }
        else
        {
          zipSHA1Hash = sha1;
        }
      }

      //    "archives" : [
      //      {
      //        "title" : "自己解凍インストーラー",
      //        "archive" : "atcs3600.exe",
      //        "md5" : "8ed3471dba35d7a75573da6703335b74",
      //        "sha1" : "7a9c14aaaa7ef52896982be72c3228a0b0452be4"
      //      },
      //      {
      //        "title" : "ZIPファイル",
      //        "archive" : "atcs3600.zip",
      //        "md5" : "d8def05a5eef5540f12f25eeef5226b4",
      //        "sha1" : "5ccff3566e6b614791355b0d9ff3a388c1f502fa"
      //      }
      //    ]
      using (StreamWriter sw = new StreamWriter(Path.Combine(dirpath, basefilename + ".json"), false, Encoding.UTF8))
      {
        sw.WriteLine("    \"archives\" : [");
        sw.WriteLine("      {");
        sw.WriteLine("        \"title\" : \"自己解凍インストーラー\",");
        sw.WriteLine("        \"archive\" : \"" + exeFileName + "\",");
        sw.WriteLine("        \"md5\" : \"" + exeMD5Hash + "\",");
        sw.WriteLine("        \"sha1\" : \"" + exeSHA1Hash + "\"");
        sw.WriteLine("      },");
        sw.WriteLine("      {");
        sw.WriteLine("        \"title\" : \"ZIPファイル\",");
        sw.WriteLine("        \"archive\" : \"" + zipFileName + "\",");
        sw.WriteLine("        \"md5\" : \"" + zipMD5Hash + "\",");
        sw.WriteLine("        \"sha1\" : \"" + zipSHA1Hash + "\"");
        sw.WriteLine("      }");
        sw.WriteLine("    ]");
        sw.WriteLine("");
        sw.WriteLine("    \"archives\" : [");
        sw.WriteLine("      {");
        sw.WriteLine("        \"title\" : \"Self-executable installer\",");
        sw.WriteLine("        \"archive\" : \"" + exeFileName + "\",");
        sw.WriteLine("        \"md5\" : \"" + exeMD5Hash + "\",");
        sw.WriteLine("        \"sha1\" : \"" + exeSHA1Hash + "\"");
        sw.WriteLine("      },");
        sw.WriteLine("      {");
        sw.WriteLine("        \"title\" : \"ZIP file\",");
        sw.WriteLine("        \"archive\" : \"" + zipFileName + "\",");
        sw.WriteLine("        \"md5\" : \"" + zipMD5Hash + "\",");
        sw.WriteLine("        \"sha1\" : \"" + zipSHA1Hash + "\"");
        sw.WriteLine("      }");
        sw.WriteLine("    ]");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private static string getMd5Hash(string FilePath)
    {
      byte[] bs;
      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        //MD5CryptoServiceProviderオブジェクト
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        bs = md5.ComputeHash(fs);
        md5.Clear();
      }

      //byte型配列を16進数の文字列に変換
      StringBuilder result = new StringBuilder();
      foreach (byte b in bs)
      {
        result.Append(b.ToString("x2"));
      }
      return (result.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private static string getSha1Hash(string FilePath)
    {
      byte[] bs;

      using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
      {
        //SHA1CryptoServiceProviderオブジェクト
        SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        bs = sha1.ComputeHash(fs);
        sha1.Clear();
      }

      StringBuilder result = new StringBuilder();
      foreach (byte b in bs)
      {
        result.Append(b.ToString("x2"));
      }

      return (result.ToString());

    }
    

  }

  /// <summary>
  /// Represents a wildcard running on the
  /// <see cref="System.Text.RegularExpressions"/> engine.
  /// http://www.codeproject.com/Articles/11556/Converting-Wildcards-to-Regexes
  /// </summary>
  public class Wildcard : Regex
  {
    /// <summary>
    /// Initializes a wildcard with the given search pattern.
    /// </summary>
    /// <param name="pattern">The wildcard pattern to match.</param>
    public Wildcard(string pattern)
     : base(WildcardToRegex(pattern))
    {
    }

    /// <summary>
    /// Initializes a wildcard with the given search pattern and options.
    /// </summary>
    /// <param name="pattern">The wildcard pattern to match.</param>
    /// <param name="options">A combination of one or more
    /// <see cref="System.Text.RegexOptions"/>.</param>
    public Wildcard(string pattern, RegexOptions options)
     : base(WildcardToRegex(pattern), options)
    {
    }

    /// <summary>
    /// Converts a wildcard to a regex.
    /// </summary>
    /// <param name="pattern">The wildcard pattern to convert.</param>
    /// <returns>A regex equivalent of the given wildcard.</returns>
    public static string WildcardToRegex(string pattern)
    {
      return "^" + Regex.Escape(pattern).
       Replace("\\*", ".*").
       Replace("\\?", ".") + "$";
    }
  }



}
