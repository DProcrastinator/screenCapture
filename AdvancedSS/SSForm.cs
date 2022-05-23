using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyloggerDll;

namespace AdvancedSS
{
    public partial class SSForm : Form
    {
        public SSForm()
        {  
            InitializeComponent();
            keymonitor = new KeyMonitor();
            keymonitor.Start();
            keymonitor.KeyIsDown += Keymonitor_KeyIsDown;
            keymonitor.keyIsUp += Keymonitor_keyIsUp;
            notifyIcon.Icon = Properties.Resources.Custom_Icon_Design_Pretty_Office_9_Open_file;
            notifyIcon.Text = "ScreenShot";
           
        }

        private void Keymonitor_keyIsUp(object sender, EventArgs e)
        {

        }


        
        private void Keymonitor_KeyIsDown(object sender, EventArgs e)
        {
            if (keymonitor.keyaction.TakeScreenShot)
            {
                form1 = new Form1(takeScrenShot());
                form1.ShowDialog();
               
            }

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.S)
            {
                
                form1 = new Form1(takeScrenShot());
                form1.ShowDialog();
              
            }

        }
        public Form1 form1;
        public KeyMonitor keymonitor;
        private Rectangle bounds;
        private Bitmap bitmap;
        private Graphics graphics;

        private void btn_Ok_Click(object sender, EventArgs e)
        {

        }
        public Bitmap takeScrenShot()
        {
            bounds = Screen.GetBounds(Point.Empty);

            bitmap = new Bitmap(bounds.Width, bounds.Height);

            graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

            return bitmap;

        }
        private void SSForm_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
                keymonitor.Start();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
           keymonitor.Stop();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;

        }
    }
}
