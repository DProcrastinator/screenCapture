using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedSS
{
    public class Boundry
    {
        public RectangleF rectangle { get; set; }

        RectangleF topLeft { get { return new RectangleF(rectangle.X -2.5f, rectangle.Y-2.5f ,5,5); } }

        RectangleF topRight { get {return new RectangleF(rectangle.X + rectangle.Width - 2.5f , rectangle.Y - 2.5f ,5,5); } }

        RectangleF bottomLeft { get{ return  new RectangleF(rectangle.X - 2.5f , rectangle.Y + rectangle.Height - 2.5f ,5,5); } }

        RectangleF bottomRight { get { return new RectangleF(rectangle.X + rectangle.Width - 2.5f , rectangle.Y + rectangle.Height - 2.5f ,5,5);} }



        public enum corners
        {
            topLeft,
            topRight,
            bottomLeft,
            bottomRight,
            body,
            none
        }
       public corners selectedPart;
        public Pen boundryPen;
        bool isClipBoundry;
      public  Boundry(RectangleF rectangleF , bool clipBoundry )
        {
            rectangle = rectangleF;
            isClipBoundry = clipBoundry;
            boundryPen = new Pen(new HatchBrush(HatchStyle.DottedGrid,Color.Black),2f);
        }

        internal bool IsMouseIntersect(Point location)
        {
            RectangleF mouseRectangle = new RectangleF(location, new SizeF(1, 1));
            if (isClipBoundry)
            {
                
                if (mouseRectangle.IntersectsWith(topLeft))
                {
                    selectedPart = corners.topLeft;
                    return true;
                }
                else if (mouseRectangle.IntersectsWith(topRight))
                {
                    selectedPart = corners.topRight;
                    return true;
                }
                else if (mouseRectangle.IntersectsWith(bottomLeft))
                {
                    selectedPart = corners.bottomLeft;
                    return true;
                }
                else if (mouseRectangle.IntersectsWith(bottomRight))
                {
                    selectedPart = corners.bottomRight;
                    return true;
                }
                else
                {
                    if (mouseRectangle.IntersectsWith(rectangle))
                    {
                        selectedPart = corners.body;
                        return true;
                    }
                    else
                    {
                        selectedPart = corners.none;
                        return false;
                    }

                }

            }
            else
            {
                 if (mouseRectangle.IntersectsWith(rectangle))
                {
                    selectedPart = corners.body;
                    return true;
                }
                else
                {
                    selectedPart = corners.none;
                    return false;
                }
            }
        }

        public  Cursor getCurrentCursor()
        {
            switch (selectedPart)
            {
                case corners.topLeft:
                    return Cursors.SizeNWSE;

                case corners.bottomRight:
                    return Cursors.SizeNWSE;


                case corners.topRight:
                    return Cursors.SizeNESW;

                case corners.bottomLeft:
                    return Cursors.SizeNESW;
                case corners.body:
                    return Cursors.SizeAll;
            }
            return Cursors.Arrow;
        }

        internal void Move(int dx, int dy)
        {
            if (isClipBoundry)
            {
                switch (selectedPart)
                {
                    case corners.body:
                        rectangle = new RectangleF(rectangle.X + dx, rectangle.Y + dy, rectangle.Width, rectangle.Height);
                        break;

                    case corners.topLeft:
                        rectangle = new RectangleF(rectangle.X + dx, rectangle.Y + dy, rectangle.Width - dx, rectangle.Height - dy);
                        break;
                    case corners.topRight:
                        rectangle = new RectangleF(rectangle.X, rectangle.Y + dy,rectangle.Width + dx, rectangle.Height - dy);
                        break;
                    case corners.bottomLeft:
                        rectangle = new RectangleF(rectangle.X + dx, rectangle.Y, rectangle.Width - dx, rectangle.Height + dy);
                        break;
                    case corners.bottomRight:
                        rectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width + dx, rectangle.Height + dy);
                        break;
                }
            }
            else
            {
                rectangle = new RectangleF(rectangle.X + dx, rectangle.Y + dy, rectangle.Width, rectangle.Height);
            }

         
        
        }
        internal void Centerlize(RectangleF parentrect)
        {
            rectangle = new RectangleF(parentrect.Width / 2 - rectangle.Width / 2, parentrect.Height / 2 - rectangle.Height / 2, rectangle.Width, rectangle.Height);

        }

        internal void paint(Graphics graphics)
        {
          
            graphics.DrawRectangle(boundryPen,rectangle.X,rectangle.Y,rectangle.Width,rectangle.Height);
            if (isClipBoundry)
            {
                graphics.DrawRectangle(boundryPen, topLeft.X, topLeft.Y, topLeft.Width, topLeft.Height);
                graphics.DrawRectangle(boundryPen, topRight.X, topRight.Y, topRight.Width, topRight.Height);
                graphics.DrawRectangle(boundryPen, bottomLeft.X, bottomLeft.Y, bottomLeft.Width, bottomLeft.Height);
                graphics.DrawRectangle(boundryPen, bottomRight.X, bottomRight.Y, bottomRight.Width, bottomRight.Height);
            }
           
        }
    }
}
