using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedSS
{
    public partial class Form1 : Form
    {
        public Form1( Bitmap ScreenShot)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.Text = String.Empty;
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            InitializeComponent();
            
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            GlobalClass.screenShot = ScreenShot;
            GlobalClass.initilization(this);
            pageObj = new Page();
            
            
        }
        Page pageObj;
       
        protected override void OnPaint(PaintEventArgs e)
        {
            pageObj.paint(e.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            pageObj.mouseDown(e.Location);
            Cursor = pageObj.getCurrentCursor();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            pageObj.mouseUp(e.Location);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            pageObj.mouseMove(e.Location);
            this.Invalidate();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                pageObj.Save();
                this.Dispose();
            }
            if (e.Control && e.KeyCode == Keys.E)
            {
                this.Dispose();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
            GlobalClass.SetPageRectangle();
            pageObj.formSizeChanged();
            this.Invalidate();
        }

      
    }
}
