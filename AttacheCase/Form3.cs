//---------------------------------------------------------------------- 
// "アタッシェケース#3 ( AttachéCase#3 )" -- File encryption software.
// Copyright (C) 2016-2022  Mitsuhiro Hibara
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
using System.Runtime.InteropServices;
using AttacheCase.Properties;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security;

namespace AttacheCase
{
  public partial class Form3 : Form
	{
    [SuppressUnmanagedCodeSecurityAttribute]
    internal static class UnsafeNativeMethods
    {
      // Get shield icon.
      [DllImport("user32")]
      internal static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
      //[DllImport("kernel32", BestFitMapping = false, ThrowOnUnmappableChar = true)]
      //internal static extern uint WritePrivateProfileString(string section, string key, string val, string filePath);
    }

		private string OneLineHelpURL = "https://hibara.org/software/attachecase/help/";
		private string CommandLineReferenceURL = "https://hibara.org/software/attachecase/help/howto/#command-line-option";

    private const int BCM_FIRST = 0x1600;
		private const int BCM_SETSHIELD = BCM_FIRST + 0x000C;

    private Panel[] panelObjects = new Panel[17];

		private bool fAssociationSettings = false;
		private bool fLoading = false;

    private bool fTemporarySettings = false;

    // File type
    private const int FILE_TYPE_ERROR        = -1;
    private const int FILE_TYPE_NONE         = 0;
    private const int FILE_TYPE_ATC          = 1;
    private const int FILE_TYPE_ATC_EXE      = 2;
    private const int FILE_TYPE_PASSWORD_ZIP = 3;

    // Process Type
    private const int PROCESS_TYPE_ERROR        = -1;
    private const int PROCESS_TYPE_NONE         = 0;
    private const int PROCESS_TYPE_ATC          = 1;
    private const int PROCESS_TYPE_ATC_EXE      = 2;
    private const int PROCESS_TYPE_PASSWORD_ZIP = 3;
    private const int PROCESS_TYPE_DECRYPTION   = 4;

    /// <summary>
    /// Form3 Constructor
    /// </summarey>
    public Form3()
		{
			InitializeComponent();

			panelObjects[0] = this.panelGeneralOption;
			panelObjects[1] = this.panelPasswordsOption;
			panelObjects[2] = this.panelWindowOption;
			panelObjects[3] = this.panelSaveOption;
			panelObjects[4] = this.panelSaveEncryptOption;
			panelObjects[5] = this.panelSaveDecryptOption;
      panelObjects[6] = this.panelSaveZipOption;
      panelObjects[7] = this.panelDeleteOption;
			panelObjects[8] = this.panelCompressOption;
      panelObjects[9] = this.panelSystemOption;
      panelObjects[10] = this.panelSettingImportExportOption;
      panelObjects[11] = this.panelPasswordFileOption;
			panelObjects[12] = this.panelCamouflageExtOption;
			panelObjects[13] = this.panelPasswordInputLimitOption;
			panelObjects[14] = this.panelSalvageDataOption;
			panelObjects[15] = this.panelLicenseOption;
			panelObjects[16] = this.panelDevelopmentOption;

			foreach (Panel obj in panelObjects)
			{
				obj.Parent = splitContainer1.Panel2;
				obj.Visible = false;
			}

			tabControl1.Visible = false;

			pictureBoxCheckmark00.Parent = pictureBoxIcon00;
			pictureBoxCheckmark00.BackColor = Color.Transparent;
			pictureBoxCheckmark00.Left = 0;
			pictureBoxCheckmark00.Top = 0;

			pictureBoxCheckmark01.Parent = pictureBoxIcon01;
			pictureBoxCheckmark01.BackColor = Color.Transparent;
			pictureBoxCheckmark01.Left = 0;
			pictureBoxCheckmark01.Top = 0;
			
			pictureBoxCheckmark02.Parent = pictureBoxIcon02;
			pictureBoxCheckmark02.BackColor = Color.Transparent;
			pictureBoxCheckmark02.Left = 0;
			pictureBoxCheckmark02.Top = 0;

			pictureBoxCheckmark03.Parent = pictureBoxIcon03;
			pictureBoxCheckmark03.BackColor = Color.Transparent;
			pictureBoxCheckmark03.Left = 0;
			pictureBoxCheckmark03.Top = 0;
			
		}

		//---------------------------------------------------------------------- 
		/// <summary>
		/// 現在選択中のノードの絶対インデックスを取得する
		/// Get selected TreeView node index absolutely. 
		/// </summary>
		/// <returns></returns>
		//---------------------------------------------------------------------- 
		private int getTreeViewNodeIndex()
		{
			int number = 0;
			if (getChildNodes(treeView1.Nodes, ref number) == true)
			{
				return (number);
			}
			else
			{
				return (0);
			}
		}
		// Recursive function
		private bool getChildNodes(TreeNodeCollection nodes, ref int num)
		{
			foreach (TreeNode node in nodes)
			{
				num++;

				if (node == treeView1.SelectedNode)
				{
					return (true);
				}
				else
				{
					if (node.Nodes.Count > 0)
					{
						if (getChildNodes(node.Nodes, ref num) == true)
						{
							return (true);
						}
					}
				}
			}
			return (false);

		}

		//---------------------------------------------------------------------- 
		/// <summary>
		/// 記録していたインデックスからノードを選択状態にする
		/// Be selected the node from stored index.
		/// </summary>
		/// <returns></returns>
		//---------------------------------------------------------------------- 
		private bool setTreeViewNodeIndex(TreeNodeCollection nodes, int SelectedIndex, ref int num)
		{
			foreach (TreeNode node in nodes)
			{
				num++;
				if (num == SelectedIndex)
				{
					treeView1.Focus();
					treeView1.SelectedNode = node;
					return (true);
				}
				else
				{
					if (node.Nodes.Count > 0)
					{
						if (setTreeViewNodeIndex(node.Nodes, SelectedIndex, ref num) == true)
						{
							return (true);
						}
					}
				}
			}
			return (false);

		}


		//======================================================================
		/// <summary>
		/// This Form3 Load event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//======================================================================
		private void Form3_Load(object sender, EventArgs e)
		{
			fLoading = true;

#if (MS_STORE)
      if (treeView1.Nodes.ContainsKey("nodeSystem"))
      {
        treeView1.Nodes.Remove(treeView1.Nodes["nodeSystem"]);
      }
#endif
      //-----------------------------------
      // 読み込み先の表示

      // Registry
      using (Bitmap bitmap = new Bitmap(pictureBoxRegistryIcon.Image))
      {
        bitmap.SetResolution(24, 24);
        this.Icon = Icon.FromHandle(bitmap.GetHicon());
        this.Text = Resources.DialogTitleSettings + " - " + Resources.DialogTitleRegistry;
      }


      // INI file
      if (File.Exists(AppSettings.Instance.IniFilePath) == true)
			{
				// Bitmap to Icon
				using (Bitmap bitmap = new Bitmap(pictureBoxIniFileIcon.Image))
				{
					bitmap.SetResolution(24, 24);
					this.Icon = Icon.FromHandle(bitmap.GetHicon());
					this.Text = Resources.DialogTitleSettings + " - " + AppSettings.Instance.IniFilePath;
				}
        fTemporarySettings = true;
        buttonReplaceCurrentByTemporary.Enabled = true;

      }
      
      // Command line 
      if (AppSettings.Instance.CommandLineArgsNum > 0)
			{
				using (Bitmap bitmap = new Bitmap(pictureBoxCommandLineIcon.Image))
				{
					bitmap.SetResolution(24, 24);
					this.Icon = Icon.FromHandle(bitmap.GetHicon());
					this.Text = Resources.DialogTitleSettings + " - " + Resources.DialogTitleCommandLine;
				}
        fTemporarySettings = true;
        buttonReplaceCurrentByTemporary.Enabled = true;

      }

			//-----------------------------------
			// 盾アイコンの取得とボタン上での表示
			//-----------------------------------
			// Get shield icon

			// 1st parameter is window handle.
			HandleRef hwnd = new HandleRef(buttonAssociateAtcFiles, buttonAssociateAtcFiles.Handle);
			// 2nd parameter is flaf of setting shield icon
			uint BCM_SETSHIELD = 0x0000160C;
      // Call SendMessage function
      UnsafeNativeMethods.SendMessage(hwnd, BCM_SETSHIELD, new IntPtr(0), new IntPtr(1));

			hwnd = new HandleRef(buttonAssociateAtcFiles, buttonUnAssociateAtcFiles.Handle);
      UnsafeNativeMethods.SendMessage(hwnd, BCM_SETSHIELD, new IntPtr(0), new IntPtr(1));

			//-----------------------------------
			// All items of TreeView is expanded 
			//ツリービューのノードをすべて展開
			treeView1.ExpandAll();

			//======================================================================
			// Read settings from AppSettings class 
			// 各設定を読み込む
			//======================================================================

			//-----------------------------------
			// General
			//-----------------------------------
      #region
			checkBoxEndToExit.Checked = AppSettings.Instance.fEndToExit;

			checkBoxOpenFile.Checked = AppSettings.Instance.fOpenFile;

      checkBoxShowDialogWhenExeFile.Checked = AppSettings.Instance.fShowDialogWhenExeFile;

      if( AppSettings.Instance.ShowDialogWhenMultipleFilesNum > 0)
      {
        checkBoxShowDialogWhenMultipleFiles.Checked = true;
        numericUpDownLaunchFiles.Value = AppSettings.Instance.ShowDialogWhenMultipleFilesNum;
      }

      checkBoxAskEncDecode.Checked = AppSettings.Instance.fAskEncDecode;

			checkBoxNoHidePassword.Checked = AppSettings.Instance.fNotMaskPassword;

			// Localization
			switch (AppSettings.Instance.Language)
			{
				case "ja":
					comboBoxLanguage.Items.Add("既定値");
					comboBoxLanguage.Items.Add("日本語");
					comboBoxLanguage.Items.Add("英語");
					comboBoxLanguage.SelectedIndex = 1;
					break;

				case "en":
					comboBoxLanguage.Items.Add("Default");
					comboBoxLanguage.Items.Add("Japanese");
					comboBoxLanguage.Items.Add("English");
					comboBoxLanguage.SelectedIndex = 2;
					break;

				case "":	// Default（既定値）
				default:
					if (Thread.CurrentThread.CurrentCulture.Name == "ja-JP")
					{
						comboBoxLanguage.Items.Add("既定値");
						comboBoxLanguage.Items.Add("日本語");
						comboBoxLanguage.Items.Add("英語");
					}
					else
					{
						comboBoxLanguage.Items.Add("Default");
						comboBoxLanguage.Items.Add("Japanese");
						comboBoxLanguage.Items.Add("English");
					}
					comboBoxLanguage.SelectedIndex = 0;
					break;
			}

			// Active tab
			TreeNode treeNode = treeView1.Nodes[AppSettings.Instance.TabSelectedIndex];
			treeView1.SelectedNode = treeNode;

      #endregion

			//-----------------------------------
			// Password
			//-----------------------------------
      #region
			checkBoxMyEncodePasswordKeep.Checked = AppSettings.Instance.fMyEncryptPasswordKeep;
			if (AppSettings.Instance.MyEncryptPasswordString != null)
			{
				textBoxMyEncodePassword.Text = new string('*', 16);
			}
			checkBoxMyDecodePasswordKeep.Checked = AppSettings.Instance.fMyDecryptPasswordKeep;
			if (AppSettings.Instance.MyDecryptPasswordString != null)
			{
				textBoxMyDecodePassword.Text = new string('*', 16);
			}
			checkBoxDobyMemorizedPassword.Checked = AppSettings.Instance.fMemPasswordExe;

      checkBoxEnablePassStrengthMeter.Checked = AppSettings.Instance.fPasswordStrengthMeter;

      #endregion

			//-----------------------------------
			// Window
			//-----------------------------------
      #region
			checkBoxMainWindowMinimize.Checked = AppSettings.Instance.fMainWindowMinimize;
			checkBoxTaskBarHide.Checked = AppSettings.Instance.fTaskBarHide;
			checkBoxTaskTrayIcon.Checked = AppSettings.Instance.fTaskTrayIcon;
			checkBoxWindowForeground.Checked = AppSettings.Instance.fWindowForeground;
			checkBoxNoMultipleInstance.Checked = AppSettings.Instance.fNoMultipleInstance;
			checkBoxTurnOnAllIMEs.Checked = AppSettings.Instance.fTurnOnIMEsTextBoxForPasswordEntry;

			#endregion

			//-----------------------------------
			// Save
			//-----------------------------------
			#region

			// Encryption will be the same file type always.
			if (AppSettings.Instance.EncryptionSameFileTypeAlways == FILE_TYPE_ATC)
      {
        radioButtonEncryptionFileTypeATC.Checked = true;
      }
      else if (AppSettings.Instance.EncryptionSameFileTypeAlways == FILE_TYPE_ATC_EXE)
      {
        radioButtonEncryptionFileTypeEXE.Checked = true;
      }
      else if (AppSettings.Instance.EncryptionSameFileTypeAlways == FILE_TYPE_PASSWORD_ZIP)
      {
        radioButtonEncryptionFileTypeZIP.Checked = true;
      }
      else
      {
        radioButtonNotSpecified.Checked = true;
      }

      // Save same encryption type that was used to before.
      if (AppSettings.Instance.fEncryptionSameFileTypeBefore == true)
      {
        checkBoxEncryptionSameFileTypeBefore.Checked = true;
      }
      else
      {
        checkBoxEncryptionSameFileTypeBefore.Checked = false;
      }

      #endregion

      //-----------------------------------
      // Save Encrypt
      //-----------------------------------
      #region
      checkBoxSaveToSameFldr.Checked = AppSettings.Instance.fSaveToSameFldr;
			if (checkBoxSaveToSameFldr.Checked == true)
			{
				textBoxSaveEncryptionToSameFolder.Enabled = true;
				textBoxSaveEncryptionToSameFolder.BackColor = SystemColors.Window;
				buttonSaveEncryptedFileToFolder.Enabled = true;
			}
			else
			{
				textBoxSaveEncryptionToSameFolder.Enabled = false;
				textBoxSaveEncryptionToSameFolder.BackColor = SystemColors.ButtonFace;
				buttonSaveEncryptedFileToFolder.Enabled = false;
			}
			textBoxSaveEncryptionToSameFolder.Text = AppSettings.Instance.SaveToSameFldrPath;
			checkBoxConfirmSameFileName.Checked = AppSettings.Instance.fEncryptConfirmOverwrite;
			
			radioButtonNormal.Checked = AppSettings.Instance.fNormal;
			radioButtonAllFilePack.Checked = AppSettings.Instance.fAllFilePack;
			radioButtonFilesOneByOne.Checked = AppSettings.Instance.fFilesOneByOne;
			
			checkBoxKeepTimeStamp.Checked = AppSettings.Instance.fKeepTimeStamp;
			checkBoxExtInAtcFileName.Checked = AppSettings.Instance.fExtInAtcFileName;
			checkBoxAutoName.Checked = AppSettings.Instance.fAutoName;
			textBoxAutoNameFormatText.Text = AppSettings.Instance.AutoNameFormatText;
			ToolStripMenuItemAlphabet.Checked = AppSettings.Instance.fAutoNameAlphabets;
			ToolStripMenuItemLowerCase.Checked = AppSettings.Instance.fAutoNameLowerCase;
			ToolStripMenuItemUpperCase.Checked = AppSettings.Instance.fAutoNameUpperCase;
			ToolStripMenuItemNumbers.Checked = AppSettings.Instance.fAutoNameNumbers;
			ToolStripMenuItemSymbols.Checked = AppSettings.Instance.fAutoNameSymbols;

      #endregion
			
			//-----------------------------------
			// Save Decrypt
			//-----------------------------------
      #region
			checkBoxDecodeToSameFldr.Checked = AppSettings.Instance.fDecodeToSameFldr;
      if (checkBoxDecodeToSameFldr.Checked == true)
      {
        textBoxDecodeToSameFldrPath.Enabled = true;
        textBoxDecodeToSameFldrPath.BackColor = SystemColors.Window;
        buttonSaveDecryptedFileToFolder.Enabled = true;
      }
      else
      {
        textBoxDecodeToSameFldrPath.Enabled = false;
        textBoxDecodeToSameFldrPath.BackColor = SystemColors.ButtonFace;
        buttonSaveDecryptedFileToFolder.Enabled = false;
      }
      textBoxDecodeToSameFldrPath.Text = AppSettings.Instance.DecodeToSameFldrPath;
			checkBoxDecryptConfirmOverwrite.Checked = AppSettings.Instance.fDecryptConfirmOverwrite;
			checkBoxfNoParentFldr.Checked = AppSettings.Instance.fNoParentFldr;
			checkBoxSameTimeStamp.Checked = AppSettings.Instance.fSameTimeStamp;

      #endregion

      //-----------------------------------
      // Save ZIP
      //-----------------------------------
      #region
      checkBoxZipToSameFldr.Checked = AppSettings.Instance.fZipToSameFldr;
      textBoxZipToSameFldrPath.Text = AppSettings.Instance.ZipToSameFldrPath;
      checkBoxZipConfirmOverwrite.Checked = AppSettings.Instance.fZipConfirmOverwrite;
      comboBoxZipEncryptAlgo.SelectedIndex = AppSettings.Instance.ZipEncryptionAlgorithm;

      #endregion

      //-----------------------------------
      // Delete
      //-----------------------------------
      #region
      checkBoxDelOrgFile.Checked = AppSettings.Instance.fDelOrgFile;
			checkBoxEncryptShowDeleteChkBox.Checked = AppSettings.Instance.fEncryptShowDelChkBox;
			checkBoxConfirmToDeleteAfterEncryption.Checked = AppSettings.Instance.fConfirmToDeleteAfterEncryption;
			checkBoxDelEncFile.Checked = AppSettings.Instance.fDelEncFile;
			checkBoxDecryptShowDeleteChkBox.Checked = AppSettings.Instance.fDecryptShowDelChkBox;
			checkBoxConfirmToDeleteAfterDecryption.Checked = AppSettings.Instance.fConfirmToDeleteAfterDecryption;

      //[0: Not delete, 1: Normal Delete, 2: Send to Trash, 3: Complete erase ]         
      if (AppSettings.Instance.fCompleteDelFile == 2)
      {
        // Send to trash
        radioNormalDelete.Checked = false;
        radioButtonSendToTrash.Checked = true;
        radioButtonCompleteErase.Checked = false;
      }
      else if(AppSettings.Instance.fCompleteDelFile == 3)
      {
        // Complete erase
        radioNormalDelete.Checked = false;
        radioButtonSendToTrash.Checked = false;
        radioButtonCompleteErase.Checked = true;
      }
      else
      {
        // Normal Delete
        radioNormalDelete.Checked = true;
        radioButtonSendToTrash.Checked = false;
        radioButtonCompleteErase.Checked = false;
      }

      if (radioButtonCompleteErase.Checked == true)
      {
        groupBoxCompleteDelete.Enabled = true;
      }
      else
      {
        groupBoxCompleteDelete.Enabled = false;
      }
      numericUpDownDelRandNum.Value = AppSettings.Instance.DelRandNum;
			numericUpDownDelZeroNum.Value = AppSettings.Instance.DelZeroNum;
			numericUpDownDelRandNum.Enabled = false;
			numericUpDownDelZeroNum.Enabled = false;
			pictureBoxArrow.Image = pictureBoxDisabled.Image;

			// Componet refresh;
			radioButtonCompleteErase_CheckedChanged(sender, e);
			checkBoxDelOrgFile_CheckedChanged(sender, e);

      #endregion

			//-----------------------------------
			// Compress
			//-----------------------------------
      #region
			if (AppSettings.Instance.CompressRate > 0)
			{
				checkBoxCompressionOption.Checked = true;
				trackBarCompressRate.Value = AppSettings.Instance.CompressRate;
				labelCompressionRateOption.Text = AppSettings.Instance.CompressRate.ToString();
			}
			else
			{
				checkBoxCompressionOption.Checked = false;
				trackBarCompressRate.Enabled = false;
				trackBarCompressRate.Value = 0;
			}

			trackBarCompressRate_ValueChanged(sender, e);

      #endregion

			//-----------------------------------
			// System
			//-----------------------------------
      #region
			buttonAssociateAtcFiles.Enabled = true;
			buttonUnAssociateAtcFiles.Enabled = false;

			// 関連付け設定の確認
      using (Microsoft.Win32.RegistryKey regAtc = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@".atc"))
      {
        if (regAtc == null)
        {
          buttonAssociateAtcFiles.Enabled = true;
          buttonUnAssociateAtcFiles.Enabled = false;
        }
        else
        {
          using (Microsoft.Win32.RegistryKey regAppName = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regAtc + @"\DefaultIcon"))
          {
            if (regAppName == null)
            {
              buttonAssociateAtcFiles.Enabled = true;
              buttonUnAssociateAtcFiles.Enabled = true;
            }
            else
            {
              buttonAssociateAtcFiles.Enabled = true;
              buttonUnAssociateAtcFiles.Enabled = false;
            }
          }
        }
      }
			//----------------------------------------------------------------------
			// 関連付けアイコンの設定
			//----------------------------------------------------------------------
			if (File.Exists(AppSettings.Instance.UserRegIconFilePath) == true)
			{
				// Construct an Icon.
				Icon icn = new Icon(AppSettings.Instance.UserRegIconFilePath, 48, 48);
				// Create Canvas 50x50
				Bitmap canvas = new Bitmap(50, 50);
				Graphics g = Graphics.FromImage(canvas);
				// Paint icon
				g.DrawIcon(icn, new Rectangle(2, 2, 50, 50));

				Bitmap bmpmark = new Bitmap(pictureBoxCheckmarkMyIcon.Image);
				bmpmark.MakeTransparent();

				g.DrawImage(bmpmark, new Point(0, 0));	// Left, Top
				g.Dispose();

				pictureBoxMyIcon.Image = canvas;

			}
			else
			{
				AppSettings.Instance.UserRegIconFilePath = "";

				switch (AppSettings.Instance.AtcsFileIconIndex.ToString())
				{
					case "1":
						pictureBoxCheckmark01.Visible = true;
						break;
					case "2":
						pictureBoxCheckmark02.Visible = true;
						break;
					case "3":
						pictureBoxCheckmark03.Visible = true;
						break;
					default:	// 0
						pictureBoxCheckmark00.Visible = true;
						break;
				}

      }

      #endregion

      //-----------------------------------
      // Import / Export
      #region
      if (AppSettings.Instance.fAlwaysReadIniFile == true)
      {
        checkBoxAlwaysReadIniFile.Checked = true;
      }
      else
      {
        checkBoxAlwaysReadIniFile.Checked = false;
      }

      if (AppSettings.Instance.fShowDialogToConfirmToReadIniFile == true)
      {
        checkBoxShowDialogToConfirmToReadIniFileAlways.Checked = true;
      }
      else
      {
        checkBoxShowDialogToConfirmToReadIniFileAlways.Checked = false;
      }
			#endregion

			//-----------------------------------
			// Advanced
			//-----------------------------------
			#region
			//-----------------------------------
			// Password file
			checkBoxAllowPassFile.Checked = AppSettings.Instance.fAllowPassFile;
			checkBoxCheckPassFile.Checked = AppSettings.Instance.fCheckPassFile;
			textBoxPassFilePath.Text = AppSettings.Instance.PassFilePath;

			checkBoxCheckPassFileDecrypt.Checked = AppSettings.Instance.fCheckPassFileDecrypt;
			textBoxPassFilePathDecrypt.Text = AppSettings.Instance.PassFilePathDecrypt;
			checkBoxNoErrMsgOnPassFile.Checked = AppSettings.Instance.fNoErrMsgOnPassFile;
			checkBoxDoByPasswordFile.Checked = AppSettings.Instance.fPasswordFileExe;

			//-----------------------------------
			// Input Password limit
			comboBoxMissTypeLimitsNum.SelectedIndex = AppSettings.Instance.MissTypeLimitsNum - 1;
			checkBoxBroken.Checked = AppSettings.Instance.fBroken;

      //-----------------------------------
      // Salvage data
      checkBoxSalvageIntoSameDirectory.Checked = AppSettings.Instance.fSalvageIntoSameDirectory;
      checkBoxSalvageToCreateParentFolderOneByOne.Checked = AppSettings.Instance.fSalvageToCreateParentFolderOneByOne;
      checkBoxSalvageIgnoreHashCheck.Checked = AppSettings.Instance.fSalvageIgnoreHashCheck;

			//-----------------------------------
			// Camouflage Extension
			checkBoxAddCamoExt.Checked = AppSettings.Instance.fAddCamoExt;
			textBoxCamoExt.Text = AppSettings.Instance.CamoExt;

			//-----------------------------------
			// Development mode
			checkBoxDeveloperConsole.Checked = AppSettings.Instance.fDeveloperConsole;

			#endregion

			//-----------------------------------
			// License
			//-----------------------------------
			#region zlib license
			richTextBox1.Text =
@"zlib
http://www.zlib.net/

 (C) 1995-2012 Jean-loup Gailly and Mark Adler

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  Jean-loup Gailly        Mark Adler
  jloup@gzip.org          madler@alumni.caltech.edu

If you use the zlib library in a product, we would appreciate *not* receiving
lengthy legal documents to sign.  The sources are provided for free but without
warranty of any kind.  The library has been entirely written by Jean-loup
Gailly and Mark Adler; it does not include third-party code.

If you redistribute modified sources, we would appreciate that you include in
the file ChangeLog history information documenting your changes.  Please read
the FAQ for more information on the distribution of modified source versions.

-----------------------------------
DotNetZip library
https://dotnetzip.codeplex.com/


Microsoft Public License (Ms-PL)

This license governs use of the accompanying software, the DotNetZip library (""the software""). If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions

The terms \""reproduce,"" ""reproduction,"" ""derivative works,"" and ""distribution"" have the same meaning here as under U.S. copyright law.

A ""contribution"" is the original software, or any additions or changes to the software.

A ""contributor"" is any person that distributes its contribution under this license.

""Licensed patents"" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed ""as-is."" You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.

-----------------------------------
Securely Delete a File using .NET
http://www.codeproject.com/Articles/22736/Securely-Delete-a-File-using-NET

Preamble

This License governs Your use of the Work. This License is intended to allow developers to use the Source Code and Executable Files provided as part of the Work in any application in any form.

The main points subject to the terms of the License are:

Source Code and Executable Files can be used in commercial applications;
Source Code and Executable Files can be redistributed; and
Source Code can be modified to create derivative works.
No claim of suitability, guarantee, or any warranty whatsoever is provided. The software is provided ""as-is"".
The Article accompanying the Work may not be distributed or republished without the Author's consent
This License is entered between You, the individual or other entity reading or otherwise making use of the Work licensed pursuant to this License and the individual or other entity which offers the Work under the terms of this License (""Author"").

License

THE WORK (AS DEFINED BELOW) IS PROVIDED UNDER THE TERMS OF THIS CODE PROJECT OPEN LICENSE (""LICENSE""). THE WORK IS PROTECTED BY COPYRIGHT AND/OR OTHER APPLICABLE LAW. ANY USE OF THE WORK OTHER THAN AS AUTHORIZED UNDER THIS LICENSE OR COPYRIGHT LAW IS PROHIBITED.

BY EXERCISING ANY RIGHTS TO THE WORK PROVIDED HEREIN, YOU ACCEPT AND AGREE TO BE BOUND BY THE TERMS OF THIS LICENSE. THE AUTHOR GRANTS YOU THE RIGHTS CONTAINED HEREIN IN CONSIDERATION OF YOUR ACCEPTANCE OF SUCH TERMS AND CONDITIONS. IF YOU DO NOT AGREE TO ACCEPT AND BE BOUND BY THE TERMS OF THIS LICENSE, YOU CANNOT MAKE ANY USE OF THE WORK.

Definitions.
""Articles"" means, collectively, all articles written by Author which describes how the Source Code and Executable Files for the Work may be used by a user.
""Author"" means the individual or entity that offers the Work under the terms of this License.
""Derivative Work"" means a work based upon the Work or upon the Work and other pre-existing works.
""Executable Files"" refer to the executables, binary files, configuration and any required data files included in the Work.
""Publisher"" means the provider of the website, magazine, CD-ROM, DVD or other medium from or by which the Work is obtained by You.
""Source Code"" refers to the collection of source code and configuration files used to create the Executable Files.
""Standard Version"" refers to such a Work if it has not been modified, or has been modified in accordance with the consent of the Author, such consent being in the full discretion of the Author.
""Work"" refers to the collection of files distributed by the Publisher, including the Source Code, Executable Files, binaries, data files, documentation, whitepapers and the Articles.
""You"" is you, an individual or entity wishing to use the Work and exercise your rights under this License.
Fair Use/Fair Use Rights. Nothing in this License is intended to reduce, limit, or restrict any rights arising from fair use, fair dealing, first sale or other limitations on the exclusive rights of the copyright owner under copyright law or other applicable laws.
License Grant. Subject to the terms and conditions of this License, the Author hereby grants You a worldwide, royalty-free, non-exclusive, perpetual (for the duration of the applicable copyright) license to exercise the rights in the Work as stated below:
You may use the standard version of the Source Code or Executable Files in Your own applications.
You may apply bug fixes, portability fixes and other modifications obtained from the Public Domain or from the Author. A Work modified in such a way shall still be considered the standard version and will be subject to this License.
You may otherwise modify Your copy of this Work (excluding the Articles) in any way to create a Derivative Work, provided that You insert a prominent notice in each changed file stating how, when and where You changed that file.
You may distribute the standard version of the Executable Files and Source Code or Derivative Work in aggregate with other (possibly commercial) programs as part of a larger (possibly commercial) software distribution.
The Articles discussing the Work published in any form by the author may not be distributed or republished without the Author's consent. The author retains copyright to any such Articles. You may use the Executable Files and Source Code pursuant to this License but you may not repost or republish or otherwise distribute or make available the Articles, without the prior written consent of the Author.
Any subroutines or modules supplied by You and linked into the Source Code or Executable Files of this Work shall not be considered part of this Work and will not be subject to the terms of this License.
Patent License. Subject to the terms and conditions of this License, each Author hereby grants to You a perpetual, worldwide, non-exclusive, no-charge, royalty-free, irrevocable (except as stated in this section) patent license to make, have made, use, import, and otherwise transfer the Work.
Restrictions. The license granted in Section 3 above is expressly made subject to and limited by the following restrictions:
You agree not to remove any of the original copyright, patent, trademark, and attribution notices and associated disclaimers that may appear in the Source Code or Executable Files.
You agree not to advertise or in any way imply that this Work is a product of Your own.
The name of the Author may not be used to endorse or promote products derived from the Work without the prior written consent of the Author.
You agree not to sell, lease, or rent any part of the Work. This does not restrict you from including the Work or any part of the Work inside a larger software distribution that itself is being sold. The Work by itself, though, cannot be sold, leased or rented.
You may distribute the Executable Files and Source Code only under the terms of this License, and You must include a copy of, or the Uniform Resource Identifier for, this License with every copy of the Executable Files or Source Code You distribute and ensure that anyone receiving such Executable Files and Source Code agrees that the terms of this License apply to such Executable Files and/or Source Code. You may not offer or impose any terms on the Work that alter or restrict the terms of this License or the recipients' exercise of the rights granted hereunder. You may not sublicense the Work. You must keep intact all notices that refer to this License and to the disclaimer of warranties. You may not distribute the Executable Files or Source Code with any technological measures that control access or use of the Work in a manner inconsistent with the terms of this License.
You agree not to use the Work for illegal, immoral or improper purposes, or on pages containing illegal, immoral or improper material. The Work is subject to applicable export laws. You agree to comply with all such laws and regulations that may apply to the Work after Your receipt of the Work.
Representations, Warranties and Disclaimer. THIS WORK IS PROVIDED ""AS IS"", ""WHERE IS"" AND ""AS AVAILABLE"", WITHOUT ANY EXPRESS OR IMPLIED WARRANTIES OR CONDITIONS OR GUARANTEES. YOU, THE USER, ASSUME ALL RISK IN ITS USE, INCLUDING COPYRIGHT INFRINGEMENT, PATENT INFRINGEMENT, SUITABILITY, ETC. AUTHOR EXPRESSLY DISCLAIMS ALL EXPRESS, IMPLIED OR STATUTORY WARRANTIES OR CONDITIONS, INCLUDING WITHOUT LIMITATION, WARRANTIES OR CONDITIONS OF MERCHANTABILITY, MERCHANTABLE QUALITY OR FITNESS FOR A PARTICULAR PURPOSE, OR ANY WARRANTY OF TITLE OR NON-INFRINGEMENT, OR THAT THE WORK (OR ANY PORTION THEREOF) IS CORRECT, USEFUL, BUG-FREE OR FREE OF VIRUSES. YOU MUST PASS THIS DISCLAIMER ON WHENEVER YOU DISTRIBUTE THE WORK OR DERIVATIVE WORKS.
Indemnity. You agree to defend, indemnify and hold harmless the Author and the Publisher from and against any claims, suits, losses, damages, liabilities, costs, and expenses (including reasonable legal or attorneys� fees) resulting from or relating to any use of the Work by You.
Limitation on Liability. EXCEPT TO THE EXTENT REQUIRED BY APPLICABLE LAW, IN NO EVENT WILL THE AUTHOR OR THE PUBLISHER BE LIABLE TO YOU ON ANY LEGAL THEORY FOR ANY SPECIAL, INCIDENTAL, CONSEQUENTIAL, PUNITIVE OR EXEMPLARY DAMAGES ARISING OUT OF THIS LICENSE OR THE USE OF THE WORK OR OTHERWISE, EVEN IF THE AUTHOR OR THE PUBLISHER HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
Termination.
This License and the rights granted hereunder will terminate automatically upon any breach by You of any term of this License. Individuals or entities who have received Derivative Works from You under this License, however, will not have their licenses terminated provided such individuals or entities remain in full compliance with those licenses. Sections 1, 2, 6, 7, 8, 9, 10 and 11 will survive any termination of this License.
If You bring a copyright, trademark, patent or any other infringement claim against any contributor over infringements You claim are made by the Work, your License from such contributor to the Work ends automatically.
Subject to the above terms and conditions, this License is perpetual (for the duration of the applicable copyright in the Work). Notwithstanding the above, the Author reserves the right to release the Work under different license terms or to stop distributing the Work at any time; provided, however that any such election will not serve to withdraw this License (or any other license that has been, or is required to be, granted under the terms of this License), and this License will continue in full force and effect unless terminated as stated above.
Publisher. The parties hereby confirm that the Publisher shall not, under any circumstances, be responsible for and shall not have any liability in respect of the subject matter of this License. The Publisher makes no warranty whatsoever in connection with the Work and shall not be liable to You or any party on any legal theory for any damages whatsoever, including without limitation any general, special, incidental or consequential damages arising in connection to this license. The Publisher reserves the right to cease making the Work available to You at any time without notice
Miscellaneous
This License shall be governed by the laws of the location of the head office of the Author or if the Author is an individual, the laws of location of the principal place of residence of the Author.
If any provision of this License is invalid or unenforceable under applicable law, it shall not affect the validity or enforceability of the remainder of the terms of this License, and without further action by the parties to this License, such provision shall be reformed to the minimum extent necessary to make such provision valid and enforceable.
No term or provision of this License shall be deemed waived and no breach consented to unless such waiver or consent shall be in writing and signed by the party to be charged with such waiver or consent.
This License constitutes the entire agreement between the parties with respect to the Work licensed herein. There are no understandings, agreements or representations with respect to the Work not specified herein. The Author shall not be bound by any additional provisions that may appear in any communication from You. This License may not be modified without the mutual written agreement of the Author and You.

-----------------------------------
Sha256.cs

Copyright (c) 2010 Yuri K. Schlesner

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
copies of the Software, and to permit persons to whom the Software is
 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.


THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

-----------------------------------
zxcvbn
https://github.com/dropbox/zxcvbn

Copyright (c) 2012-2016 Dan Wheeler and Dropbox, Inc.

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/ or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

      The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
";
			#endregion

			//-----------------------------------
			splitButton1.Text = toolStripMenuItemOnlineHelp.Text;
			buttonApply.Enabled = false;

			fLoading = false;

		}

		private void Form3_Shown(object sender, EventArgs e)
		{
			// The item in the TreeView in the selected state and same panel is open.
			//ツリービューで特定の設定項目を選択状態にする（パネル表示も行う）
			//treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1];
			int Index = 0;
			int ActiveIndexNum = (int)AppSettings.Instance.ActiveTreeNode;

			//ツリービューで特定の設定項目を選択状態にする（パネル表示も行う）
			setTreeViewNodeIndex(treeView1.Nodes, ActiveIndexNum, ref Index);

      //TreeNode treeNode = treeView1.Nodes[AppSettings.Instance.ActiveTreeNode];
      //treeView1.SelectedNode = treeNode;

      labelTimes.Left = comboBoxMissTypeLimitsNum.Left + comboBoxMissTypeLimitsNum.Width + 4;

      treeView1.Focus();

		}

		private void Form3_FormClosed(object sender, FormClosedEventArgs e)
		{
			// Save the index number of the option treeview node
			//開いているノードのインデックス番号を保存する
			AppSettings.Instance.ActiveTreeNode = getTreeViewNodeIndex();

		}

		/// <summary>
		/// Show panel of the selected item in the TreeView control
		/// ツリービューで選択した項目に該当するパネルを表示する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			//After invisible once all panels...
			//いったんすべてのパネルを非表示にしてから...
			foreach (Panel obj in panelObjects)
			{
				obj.Parent = splitContainer1.Panel2;
				obj.Visible = false;
			}

			switch (e.Node.Name)
			{
				case "nodePasswords":
					panelPasswordsOption.Visible = true;
					panelPasswordsOption.Focus();
					break;
				case "nodeWindow":
					panelWindowOption.Visible = true;
					panelWindowOption.Focus();
					break;
				case "nodeSave":
					panelSaveOption.Visible = true;
					panelSaveOption.Focus();
					break;
				case "nodeEncrypt":
					panelSaveEncryptOption.Visible = true;
					panelSaveEncryptOption.Focus();
					break;
				case "nodeDecrypt":
					panelSaveDecryptOption.Visible = true;
					panelSaveDecryptOption.Focus();
					break;
        case "nodeZip":
          panelSaveZipOption.Visible = true;
          panelSaveZipOption.Focus();
          break;
        case "nodeDelete":
					panelDeleteOption.Visible = true;
					panelDeleteOption.Focus();
					break;
				case "nodeCompress":
					panelCompressOption.Visible = true;
					panelCompressOption.Focus();
					break;
				case "nodeSystem":
					panelSystemOption.Visible = true;
					panelSystemOption.Focus();
					break;
        case "nodeSettingsImportExport":
          panelSettingImportExportOption.Visible = true;
          panelSettingImportExportOption.Focus();
          break;
        case "nodeAdvanced":
				case "nodePasswordFile":
					panelPasswordFileOption.Visible = true;
					panelPasswordFileOption.Focus();
					break;
        case "nodeCamouflageExt":
          panelCamouflageExtOption.Visible = true;
          panelCamouflageExtOption.Focus();
          break;
				case "nodeDevelopment":
					panelDevelopmentOption.Visible = true;
					panelDevelopmentOption.Focus();
					break;
				case "nodePasswordInputLimit":
					panelPasswordInputLimitOption.Visible = true;
					panelPasswordInputLimitOption.Focus();
					break;
				case "nodeSalvageData":
					panelSalvageDataOption.Visible = true;
					panelSalvageDataOption.Focus();
					break;
				case "nodeLicense":
					panelLicenseOption.Visible = true;
					richTextBox1.Focus();
					break;
				default:
					panelGeneralOption.Visible = true;
					panelGeneralOption.Focus();
					break;
			}

		}

		/// <summary>
		/// OK button click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (buttonApply.Enabled == true)
			{
				buttonApply_Click(sender, e);
			}
			this.Close();
		}

		/// <summary>
		/// Cancel button click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Click apply button event ( Save changed options to registry )
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonApply_Click(object sender, EventArgs e)
		{
      if(AppSettings.Instance.CommandLineArgsNum > 0 && fTemporarySettings == true)
      {
        DialogResult ret = MessageBox.Show(Resources.DialogMessagefTemporarySettings,
        Resources.DialogTitleQuestion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      
      //-----------------------------------
      // Save settings to AppSettings class 
      // 変更した各設定をクラスに格納する
      //-----------------------------------
      // General
      AppSettings.Instance.fEndToExit = checkBoxEndToExit.Checked;
			AppSettings.Instance.fOpenFile = checkBoxOpenFile.Checked;

      AppSettings.Instance.fShowDialogWhenExeFile = checkBoxShowDialogWhenExeFile.Checked;
      AppSettings.Instance.ShowDialogWhenMultipleFilesNum = Decimal.ToInt32(numericUpDownLaunchFiles.Value);
                                                           
      AppSettings.Instance.fAskEncDecode = checkBoxAskEncDecode.Checked;
			AppSettings.Instance.fNotMaskPassword = checkBoxNoHidePassword.Checked;
			AppSettings.Instance.TabSelectedIndex = tabControl1.SelectedIndex;

			// Language
			switch (comboBoxLanguage.SelectedIndex)
			{
				case 1:	// Japanese
					AppSettings.Instance.Language = "ja";
					Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP", true);
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP", true);
					break;

				case 2:	// English
					AppSettings.Instance.Language = "en";
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("", true);
					break;

				case 0:	// Default
				default:
					AppSettings.Instance.Language = "";
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("", true);
					break;
			}
			
			//-----------------------------------
			// Password
			AppSettings.Instance.fMyEncryptPasswordKeep = checkBoxMyEncodePasswordKeep.Checked;
			AppSettings.Instance.fMyDecryptPasswordKeep = checkBoxMyDecodePasswordKeep.Checked;

			AppSettings.Instance.fMemPasswordExe = checkBoxDobyMemorizedPassword.Checked;

      AppSettings.Instance.fPasswordStrengthMeter = checkBoxEnablePassStrengthMeter.Checked;

			//-----------------------------------
			// Window
			AppSettings.Instance.fMainWindowMinimize = checkBoxMainWindowMinimize.Checked;
			AppSettings.Instance.fTaskBarHide = checkBoxTaskBarHide.Checked;
			AppSettings.Instance.fTaskTrayIcon = checkBoxTaskTrayIcon.Checked;
			AppSettings.Instance.fWindowForeground = checkBoxWindowForeground.Checked;
			AppSettings.Instance.fNoMultipleInstance = checkBoxNoMultipleInstance.Checked;
			AppSettings.Instance.fTurnOnIMEsTextBoxForPasswordEntry = checkBoxTurnOnAllIMEs.Checked;

			//-----------------------------------
			// Save
			AppSettings.Instance.fSaveToExeout = false;
      if (radioButtonEncryptionFileTypeATC.Checked == true)
      {
        AppSettings.Instance.EncryptionSameFileTypeAlways = FILE_TYPE_ATC;
      }
      else if (radioButtonEncryptionFileTypeEXE.Checked == true)
      {
        AppSettings.Instance.EncryptionSameFileTypeAlways = FILE_TYPE_ATC_EXE;
        AppSettings.Instance.fSaveToExeout = true;
      }
      else if (radioButtonEncryptionFileTypeZIP.Checked == true)
      {
        AppSettings.Instance.EncryptionSameFileTypeAlways = FILE_TYPE_PASSWORD_ZIP;
      }
      else
      {
        AppSettings.Instance.EncryptionSameFileTypeAlways = FILE_TYPE_NONE;
      }

      AppSettings.Instance.fEncryptionSameFileTypeBefore = checkBoxEncryptionSameFileTypeBefore.Checked;
      
      //-----------------------------------
      // Save Encrypt
      AppSettings.Instance.fSaveToSameFldr = checkBoxSaveToSameFldr.Checked;
			AppSettings.Instance.SaveToSameFldrPath = textBoxSaveEncryptionToSameFolder.Text;
			AppSettings.Instance.fEncryptConfirmOverwrite = checkBoxConfirmSameFileName.Checked;

			if (radioButtonAllFilePack.Checked == true)
			{
				AppSettings.Instance.fAllFilePack = true;
			}
			else if (radioButtonFilesOneByOne.Checked == true)
			{
				AppSettings.Instance.fFilesOneByOne = true;
			}
			else	//if (radioButtonNormal.Checked == true)
			{
				AppSettings.Instance.fNormal = true;
			}

			AppSettings.Instance.fKeepTimeStamp = checkBoxKeepTimeStamp.Checked;
			AppSettings.Instance.fExtInAtcFileName = checkBoxExtInAtcFileName.Checked;
			AppSettings.Instance.fAutoName = checkBoxAutoName.Checked;
			AppSettings.Instance.AutoNameFormatText = textBoxAutoNameFormatText.Text;
			AppSettings.Instance.fAutoNameAlphabets = ToolStripMenuItemAlphabet.Checked;
			AppSettings.Instance.fAutoNameLowerCase = ToolStripMenuItemLowerCase.Checked;
			AppSettings.Instance.fAutoNameUpperCase = ToolStripMenuItemUpperCase.Checked;
			AppSettings.Instance.fAutoNameNumbers = ToolStripMenuItemAlphabet.Checked;
			AppSettings.Instance.fAutoNameSymbols = ToolStripMenuItemSymbols.Checked;

			//-----------------------------------
			// Save Decrypt
			AppSettings.Instance.fDecodeToSameFldr = checkBoxDecodeToSameFldr.Checked;
			AppSettings.Instance.DecodeToSameFldrPath = textBoxDecodeToSameFldrPath.Text;
			AppSettings.Instance.fDecryptConfirmOverwrite = checkBoxDecryptConfirmOverwrite.Checked;
			AppSettings.Instance.fNoParentFldr = checkBoxfNoParentFldr.Checked;
			AppSettings.Instance.fSameTimeStamp = checkBoxSameTimeStamp.Checked;
      //AppSettings.Instance.fCompareFile = checkBoxCompareFile.Checked;

      //-----------------------------------
      // Save ZIP
      AppSettings.Instance.fZipToSameFldr = checkBoxZipToSameFldr.Checked;
      AppSettings.Instance.ZipToSameFldrPath = textBoxZipToSameFldrPath.Text;
      AppSettings.Instance.fZipConfirmOverwrite = checkBoxZipConfirmOverwrite.Checked;
      AppSettings.Instance.ZipEncryptionAlgorithm = comboBoxZipEncryptAlgo.SelectedIndex;
                                                                                         
      //-----------------------------------
      // Delete
      AppSettings.Instance.fDelOrgFile = checkBoxDelOrgFile.Checked;
			AppSettings.Instance.fEncryptShowDelChkBox = checkBoxEncryptShowDeleteChkBox.Checked;
			AppSettings.Instance.fConfirmToDeleteAfterEncryption = checkBoxConfirmToDeleteAfterEncryption.Checked;
			AppSettings.Instance.fDelEncFile = checkBoxDelEncFile.Checked;
			AppSettings.Instance.fDecryptShowDelChkBox = checkBoxDecryptShowDeleteChkBox.Checked;
			AppSettings.Instance.fConfirmToDeleteAfterDecryption = checkBoxConfirmToDeleteAfterDecryption.Checked;
      //[0: Not delete, 1: Normal Delete, 2: Send to Trash, 3: Complete erase ]
      if (radioNormalDelete.Checked == true)
      {
        AppSettings.Instance.fCompleteDelFile = 1;
      }
      else if (radioButtonSendToTrash.Checked == true)
			{
				AppSettings.Instance.fCompleteDelFile = 2;
			}
			else if (radioButtonCompleteErase.Checked == true)
			{
				AppSettings.Instance.fCompleteDelFile = 3;
			}
      else
      {
        AppSettings.Instance.fCompleteDelFile = 0;  // Not delete
      }
      AppSettings.Instance.DelRandNum = (int)numericUpDownDelRandNum.Value;
			AppSettings.Instance.DelZeroNum = (int)numericUpDownDelZeroNum.Value;

      //-----------------------------------
      // Compression
      AppSettings.Instance.CompressRate = trackBarCompressRate.Value;

      //-----------------------------------
      // System
      //buttonAssociateAtcFiles
      //buttonUnAssociateAtcFiles

      if (File.Exists(AppSettings.Instance.UserRegIconFilePath) == false)
			{
				AppSettings.Instance.UserRegIconFilePath = "";
				AppSettings.Instance.AtcsFileIconIndex = -1;
			}

			if (pictureBoxCheckmark01.Visible == true)
			{
				AppSettings.Instance.AtcsFileIconIndex = 1;
			}
			else if (pictureBoxCheckmark02.Visible == true)
			{
				AppSettings.Instance.AtcsFileIconIndex = 2;
			}
			else if (pictureBoxCheckmark03.Visible == true)
			{
				AppSettings.Instance.AtcsFileIconIndex = 3;
			}
			else
			{
				AppSettings.Instance.AtcsFileIconIndex = 0;
			}

			if (fAssociationSettings == true)
			{
				// 関連付け設定
				// Association setting
				buttonAssociateAtcFiles_Click(sender, e);
			}

      //-----------------------------------
      // Import / Export
      AppSettings.Instance.fAlwaysReadIniFile = checkBoxAlwaysReadIniFile.Checked;
      AppSettings.Instance.fShowDialogToConfirmToReadIniFile = checkBoxShowDialogToConfirmToReadIniFileAlways.Checked; 
      
      //-----------------------------------
      // Password file
      AppSettings.Instance.fAllowPassFile = checkBoxAllowPassFile.Checked;
			AppSettings.Instance.fCheckPassFile = checkBoxCheckPassFile.Checked;
			AppSettings.Instance.PassFilePath = textBoxPassFilePath.Text;

			AppSettings.Instance.fCheckPassFileDecrypt = checkBoxCheckPassFileDecrypt.Checked;
			AppSettings.Instance.PassFilePathDecrypt = textBoxPassFilePathDecrypt.Text;
			AppSettings.Instance.fNoErrMsgOnPassFile = checkBoxNoErrMsgOnPassFile.Checked;
      AppSettings.Instance.fPasswordFileExe = checkBoxDoByPasswordFile.Checked;

      //-----------------------------------
      // Salvage data
      AppSettings.Instance.fSalvageIntoSameDirectory = checkBoxSalvageIntoSameDirectory.Checked;
      AppSettings.Instance.fSalvageToCreateParentFolderOneByOne = checkBoxSalvageToCreateParentFolderOneByOne.Checked;
      AppSettings.Instance.fSalvageIgnoreHashCheck = checkBoxSalvageIgnoreHashCheck.Checked;

      //----------------------------------------------------------------------
      // Camouflage extension
      AppSettings.Instance.fAddCamoExt = checkBoxAddCamoExt.Checked;
			AppSettings.Instance.CamoExt = textBoxCamoExt.Text;

			//----------------------------------------------------------------------
			// Input Password limit
			AppSettings.Instance.MissTypeLimitsNum = comboBoxMissTypeLimitsNum.SelectedIndex + 1;
			AppSettings.Instance.fBroken = checkBoxBroken.Checked;

			//----------------------------------------------------------------------
			// Developer mode
			AppSettings.Instance.fDeveloperConsole = checkBoxDeveloperConsole.Checked;

			//----------------------------------------------------------------------
			// Save to the settings of each source
			AppSettings.Instance.SaveOptions(fTemporarySettings);

			buttonApply.Enabled = false;
      
		}

		private void toolStripMenuItemOnlineHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(OneLineHelpURL);
		}

		private void toolStripMenuItemCmdReference_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(CommandLineReferenceURL);
		}

		private void splitButton1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(OneLineHelpURL);
		}

		/// <summary>
		/// All of the options component in this form has been changed
		/// このフォームにある特定のコンポーネントに変更があったとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void options_ComponentChanged(object sender, EventArgs e)
		{
			// Button 'Apply' is enabled.
			buttonApply.Enabled = true;
		}

    private void numericUpDownDeleteNum_Leave(object sender, EventArgs e)
    {
      if(numericUpDownDelRandNum.Value == 0 && numericUpDownDelZeroNum.Value == 0)
      {
        // エラー
        // ランダム値と、ゼロ値の設定の両方を0に設定することはできません。
        //
        // Alert
        // Both Random setting and Zero setting are not able to set to zero.
        MessageBox.Show(Resources.DialogMessageBothRandomAndZeroNotZero,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        numericUpDownDelZeroNum.Value = 1;
        numericUpDownDelZeroNum.Focus();
        numericUpDownDelZeroNum.Select();

      }

    }

    //======================================================================
    // General
    //======================================================================
#region

    private void checkBoxShowDialogWhenMultipleFiles_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxShowDialogWhenMultipleFiles.Checked == false)
      {
        numericUpDownLaunchFiles.Enabled = false;
        numericUpDownLaunchFiles.BackColor = SystemColors.ButtonFace;
      }
      else
      {
        numericUpDownLaunchFiles.Enabled = true;
        numericUpDownLaunchFiles.BackColor = Color.White;
      }
    }

    private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (fLoading == false)
			{
				switch (comboBoxLanguage.SelectedIndex)
				{
					case 1:
						AppSettings.Instance.Language = "ja";
						break;

					case 2:
            AppSettings.Instance.Language = "en";
						break;

					case 0:
					default:
						AppSettings.Instance.Language = "";
						break;
				}

        // The message prompt to restart the application

        // 問い合わせ
        // 設定を有効にするには、アタッシェケースを再起動する必要があります。
        // 再起動しますか？
        //
        // Question
        // To enable this setting, you will need to restart the AttacheCase.
        // Do you restart AttacheCase now?
        // 
        DialogResult ret = MessageBox.Show(Resources.DialogMessageApplicationRestart,
				Resources.DialogTitleQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (ret == System.Windows.Forms.DialogResult.Yes)
				{
					// Save the setting and then, restart this application.
					buttonOK_Click(sender, e);
					Application.Restart();
				}

			} //end if (comboBoxLanguage.Items.Count > 0);

		}
#endregion

		//======================================================================
		// Passwords
		//======================================================================
#region
		private void checkBoxMyEncodePasswordKeep_CheckedChanged(object sender, EventArgs e)
		{
			if (fLoading == true)
			{
				return;
			}

			if (checkBoxMyEncodePasswordKeep.Checked == true)
			{
				// Show Option form
				Form4 frm4 = new Form4("EncryptPassword", "");
				frm4.ShowDialog();
				frm4.Dispose();

				if (AppSettings.Instance.MyEncryptPasswordString == "")
				{
					checkBoxMyEncodePasswordKeep.Checked = false;
          if (checkBoxMyDecodePasswordKeep.Checked == false)
          {
            checkBoxDobyMemorizedPassword.Checked = false;
            checkBoxDobyMemorizedPassword.Enabled = false;
          }             
				}
				else
				{
					//textBoxMyEncodePassword.Text = new string('*', AppSettings.Instance.MyEncryptPasswordString.Length);
					textBoxMyEncodePassword.Text = new string('*', 16);
					textBoxAutoNameFormatText.UseSystemPasswordChar = true;

          //checkBoxDobyMemorizedPassword.Checked = true;
          checkBoxDobyMemorizedPassword.Enabled = true;
        }
      }
      else
      {
        checkBoxMyEncodePasswordKeep.Checked = false;
        if (checkBoxMyDecodePasswordKeep.Checked == false)
        {
          checkBoxDobyMemorizedPassword.Checked = false;
          checkBoxDobyMemorizedPassword.Enabled = false;
        }
      }

      buttonApply.Enabled = true;

		}

		private void textBoxMyEncodePassword_Click(object sender, EventArgs e)
		{
			checkBoxMyEncodePasswordKeep.Checked = true;
		}

		private void checkBoxMyDecodePasswordKeep_CheckedChanged(object sender, EventArgs e)
		{
			if (fLoading == true)
			{
				return;
			}

			if (checkBoxMyDecodePasswordKeep.Checked == true)
			{
				// Show Option form
				Form4 frm4 = new Form4("DecryptPassword", "");
				frm4.ShowDialog();
				frm4.Dispose();

				if (AppSettings.Instance.MyDecryptPasswordString == "")
				{
					textBoxMyDecodePassword.Text = "";
					checkBoxMyDecodePasswordKeep.Checked = false;

          if (checkBoxMyEncodePasswordKeep.Checked == false)
          {
            checkBoxDobyMemorizedPassword.Checked = false;
            checkBoxDobyMemorizedPassword.Enabled = false;
          }
        }
				else
				{
					//textBoxMyEncodePassword.Text = new string('*', AppSettings.Instance.MyEncryptPasswordString.Length);
					textBoxMyDecodePassword.Text = new string('*', 16);

          //checkBoxDobyMemorizedPassword.Checked = true;
          checkBoxDobyMemorizedPassword.Enabled = true;
        }
      }

      buttonApply.Enabled = true;

		}

		private void textBoxMyDecodePassword_Click(object sender, EventArgs e)
		{
			checkBoxMyDecodePasswordKeep.Checked = true;
		}


		private void buttonInputEncryptionPassword_Click(object sender, EventArgs e)
		{
			checkBoxMyEncodePasswordKeep.Checked = true;
			checkBoxMyEncodePasswordKeep_CheckedChanged(sender, e);
		}

		private void buttonInputDecryptionPassword_Click(object sender, EventArgs e)
		{
			checkBoxMyDecodePasswordKeep.Checked = true;
			checkBoxMyDecodePasswordKeep_CheckedChanged(sender, e);
		}


#endregion

		//======================================================================
		// Window
		//======================================================================
#region
		private void checkBoxTaskBarHide_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxTaskBarHide.Checked == true)
			{
				checkBoxTaskTrayIcon.Enabled = true;
			}
			else
			{
				checkBoxTaskTrayIcon.Enabled = true;
			}
			buttonApply.Enabled = true;
		}

#endregion

		//======================================================================
		// Save
		//======================================================================
#region

		private void checkBoxEncryptionSameFileTypeBefore_CheckedChanged(object sender, EventArgs e)
		{
      buttonApply.Enabled = true;
		}

#endregion

		//======================================================================
		// SaveEncrypt
		//======================================================================
#region
		private void checkBoxSaveToSameFldr_CheckedChanged(object sender, EventArgs e)
		{
			if (fLoading == true)
			{
				return;
			}
			if (checkBoxSaveToSameFldr.Checked == true)
			{
				textBoxSaveEncryptionToSameFolder.Enabled = true;
				textBoxSaveEncryptionToSameFolder.BackColor = SystemColors.Window;
				buttonSaveEncryptedFileToFolder.Enabled = true;
				if (Directory.Exists(textBoxSaveEncryptionToSameFolder.Text) == false)
				{
					buttonSaveEncryptedFileToFolder_Click(sender, e);
				}
			}
			else
			{
				textBoxSaveEncryptionToSameFolder.Enabled = false;
				textBoxSaveEncryptionToSameFolder.BackColor = SystemColors.ButtonFace;
				buttonSaveEncryptedFileToFolder.Enabled = false;
			}
			buttonApply.Enabled = true;

		}

		private void buttonSaveEncryptedFileToFolder_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.Description = Resources.folderBrowserDialogSaveToEncryptedFile;
			if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				textBoxSaveEncryptionToSameFolder.Text = folderBrowserDialog1.SelectedPath;
				AppSettings.Instance.InitDirPath = folderBrowserDialog1.SelectedPath;
			}
			else
			{
				if (Directory.Exists(textBoxSaveEncryptionToSameFolder.Text) == false)
				{
					checkBoxSaveToSameFldr.Checked = false;
				}
			}
		}

    private void checkBoxAutoName_Click(object sender, EventArgs e)
    {
      if (checkBoxAutoName.Checked == true)
      {
        textBoxAutoNameFormatText.Enabled = true;
        textBoxAutoNameFormatText.BackColor = SystemColors.Window;
        buttonInsertFormat.Enabled = true;
        labelEncryptedFileNameFormat.Enabled = true;
      }
      else
      {
        textBoxAutoNameFormatText.Enabled = false;
        textBoxAutoNameFormatText.BackColor = SystemColors.ButtonFace;
        buttonInsertFormat.Enabled = false;
        labelEncryptedFileNameFormat.Enabled = false;
      }
    }

    private void buttonInsertFormat_Click(object sender, EventArgs e)
		{
			Point btnClientCurPos = buttonInsertFormat.PointToClient(Cursor.Position);
			contextMenuStrip1.Show(Cursor.Position);
		}

		private void textBoxAutoNameFormatText_TextChanged(object sender, EventArgs e)
		{
			string FilePath = @"C:\Test.atc";
			labelEncryptedFileNameFormat.Text = "ex). " +
				AppSettings.Instance.getSpecifyFileNameFormat(textBoxAutoNameFormatText.Text, FilePath);
      buttonApply.Enabled = true;
    }

    private void textBoxAutoNameFormatText_Leave(object sender, EventArgs e)
    {
      if (textBoxAutoNameFormatText.Text == "")
      {
        checkBoxAutoName.Checked = false;
      }
    }

    //-----------------------------------
    // Insert the specific format object
    //-----------------------------------
    private void InsertFileNameOject(object sender, EventArgs e)
    {
      textBoxAutoNameFormatText.SelectedText = @"<filename>";
    }

    private void InsertExtensionOject(object sender, EventArgs e)
    {
      textBoxAutoNameFormatText.SelectedText = @"<ext>";
    }

    private void InsertDateTimeOject(object sender, EventArgs e)
    {
      textBoxAutoNameFormatText.SelectedText = @"<date:yyyy_MM_dd-hh_mm_ss>";
    }

    private void InsertRandomStringOject(object sender, EventArgs e)
		{
			Regex r = new Regex(@"<random:[0-9]+>", RegexOptions.IgnoreCase);
			Match m = r.Match(textBoxAutoNameFormatText.Text);
			if (m.Success == false)
			{
				textBoxAutoNameFormatText.SelectedText = "<random:8>";
			}
		}

		private void InsertSerialNumberOject(object sender, EventArgs e)
		{
			textBoxAutoNameFormatText.SelectedText = "<num:3>";
		}
		
		private void checkBoxAddCamoExt_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAddCamoExt.Checked == true)
			{
				textBoxCamoExt.Enabled = true;
				textBoxCamoExt.BackColor = SystemColors.Window;
			}
			else
			{
				textBoxCamoExt.Enabled = false;
				textBoxCamoExt.BackColor = SystemColors.ButtonFace;
			}
			buttonApply.Enabled = true;
		}

#endregion

		//======================================================================
		// SaveDecrypt
		//======================================================================
#region
		private void checkBoxDecodeToSameFldr_CheckedChanged(object sender, EventArgs e)
		{
			if (fLoading == true)
			{
				return;
			}
			if (checkBoxDecodeToSameFldr.Checked == true)
			{
				buttonSaveDecryptedFileToFolder.Enabled = true;
				textBoxDecodeToSameFldrPath.Enabled = true;
				textBoxDecodeToSameFldrPath.BackColor = SystemColors.Window;
				if (Directory.Exists(textBoxDecodeToSameFldrPath.Text) == false)
				{
					buttonSaveDecryptedFileToFolder_Click(sender, e);
				}
			}
			else
			{
				buttonSaveDecryptedFileToFolder.Enabled = false;
				textBoxDecodeToSameFldrPath.Enabled = false;
				textBoxDecodeToSameFldrPath.BackColor = SystemColors.ButtonFace;
			}
			buttonApply.Enabled = true;

		}

		private void buttonSaveDecryptedFileToFolder_Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.Description = Resources.folderBrowserDialogSaveToDecryptedFile;
			if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				textBoxDecodeToSameFldrPath.Text = folderBrowserDialog1.SelectedPath;
				AppSettings.Instance.InitDirPath = folderBrowserDialog1.SelectedPath;
			}
			else
			{
				if (Directory.Exists(textBoxDecodeToSameFldrPath.Text) == false)
				{
					checkBoxDecodeToSameFldr.Checked = false;
				}
			}
			buttonApply.Enabled = true;
		}

#endregion

    //======================================================================
    // Save Password ZIP        
    //======================================================================
#region

    private void checkBoxZipToSameFldr_CheckedChanged(object sender, EventArgs e)
    {
      if (fLoading == true)
      {
        return;
      }
      if (checkBoxZipToSameFldr.Checked == true)
      {
        buttonSaveZipFileToFolder.Enabled = true;
        textBoxZipToSameFldrPath.Enabled = true;
        textBoxZipToSameFldrPath.BackColor = SystemColors.Window;
        if (Directory.Exists(textBoxZipToSameFldrPath.Text) == false)
        {
          buttonSaveZipFileToFolder_Click(sender, e);
        }
      }
      else
      {
        buttonSaveZipFileToFolder.Enabled = false;
        textBoxZipToSameFldrPath.BackColor = SystemColors.ButtonFace;
        textBoxZipToSameFldrPath.Enabled = false;
      }
      buttonApply.Enabled = true;

    }

    private void buttonSaveZipFileToFolder_Click(object sender, EventArgs e)
    {
      folderBrowserDialog1.Description = Resources.folderBrowserDialogSaveToZipFile;
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        textBoxZipToSameFldrPath.Text = folderBrowserDialog1.SelectedPath;
        AppSettings.Instance.InitDirPath = folderBrowserDialog1.SelectedPath;
      }
      else
      {
        if (Directory.Exists(textBoxZipToSameFldrPath.Text) == false)
        {
          checkBoxZipToSameFldr.Checked = false;
          buttonSaveZipFileToFolder.Enabled = false;
          textBoxZipToSameFldrPath.BackColor = SystemColors.ButtonFace;
          textBoxZipToSameFldrPath.Enabled = false;
        }
      }
      buttonApply.Enabled = true;

    }

    private void comboBoxZipEncryptAlgo_SelectedIndexChanged(object sender, EventArgs e)
    {
      AppSettings.Instance.ZipEncryptionAlgorithm = comboBoxZipEncryptAlgo.SelectedIndex;

      switch (comboBoxZipEncryptAlgo.SelectedIndex)
      {
        case 1:
        case 2:
          labelEncryptAlgoDescription.Text = Resources.labelEncryptAlgoDescriptionAES;
          break;
        default:
          labelEncryptAlgoDescription.Text = Resources.labelEncryptAlgoDescriptionPkzipWeak;
          break;
      }
      buttonApply.Enabled = true;

    }


#endregion

    //======================================================================
    // Delete
    //======================================================================
#region

    /// <summary>
    /// Delete &original files or directories after encryption
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void checkBoxDelOrgFile_CheckedChanged(object sender, EventArgs e)
    {
      if(checkBoxDelOrgFile.Checked == true || checkBoxEncryptShowDeleteChkBox.Checked == true)
      {
        checkBoxConfirmToDeleteAfterEncryption.Enabled = true;
        groupBoxAdvancedDeleteOption.Enabled = true;
      }
      else
      {
        checkBoxConfirmToDeleteAfterEncryption.Checked = false;
        checkBoxConfirmToDeleteAfterEncryption.Enabled = false;

        if (checkBoxDelEncFile.Checked == false && checkBoxDecryptShowDeleteChkBox.Checked == false)
        {
          groupBoxAdvancedDeleteOption.Enabled = false;
        }
      }
      buttonApply.Enabled = true;
    }

    private void checkBoxDelEncFile_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxDelEncFile.Checked == true || checkBoxDecryptShowDeleteChkBox.Checked == true)
      {
        checkBoxConfirmToDeleteAfterDecryption.Enabled = true;
        groupBoxAdvancedDeleteOption.Enabled = true;
      }
      else
      {
        checkBoxConfirmToDeleteAfterDecryption.Checked = false;
        checkBoxConfirmToDeleteAfterDecryption.Enabled = false;

        if (checkBoxDelOrgFile.Checked == false && checkBoxEncryptShowDeleteChkBox.Checked == false)
        {
          groupBoxAdvancedDeleteOption.Enabled = false;
        }
      }
      buttonApply.Enabled = true;

    }

    private void radioButtonCompleteErase_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonCompleteErase.Checked == true)
			{
        groupBoxCompleteDelete.Enabled = true;
        numericUpDownDelRandNum.Enabled = true;
				numericUpDownDelZeroNum.Enabled = true;
				pictureBoxArrow.Image = pictureBoxEnabled.Image;
			}
			else
			{
        groupBoxCompleteDelete.Enabled = false;
        numericUpDownDelRandNum.Enabled = false;
				numericUpDownDelZeroNum.Enabled = false;
				pictureBoxArrow.Image = pictureBoxDisabled.Image;
			}
			buttonApply.Enabled = true;
		}

#endregion
		
		//======================================================================
		// Compress
		//======================================================================
#region
		private void checkBoxCompressionOption_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCompressionOption.Checked == true)
			{
				labelCompressionRateOption.Text = trackBarCompressRate.Value.ToString();
        trackBarCompressRate.Enabled = true;
        trackBarCompressRate.Value = 6;

      }
			else
			{
				trackBarCompressRate.Value = 0;
        trackBarCompressRate.Enabled = false;

      }
      buttonApply.Enabled = true;
    }

    private void trackBarCompressRate_ValueChanged(object sender, EventArgs e)
		{
			if (trackBarCompressRate.Value > 0)
			{
				checkBoxCompressionOption.Checked = true;
			}
			else
			{
				checkBoxCompressionOption.Checked = false;
			}

			string CompressMessageText = "";
			switch (trackBarCompressRate.Value)
			{
				case 0:	// None
					CompressMessageText = Resources.labelCompressNone;
					break;
				case 6:	// Default
					CompressMessageText = Resources.labelCompressDefault;
					break;
				case 9:	// Maximum
					CompressMessageText = Resources.labelCompressMaximum;
					break;
				default:
					CompressMessageText = "";
					break;
			}
      buttonApply.Enabled = true;
      labelCompressionRateOption.Text = trackBarCompressRate.Value.ToString() + CompressMessageText;
		
		}

#endregion

		//======================================================================
		// System
		//======================================================================
#region
		private void buttonAssociateAtcFiles_Click(object sender, EventArgs e)
		{			
			string AtcSetupExePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AtcSetup.exe");
			if (File.Exists(AtcSetupExePath) == false)
			{
				// 注意
				// ファイル関連付けツールが見つかりません。
				//
				// Alert
				// File association tool is not found.
				MessageBox.Show(Resources.DialogMessageFileAssociationToolNotFound + Environment.NewLine + AtcSetupExePath,
				Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			System.Diagnostics.ProcessStartInfo psi =	new System.Diagnostics.ProcessStartInfo();
			psi.FileName = AtcSetupExePath;
			psi.Verb = "runas";

			int IconIndex = 0;
			if (pictureBoxCheckmark01.Visible == true)
			{
				IconIndex = 2;
			}
			else if (pictureBoxCheckmark02.Visible == true)
			{
				IconIndex = 3;
			}
			else if (pictureBoxCheckmark03.Visible == true)
			{
				IconIndex = 4;
			}
			else
			{
				IconIndex = 1;
			}

			if (File.Exists(AppSettings.Instance.UserRegIconFilePath) == true)
			{
				psi.Arguments = string.Format("-t=0 -icn=\"{0}\"", AppSettings.Instance.UserRegIconFilePath);
			}
			else
			{
				psi.Arguments = string.Format("-t=0 -icn={0}", IconIndex);
			}
			
			try
			{
				System.Diagnostics.Process.Start(psi);
        buttonUnAssociateAtcFiles.Enabled = true;
      }
			catch (System.ComponentModel.Win32Exception ex)
			{
				// 注意
				// ファイル関連付けツールが起動できませんでした。
				//
				// Alert
				// File association tool can be not launched.
				MessageBox.Show(Resources.DialogMessageFileAssociationToolNotLaunched + Environment.NewLine + ex.Message,
				Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		private void buttonUnAssociateAtcFiles_Click(object sender, EventArgs e)
		{
			string AtcSetupExePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AtcSetup.exe");
			if (File.Exists(AtcSetupExePath) == false)
			{
				// 注意
				// ファイル関連付けツールが見つかりません。
				//
				// Alert
				// File association tool is not found.
				MessageBox.Show(Resources.DialogMessageFileAssociationToolNotFound + Environment.NewLine + AtcSetupExePath,
				Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
			psi.FileName = AtcSetupExePath;
			psi.Verb = "runas";
			psi.Arguments = "-t=1";

			try
			{
				System.Diagnostics.Process.Start(psi);
			}
			catch (System.ComponentModel.Win32Exception ex)
			{
				// 注意
				// ファイル関連付けツールが起動できませんでした。
				//
				// Alert
				// File association tool can be not launched.
				MessageBox.Show(Resources.DialogMessageFileAssociationToolNotLaunched + Environment.NewLine + ex.Message,
				Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

    private void buttonAddSendToFolder_Click(object sender, EventArgs e)
    {
      string FileName = "AttacheCase.lnk";
      if (AppSettings.Instance.Language == "ja")
      {
        FileName = "アタッシェケース.lnk";
      }

      string shortcutPath = System.IO.Path.Combine(
          Environment.GetFolderPath(System.Environment.SpecialFolder.SendTo), FileName);

      string targetPath = Application.ExecutablePath;

      Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
      dynamic shell = Activator.CreateInstance(t);

      var shortcut = shell.CreateShortcut(shortcutPath);

      shortcut.TargetPath = targetPath;
      shortcut.IconLocation = Application.ExecutablePath + ",0";

      shortcut.Save();

      System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
      System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
    }

    private void buttonOpenSendToFolder_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(
        "EXPLORER.EXE", Environment.GetFolderPath(System.Environment.SpecialFolder.SendTo));
    }


    private void pictureBoxIcon00_Click(object sender, EventArgs e)
		{
			pictureBoxCheckmark00.Visible = true;
			pictureBoxCheckmark01.Visible = false;
			pictureBoxCheckmark02.Visible = false;
			pictureBoxCheckmark03.Visible = false;
			pictureBoxMyIcon.Image = pictureBoxMyIcon.InitialImage;
			AppSettings.Instance.UserRegIconFilePath = "";
			fAssociationSettings = true;
			buttonApply.Enabled = true;
		}

		private void pictureBoxIcon01_Click(object sender, EventArgs e)
		{
			pictureBoxCheckmark00.Visible = false;
			pictureBoxCheckmark01.Visible = true;
			pictureBoxCheckmark02.Visible = false;
			pictureBoxCheckmark03.Visible = false;
			pictureBoxMyIcon.Image = pictureBoxMyIcon.InitialImage;
			AppSettings.Instance.UserRegIconFilePath = "";
			fAssociationSettings = true;
			buttonApply.Enabled = true;
		}

		private void pictureBoxIcon02_Click(object sender, EventArgs e)
		{
			pictureBoxCheckmark00.Visible = false;
			pictureBoxCheckmark01.Visible = false;
			pictureBoxCheckmark02.Visible = true;
			pictureBoxCheckmark03.Visible = false;
			pictureBoxMyIcon.Image = pictureBoxMyIcon.InitialImage;
			AppSettings.Instance.UserRegIconFilePath = "";
			fAssociationSettings = true;
			buttonApply.Enabled = true;
		}

		private void pictureBoxIcon03_Click(object sender, EventArgs e)
		{
			pictureBoxCheckmark00.Visible = false;
			pictureBoxCheckmark01.Visible = false;
			pictureBoxCheckmark02.Visible = false;
			pictureBoxCheckmark03.Visible = true;
			pictureBoxMyIcon.Image = pictureBoxMyIcon.InitialImage;
			AppSettings.Instance.UserRegIconFilePath = "";
			fAssociationSettings = true;
			buttonApply.Enabled = true;
		}

		private void pictureBoxMyIcon_Click(object sender, EventArgs e)
		{
			if (File.Exists(AppSettings.Instance.UserRegIconFilePath) == false)
			{
				buttonLoadIconFile_Click(sender, e);
				return;
			}
			
			pictureBoxCheckmark00.Visible = false;
			pictureBoxCheckmark01.Visible = false;
			pictureBoxCheckmark02.Visible = false;
			pictureBoxCheckmark03.Visible = false;
			pictureBoxCheckmarkMyIcon.Visible = true;
			fAssociationSettings = true;
			buttonApply.Enabled = true;

		}

		private void buttonLoadIconFile_Click(object sender, EventArgs e)
		{
			openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
			openFileDialog1.Filter = Resources.OpenDialogFilterIconFiles;
			openFileDialog1.Title = Resources.OpenDialogTitleIconFiles;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				AppSettings.Instance.UserRegIconFilePath = openFileDialog1.FileName;

				// Construct an Icon.
				Icon icn = new Icon(openFileDialog1.FileName, 48, 48);
				// Create Canvas 50x50
				Bitmap canvas = new Bitmap(50, 50);
				Graphics g = Graphics.FromImage(canvas);
				// Paint icon
				g.DrawIcon(icn, new Rectangle(2, 2, 50, 50));

				Bitmap bmpmark = new Bitmap(pictureBoxCheckmarkMyIcon.Image);
				bmpmark.MakeTransparent();

				g.DrawImage(bmpmark, new Point(0, 0));	// Left, Top
				g.Dispose();

				pictureBoxMyIcon.Image = canvas;
				fAssociationSettings = true;
				buttonApply.Enabled = true;

        pictureBoxCheckmark00.Visible = false;
        pictureBoxCheckmark01.Visible = false;
        pictureBoxCheckmark02.Visible = false;
        pictureBoxCheckmark03.Visible = false;

      }
    }

		/// <summary>
		/// Output the current configuration as INI file
		/// 現在の設定をINIファイルとして出力する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExportCurrentConf_Click(object sender, EventArgs e)
		{
			saveFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
			saveFileDialog1.Filter = Resources.SaveDialogFilterIniFilles;
			saveFileDialog1.FileName = "_AtcCase.ini";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				AppSettings.Instance.WriteOptionToIniFile(saveFileDialog1.FileName);
				AppSettings.Instance.InitDirPath = Path.GetDirectoryName(saveFileDialog1.FileName);
			}
		}

    /// <summary>
    /// Import INI file to this current configuration
    /// INIファイルから現在の設定としてインポートする。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonImportCurrentConf_Click(object sender, EventArgs e)
    {
      openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
      openFileDialog1.Filter = Resources.SaveDialogFilterIniFilles;
      openFileDialog1.FileName = "_AtcCase.ini";
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        AppSettings.Instance.ReadOptionFromIniFile(saveFileDialog1.FileName);
      }

    }

    /// <summary>
    /// Replace the current configuration by this temporary configuration
    /// 一時的な設定を現在の設定に適用する
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonReplaceCurrentByTemporary_Click(object sender, EventArgs e)
		{
      fTemporarySettings = false;
      using (Bitmap bitmap = new Bitmap(pictureBoxRegistryIcon.Image))
      {
        bitmap.SetResolution(24, 24);
        this.Icon = Icon.FromHandle(bitmap.GetHicon());
        this.Text = Resources.DialogTitleSettings + " - " + Resources.DialogTitleRegistry;
      }
      buttonApply.Enabled = true;

    }

    #endregion

    //======================================================================
    // PasswordFile
    //======================================================================
#region
		private void checkBoxCheckPassFile_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCheckPassFile.Checked == true)
			{
				textBoxPassFilePath.Enabled = true;
				buttonOpenFileDialogForEncryption.Enabled = true;
				textBoxPassFilePath.BackColor = SystemColors.Window;
			}
			else
			{
				textBoxPassFilePath.Enabled = false;
				buttonOpenFileDialogForEncryption.Enabled = false;
				textBoxPassFilePath.BackColor = SystemColors.ButtonFace;
			}
			buttonApply.Enabled = true;

		}

		private void checkBoxCheckPassFileDecrypt_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCheckPassFileDecrypt.Checked == true)
			{
				textBoxPassFilePathDecrypt.Enabled = true;
				buttonOpenFileDialogForDecryption.Enabled = true;
				textBoxPassFilePathDecrypt.BackColor = SystemColors.Window;
			}
			else
			{
				textBoxPassFilePathDecrypt.Enabled = false;
				buttonOpenFileDialogForDecryption.Enabled = false;
				textBoxPassFilePathDecrypt.BackColor = SystemColors.ButtonFace;
			}
			buttonApply.Enabled = true;
			
		}

		private void buttonOpenFileDialogForEncryption_Click(object sender, EventArgs e)
		{
			openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
			openFileDialog1.Title = Resources.DialogTitleSelectPasswordFile;
			openFileDialog1.Filter = Resources.OpenDialogFilterAllFiles;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBoxPassFilePath.Text = openFileDialog1.FileName;
				buttonApply.Enabled = true;
			}
		}

    private void textBoxPassFilePath_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
        textBoxPassFilePath.BackColor = Color.Honeydew;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }

    }

    private void textBoxPassFilePath_DragLeave(object sender, EventArgs e)
    {
      textBoxPassFilePath.BackColor = SystemColors.Window;
    }

    private void textBoxPassFilePath_DragDrop(object sender, DragEventArgs e)
    {
      string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if (File.Exists(FilePaths[0]) == true)
      {
        textBoxPassFilePath.Text = FilePaths[0];
      }
      else
      {
        // 注意
        // パスワードファイルにフォルダーを使うことはできません。
        //
        // Alert
        // Not use the folder to the password file.
        DialogResult ret = MessageBox.Show(Resources.DialogMessageNotDirectoryInPasswordFile,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      textBoxPassFilePath.BackColor = SystemColors.Window;

    }

    private void buttonOpenFileDialogForDecryption_Click(object sender, EventArgs e)
		{
			openFileDialog1.InitialDirectory = AppSettings.Instance.InitDirPath;
			openFileDialog1.Title = Resources.DialogTitleSelectPasswordFile;
			openFileDialog1.Filter = Resources.OpenDialogFilterAllFiles;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
        textBoxPassFilePathDecrypt.Text = openFileDialog1.FileName;
			}
			buttonApply.Enabled = true;
		}

    private void textBoxPassFilePathDecrypt_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
        textBoxPassFilePathDecrypt.BackColor = Color.Honeydew;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void textBoxPassFilePathDecrypt_DragLeave(object sender, EventArgs e)
    {
      textBoxPassFilePathDecrypt.BackColor = SystemColors.Window;
    }

    private void textBoxPassFilePathDecrypt_DragDrop(object sender, DragEventArgs e)
    {
      string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if (File.Exists(FilePaths[0]) == true)
      {
        textBoxPassFilePathDecrypt.Text = FilePaths[0];
      }
      else
      {
        // 注意
        // パスワードファイルにフォルダーを使うことはできません。
        //
        // Alert
        // Not use the folder to the password file.
        DialogResult ret = MessageBox.Show(Resources.DialogMessageNotDirectoryInPasswordFile,
        Resources.DialogTitleAlert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      textBoxPassFilePathDecrypt.BackColor = SystemColors.Window;

    }


		#endregion

	}



}
