using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Block
    {

        private int X;
        private int Y;
        private const int width = 10;
        public Block(int x, int y)
        {
            this.X = 7 + x * 10;
            this.Y = 7 + y * 10;
        }
        public int getX()
        {
            return (X - 7) / 10;
        }
        public int getY()
        {
            return (Y - 7) / 10;
        }
        public override bool Equals(object obj)
        {
            Block f;
            if (obj == null)
            {
                return false;
            }
            f = obj as Block;
            if (f.getX() == getX() && f.getY() == getY())
            {
                return true;
            }
            else return false;
        }
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Black);
            g.FillRectangle(b, X - 5, Y - 5, width, width);
            b.Dispose();
        }
    }
}
