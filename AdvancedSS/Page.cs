using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedSS
{
    public class Page
    {
       public ImageFrame imageframe;
        public Boundry pageBoundry;
       public Page()
        {
           imageframe = new ImageFrame(GlobalClass.screenShot);
             pageBoundry = new Boundry(new RectangleF(GlobalClass.initialPageRect.X , GlobalClass.initialPageRect.Y,
                GlobalClass.initialPageRect.Width,GlobalClass.initialPageRect.Height),false);
            pageBoundry.Centerlize(GlobalClass.parentForm.ClientRectangle);

        }

        internal void paint(Graphics graphics)
        {
           // pageBoundry.paint(graphics);
            imageframe.paint(graphics);
        }

        internal void mouseDown(Point location)
        {
            imageframe.mouseDown(location);
          
        }

        internal void mouseUp(Point location)
        {
            imageframe.mouseUp(location);
        }

        internal void mouseMove(Point location)
        {
            imageframe.mouseMove(location);
        }

        internal Cursor getCurrentCursor()
        {
            return imageframe.getCurrentCursor();
        }

        internal void formSizeChanged()
        {
            pageBoundry.Centerlize(GlobalClass.parentForm.ClientRectangle);
            imageframe.setRectangles();  
        }
        internal void Save()
        {
            imageframe.Save();    
        }
    }
    }

