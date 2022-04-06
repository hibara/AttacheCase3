//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016-2020  Mitsuhiro Hibara
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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AtcSetup
{
	static class Program
	{
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		static extern bool SetDllDirectory(string lpPathName);
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		static extern bool SetDefaultDllDirectories(uint directoryFlags);
		// LOAD_LIBRARY_SEARCH_APPLICATION_DIR : 0x00000200
		// LOAD_LIBRARY_SEARCH_DEFAULT_DIRS    : 0x00001000
		// LOAD_LIBRARY_SEARCH_SYSTEM32        : 0x00000800
		// LOAD_LIBRARY_SEARCH_USER_DIRS       : 0x00000400
		private const uint DllSearchFlags = 0x00000800;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Countermeasure that "Font '?' cannot be found" error
			// ref. https://chowdera.com/2022/03/202203241328277504.html
			var font = System.Drawing.SystemFonts.DefaultFont; // Load first 

			// DLLプリロード攻撃対策
			// Prevent DLL preloading attacks
			try
			{
				SetDllDirectory("");
				SetDefaultDllDirectories(DllSearchFlags);
			}
			catch
			{
				// Pre-Windows 7, KB2533623 
				SetDllDirectory("");
			}

			CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
			//Console.WriteLine(ci.Name);  // ja-JP
			if (ci.Name == "ja-JP")
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP", true);
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
