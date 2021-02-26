using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AttacheCase
{
  public partial class Form5 : Form
  {
    public Form5()
    {
      InitializeComponent();
    }

    public static Form5 Instance { get; private set; }

    public string textBoxDataSebVersionText
    {
      get { return textBoxDataSebVersion.Text; }
      set { textBoxDataSebVersion.Text = value; }
    }

    public string textBrokenText
    {
      get { return textBroken.Text; }
      set { textBroken.Text = value; }
    }
    public string textBoxTokenStrText
    {
      get { return textBoxTokenStr.Text; }
      set { textBoxTokenStr.Text = value; }
    }
    public string textBoxDataFileVersionText
    {
      get { return textBoxDataFileVersion.Text; }
      set { textBoxDataFileVersion.Text = value; }
    }
    public string textBoxTypeAlgorismText
    {
      get { return textBoxTypeAlgorism.Text; }
      set { textBoxTypeAlgorism.Text = value; }
    }
    public string textBoxAtcHeaderSizeText
    {
      get { return textBoxAtcHeaderSize.Text; }
      set { textBoxAtcHeaderSize.Text = value; }
    }
    public string textSaltText
    {
      get { return textBoxSalt.Text; }
      set { textBoxSalt.Text = value; }
    }
    public string textBoxRfc2898DeriveBytesText
    {
      get { return textBoxRfc2898DeriveBytes.Text; }
      set { textBoxRfc2898DeriveBytes.Text = value; }
    }
    public string textBoxOutputFileListText
    {
      get { return textBoxOutputFileList.Text; }
      set { textBoxOutputFileList.Text = value; }
    }

    //----------------------------------------------------------------------

    private void Form5_Load(object sender, EventArgs e)
    {
      Form5.Instance = this;
    }

    private void textBoxOutputFileList_TextChanged(object sender, EventArgs e)
    {
      tabControl1.SelectedTab = tabPageAtc3;
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void Form5_FormClosed(object sender, FormClosedEventArgs e)
    {
      AppSettings.Instance.DeveloperConsolePosX = this.Left;
      AppSettings.Instance.DeveloperConsolePosY = this.Top;
      AppSettings.Instance.DeveloperConsoleWidth = this.Width;
      AppSettings.Instance.DeveloperConsoleHeight = this.Height;
    }
  }
}
