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
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;


using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using Microsoft.Win32;


namespace Exeout
{

  static class Program
	{
    //[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    //internal static extern IntPtr LoadLibrary(string lpFileName);

    //[DllImport("kernel32.dll", SetLastError = true)]
    //static extern bool SetSearchPathMode(uint Flags);

    //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //public static extern bool SetDllDirectory(string lpPathName);

    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
		static void Main()
		{


      //SetDllDirectory("");
      //MessageBox.Show("SetDllDirectory!");

      //IntPtr handle = LoadLibrary("");

      //SetSearchPathMode(0x00000001 | 0x00008000);
      //IntPtr handle = LoadLibrary("schannel.dll");



      CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
			//Console.WriteLine(ci.Name);  // ja-JP
			if (ci.Name == "ja-JP")
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP", false);
			}

      Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());

		}
	}
}
