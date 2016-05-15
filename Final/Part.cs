using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Part
    {
        private int X;
        private int Y;
        private const int width = 10;
        public Color color { get; set; }
        public float h { get; set; }
        public float v { get; set; }
        public enum Direction
        {
            Top = 1,
            Right = 2,
            Bottom = 3,
            Left = 4
        }
        public Part(int x,int y,Color color)
        {
            this.X = 7+x*10;
            this.Y = 7+y*10;
            this.color = color;
        }
        public int getX()
        {
            return (X-7)/10;
        }
        public int getY()
        {
            return (Y-7)/10;
        }
        /*
       TOP = 0 -1
       RIGHT = 1 0
       BOTTOM = 0 1
       LEFT = -1 0
      */
        public void move(int direction)
        {
            int x, y;
            x = 0;
            y = 0;
            switch (direction)
            {
                case 1: x = 0; y = -1; break;
                case 2: x = 1; y = 0; break;
                case 3: x = 0; y = 1; break;
                case 4: x = -1; y = 0; break;
                default: break;
            }
            X += x * 10;
            Y += y * 10;
        }
        public override bool Equals(object obj)
        {
            if(obj==null)
            {
                return false;
            }
            Part p = obj as Part;
            if (p.getX() == getX() && p.getY() == getY())
            {
                return true;
            }
            else return false;
        }
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(color);
            g.FillRectangle(b, X - 5+h, Y - 5+v, width, width);
            b.Dispose();
        }
    }
}
