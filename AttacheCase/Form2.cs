﻿//---------------------------------------------------------------------- 
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
using System.Reflection;
using System.Net;
using AttacheCase.Properties;
using System.IO;

namespace AttacheCase
{
  public partial class Form2 : Form
  {

    public Form2()
    {
      InitializeComponent();
    }

    private void Form2_Load(object sender, EventArgs e)
    {
      //labelAppName.Text = Application.ProductName;
      labelVersion.Text = "Version." + ApplicationInfo.Version;
      labelCopyright.Text = ApplicationInfo.CopyrightHolder;

      linkLabelCheckForUpdates.Left = pictureBoxApplicationIcon.Left;

#if (MS_STORE)
      linkLabelCheckForUpdates.Visible = false;
#endif

    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.Close();
    }
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      linkLabel1.LinkVisited = true;
      System.Diagnostics.Process.Start(linkLabel1.Text);
      this.Close();
    }
    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      linkLabel2.LinkVisited = true;
      System.Diagnostics.Process.Start(linkLabel2.Text);
      this.Close();
    }
    private void linkLabelCheckForUpdates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (pictureBoxProgressCircle.Image == pictureBoxExclamationMark.Image)
      {
        System.Diagnostics.Process.Start("https://hibara.org/software/attachecase/");
        this.Close();
        return;
      }

      pictureBoxProgressCircle.Visible = true;
      linkLabelCheckForUpdates.Left = pictureBoxProgressCircle.Left + pictureBoxProgressCircle.Width;
      // "Checking for update..."
      linkLabelCheckForUpdates.Text = Resources.linkLabelCheckingForUpdates;
      this.Refresh();

      try
      {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        // 将来的には、.NET 4.5以上で以下を有効にする
        // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        using (WebClient webClient = new WebClient())
        {
          Stream stream = webClient.OpenRead(new Uri("https://hibara.org/software/attachecase/current/index.php"));
          using (StreamReader sr = new StreamReader(stream))
          {
            var content = sr.ReadToEnd();
            int current = int.Parse(content);
            if (current > AppSettings.Instance.AppVersion)
            {
              pictureBoxProgressCircle.Image = pictureBoxExclamationMark.Image;
              // "The latest version is released!"
              linkLabelCheckForUpdates.Text = Resources.linkLabelLatestVersionReleased;
            }
            else
            {
              pictureBoxProgressCircle.Image = pictureBoxCheckMark.Image;
              // "Your version is latest."
              linkLabelCheckForUpdates.Text = Resources.linkLabelLatestVersion;
              linkLabelCheckForUpdates.Enabled = false;
            }
          }
        }
      }
      catch(Exception ex)
      {
        // "Getting updates information is failed."
        linkLabelCheckForUpdates.Text = Resources.linkLabelCheckForUpdatesFailed;
        linkLabelCheckForUpdates.Enabled = false;
        MessageBox.Show(ex.Message);

      }

    }

  }

  /// <summary>
  /// アセンブリ情報を取得する
  /// Get assembly infomations
  /// http://stackoverflow.com/questions/909555/how-can-i-get-the-assembly-file-version
  /// </summary>
  static public class ApplicationInfo
  {
    public static Version Version { get { return Assembly.GetCallingAssembly().GetName().Version; } }
	  public static string Title
	  {
		  get
		  {
			  object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			  if (attributes.Length > 0)
			  {
				  AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
				  if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
			  }
			  return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
		  }
	  }

	  public static string ProductName
	  {
		  get
		  {
			  object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			  return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
		  }
	  }

	  public static string Description
	  {
		  get
		  {
			  object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
			  return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
		  }
	  }

	  public static string CopyrightHolder
	  {
		  get
		  {
			  object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			  return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
		  }
	  }

	  public static string CompanyName
	  {
		  get
		  {
			  object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
			  return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
		  }
	  }

  }

}
