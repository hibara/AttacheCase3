//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016-2021  Mitsuhiro Hibara
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
using System.Runtime.InteropServices;

namespace AttacheCase
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
    /// This is the main entry point for this application.
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      // DLLプリロード攻撃対策
      // Prevent DLL preloading attacks
      SetDllDirectory(null);
      SetDefaultDllDirectories(DllSearchFlags);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      //-----------------------------------
      // Load Options
      AppSettings.Instance.ReadOptions();

      //-----------------------------------
      // Not Allow multiple in&stance of AttcheCase
      // Create Mutex
      Mutex mutex = new Mutex(false, "AttacheCase");

      bool mutexHandle = false;
      try
      {
        try
        {
          mutexHandle = mutex.WaitOne(0, false);
        }
        catch (AbandonedMutexException)
        {
          mutexHandle = true;
        }

        if (mutexHandle == false)
        {
          if (AppSettings.Instance.fNoMultipleInstance == true)
          {
            return;
          }
        }

        //-----------------------------------
        // Check culture
        switch (AppSettings.Instance.Language)
        {
          case "ja":
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            break;
          case "en":
            Thread.CurrentThread.CurrentCulture = new CultureInfo("", true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("", true);
            break;
          case "":
          default:
            if (CultureInfo.CurrentCulture.Name == "ja-JP")
            {
              Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            }
            else
            {
              Thread.CurrentThread.CurrentCulture = new CultureInfo("", true);
              Thread.CurrentThread.CurrentUICulture = new CultureInfo("", true);
            }
            break;
        }

        //-----------------------------------
        // Application executable file path & version
        AppSettings.Instance.ApplicationPath = Application.ExecutablePath;

        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        System.Version ver = asm.GetName().Version;
        AppSettings.Instance.AppVersion = int.Parse(ver.ToString().Replace(".", ""));

        //-----------------------------------       
        Application.Run(new Form1());

      }
      finally
      {
        if (mutexHandle)
        {
          // Release Mutex
          mutex.ReleaseMutex();
        }
        mutex.Close();
      }
    }

  }

}

