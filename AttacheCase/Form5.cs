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

    public string textBoxAppFileVersionText
    {
      get { return textBoxAppFileVersion.Text; }
      set { textBoxAppFileVersion.Text = value; }
    }

    public string textBrokenText3
    {
      get { return textBroken3.Text; }
      set { textBroken3.Text = value; }
    }
    public string textBoxFileSignature3Text
    {
      get { return textBoxFileSignature3.Text; }
      set { textBoxFileSignature3.Text = value; }
    }
    public string textBoxMissTypeLimit3Text
    {
      get { return textBoxMissTypeLimit3.Text; }
      set { textBoxMissTypeLimit3.Text = value; }
    }
    public string textBoxDataFileVersion3Text
    {
      get { return textBoxDataFileVersion3.Text; }
      set { textBoxDataFileVersion3.Text = value; }
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
    public string toolStripStatusLabelDecryptionTimeText
    {
      get { return toolStripStatusLabelDecryptionTime.Text; }
      set { toolStripStatusLabelDecryptionTime.Text = value; }
    }

    //----------------------------------------------------------------------
    // Version.2
    public string textBoxDataSubVersionText
    {
      get { return textBoxDataSubVersion.Text; }
      set { textBoxDataSubVersion.Text = value; }
    }
    public string textBoxReservedText
    {
      get { return textBoxReserved.Text; }
      set { textBoxReserved.Text = value; }
    }
    public string textBoxMissTypeLimit2Text
    {
      get { return textBoxMissTypeLimit2.Text; }
      set { textBoxMissTypeLimit2.Text = value; }
    }
    public string textBoxfBroken2Text
    {
      get { return textBoxfBroken2.Text; }
      set { textBoxfBroken2.Text = value; }
    }
    public string textBoxlFileSignature2Text
    {
      get { return textBoxlFileSignature2.Text; }
      set { textBoxlFileSignature2.Text = value; }
    }
    public string textBoxDataFileVersion2Text
    {
      get { return textBoxDataFileVersion2.Text; }
      set { textBoxDataFileVersion2.Text = value; }
    }
    public string textBoxTypeAlgorismText
    {
      get { return textBoxTypeAlgorism.Text; }
      set { textBoxTypeAlgorism.Text = value; }
    }
    public string textBoxAtcHeaderSize2Text
    {
      get { return textBoxAtcHeaderSize2.Text; }
      set { textBoxAtcHeaderSize2.Text = value; }
    }
    public string textBoxOutputFileList2Text
    {
      get { return textBoxOutputFileList2.Text; }
      set { textBoxOutputFileList2.Text = value; }
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

    private void textBoxOutputFileList2_TextChanged(object sender, EventArgs e)
    {
      tabControl1.SelectedTab = tabPageAtc2;
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
