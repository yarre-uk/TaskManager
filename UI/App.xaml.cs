using System;
using System.Drawing;
using System.IO;
using System.Windows;

namespace UI
{
    public partial class App : Application
    {
        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        public App()
        {
            nIcon.Icon = new Icon(Directory.GetCurrentDirectory() + @"\ico.ico");
            nIcon.Visible = true;

            nIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            nIcon.ContextMenuStrip.Items.Add("Close", null, (o, e) => { MainWindow.Close(); });

            nIcon.Click += nIcon_Click;
        }

        void nIcon_Click(object sender, EventArgs e)
        {
            if (((dynamic)e).Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (MainWindow.Visibility != Visibility.Collapsed)
                {
                    MainWindow.Visibility = Visibility.Visible;
                    MainWindow.WindowState = WindowState.Normal;
                }
            }
        }
    }
}
