using System;
using System.Drawing;
using System.IO;
using System.Windows;
using Forms = System.Windows.Forms;

namespace UI;

public partial class App : Application
{
    Forms.NotifyIcon ico = new Forms.NotifyIcon();
    public App()
    {
        ico.Icon = new Icon(Directory.GetCurrentDirectory() + @"\ico.ico");
        ico.Visible = true;

        ico.ContextMenuStrip = new Forms.ContextMenuStrip();
        ico.ContextMenuStrip.Items.Add("Close", null, (o, e) => { MainWindow.Close(); });

        ico.Click += IcoClick!;
    }

    void IcoClick(object sender, dynamic e)
    {
        if (e.Button == Forms.MouseButtons.Left)
        {
            if (MainWindow.Visibility != Visibility.Collapsed)
            {
                MainWindow.Visibility = Visibility.Visible;
                MainWindow.WindowState = WindowState.Normal;
            }
        }
    }
}
