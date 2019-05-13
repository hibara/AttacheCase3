using System;
using System.Windows.Forms;
using System.Timers;

namespace AttacheCase
{
  /// <summary>
  /// refer to: https://www.codeproject.com/Articles/20068/Custom-TextBox-that-Delays-the-TextChanged-Event
  /// </summary>
  public class DelayTextBox : TextBox
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private readonly System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.ResumeLayout(false);
    }

    private readonly System.Timers.Timer DelayTimer; // used for the delay
    private bool TimerElapsed = false; // if true OnTextChanged is fired.
    private bool KeysPressed = false; // makes event fire immediately if it wasn't a keypress
    private int DELAY_TIME = 200;

    // Delay property
    public int Delay
    {
      set { DELAY_TIME = value; }
    }

    public DelayTextBox()
    {
      InitializeComponent();

      // Initialize Timer
      DelayTimer = new System.Timers.Timer(DELAY_TIME);
      DelayTimer.Elapsed += new ElapsedEventHandler(DelayTimer_Elapsed);
    }

    void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      // stop timer.
      DelayTimer.Enabled = false;

      // set timer elapsed to true, so the OnTextChange knows to fire
      TimerElapsed = true;

      // use invoke to get back on the UI thread.
      this.Invoke(new DelayOverHandler(DelayOver), null);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (!DelayTimer.Enabled)
        DelayTimer.Enabled = true;
      else
      {
        DelayTimer.Enabled = false;
        DelayTimer.Enabled = true;
      }

      KeysPressed = true;

      base.OnKeyPress(e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      // if the timer elapsed or text was changed by something besides a keystroke
      // fire base.OnTextChanged
      if (TimerElapsed || !KeysPressed)
      {
        TimerElapsed = false;
        KeysPressed = false;
        base.OnTextChanged(e);
      }
    }

    public delegate void DelayOverHandler();

    private void DelayOver()
    {
      OnTextChanged(new EventArgs());
    }

  }
}
