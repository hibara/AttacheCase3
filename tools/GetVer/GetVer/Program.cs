using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      if (File.Exists(AppFilePath) == false)
      {
        return;
      }

      FileVersionInfo vi = FileVersionInfo.GetVersionInfo(AppFilePath);

      //バージョン番号
      //Console.WriteLine("FileVersion:{0}", vi.FileVersion);
      //メジャー、マイナー、ビルド、プライベートパート番号
      Console.WriteLine("{0}{1}{2}{3}", vi.ProductMajorPart, vi.ProductMinorPart, vi.ProductPrivatePart, vi.ProductBuildPart);

#if DEBUG
      System.Console.ReadLine();
#endif

    }
  }
}
