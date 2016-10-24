//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016  Mitsuhiro Hibara
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
using System.Threading;
using System.Globalization;

namespace AttacheCase
{

  static class Program
  {
    /// <summary>
    /// This is the main entry point for this application.
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      //-----------------------------------
      // Load Options
      AppSettings.Instance.ReadOptions();

      //-----------------------------------
      // Not Allow multiple in&stance of AttcheCase
      // Create Mutex
      bool fNewCreate;
      Mutex mutex = new Mutex(true, "AttacheCase", out fNewCreate);
      if (AppSettings.Instance.fNoMultipleInstance == true)
      {
        if (fNewCreate == false)
        {
          return;
        }
      }

      //-----------------------------------
      // Check culture

      if (AppSettings.Instance.Language == "")
      {
        if (Application.CurrentCulture.TwoLetterISOLanguageName == "ja")
        {
          AppSettings.Instance.Language = "ja";
        }
        else
        {
          AppSettings.Instance.Language = "en";
        }
      }

      if (AppSettings.Instance.Language == "ja")
      {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
        //AppSettings.Instance.Language = "ja";
      }
      else
      {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("", false);
        //AppSettings.Instance.Language = "en";
      }

      //-----------------------------------
      // Application executable file path & version
      AppSettings.Instance.ApplicationPath = Application.ExecutablePath;

      System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
      System.Version ver = asm.GetName().Version;
      AppSettings.Instance.AppVersion = int.Parse(ver.ToString().Replace(".", ""));

      //-----------------------------------       
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());

      // Release Mutex
      mutex.ReleaseMutex();

    }

  }

}

