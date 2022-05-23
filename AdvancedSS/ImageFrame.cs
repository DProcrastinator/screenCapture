using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDCollection;

namespace AdvancedSS
{
   public class ImageFrame
    {

        public Boundry displayBoundry { get; set; }
        public Boundry sourceBoundry { get; set; }
        public Boundry clipBoundry { get;  set; }

        public Image image;

        public ImageFrame()
        {

        }
        public ImageFrame(Bitmap bitmap)
        {
            image = bitmap;
            setRectangles();
        }

        public void setRectangles()
        {
            sourceBoundry = new Boundry(new RectangleF(0, 0, image.Width, image.Height) , false);
            displayBoundry = new Boundry(  
                new RectangleF(getCenterLizedLocation(GlobalClass.initailRect.Size),

                 getResizedRectangle(sourceBoundry.rectangle.Size , GlobalClass.initailRect.Size)
                ) , false);

            clipBoundry = new Boundry(displayBoundry.rectangle , true);
        }

        private SizeF getResizedRectangle(SizeF SrcSize, SizeF desSize)
        {
            float sw = SrcSize.Width;
            float sh = SrcSize.Height;
            float dw = desSize.Width;
            float dh = desSize.Height;
            int finalHeight, finalWidth;
            float Sourceratio = sw / sh;

            if (Sourceratio >= 1)
            {
                finalWidth = (int)dw;
                float ratio = sw / dw;
                finalHeight = (int)(sh / ratio);
            }
            else
            {
                finalHeight = (int)dh;
                float ratio = sh / dh;
                finalWidth = (int)(sw / ratio);
            }
            return new SizeF(finalWidth, finalHeight);
        }

        internal void Save()
        {
            Bitmap bitmap = new Bitmap((int)displayBoundry.rectangle.Width, (int)displayBoundry.rectangle.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TranslateTransform(-displayBoundry.rectangle.X , -displayBoundry.rectangle.Y);
            GlobalClass.savingImage = true;
            paint(graphics);
            GlobalClass.savingImage = false;
            bitmap.Save(@"D:\screen shots\" + "image"+DateTime.Now.Ticks + ".Jpeg", ImageFormat.Jpeg);

        }


        //(form.Size.Width - 800) / 2
        private PointF getCenterLizedLocation(SizeF size)
        {
           return  new PointF( GlobalClass.parentForm.Location.X+ (GlobalClass.parentForm.Width - size.Width)/2 ,
             GlobalClass.parentForm.Location.Y + (GlobalClass.parentForm.Height - size.Height) / 2);
        }

        internal Cursor getCurrentCursor()
        {
            return clipBoundry.getCurrentCursor();
        }

        Point initialpoint;
        bool drag = false;
        internal void mouseDown(Point Location)
        {
            initialpoint = Location;
            if (clipBoundry.IsMouseIntersect(Location))
            {
                drag = true;
            }
        }

        internal void mouseMove(Point Location)
        {
            if (drag)
            {
                if (clipBoundry.selectedPart == Boundry.corners.body)
                {
                    displayBoundry.Move(Location.X - initialpoint.X, Location.Y - initialpoint.Y);
                    clipBoundry.Move(Location.X - initialpoint.X, Location.Y - initialpoint.Y);
                }
                else if (clipBoundry.selectedPart != Boundry.corners.none)
                    clipBoundry.Move(Location.X - initialpoint.X, Location.Y - initialpoint.Y);
            }
            initialpoint = Location;
        }
        //location.X - Initial.X, location.Y - Initial.Y
        internal void mouseUp(Point Location)
        {
            drag = false;
        }

        internal void paint(Graphics graphics)
        {
           // if (!GlobalClass.savingImage)
           // {
                graphics.SetClip(clipBoundry.rectangle);
                graphics.DrawImage(image, displayBoundry.rectangle, sourceBoundry.rectangle, GraphicsUnit.Pixel);
                graphics.ResetClip();
                clipBoundry.paint(graphics);
            //}
            //graphics.DrawImage(image, displayBoundry.rectangle, sourceBoundry.rectangle, GraphicsUnit.Pixel);

        }
    }
}
