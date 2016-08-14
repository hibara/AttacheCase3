using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ExeToHex
{
  class Program
	{
		private const int BUFFER_SIZE = 16;
    private static int ExeOutSize = 0;

		static int Main(string[] args)
		{
			if (args.Length < 1)
			{
				return (1);
			}

			// Executable file that is written to binary data
			string ExecutableFilePath = args[0];
			if (File.Exists(ExecutableFilePath) == false)
			{
				MessageBox.Show("ExecutableFilePath is not found!");
				return (1);
			}
			else if (Path.GetExtension(ExecutableFilePath).ToLower() != (".exe").ToLower())
			{
				MessageBox.Show("Executable file extension is invalid!");
				return (1);
			}

			// CS file that the binary data is written to
			string CShrpSourceFilePath = args[1];
			if (File.Exists(CShrpSourceFilePath) == false)
			{
				MessageBox.Show("CSharp source is not found!");
				return (1);
			}
			else if (Path.GetExtension(CShrpSourceFilePath).ToLower() != (".cs").ToLower())
			{
				MessageBox.Show("CSharp source file is invalid!");
				return (1);
			}
			
			int len;
			byte[] byteArray = new byte[16];

			//----------------------------------------------------------------------
			// Read the binary data of "Exeout.exe"
			//----------------------------------------------------------------------

			using (MemoryStream ms = new MemoryStream())
			{
				using (FileStream fs = new FileStream(ExecutableFilePath, FileMode.Open, FileAccess.Read))
				{
          int TotalSize = 0;
          ExeOutSize = (int)fs.Length;
					
					//----------------------------------------------------------------------
					//【デバッグ】
					//System.Windows.Forms.MessageBox.Show("ExeOutSize: " + ExeOutSize.ToString());
					//----------------------------------------------------------------------

          using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
					{
						sw.WriteLine("\t\tpublic static byte[] rawData = {");

            while ((len = fs.Read(byteArray, 0, BUFFER_SIZE)) > 0)
						{
              List<String> StringList = new List<string>();
							for (int i = 0; i < len; i++)
							{
								StringList.Add(string.Format("0x{0:X2}", byteArray[i]));
                TotalSize++;
              }

							string[] OneLineArray = StringList.ToArray();

              string OneLine = "\t\t\t" + string.Join(", ", OneLineArray);

							if (fs.Position == fs.Length)
							{
								sw.WriteLine(OneLine);	// Last line of array.
							}
							else
							{
								sw.WriteLine(OneLine + ",");
							}

						}//end while();

						sw.WriteLine("\t\t};");

					}// end using (StreamWriter sw = new StreamWriter(ms, System.Text.Encoding.UTF8));

					//----------------------------------------------------------------------
					//
					//System.Windows.Forms.MessageBox.Show("TotalSize: " + TotalSize.ToString());
					//----------------------------------------------------------------------

				}// end using (FileStream fs = new FileStream(ExecutableFilePath, FileMode.Open, FileAccess.Read));


		
				string ExeHexString = Encoding.UTF8.GetString(ms.ToArray());

				//----------------------------------------------------------------------
				// Output src file text
				//----------------------------------------------------------------------

				string[] lines = System.IO.File.ReadAllLines(CShrpSourceFilePath, Encoding.UTF8);

				List<string> SrcTextList = new List<string>();

				bool fDelete = false;
				for (int i = 0; i < lines.Count(); i++)
				{
          if (lines[i].IndexOf("public int ExeOutFileSize") > 0)
          {
            SrcTextList.Add(string.Format("\t\tpublic int ExeOutFileSize = {0};\r\n", ExeOutSize));
          }
          else if (lines[i].IndexOf("#region ATC executable file bytes data") > 0)
					{
						fDelete = true;
						SrcTextList.Add(lines[i] + "\r\n");
						SrcTextList.Add(ExeHexString);
					}
					else if (lines[i].IndexOf("#endregion") > 0)
					{
						fDelete = false;
						SrcTextList.Add(lines[i] + "\r\n");
					}
					else
					{
						if (fDelete == false)
						{
							SrcTextList.Add(lines[i] + "\r\n");
						}
					}

				}

				string src = string.Join("", SrcTextList.ToArray());

				/*
				// データ量が多いと、Regexでtime out エラーが発生するようだ。 
				Regex r = new Regex("\t{1,}#region ATC executable file bytes data(\r|\n|\r\n|.)*#endregion", RegexOptions.Multiline);
				MatchCollection matches = r.Matches(src);
				if (matches.Count != 1)
				{
					MessageBox.Show("matches.Count: " + matches.Count.ToString());
				}
				// Replace
				src = r.Replace(src, "\t\t#region ATC executable file bytes data\r\n" + ExeHexString + "\r\n\t\t#endregion");
				*/

				File.WriteAllText(CShrpSourceFilePath, src, Encoding.UTF8);

        //----------------------------------------------------------------------
        string ExeOutSizeString = String.Format("{0:#,0} Bytes", ExeOutSize);
        MessageBox.Show("以下のファイルに、" + ExeOutSizeString + " を書き込みました。\n" + CShrpSourceFilePath);
        //----------------------------------------------------------------------
        
      }// end using (MemoryStream ms = new MemoryStream());

      return (0);

		}// end static int Main(string[] args);

	}

}
