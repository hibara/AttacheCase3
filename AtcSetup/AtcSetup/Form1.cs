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
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using AtcSetup.Properties;

namespace AtcSetup
{
	public partial class Form1 : Form
	{
		// Shield icon
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		private const int BCM_FIRST = 0x1600;
		private const int BCM_SETSHIELD = BCM_FIRST + 0x000C;

    // Refresh desktop window.
    [DllImport("Shell32.dll")]
    private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
         
    public Dictionary<string, string> CommandData = new Dictionary<string, string>();
	
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			//-----------------------------------
			// Get the version of this aooication from assembly infos.
			Assembly asm = Assembly.GetExecutingAssembly();
			Version ver = asm.GetName().Version;
			toolStripStatusLabel1.Text = "ver. " + ver;

			//-----------------------------------
			// on the center of the screen.
			this.Left = Screen.GetBounds(this).Width / 2 - this.Width / 2;
			this.Top = Screen.GetBounds(this).Height / 2 - this.Height / 2;
			
			//-----------------------------------
			// Shield icon
			// 盾アイコン
			//-----------------------------------

			//Windows Vista later?
			if (Environment.OSVersion.Platform != PlatformID.Win32NT ||	Environment.OSVersion.Version.Major < 6)
			{
			}
			else
			{
				//FlatStyle=System
				buttonAssociation.FlatStyle = FlatStyle.System;
				buttonUnAssociation.FlatStyle = FlatStyle.System;
				//Shield icon
				SendMessage(new HandleRef(buttonAssociation, buttonAssociation.Handle), BCM_SETSHIELD, IntPtr.Zero, new IntPtr(1));
				SendMessage(new HandleRef(buttonUnAssociation, buttonUnAssociation.Handle), BCM_SETSHIELD, IntPtr.Zero, new IntPtr(1));

			}

			//-----------------------------------
			//コマンドライン引数を連想配列へ
			string[] cmds = Environment.GetCommandLineArgs();
			foreach (string cmd in cmds)
			{
				string[] SplitData = cmd.Split(new Char[] {'='});
				if (SplitData.Length > 1 )
				{
					CommandData.Add(SplitData[0], SplitData[1]);
				}
			}
			
			// Association, or UnAssociation ?
			if (CommandData.ContainsKey("-t") == true)
			{
				if (CommandData["-t"] == "0")
				{
					if (AssociateAtcFileToAttacheCase() == true)
					{
						labelInfo.Text = Resources.AssociationComplete;
						Application.Exit();
					}
					else
					{
						labelInfo.Text = Resources.UnAssociationFailed;
					}
				}
				else if (CommandData["-t"] == "1")
				{
					if (UnAssociateAtcFileToAttacheCase() == true)
					{
						labelInfo.Text = Resources.UnAssociationComplete;
						Application.Exit();
					}
					else
					{
						labelInfo.Text = Resources.UnAssociationFailed;
					}
				}
				else
				{	// Other
					Application.Exit();
				}
			}
			else
			{
				// View this form.
			}

		}

		//======================================================================
		/// <summary>
		/// Settings of Associating file to AttacheCase ( Executable file )
		/// </summary>
		/// <returns>[Boolean] true: Succeeded, false: failed</returns>
		//======================================================================
		private bool AssociateAtcFileToAttacheCase()
		{
			this.Text = Resources.LabelAssociation;
			progressBar1.Style = ProgressBarStyle.Marquee;
			// "It is setting to associate the '.atc' file to the AttacheCase application..."
			labelInfo.Text = Resources.Associating;

			//-----------------------------------
			string AttacheCaseFilePath = "";
			if (CommandData.ContainsKey("-p") == true)
			{
				AttacheCaseFilePath = CommandData["-p"];
			}
			else
			{
				AttacheCaseFilePath = getAttacheCaseExeFilePath();
			}

			// Because File.Exists is Case-Sensitive.
			if (AttacheCaseFilePath == "")
			{
				// 注意
				// Alert
				//
				// "AttacheCase.exe" is not found!
				// アタッシェケース本体が見つかりません！
				DialogResult ret = MessageBox.Show(Resources.DialogMessageAttacheCaseNotFound + Environment.NewLine + AttacheCaseFilePath.ToLower(),
				Resources.DialogMessageAttacheCaseNotFound, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				progressBar1.Style = ProgressBarStyle.Continuous;
				labelInfo.Text = "-";
				progressBar1.Value = 100;
				return(false);

			}
			
			//-----------------------------------
			int IconIndex = 1;
			string MyIconFilePath = "";

			if (CommandData.ContainsKey("-icn"))
			{
				if (int.TryParse(CommandData["-icn"], out IconIndex) == false)
				{
					MyIconFilePath = CommandData["-icn"];
					if (File.Exists(MyIconFilePath) == false)
					{
						MyIconFilePath = "";
						IconIndex = 1;
					}
				}
			}
			else
			{
				IconIndex = 1;
			}

			//-----------------------------------
			// HKEY_CLASSES_ROOT\.atc
			using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(@".atc"))
			{
				regkey.SetValue("", "AttacheCase.DataFile");
			}
			
			//-----------------------------------
			// HKEY_CLASSES_ROOT\AttacheCase.DataFile
			using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(@"AttacheCase.DataFile\DefaultIcon"))
			{
				if (MyIconFilePath == "")
				{
					regkey.SetValue("", "\"" + AttacheCaseFilePath + "\"," + IconIndex);
				}
				else
				{
					regkey.SetValue("", MyIconFilePath);	// My Icon
				}
			}

			using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(@"AttacheCase.DataFile\Shell\decode"))
			{
				regkey.SetValue("", "アタッシェケースファイルを復号する");
			}

			using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(@"AttacheCase.DataFile\Shell\decode\command"))
			{
				regkey.SetValue("", "\"" + AttacheCaseFilePath + "\",\"%1\"");
			}

			using (RegistryKey regkey = Registry.ClassesRoot.CreateSubKey(@"AttacheCase.DataFile\Shell\open\command"))
			{
				regkey.SetValue("", "\"" + AttacheCaseFilePath + "\",\"%1\"");
			}

			progressBar1.Style = ProgressBarStyle.Continuous;
			progressBar1.Value = 100;
			labelInfo.Text = Resources.AssociationComplete;

      // Refresh desktop window icons.
      SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);

      return (true);
			
		}

		//======================================================================
		/// <summary>
		/// UnAssociating file from AttacheCase
		/// </summary>
		/// <returns></returns>
		//======================================================================
		private bool UnAssociateAtcFileToAttacheCase()
		{
			progressBar1.Style = ProgressBarStyle.Marquee;
			// "It is setting to Unassociate the '.atc' file in the AttacheCase application..."
			labelInfo.Text = Resources.UnAssociating;

      RegistryKey regAtc = Registry.ClassesRoot.OpenSubKey(".atc");
      RegistryKey regAttacheCaseDataFile = Registry.ClassesRoot.OpenSubKey("AttacheCase.DataFile");
                                
      // Delete registry keys
      if (regAtc != null)
      {
        Registry.ClassesRoot.DeleteSubKeyTree(".atc");
      }
      if (regAttacheCaseDataFile != null)
      {
        Registry.ClassesRoot.DeleteSubKeyTree("AttacheCase.DataFile");
      }

      progressBar1.Style = ProgressBarStyle.Continuous;
			progressBar1.Value = 100;
			labelInfo.Text = Resources.UnAssociationComplete;

      // Refresh desktop window icons.
      SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);

      return (true);
			
		}

		/// <summary>
		/// アタッシェケース#3の方のレジストリデータから本体のフルパスを取得する
		/// Get full path of executable file from the registry of different application AttacheCase#3.
		/// </summary>
		/// <returns></returns>
		private string getAttacheCaseExeFilePath()
		{
			string AppFilePath = string.Empty;
			using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"Software\Hibara\AttacheCase3\AppInfo"))
			{
				if (regkey.GetValue("AppPath") != null)
				{
					AppFilePath = (string)regkey.GetValue("AppPath");
				}
			}
			return (AppFilePath);
		}

		private void buttonAssociation_Click(object sender, EventArgs e)
		{
			AssociateAtcFileToAttacheCase();
		}

		private void buttonUnAssociation_Click(object sender, EventArgs e)
		{
			UnAssociateAtcFileToAttacheCase();
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}




	}
}
