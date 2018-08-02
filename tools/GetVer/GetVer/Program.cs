using System;
using System.IO;
using System.Diagnostics;

namespace GetVer
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        return;
      }
      string AppFilePath = Path.GetFullPath(args[0]);

      bool fString = false;
      foreach(string arg in args)
      {
        if (arg == "-s")
        {
          fString = true;
        }
        else
        {
          if (File.Exists(arg) == true)
          {
            AppFilePath = arg;
          }
        }
      }
      
      if (File.Exists(AppFilePath) == false)
      {
        return;
      }

      FileVersionInfo vi = FileVersionInfo.GetVersionInfo(AppFilePath);

      //バージョン番号
      //Console.WriteLine("FileVersion:{0}", vi.FileVersion);
      //メジャー、マイナー、ビルド、プライベートパート番号
      if (fString == true)
      {
        Console.WriteLine("{0}.{1}.{2}.{3}", vi.ProductMajorPart, vi.ProductMinorPart, vi.ProductBuildPart, vi.ProductPrivatePart);
      }
      else
      {
        Console.WriteLine("{0}{1}{2}{3}", vi.ProductMajorPart, vi.ProductMinorPart, vi.ProductBuildPart, vi.ProductPrivatePart);
      }

#if DEBUG
      System.Console.ReadLine();
#endif

    }
  }
}
