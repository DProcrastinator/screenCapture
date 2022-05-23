using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedSS
{
 public static   class GlobalClass
    {
        public static Form parentForm;
        public static RectangleF initialPageRect;
        public static bool savingImage = false;
        public static RectangleF initailRect;
        public static Bitmap screenShot;

       public static void   initilization(Form Parentform)
        { 
            parentForm = Parentform;
            initailRect = new RectangleF(0,0,screenShot.Width-100,screenShot.Height-100);
              SetPageRectangle();
        }

        public static void SetPageRectangle()
        {
            //  initialPageRect = new RectangleF(parentForm.Width / 2 - 900 / 2, parentForm.Height / 2 - 600 / 2, 800, 600); 
            initialPageRect = new RectangleF(0,0,parentForm.Width,parentForm.Height);
        }

    }
}
